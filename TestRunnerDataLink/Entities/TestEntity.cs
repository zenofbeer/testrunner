using System.Collections.Generic;

namespace net.PaulChristensen.TestRunnerDataLink.Entities
{
    public class TestEntity
    {
        public int TestEntityId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string TypeName { get; set; }
        public int RepeatCount { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        // add dependencies list
    }
}