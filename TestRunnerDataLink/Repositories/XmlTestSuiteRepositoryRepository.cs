using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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
            IEnumerable<XAttribute> attributeCollection = _xDoc.Element("tests").Attributes();
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
    }
}