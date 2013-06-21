/*
 * TestRunner - TestHarnessLib, a test harness tool.
 * Copyright (C) 2010 - Paul Christensen
 * http://www.PaulChristensen.net
 * Paul@PaulChristensen.net
 * 
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace net.PaulChristensen.TestHarnessLib
{
    public class Harness : IHarness
    {
        #region private fields
        private ITestBuilder _testBatch;
        private ITest _currentTest;
        private TestResult _currentTestStatus = TestResult.StatusUnknown;
        private TestRunStatus _currentTestRunStatus;
        private Dictionary<string, ITest> _availableTests;
        private Dictionary<int, ITest> _queuedTests;
        private int _lastLineLength;
        private int _testDetailsPostition = 0;
        private int _testDetailsLineLength = 0;
        private int _testCount = 0;
        private int _testId = 0;
        private IW32Console _iW32Console;
        private string _testTitle;
        private string _currentTestName;
        private string _currentTestCountString;
        private string _testCountString;
        private object[] _constructorParameters;
        private StreamWriter _writer;
        #endregion private fields

        private delegate void ExecuteTestDelegate(ITest test);

        public Harness(IW32Console iW32Console)
        {
            _iW32Console = iW32Console;
            InitializeHarness();
            LoadAllTests();
            foreach (var availableTest in _availableTests.Values)
                _iW32Console.AddAvailableTest(availableTest);
        }

        public Harness()
        {
            InitializeHarness();
            Console.CursorVisible = false;

            LoadAllTests();
            foreach (var availableTest in _availableTests.Values)
            {
                _queuedTests.Add(_queuedTests.Count, availableTest);
            }

            ExecuteTests();
            
            Console.CursorVisible = true;
            Console.WriteLine("Press any key to quit");
            Console.ReadLine();
        }

        #region properties
        /// <summary>
        /// Sets the test title value.
        /// </summary>
        public string TestTitle
        {
            get { return _testTitle; }
            set
            {
                _testTitle = value;
                SetViewTestTitle();
            }
        }

        public object[] ConstructorParameters
        {
            set { _constructorParameters = value; }
        }

        private void SetViewTestTitle()
        {
            if (null != _iW32Console)
                _iW32Console.TestTitle = TestTitle;
        }

        /// <summary>
        /// sets a string indicating the total test count.
        /// </summary>
        private string TestCountString
        {
            get { return _testCountString; }
            set
            {
                _testCountString = value;
                SetViewTestCountString();
            }
        }

        private void SetViewTestCountString()
        {
            if (null != _iW32Console)
                _iW32Console.TestCountString = TestCountString;
        }
        /// <summary>
        /// updates the current test count.
        /// </summary>
        private string CurrentTestCountString
        {
            get { return _currentTestCountString; }
            set
            {
                _currentTestCountString = value;
                SetViewCurrentTestCountString();
            }
        }       

        private void SetViewCurrentTestCountString()
        {
            if (null != _iW32Console)
                _iW32Console.CurrentTestCountString = CurrentTestCountString;
        }
 
        private string CurrentTestName
        {
            get { return _currentTestName; }
            set
            {
                _currentTestName = value;
                SetViewCurrentTestName();
            }
        }

        private void SetViewCurrentTestName()
        {
            if (null != _iW32Console)
                _iW32Console.CurrentTestName = CurrentTestName;
        }
        #endregion properties

        private void InitializeHarness()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(ResolveEventHandler);
            currentDomain.SetData("Harness", this);
            _testBatch = new TestBuilder();
            _testCount = _testBatch.TestCount;
            TestCountString = _testCount.ToString();
            _queuedTests = new Dictionary<int, ITest>();
        }

        private void LoadAllTests()
        {
            _availableTests = new Dictionary<string, ITest>();
            for (int i = 0; i < _testCount; i++)
            {
                _testBatch.GetNextTest(out _currentTest, this);
                ITest iTest = _currentTest;
                _availableTests.Add(iTest.TestName, iTest);
            }
        }

        private void ExecuteTests()
        {
            for (int i = 0; i < _queuedTests.Count; i++)
            {
                
                if (null != _iW32Console)
                    _iW32Console.BeginNewTest();

                _currentTest = _queuedTests[i];

                if (!Directory.Exists("./Log"))
                    Directory.CreateDirectory("./Log");

                string fileName = string.Format("{0}_{1}_{2}", i, _currentTest.TestName, DateTime.Now.ToFileTimeUtc());
                _writer = new StreamWriter("./Log/" + fileName + ".log");

                string logDir = Directory.GetCurrentDirectory() + "/Log/";
                if(null != _iW32Console)
                    _iW32Console.SetLogFilePath(logDir + fileName + ".log");

                _currentTestRunStatus = TestRunStatus.StatusTestPreparing;
                SetStatusMessage("Preparing to run next test...");

                if (null != _iW32Console)
                {
                    _iW32Console.CurrentTestResults = GetStatusString();
                    _iW32Console.CurrentTestId = i.ToString();
                }
                CurrentTestCountString = (i + 1).ToString();
                CurrentTestName = _currentTest.TestName;                

                ExecuteTestDelegate executeTest = new ExecuteTestDelegate(TestThread);
                AsyncCallback executeTestCallback = new AsyncCallback(TestThreadCallback);
                executeTest.BeginInvoke(_currentTest, executeTestCallback, executeTest);

                while (TestRunStatus.StatusTestComplete != _currentTestRunStatus)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                _writer.Close();
                _testId++;
            }
        }

        private void TestThread(ITest test)
        {
            _currentTestRunStatus = TestRunStatus.StatusTestInprocess;
            test.ExecuteTest();
        }

        private void TestThreadCallback(IAsyncResult result)
        {
            System.Threading.Thread.Sleep(1000);
            _currentTestRunStatus = TestRunStatus.StatusTestComplete;
        }

        private void WriteTestDetails(string message)
        {
            if (null != _iW32Console)
                _iW32Console.CurrentStatusMessage = message;
            if (null == _iW32Console)
            {
                lock (typeof(Console))
                {
                    Console.SetCursorPosition(_testDetailsLineLength, _testDetailsPostition);
                    try
                    {
                        if (Console.WindowWidth < message.Length + _testDetailsLineLength)
                        {
                            Console.WindowWidth = message.Length + _testDetailsLineLength;
                        }
                    }
                    finally
                    {
                        Console.WriteLine(message);
                    }
                }
                _testDetailsLineLength = message.Length + 5;
            }
        }

        private void RecordResults()
        {
            WriteTestDetails(GetStatusString());
        }

        private string GetStatusString()
        {
            string resultString = "\tSTATUS\t:\t";
            switch (_currentTestStatus)
            {
                case TestResult.StatusSuccess:
                    resultString += "Success";
                    break;
                case TestResult.StatusFailed:
                    resultString += "Failed";
                    break;
                case TestResult.StatusInProgress:
                    resultString += "In Progress";
                    break;
                case TestResult.StatusPreparingTest:
                    resultString += "Preparing Test";
                    break;
                default:
                    resultString += "Unknown";
                    break;
            }
            return resultString;
        }

        #region Implementation of IHarness
        public void SetStatusMessage(string statusMessage)
        {
            if (null != _iW32Console)
            {
                _iW32Console.CurrentStatusMessage = statusMessage;
                LogInfo(statusMessage);
            }
            if (null == _iW32Console)
            {
                lock (typeof(Console))
                {

                    Console.SetCursorPosition(0, _testDetailsPostition + 2);
                    string padString = "";
                    if (_lastLineLength < statusMessage.Length)
                    {
                        padString = padString.PadLeft(statusMessage.Length, '\u0000');
                        Console.WriteLine(padString);
                        _lastLineLength = statusMessage.Length;
                    }
                    else
                    {
                        padString = padString.PadLeft(_lastLineLength, '\u0000');
                        _lastLineLength = statusMessage.Length;
                        Console.WriteLine(padString);
                    }
                    Console.SetCursorPosition(0, _testDetailsPostition + 2);
                    try
                    {
                        if (Console.WindowWidth < statusMessage.Length)
                        {
                            Console.WindowWidth = statusMessage.Length;
                        }
                    }
                    finally
                    {
                        Console.WriteLine(statusMessage);
                    }
                }
            }
        }

        public void LogDebug(string message)
        {
            string messageText = string.Format("DEBUG {0} {1}", DateTime.Now.ToLongTimeString(), message);
            _writer.WriteLine(messageText);
        }

        public void LogError(string message)
        {
            string messageText = string.Format("ERROR {0} {1}", DateTime.Now.ToLongTimeString(), message);
            _writer.WriteLine(messageText);
        }

        public void LogInfo(string message)
        {
            string messageText = string.Format("INFO  {0} {1}", DateTime.Now.ToLongTimeString(), message);
            _writer.WriteLine(messageText);
        }
        
        public void SetConsoleTitle(string consoleTitle)
        {
            WriteTestDetails(consoleTitle);
            if (null == _iW32Console)
            {
                Console.Title = consoleTitle;
            }
        }

        public void SetTestResults(TestResult status)
        {
            _currentTestStatus = status;
            if(null != _iW32Console)
                _iW32Console.CurrentTestResults = GetStatusString();
        }

        public void AddTestToQue(ITest iTest)
        {
            _queuedTests.Add(_queuedTests.Count, iTest);
        }

        public bool GetAssemblyDependency(Dictionary<string, string> dependencyDictionary, string dependencyKey, out string dependencyPath, out string dependencyType)
        {
            bool retVal = false;
            dependencyPath = null;
            dependencyType = null;

            if (dependencyDictionary.ContainsKey(dependencyKey))
            {
                string[] dependencyFields = GetDependencyFields(dependencyDictionary, dependencyKey);
                Dictionary<string, string> dependencyFieldsDictionary = GetDependencyDetails(dependencyFields);
                string extension = dependencyFieldsDictionary["extension"];
                extension = extension.TrimStart('.');
                dependencyPath = string.Format("{0}{1}.{2}", dependencyFieldsDictionary["path"], dependencyKey, extension);
                dependencyType = dependencyFieldsDictionary["typeName"];
                retVal = true;
            }
            return retVal;
        }

        public string GetDependencyValue(Dictionary<string, string> dependencyDictionary, string dependencyKey)
        {
            string[] dependencyArray = GetDependencyFields(dependencyDictionary, dependencyKey);
            Dictionary<string, string> dependencyFields = GetDependencyDetails(dependencyArray);
            return dependencyFields["value"];
        }

        public T GetDependencyValue<T>(Dictionary<string, string> dependencyDictionary, string dependencyKey)
        {
            string[] dependencyArray = GetDependencyFields(dependencyDictionary, dependencyKey);
            Dictionary<string, string> dependencyFields = GetDependencyDetails(dependencyArray);
            return (T)Convert.ChangeType(dependencyFields["value"], typeof(T));
        }

        private string[] GetDependencyFields(Dictionary<string, string> dependencyDictionary, string dependencyKey)
        {
            return dependencyDictionary[dependencyKey].Split('|');
        }

        private Dictionary<string, string> GetDependencyDetails(string[] dependencyFields)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            foreach (string dependencyField in dependencyFields)
            {
                string[] parsedField = dependencyField.Split('=');
                details[parsedField[0]] = parsedField[1];
            }
            return details;
        }
        #endregion Implementation of IHarness

        #region event handlers
        
        #region view event handlers
        public void ExecuteButton_Click(object sender, EventArgs e)
        {
            ExecuteTests();
        }

        public void ClearQue_Click(object sender, EventArgs e)
        {
            _queuedTests.Clear();
        }
        #endregion view event handlers

        private static Assembly ResolveEventHandler(object sender, ResolveEventArgs e)
        {
            string assemblyToFind = e.Name.Substring(0, e.Name.IndexOf(','));
            string dependencyPath;
            string dependencyType;
            Harness thisHarness = (Harness)((AppDomain)sender).GetData("Harness");
            Assembly retVal = null;

            if (thisHarness._currentTest.GetDependency(assemblyToFind, out dependencyPath, out dependencyType))
            {
                string[] dependencyTypes = dependencyType.Split(',');
                foreach (string type in dependencyTypes)
                {
                    try
                    {
                        retVal = Assembly.LoadFrom(dependencyPath);
                        Type typeDepends = retVal.GetType(type);
                        if (null != thisHarness._constructorParameters)
                        {
                            Activator.CreateInstance(typeDepends, thisHarness._constructorParameters);
                            thisHarness._constructorParameters = null;
                        }
                        else
                            Activator.CreateInstance(typeDepends);
                    }
                    catch (MissingMethodException)
                    {
                        // there is no constructor. It must be a singleton
                        retVal = Assembly.LoadFrom(dependencyPath);
                        Type typeDepends = retVal.GetType(type);
                        MethodInfo methodInfo = typeDepends.GetMethod("GetInstance", BindingFlags.Static | BindingFlags.Public);
                        methodInfo.Invoke(retVal, null);
                    }
                }
            }
            return retVal;
        }
        #endregion event handlers

        #region IHarness Members


        public string CurrentTestStatus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IHarness Members


        public string CurrentTestResult
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}