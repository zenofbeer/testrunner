using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.PaulChristensen.TestHarnessLib;

namespace AccountAlertSolverTester
{
    public class AccountAlertSolver : ITest
    {
        IHarness _harness;
        int _primaryIteratorCount;
        private string _testName;

        #region ITest Members

        public void ConfigureTest(IHarness harnessInterface)
        {
            _harness = harnessInterface;
        }

        public void ExecuteTest()
        {
            throw new NotImplementedException();
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
            get { return _testName; }
            set { _testName = value; }
        }

        public string TestDescription { get; set; }

        public void SetDependencyList(Dictionary<string, string> dependencyList)
        {
            throw new NotImplementedException();
        }

        public bool GetDependency(string assemblyToFind, out string dependencyPath, out string dependencyType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
