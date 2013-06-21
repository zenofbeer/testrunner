using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Threading;

namespace net.PaulChristensen.TestRunnerW32.DataObjects
{
    public class ObservableCollectionBase<T> : ObservableCollection<T>
    {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        #region constructors
        public ObservableCollectionBase()
        {
        }
        #endregion constructors

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var eh = CollectionChanged;
            if (null != eh)
            {
                Dispatcher dispatcher = (from NotifyCollectionChangedEventHandler nh in eh.GetInvocationList()
                                         let dpo = nh.Target as DispatcherObject
                                         where dpo != null
                                         select dpo.Dispatcher).FirstOrDefault();
                if ((null != dispatcher) && (false == dispatcher.CheckAccess()))
                    dispatcher.Invoke(DispatcherPriority.DataBind, (Action) (() => OnCollectionChanged(e)));
                else
                    foreach(NotifyCollectionChangedEventHandler nh in eh.GetInvocationList())
                        nh.Invoke(this, e);
            }
        }
    }
}