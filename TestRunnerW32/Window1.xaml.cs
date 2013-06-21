using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using net.PaulChristensen.TestHarnessLib;
using net.PaulChristensen.TestRunnerW32.DataObjects;

namespace net.PaulChristensen.TestRunnerW32
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window, IW32Console, INotifyPropertyChanged
    {
        #region private fields
        private IHarness _iHarness;
        private Results _results;
        private Result _currentResult;
        private AvailableTests _availableTests;
        private Point _startPoint;
        private bool _executionComplete;
        private delegate void ExecuteTestDelegate(object sender, RoutedEventArgs e);
        private List<Button> _buttonList;
        #endregion private fields

        public Window1()
        {
            InitializeComponent();
            PopulateButtonList();
            _availableTests = new AvailableTests();
            _iHarness = new Harness(this);
            _results = new Results();            
        }

        #region properties
        public string CurrentTestId
        {
            get
            {
                return _currentResult.CurrentTestId;
            }
            set
            {
                _currentResult.CurrentTestId = value;
            }
        }

        public string CurrentTestName
        {
            get
            {
                return _currentResult.CurrentTestName;
            }
            set
            {
                _currentResult.CurrentTestName = value;
            }
        }

        public string CurrentTestStatus
        {
            get
            {
                return _currentResult.CurrentTestStatus;
            }
            set
            {
                _currentResult.CurrentTestStatus = value;
            }
        }

        public string CurrentTestResults
        {
            get
            {
                return _currentResult.CurrentTestResults;
            }
            set
            {
                _currentResult.CurrentTestResults = value;
            }
        }

        public string TestCountString
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// gives the count of the current executing test
        /// </summary>
        public string CurrentTestCountString
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public string TestTitle
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        #endregion properties

        #region methods
        public void BeginNewTest()
        {
            _currentResult = new Result();
            _results.Add(_currentResult);
        }

        public void AddAvailableTest(ITest iTest)
        {
            _availableTests.Add(iTest);
        }

        private void PopulateButtonList()
        {
            _buttonList = new List<Button>();
            _buttonList.Add(button1);
            _buttonList.Add(buttonClearQue);
        }

        private void UpdateControlEnabled()
        {
            //foreach (var button in _buttonList)
            //    button.IsEnabled = _executionComplete;
            foreach (var button in _buttonList)
            {
                button.Dispatcher.Invoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                            {
                                button.IsEnabled = _executionComplete;
                            }
                        ));
            }
        }

        #endregion methods

        #region events
        #region implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedHandler(string sender)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(sender));
        }
        #endregion implementation of INotifyPropertyChanged

        private void WithXaml_Load(object sender, EventArgs e)
        {
            BaseWindowGrid.DataContext = _iHarness;
            resultsView.ItemsSource = _results;
            availableTests.ItemsSource = _availableTests;
        }

        private void buttonClearQue_Click(object sender, RoutedEventArgs e)
        {
            _iHarness.ClearQue_Click(sender, (EventArgs)e);
            executionList.Items.Clear();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ExecuteTestDelegate executeTest = new ExecuteTestDelegate(HarnessThread);
            AsyncCallback executeTestCallback = new AsyncCallback(TestThreadCallback);
            _executionComplete = false;
            UpdateControlEnabled();
            executeTest.BeginInvoke(sender, e, executeTestCallback, executeTest);
        }

        private void HarnessThread(object sender, RoutedEventArgs e)
        {                     
            _iHarness.ExecuteButton_Click(sender, (EventArgs)e);
        }

        private void TestThreadCallback(IAsyncResult result)
        {
            _executionComplete = true;
            UpdateControlEnabled();
        }

        private void availableTests_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        private void availableTests_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = _startPoint - mousePos;

            if ((e.LeftButton == MouseButtonState.Pressed) &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance) &&
                (Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // find the data behind the ListViewItem
                ITest iTest = (ITest)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                // Initialize the drag & drop
                DataObject dragData = new DataObject("myFormat", iTest);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy);
            }
        }

        private void executionList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                ITest iTest = e.Data.GetData("myFormat") as ITest;
                ListView listView = sender as ListView;
                listView.Items.Add(iTest);
                _iHarness.AddTestToQue(iTest);
            }
        }

        private void executionList_DragEnter(object sender, DragEventArgs e)
        {
            if ((!e.Data.GetDataPresent("test")) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        #endregion events                
        
    }
}