using System;
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
    }
}