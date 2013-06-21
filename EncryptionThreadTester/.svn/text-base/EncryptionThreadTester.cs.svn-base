using System;
using System.Collections.Generic;
using System.Text;
using Fidelity.Mercury.CommonSession;
using net.PaulChristensen.TestHarnessLib;

namespace Fidelity.Mercury.CommonSession.ConsoleTestHarness
{
    public class EncryptionThreadTester : ITest
    {
        #region delegates
        delegate void EncryptDecryptDelegate();
        #endregion delegates

        #region constants
        private const string ENCRYPTION_KEY = "{6B407406-36C7-414a-8F12-1C696B109886}";
        #endregion constants

        private MercuryEncryption mercuryEncryption;
        private int completed = 0;
        private int runningThreadCount = 0;
        private int _maxLoopCount = 0;
        private bool _testPassed;
        private IHarness _harnessInterface;
        private Dictionary<string, string> _testDependencies;

        public EncryptionThreadTester()
        {
        }

        public string TestName
        {
            get;
            set;
        }

        public string TestDescription { get; set; }

        public bool TestPassed
        {
            get { return _testPassed; }
        }

        public void ConfigureTest(IHarness harnessInterface)
        {
            mercuryEncryption = new MercuryEncryption(ENCRYPTION_KEY);
            _harnessInterface = harnessInterface;
        }

        public void PrimaryIteratorCount(int iteratorCount)
        {
            _maxLoopCount = iteratorCount;
        }

        public void SetDependencyList(Dictionary<string, string> dependencyList)
        {
            _testDependencies = dependencyList;
        }

        public TestApplicationKind TestSource
        {
            get { return TestApplicationKind.Dll; }
        }

        public bool GetDependency(string dependencyName, out string dependencyPath, out string dependencyType)
        {
            bool retVal = false;
            dependencyPath = null;
            dependencyType = null;

            if (_testDependencies.ContainsKey(dependencyName))
            {
                string[] dependencyFields = GetDependencyFields(dependencyName);
                Dictionary<string, string> dependencyFieldsDict = GetDependencyDetails(dependencyFields);
                string extension = dependencyFieldsDict["extension"];
                extension = extension.TrimStart('.');
                dependencyPath = string.Format("{0}{1}.{2}", dependencyFieldsDict["path"], dependencyName,
                    extension);
                dependencyType = dependencyFieldsDict["typeName"];
                retVal = true;
            }


            return retVal;
        }

        private string[] GetDependencyFields(string dependencyName)
        {
            return _testDependencies[dependencyName].Split('|');
        }

        private Dictionary<string, string> GetDependencyDetails(string[] dependencyFields)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            foreach (string dependencyField in dependencyFields)
            {
                string[] parsedField = dependencyField.Split('=');
                retVal[parsedField[0]] = parsedField[1];
            }
            return retVal;
        }        

        public void ExecuteTest()
        {
            try
            {
                for (int i = 0; i <= _maxLoopCount; i++)
                {
                    EncryptDecryptDelegate newEncryptThread = new EncryptDecryptDelegate(EncryptDecrypt);
                    AsyncCallback edc = new AsyncCallback(EncryptDecryptCallback);
                    IAsyncResult ar = newEncryptThread.BeginInvoke(edc, newEncryptThread);
                }
                while (completed < _maxLoopCount)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch
            {
                _harnessInterface.SetTestStatus(TestResult.StatusFailed);
                return;
            }
            _harnessInterface.SetTestStatus(TestResult.StatusSuccess);
        }

        private void EncryptDecrypt()
        {
            Random random = new Random();
            int currentThread = runningThreadCount += 1;
            for (int i = 0; i <= _maxLoopCount; i++)
            {
                // ensure that the encryption string is unique:

                string encryptionString;
                StringBuilder sb = new StringBuilder();
                Random randLetter = new Random();
                string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw xyz1234567890";
                int length = 20;
                while (length-- >= 0)
                {
                    sb.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
                }
                encryptionString = sb.ToString();
                string encryptedString = mercuryEncryption.EncryptString(encryptionString);
                string decryptedString = mercuryEncryption.DecryptString(encryptedString);
                if (0 != string.Compare(encryptionString, decryptedString, false))
                {
                    throw new Exception("source string and decrypted string do not match");
                }
                WriteResults(string.Format("{0}=={1}|in thread:{2}|iteration count:{3}",
                    encryptionString, decryptedString, currentThread.ToString().PadLeft(3, ' '),
                    i.ToString().PadLeft(3, ' ')));
                System.Threading.Thread.Sleep(random.Next(1000));
            }
        }

        private void EncryptDecryptCallback(IAsyncResult ar)
        {
            completed += 1;
            WriteResults(string.Format("Completing thread {0}", completed.ToString()));
        }

        private void WriteResults(string outputText)
        {
            _harnessInterface.WriteStatus(outputText);
        }
    }
}
