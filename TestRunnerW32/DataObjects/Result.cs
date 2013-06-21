using System.ComponentModel;

namespace net.PaulChristensen.TestRunnerW32.DataObjects
{
    internal class Result : INotifyPropertyChanged
    {
        #region private fields
        private string _currentTestId;
        private string _currentTestName;
        private string _currentTestStatus;
        private string _currentTestResults;
        #endregion private fields

        #region properties
        public string CurrentTestResults
        {
            get
            {
                return _currentTestResults;
            }
            set
            {
                _currentTestResults = value;
                PropertyChangedHandler("CurrentTestResults");
            }
        }

        public string CurrentTestId
        {
            get
            {
                return _currentTestId;
            }
            set
            {
                _currentTestId = value;
                PropertyChangedHandler("CurrentTestId");
            }
        }
        
        public string CurrentTestName
        {
            get
            {
                return _currentTestName;
            }
            set
            {
                _currentTestName = value;
                PropertyChangedHandler("CurrentTestName");
            }
        }

        public string CurrentTestStatus
        {
            get
            {
                return _currentTestStatus;
            }
            set
            {
                _currentTestStatus = value;
                PropertyChangedHandler("CurrentTestStatus");
            }
        }
        #endregion properties

        #region implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void PropertyChangedHandler(string sender)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(sender));
        }
        #endregion implementation of INotifyPropertyChanged
    }
}