using System.Collections.Generic;

namespace net.PaulChristensen.TestRunnerDataLink.Entities
{
    public class TestSuite
    {
        public TestSuite()
        {
            SuiteProperties = new Dictionary<string, string>();
        }
        public int TestSuiteId { get; set; }
        public string TestSuiteName { get; set; }
        public Dictionary<string, string> SuiteProperties { get; set; }
        public List<TestEntity> Tests { get; set; }
    }
}