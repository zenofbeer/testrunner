using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using net.PaulChristensen.Common.Utils;
using net.PaulChristensen.TestRunnerDataLink.Entities;

namespace net.PaulChristensen.TestRunnerDataLink.Repositories
{
    public class XmlTestSuiteRepositoryRepository : ITestSuiteRepository
    {
        private readonly XDocument _xDoc;

        public XmlTestSuiteRepositoryRepository()
        {
            _xDoc = XDocument.Load(Properties.Settings.Default.HarnessConfigPath);
        }

        public int GetTestCount()
        {
            return _xDoc.Descendants("test").Count();
        }

        public Dictionary<string, string> GetTestSuiteDefinition()
        {
            IEnumerable<XAttribute> attributeCollection = _xDoc.Element("testSuite").Attributes();
            var attributes = new Dictionary<string, string>();
            foreach (var attribute in attributeCollection)
            {
                var tempString = attribute.Value;
                //ToDo: Move StringHelper to a shared location
                tempString = tempString.TrimEnd('\\') + '\\';
                attributes[attribute.Name.ToString()] = tempString;
            }
            return attributes;
        }

        public TestSuite GetTestSuite(int testSuiteId)
        {
            var result = (from a in _xDoc.Elements("testSuite")
                         where a.Attribute("testSuiteId").Value == testSuiteId.ToString()
                         select a).First();

            var suite = new TestSuite();

            foreach (var attribute in result.Attributes())
            {
                if (attribute.Name == "testSuiteId")
                    suite.TestSuiteId = StringHelpers.GetIntFromString(attribute.Value);
                else if (attribute.Name == "testSuiteName")
                    suite.TestSuiteName = attribute.Value;
                else suite.SuiteProperties[attribute.Name.ToString()] = attribute.Value;
            }
            suite.Tests = GetTests(testSuiteId);
            return suite;
        }

        public List<TestEntity> GetTests(int testSuiteId)
        {
            var results =
                (
                    from t in _xDoc.Elements("testSuite").Elements("test")
                    select t
                ).ToList();
            var tests = new List<TestEntity>();
            foreach (var result in results)
            {
                var test = new TestEntity();
                test.TestEntityId = StringHelpers.GetIntFromString(result.Attribute("testEntityId").Value);
                test.TestName = result.Attribute("name").Value;

                var description = (null != result.Attribute("description"))
                                      ? result.Attribute("description").Value
                                      : null;
                test.TestDescription = ResolveVariable(result, description);

                var path = (null != result.Attribute("path"))
                               ? result.Attribute("path").Value
                               : null;
                path = ResolveVariable(result, path);
                StringHelpers.FixPath(ref path);
                test.Path = path;                

                test.FileName = (null != result.Attribute("fileName")) ? result.Attribute("fileName").Value : null;
                test.TypeName = (null != result.Attribute("typeName")) ? result.Attribute("typeName").Value : null;
                test.RepeatCount = (null != result.Attribute("repeatCount"))
                                       ? StringHelpers.GetIntFromString(result.Attribute("repeatCount").Value)
                                       : 0;
                test.TestFileFullName = string.Concat(test.Path, test.FileName);
                tests.Add(test);
            }
            return tests;
        }

        private string ResolveVariable(XElement element, string variable)
        {
            if (null == variable || !StringHelpers.IsVariable(variable))
                return variable;
            variable = StringHelpers.StripVariableDelimiters(variable);
            var parent = element.Parent;
            var resolved = parent.Attribute(variable).Value;
            return resolved;
        }
    }
}