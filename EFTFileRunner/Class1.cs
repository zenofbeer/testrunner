using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Fidelity.Mercury.BusinessObjects.EFT;
using Fidelity.Common.Security;
using Fidelity.Mercury.CommonSession;
using Fidelity.Mercury.BusinessObjects.UnitTests;
using Fidelity.Common.ApplicationsManagement;
using Fidelity.Common.DataSourceManagement;
using net.PaulChristensen.TestHarnessLib;

namespace EFTFileRunner
{
    public class Class1 : TestBase, ITest
    {
        IHarness _harness;
        private Session _session;

        public void ConfigureTest(IHarness harness)
        {
            _harness = harness;
            _harness.SetTestStatus(TestResult.StatusUnknown);
            _harness.SetConsoleTitle("EFTFileRunner - Post File");
        }

        protected new Session CreateSession()
        {
            //object userRights = null;
            return null;
        }

        #region ITest Members


        public void ExecuteTest()
        {
            throw new NotImplementedException();
        }

        public void PrimaryIteratorCount(int iteratorCount)
        {
            throw new NotImplementedException();
        }

        public TestApplicationKind TestSource
        {
            get { throw new NotImplementedException(); }
        }

        public string TestName
        {
            get;
            set;
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
