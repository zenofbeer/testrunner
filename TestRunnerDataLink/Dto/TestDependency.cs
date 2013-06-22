using System.Collections.Generic;

namespace net.PaulChristensen.TestRunnerDataLink.Dto
{
    public class TestDependency
    {
        public string Name { get; set; }
        public Dictionary<string, string> DependencyItems { get; set; }
    }
}