using System.Collections.Generic;

namespace net.PaulChristensen.TestHarnessLib.Entities
{
    public class TestSuite
    {
        public int TestSuiteId { get; set; }
        public int TestSuiteName { get; set; }
        /// <summary>
        /// field for free-form properties
        /// </summary>
        public Dictionary<string, string> SuiteProperties { get; set; } 
        public List<TestEntity> TestEntityList { get; set; }
    }
}