namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using System;
    using System.Collections;
    using System.Reflection;

    public class ListBoxItemCollection : ICollection, IEnumerable
    {
        private UIElementCollection _items;
        private ListBox _listBox;

        public ListBoxItemCollection(ListBox listBox, UIElementCollection items)
        {
            this._listBox = listBox;
            this._items = items;
        }

        public int Add(ListBoxItem item)
        {
            item.SetListBox(this._listBox);
            return this._items.Add(item);
        }

        public int Add(UIElement element)
        {
            ListBoxItem item = new ListBoxItem {
                Child = element
            };
            return this.Add(item);
        }

        public void Clear()
        {
            this._items.Clear();
        }

        public bool Contains(ListBoxItem item)
        {
            return this._items.Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            this._items.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable) this._items).GetEnumerator();
        }

        public int IndexOf(ListBoxItem item)
        {
            return this._items.IndexOf(item);
        }

        public void Insert(int index, ListBoxItem item)
        {
            this._items.Insert(index, item);
            item.SetListBox(this._listBox);
        }

        public void Remove(ListBoxItem item)
        {
            this._items.Remove(item);
            item.SetListBox(null);
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < this._items.Count))
            {
                this[index].SetListBox(null);
            }
            this._items.RemoveAt(index);
        }

        public ListBoxItem this[int index]
        {
            get
            {
                return (ListBoxItem) this._items[index];
            }
            set
            {
                this._items[index] = value;
                value.SetListBox(this._listBox);
            }
        }

        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this._items.IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this._items.SyncRoot;
            }
        }
    }
}

