using System.Collections.ObjectModel;
using System.Collections.Specialized;
using net.PaulChristensen.TestHarnessLib;

namespace net.PaulChristensen.TestRunnerW32.DataObjects
{
    internal class AvailableTests : ObservableCollectionBase<ITest>
    {
        #region constructors
        public AvailableTests():base()
        {
        }
        #endregion constructors
    }
}