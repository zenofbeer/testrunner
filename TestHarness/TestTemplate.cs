using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using net.PaulChristensen.TestHarnessLib;

namespace net.PaulChristensen.TestHarnessLib
{
    public class TestTemplate : ITest
    {
        #region constants
        private string _testName = "Sample Sleeper Test.";
        #endregion constants

        #region private fields
        /// <summary>
        /// Required. Provides access to the parent output console.
        /// </summary>
        private IHarness _iHarness;
        /// <summary>
        /// Required. Indicates the number of times to execute the test.
        /// </summary>
        int _primaryIteratorCount;
        private Dictionary<string, string> _testDependencies;
        #endregion private fields

        #region ITest Members

        /// <summary>
        /// Configure test settings. At a minimum _iHarness needs to be set to harnessInterface. Set other test wide variables
        /// here. 
        /// </summary>
        /// <param name="harnessInterface">The harness interface allowing communication back out to the harness.</param>
        public void ConfigureTest(IHarness harnessInterface)
        {
            _iHarness = harnessInterface;
            _iHarness.SetConsoleTitle(TestName);
        }

        /// <summary>
        /// This method begins and ends the test execution. The sample is a sleep test sample.
        /// </summary>
        public void ExecuteTest()
        {
            for (int i = 0; i < _primaryIteratorCount; i++)
            {
                Random random = new Random();
                int sleepTime = random.Next(5000);
                _iHarness.WriteStatus(string.Format("Sleeping for {0} milliseconds", sleepTime.ToString()));
                System.Threading.Thread.Sleep(sleepTime);
            }

            if (false == true)
            {
                _iHarness.SetTestStatus(TestResult.STATUS_FAILED);
            }
            else
            {
                _iHarness.SetTestStatus(TestResult.STATUS_SUCCESS);
            }
        }

        /// <summary>
        /// Set _primaryIteratorCount to iteratorCount pulled from the config xml file. Test this value to know when
        /// the test is complete.
        /// </summary>
        /// <param name="iteratorCount"></param>
        public void PrimaryIteratorCount(int iteratorCount)
        {
            _primaryIteratorCount = iteratorCount;
        }

        #region properties
        /// <summary>
        /// Returns the kind of application file of the test. This tells the harness how to load and execute the file. 
        /// Currently only TestApplicationKind.DLL is supported.
        /// </summary>
        public TestApplicationKind TestSource
        {
            get { return TestApplicationKind.DLL; }
        }

        public string TestName
        {
            get { return _testName; }
            set { _testName = value; }
        }

        public string TestDescription
        {
            get;
            set;
        }
        #endregion properties

        /// <summary>
        /// set the dictionary of test dependencies for the test. Later, the harness will use this dictionary to load
        /// additional assemblies required to run the test. 
        /// </summary>
        /// <param name="dependencyList"></param>
        public void SetDependencyList(Dictionary<string, string> dependencyList)
        {
            _testDependencies = dependencyList;
        }

        /// <summary>
        /// if ResolveEvent is thrown in Harness the harness will attempt to load dependencies that are stored in 
        /// _testDependencies. This method should be valid for any situation.
        /// </summary>
        /// <param name="assemblyToFind">the assembly to be searched for.</param>
        /// <param name="dependencyPath">the path where the assembly is stored.</param>
        /// <param name="dependencyType">the extension on the file to be executed.</param>
        /// <returns></returns>
        public bool GetDependency(string assemblyToFind, out string dependencyPath, out string dependencyType)
        {
            bool retVal = false;
            dependencyPath = null;
            dependencyType = null;
            if (_testDependencies.ContainsKey(assemblyToFind))
            {
                string[] dependencyFields = GetDependencyFields(assemblyToFind);
                Dictionary<string, string> dependencyFieldsDictionary = GetDependencyDetails(dependencyFields);
                string extension = dependencyFieldsDictionary["extension"];
                extension = extension.TrimStart('.');
                dependencyPath = string.Format("{0}{1}.{2}", dependencyFieldsDictionary["path"], assemblyToFind,
                                               extension);
                dependencyType = dependencyFieldsDictionary["typeName"];
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
            foreach (var dependencyField in dependencyFields)
            {
                string[] parsedField = dependencyField.Split('=');
                retVal[parsedField[0]] = parsedField[1];
            }
            return retVal;
        }

        #endregion
    }
}