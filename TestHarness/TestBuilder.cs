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
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using net.PaulChristensen.TestRunnerDataLink.Managers;

namespace net.PaulChristensen.TestHarnessLib
{
    internal class TestBuilder : ITestBuilder
    {
        private readonly ITestBuilderManager _testBuilderManager;
        private readonly AppDomain _currentDomain;

        public TestBuilder(ITestBuilderManager manager)
        {
            _currentDomain = AppDomain.CurrentDomain;
            _currentDomain.AssemblyResolve += new ResolveEventHandler(ResolveEventHandler);

            _testBuilderManager = manager;

            SourceTestBatch = _testBuilderManager.GetTestSuite(1);
            TestCount = _testBuilderManager.GetTestCount();
        }

        public Dictionary<string, ITest> LoadAllTests(IHarness harness)
        {
            var testSet = new Dictionary<string, ITest>();

            foreach (var testEntity in SourceTestBatch.Tests)
            {
                ITest test;
                GetNextTest(out test, harness, testEntity);
                testSet.Add(test.TestName, test);
            }
            return testSet;
        }

        public int TestCount { get; set; }

        public net.PaulChristensen.TestRunnerDataLink.Entities.TestSuite SourceTestBatch { get; private set; }

        public bool GetNextTest(out ITest test, IHarness harness, TestRunnerDataLink.Entities.TestEntity testEntity)
        {            
            bool retVal = true;

            Assembly assembly = Assembly.LoadFile(testEntity.TestFileFullName);
            Type type = assembly.GetType(testEntity.TypeName);
            test = (ITest)Activator.CreateInstance(type);
            _currentDomain.SetData("ITest", test);

            test.ConfigureTest(harness);
            test.PrimaryIteratorCount(testEntity.RepeatCount);

            test.TestName = testEntity.TestName;
            test.TestDescription = testEntity.TestDescription;

            return retVal;
        }

        private string GetValue(string key, IDictionary<string, string> testProperties)
        {
            string retVal = string.Empty;
            if (testProperties.ContainsKey(key))
                retVal = testProperties[key];
            return retVal;
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