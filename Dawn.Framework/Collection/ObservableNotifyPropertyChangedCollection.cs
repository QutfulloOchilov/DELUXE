using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Dawn.Framework.Collection
{
    public class ObservableNotifyPropertyChangedCollection<T> : ObservableCollection<T>
    {

        /// <summary>
        /// 
        /// </summary>
        public ObservableNotifyPropertyChangedCollection()
        {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public ObservableNotifyPropertyChangedCollection(IEnumerable<T> collection)
            : base(collection)
        {
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public ObservableNotifyPropertyChangedCollection(List<T> list)
            : base(list)
        {
            Init();
        }

        void Init()
        {
            foreach (var item in this.OfType<INotifyPropertyChanged>())
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {

            OnCollectionModified(new CollectionModifiedEventArgs(e));
            base.OnCollectionChanged(e);
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionModified(new CollectionModifiedEventArgs(sender, e));
        }

        #region CollectionModified Event

        /// <summary>
        /// Collection changed or an item notified property changed
        /// </summary>
        public event EventHandler<CollectionModifiedEventArgs> CollectionModified;
        void OnCollectionModified(CollectionModifiedEventArgs e)
        {
            var handler = CollectionModified;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }

    public class CollectionModifiedEventArgs : EventArgs
    {
        /// <summary>
        /// The item that notified property changed
        /// </summary>
        public object PropertyChangedItem { get; private set; }

        /// <summary>
        /// Event args from an item's PropertyChanged event
        /// </summary>
        public PropertyChangedEventArgs PropertyChangedEventArgs { get; private set; }

        /// <summary>
        /// Event args from an CollectionChanged event
        /// </summary>
        public NotifyCollectionChangedEventArgs CollectionChangedEventArgs { get; private set; }

        internal CollectionModifiedEventArgs(object item, PropertyChangedEventArgs e)
        {
            PropertyChangedItem = item;
            PropertyChangedEventArgs = e;
        }

        internal CollectionModifiedEventArgs(NotifyCollectionChangedEventArgs e)
        {
            CollectionChangedEventArgs = e;
        }
    }
}
