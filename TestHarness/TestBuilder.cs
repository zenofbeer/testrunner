/*
 * TestRunner - TestHarnessLib, a test harness tool.
 * Copyright (C) 2010 - Paul Christensen
 * http://www.PaulChristensen.net
 * Paul@PaulChristensen.net
 * 
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using net.PaulChristensen.TestHarnessLib.Entities;
using net.PaulChristensen.TestHarnessLib.Util;
using net.PaulChristensen.TestRunnerDataLink.Repositories;

namespace net.PaulChristensen.TestHarnessLib
{
    internal class TestBuilder : ITestBuilder
    {
        private readonly Stack<Dictionary<string, string>> _testProperties;
        private readonly Dictionary<string, string> _testDependancies;
        private readonly ITestSuiteRepository _testSuiteRepository;
        private readonly XDocument _xDoc;
        private XElement _nextElement;
        private readonly int _testCount;
        private readonly AppDomain _currentDomain;

        public TestBuilder()
        {
            _currentDomain = AppDomain.CurrentDomain;
            _currentDomain.AssemblyResolve += new ResolveEventHandler(ResolveEventHandler);


            _testProperties = new Stack<Dictionary<string, string>>();
            _testDependancies = new Dictionary<string, string>();
            _xDoc = XDocument.Load("HarnessConfig.xml");
            //ToDo: inject this
            _testSuiteRepository = new XmlTestSuiteRepositoryRepository();
            SourceTestBatch = new TestEntities();

            ProcessTestHeader();

            _testCount = new List<XElement>(_xDoc.Descendants("test")).Count;
        }

        public int TestCount
        {
            get { return _testCount; }
        }

        public TestEntities SourceTestBatch { get; private set; }

        private string TestApplicationPath
        {
            get
            {
                var path = _testProperties.Peek()[Properties.Settings.Default.PathKey];
                StringHelper.TrimTrailingSlashes(ref path);
                path = path + '\\';
                return path + _testProperties.Peek()[Properties.Settings.Default.FileNameKey];
            }
        }

        private string TestApplicationTypeName
        {
            get
            {
                return _testProperties.Peek()[Properties.Settings.Default.TypeNameKey];
            }
        }

        private int RepeatCount
        {
            get
            {
                int repeatCount = 0;
                if (_testProperties.Peek().ContainsKey(Properties.Settings.Default.RepeatCountKey))
                {                    
                    int.TryParse(_testProperties.Peek()[Properties.Settings.Default.RepeatCountKey], out repeatCount);
                }
                return repeatCount;
            }
        }

        public bool GetNextTest(out ITest test, IHarness harness)
        {            
            bool retVal = true;

            PrepareNextTest();

            Assembly assembly = Assembly.LoadFile(TestApplicationPath);
            Type type = assembly.GetType(TestApplicationTypeName);
            test = (ITest)Activator.CreateInstance(type);
            _currentDomain.SetData("ITest", test);
            test.SetDependencyList(_testDependancies);

            test.ConfigureTest(harness);
            test.PrimaryIteratorCount(RepeatCount);
            Dictionary<string, string> testProperties = _testProperties.Peek();

            test.TestName = GetValue("name", testProperties);
            test.TestDescription = GetValue("description", testProperties);

            CompleteTest();

            return retVal;
        }

        private string GetValue(string key, IDictionary<string, string> testProperties)
        {
            string retVal = string.Empty;
            if (testProperties.ContainsKey(key))
                retVal = testProperties[key];
            return retVal;
        }

        private void PrepareNextTest()
        {
            var testEntity = new TestEntity(_nextElement, _testProperties.Peek());

            IEnumerable<XAttribute> attributeCollection = _nextElement.Attributes();
            Dictionary<string, string> currentAttributeCollection = _testProperties.Peek();
            var newAttributeCollection = new Dictionary<string, string>();

            newAttributeCollection[Constants.TestPathAttribute] = testEntity.Path;

            foreach (string key in currentAttributeCollection.Keys)
            {
                if (!newAttributeCollection.ContainsKey(key))
                {
                    newAttributeCollection[key] = currentAttributeCollection[key];
                }
            }

            foreach (var attribute in attributeCollection)
            {
                if (!newAttributeCollection.ContainsKey(attribute.Name.ToString()))
                {
                    newAttributeCollection[attribute.Name.ToString()] = attribute.Value;
                }
            }

            _testProperties.Push(newAttributeCollection);

            IEnumerable<XElement> dependancyNodes = _nextElement.Elements("dependency");
            Dictionary<string, string> tempTestDependency = new Dictionary<string, string>();
            foreach (XElement element in dependancyNodes)
            {
                IEnumerable<XAttribute> dependencyAttributes = element.Attributes();
                foreach (XAttribute attribute in dependencyAttributes)
                {
                    string tempValue = attribute.Value;
                    if(StringHelper.IsVariable(tempValue))
                    {
                        StringHelper.StripVariableDelimiters(ref tempValue);
                        StringHelper.TrimTrailingSlashes(ref tempValue);
                        if (attribute.Name.ToString() == "path")
                        {
                            if (tempValue.Length != 0)
                            {
                                tempValue = tempValue.TrimEnd('\\') + '\\';
                            }
                        }
                        tempValue = _testProperties.Peek()[attribute.Name.ToString()] + tempValue;
                    }

                    tempTestDependency[attribute.Name.ToString()] = tempValue;

                }

                IEnumerable<XElement> dependencyElements = element.Elements();
                List<string> types = new List<string>();
                foreach (XElement dependencyElement in dependencyElements)
                {
                    if (dependencyElement.Name == "typeName")
                    {
                        types.Add(dependencyElement.Value);
                    }
                }

                GetDependencyTypes(ref tempTestDependency, types);

                _testDependancies[tempTestDependency["name"]] = GetDependencyList(tempTestDependency);
                Dictionary<string, string> dependTest = new Dictionary<string, string>();
                foreach (var depends in testEntity.Dependencies)
                {
                    dependTest.Add(depends.Name, depends.Path + "|" + depends.Name + "|" +
                        depends.Extension + "|" + Constants.ElementTypeName + "=" +
                        string.Join(",", depends.TypeNames.ToArray()));
                }         
            }                    
        }

        private string GetDependencyList(Dictionary<string, string> dependency)
        {
            string retVal = null;

            foreach (string key in dependency.Keys)
            {
                retVal += string.Format("{0}={1}|", key, dependency[key]);
            }
            return retVal.TrimEnd('|');
        }

        private void GetDependencyTypes(ref Dictionary<string, string> tempTestDependency, List<string> dependencyTypeList)
        {
            string dependencyList = string.Join(",", dependencyTypeList.ToArray());
            if (tempTestDependency.ContainsKey("typeName"))
            {
                if (!string.IsNullOrEmpty(dependencyList))
                    tempTestDependency["typeName"] += "," + dependencyList;
            }
            else
            {
                if (!string.IsNullOrEmpty(dependencyList))
                    tempTestDependency.Add("typeName", dependencyList);
            }
        }

        private void CompleteTest()
        {
            _testDependancies.Clear();
            _testProperties.Pop();

            var nextElement = _nextElement.NextNode;
            if (nextElement.GetType() != typeof(XElement))
            {
                do
                {
                    nextElement = nextElement.NextNode;
                }
                while ((null != nextElement) && (nextElement.GetType() != typeof(XElement)));
                _nextElement = (XElement)nextElement;
            }
            else
            {
                _nextElement = (XElement)_nextElement.NextNode;
            }
        }        

        //ToDo: Make this return the Dictionary and set the suiteProperties set the properties from the return value;
        private void ProcessTestHeader()
        {
            var suiteProperties = _testSuiteRepository.GetTestSuiteDefinition();

            SourceTestBatch.SuiteProperties = suiteProperties;

            _testProperties.Push(suiteProperties);
            _nextElement = _xDoc.Element("tests").Element("test");
        }

        private static Assembly ResolveEventHandler(object sender, ResolveEventArgs e)
        {
            string assemblyToFind = e.Name.Substring(0, e.Name.IndexOf(','));
            string dependencyPath;
            string dependencyType;
            ITest thisTest = (ITest)((AppDomain)sender).GetData("ITest");
            Assembly retVal = null;

            if (thisTest.GetDependency(assemblyToFind, out dependencyPath, out dependencyType))
            {
                retVal = Assembly.LoadFrom(dependencyPath);
                Type typeDepends = retVal.GetType(dependencyType);
                Activator.CreateInstance(typeDepends);
            }
            return retVal;
        }
    }
}