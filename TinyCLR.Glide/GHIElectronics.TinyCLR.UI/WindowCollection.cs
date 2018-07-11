namespace GHIElectronics.TinyCLR.UI
{
    using System;
    using System.Collections;
    using System.Reflection;

    public sealed class WindowCollection : ICollection, IEnumerable
    {
        private ArrayList _list;

        public WindowCollection()
        {
            ArrayList list1 = new ArrayList {
                Capacity = 1
            };
            this._list = list1;
        }

        internal WindowCollection(int count)
        {
            ArrayList list1 = new ArrayList {
                Capacity = 1
            };
            this._list = list1;
        }

        internal int Add(Window win)
        {
            return this._list.Add(win);
        }

        internal WindowCollection Clone()
        {
            WindowCollection windows;
            object syncRoot = this._list.SyncRoot;
            lock (syncRoot)
            {
                windows = new WindowCollection(this._list.Count);
                for (int i = 0; i < this._list.Count; i++)
                {
                    windows._list.Add(this._list[i]);
                }
            }
            return windows;
        }

        public void CopyTo(Window[] array, int index)
        {
            this._list.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        internal bool HasItem(Window win)
        {
            object syncRoot = this._list.SyncRoot;
            lock (syncRoot)
            {
                for (int i = 0; i < this._list.Count; i++)
                {
                    if (this._list[i] == win)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal void Remove(Window win)
        {
            this._list.Remove(win);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this._list.CopyTo(array, index);
        }

        public Window this[int index]
        {
            get
            {
                return (this._list[index] as Window);
            }
        }

        public int Count
        {
            get
            {
                return this._list.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this._list.IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this._list.SyncRoot;
            }
        }
    }
}

