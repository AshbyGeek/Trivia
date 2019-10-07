using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Trivia
{
    public class ObservableCollectionItemChanged<T> : ObservableCollection<T>
    {
        public event EventHandler<PropertyChangedEventArgs> ItemPropertyChanged;

        public ObservableCollectionItemChanged()
        {
            CollectionChanged += ObservableCollectionChildChanged_CollectionChanged;
        }

        private void ObservableCollectionChildChanged_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach(var item in e.NewItems)
                {
                    if (item is INotifyPropertyChanged itemNotify)
                    {
                        itemNotify.PropertyChanged += ItemNotify_PropertyChanged;
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach(var item in e.OldItems)
                {
                    if (item is INotifyPropertyChanged itemNotify)
                    {
                        itemNotify.PropertyChanged -= ItemNotify_PropertyChanged;
                    }
                }
            }
        }

        private void ItemNotify_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ItemPropertyChanged(sender, e);
        }
    }
}
