using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.PaulChristensen.TestHarnessLib;

namespace RoundTripClientCall
{
    public class ServiceRunner : ITest
    {
        private delegate void CallServiceDelegate(string[] passedMessage);
        private WcfService.Service1Client _client;
        private int _completed = 0;
        private int _runningThreadCount = 0;
        private int _maxLoopCount = 0;
        private IHarness _iHarness;
        private Dictionary<string, string> _testDependencies;

        private void ServiceDelegateCallback(IAsyncResult ar)
        {
            _completed += 1;
        }

        private void CallService(string[] passedMessage)
        {
            Random rand = new Random();
            System.Threading.Thread.Sleep(rand.Next(1000));

            WriteResults(string.Format("{0} status = {1}, {2}, {3}, {4}", passedMessage[0], passedMessage[1],
                passedMessage[2], passedMessage[3], passedMessage[4]));

            try
            {
                passedMessage = _client.CallWcfService(passedMessage);
                passedMessage[2] = "wcfReturned=true";
            }
            catch
            {
                passedMessage[2] = "wcfReturned=Failed to find WCF Service";
            }

            WriteResults(string.Format("{0} status = {1}, {2}, {3}, {4}", passedMessage[0], passedMessage[1],
                passedMessage[2], passedMessage[3], passedMessage[4]));
        }

        private void WriteResults(string outputText)
        {
            _iHarness.WriteStatus(outputText);
        }

        #region ITest Members

        public void ConfigureTest(IHarness harnessInterface)
        {
            _client = new RoundTripClientCall.WcfService.Service1Client();
            _iHarness = harnessInterface;
        }

        public void ExecuteTest()
        {
            try
            {
                for (int i = 0; i <= _maxLoopCount; i++)
                {
                    string[] passedMessage = {
                                                 "client" + i.ToString(), "wcfCalled=false",
                                                 "wcfReturned=false", "webServiceCalled=false",
                                                 "webServiceReturned=false"};
                    CallServiceDelegate serviceDelegate = new CallServiceDelegate(CallService);
                    AsyncCallback callBack = new AsyncCallback(ServiceDelegateCallback);
                    IAsyncResult ar = serviceDelegate.BeginInvoke(passedMessage, ServiceDelegateCallback, serviceDelegate);
                }
                while (_completed <= _maxLoopCount)
                {
                    System.Threading.Thread.Sleep(100);
                }
                _iHarness.SetTestStatus(TestResult.StatusSuccess);
            }
            finally { }
        }

        public void PrimaryIteratorCount(int iteratorCount)
        {
            _maxLoopCount = iteratorCount;
        }

        public TestApplicationKind TestSource
        {
            get { return TestApplicationKind.Dll; }
        }

        public string TestName { get; set; }

        public string TestDescription { get; set; }

        public void SetDependencyList(Dictionary<string, string> dependencyList)
        {
            //throw new NotImplementedException();
            // if no dependencies, do nothing here. 
        }

        public bool GetDependency(string assemblyToFind, out string dependencyPath, out string dependencyType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
