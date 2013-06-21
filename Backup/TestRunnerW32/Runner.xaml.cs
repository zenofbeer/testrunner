/*
 * TestRunner - UI Component for TestHarnessLib, a test harness tool.
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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using net.PaulChristensen.TestHarnessLib;
using net.PaulChristensen.TestRunnerW32.DataObjects;

namespace net.PaulChristensen.TestRunnerW32
{
    /// <summary>
    /// Interaction logic for Runner.xaml
    /// </summary>
    public partial class Runner : Window, IW32Console, INotifyPropertyChanged
    {
        #region private fields
        private readonly IHarness _iHarness;
        private readonly Results _results;
        private Result _currentResult;
        private AvailableTests _availableTests;
        private Point _startPoint;
        private bool _executionComplete;
        private delegate void ExecuteTestDelegate(object sender, RoutedEventArgs e);
        private List<Button> _buttonList;
        private List<string> _logFileName = new List<string>();
        private string _testCountString;
        private string _runTestCountString;
        private string _currentTestCountString;
        private int _runTestCountHolder;
        private string _testTitle;
        #endregion private fields

        #region initialization
        public Runner()
        {
            InitializeComponent();
            PopulateButtonList();
            RunTestCountString = "0";
            CurrentTestCountString = "0";
            _availableTests = new AvailableTests();
            _iHarness = new Harness(this);
            _results = new Results();
        }

        private void PopulateButtonList()
        {
            _buttonList = new List<Button> 
            {
                ExecuteTestsButton, 
                ClearQueButton
            };
        }

        private void WithXaml_Load(object sender, EventArgs e)
        {
            BaseWindowGrid.DataContext = this;
            ResultsView.ItemsSource = _results;
            AvailableTests.ItemsSource = _availableTests;
        }

        private void UpdateControlEnabled()
        {
            foreach (var button in _buttonList)
            {
                button.Dispatcher.Invoke(
                    DispatcherPriority.Normal, new Action(
                                                   delegate()
                                                       {
                                                           button.IsEnabled = _executionComplete;
                                                       }
                                                   ));
            }
        }
        #endregion initialization

        #region properties
        public string CurrentTestId
        {
            get { return _currentResult.CurrentTestId; }
            set { _currentResult.CurrentTestId = value; }
        }

        public string CurrentTestName
        {
            get { return _currentResult.CurrentTestName; }
            set { _currentResult.CurrentTestName = value; }
        }

        public string CurrentStatusMessage
        {
            get { return _currentResult.CurrentTestStatus; }
            set { _currentResult.CurrentTestStatus = value; }
        }

        public string CurrentTestResults
        {
            get { return _currentResult.CurrentTestResults; }
            set { _currentResult.CurrentTestResults = value; }
        }

        public string TestCountString
        {
            get { return _testCountString; }
            set
            {
                _testCountString = value;
                PropertyChangedHandler("TestCountString");
            }
        }

        public string RunTestCountString
        {
            get { return _runTestCountString; }
            set
            {
                _runTestCountString = value;
                PropertyChangedHandler("RunTestCountString");
            }
        }


        public string CurrentTestCountString
        {
            get { return _currentTestCountString; }
            set
            {
                _currentTestCountString = value;
                PropertyChangedHandler("CurrentTestCountString");
            }
        }

        public string TestTitle
        {
            get { return _testTitle; }
            set
            {
                _testTitle = value;
                PropertyChangedHandler("TestTitle");
            }
        }
        #endregion properties

        #region public methods
        public void BeginNewTest()
        {
            _currentResult = new Result();
            _results.Add(_currentResult);
        }

        public void AddAvailableTest(ITest iTest)
        {
            _availableTests.Add(iTest);
        }

        public void SetLogFilePath(string logFilePath)
        {
            _logFileName.Add(logFilePath);
        }
        #endregion public methods

        #region private methods
        private void HarnessThread(object sender, RoutedEventArgs e)
        {
            _iHarness.ExecuteButton_Click(sender, (EventArgs) e);
        }

        private void TestThreadCallback(IAsyncResult result)
        {
            _executionComplete = true;
            UpdateControlEnabled();
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T) current;
                }
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }
        #endregion private methods

        #region implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void PropertyChangedHandler(string sender)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(sender));
        }
        #endregion implementation of INotifyPropertyChanged

        private void ClearQueButton_Click(object sender, RoutedEventArgs e)
        {
            _iHarness.ClearQue_Click(sender, (EventArgs) e);
            ExecutionList.Items.Clear();
            _logFileName = new List<string>();

            _results.Clear();
            RunTestCountString = "0";
            CurrentTestCountString = "0";
        }

        private void ExecuteTestsButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteTestDelegate executeTest = new ExecuteTestDelegate(HarnessThread);
            AsyncCallback executeTestCallback = new AsyncCallback(TestThreadCallback);
            _executionComplete = false;
            UpdateControlEnabled();
            executeTest.BeginInvoke(sender, e, executeTestCallback, executeTest);
        }        

        private void AvailableTests_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        private void AvailableTests_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = _startPoint - mousePos;

            if ((e.LeftButton == MouseButtonState.Pressed) &&
                //(Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance) &&
                //(Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                (Math.Abs(diff.X) >= 0) &&
                (Math.Abs(diff.Y) >= 0))
            {
                // get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject) e.OriginalSource);
                if (null != listViewItem)
                {
                    // find the data behind the ListViewItem
                    ITest iTest = (ITest) listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                    // initialize the drag and drop
                    DataObject dragData = new DataObject("myFormat", iTest);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy);
                }
            }
        }

        private void ExecutionList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                ITest iTest = e.Data.GetData("myFormat") as ITest;
                ListView listView = sender as ListView;
                listView.Items.Add(iTest);
                _iHarness.AddTestToQue(iTest);
                _runTestCountHolder++;
                RunTestCountString = _runTestCountHolder.ToString();
            }
        }

        private void ExecutionList_DragEnter(object sender, DragEventArgs e)
        {
            if ((!e.Data.GetDataPresent("myData")) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ResultsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResultsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedIndex = this.ResultsView.SelectedIndex;
            if (selectedIndex > -1)
            {
                System.Diagnostics.Process.Start(_logFileName[selectedIndex]);
            }
        }

        private void QueAllTests_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Yet Implemented. This will load all tests in to the queue.");
        }

        private void SaveSuiteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Yet Implemented. This will save the set of queued tests to a configuration file for future executions.");
        }
    }
}