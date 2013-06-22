using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Threading;

namespace net.PaulChristensen.TestRunnerW32.DataObjects
{
    internal class Results : ObservableCollectionBase<Result>
    {
        public Results():base()
        {
        }
    }
}