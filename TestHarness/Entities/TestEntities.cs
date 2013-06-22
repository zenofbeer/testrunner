using System.Collections.Generic;

namespace net.PaulChristensen.TestHarnessLib.Entities
{
    public class TestEntities
    {
        public Dictionary<string, string> SuiteProperties { get; set; } 
        public List<TestEntity> TestEntityList { get; set; }
    }
}