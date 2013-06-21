using System;
using System.Collections.Generic;
using net.PaulChristensen.TestHarnessLib;

namespace SleepTest
{
    public class Class1 : ITest
    {
        IHarness _harness;
        int _primaryIteratorCount;

        public Class1() { }

        public void ConfigureTest(IHarness harness)
        {
            _harness = harness;
        }

        public void PrimaryIteratorCount(int iteratorCount)
        {
            _primaryIteratorCount = iteratorCount;
        }

        public TestApplicationKind TestSource
        {
            get { return TestApplicationKind.Dll; }
        }

        public string TestName
        {
            get;
            set;
        }

        public string TestDescription { get; set; }

        public void SetDependencyList(Dictionary<string, string> dependencyList)
        {

        }

        public bool GetDependency(string assemblyToFind, out string dependencyPath, out string dependencyType)
        {
            throw new NotImplementedException();
        }

        public void ExecuteTest()
        {
            //_harness.SetConsoleTitle("Sleeper Test");
            //for (int i = 0; i < _primaryIteratorCount; i++)
            //{                
            //    _harness.SetTestStatus(TestResult.StatusUnknown);
            //    var random = new Random();
            //    int sleepTime = random.Next(5000);
            //    _harness.WriteStatus(string.Format("Sleeping for {0} milliseconds", sleepTime.ToString()));
            //    System.Threading.Thread.Sleep(sleepTime);
            //}

            //_harness.SetTestStatus(TestResult.StatusSuccess);
        }
    }
}
