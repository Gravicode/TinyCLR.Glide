namespace GHIElectronics.TinyCLR.UI
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class UIElementCollection : ICollection, IEnumerable
    {
        internal UIElement[] _items;
        internal int _size;
        private int _version;
        private UIElement _owner;
        private const int c_defaultCapacity = 2;
        private const int c_growFactor = 2;

        public UIElementCollection(UIElement owner)
        {
            this._owner = owner;
        }

        public int Add(UIElement element)
        {
            this._owner.VerifyAccess();
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if ((element._parent != null) || element.GetIsRootElement())
            {
                throw new ArgumentException("element has parent");
            }
            if ((this._items == null) || (this._size == this._items.Length))
            {
                this.EnsureCapacity(this._size + 1);
            }
            int num2 = this._size;
            this._size = num2 + 1;
            int index = num2;
            this.ConnectChild(index, element);
            this._version++;
            this._owner.InvalidateMeasure();
            return index;
        }

        public void Clear()
        {
            this._owner.VerifyAccess();
            for (int i = 0; i < this._size; i++)
            {
                if (this._items[i] != null)
                {
                    this.DisconnectChild(i);
                }
                this._items[i] = null;
            }
            this._size = 0;
            this._version++;
            this._owner.InvalidateMeasure();
        }

        private void ConnectChild(int index, UIElement value)
        {
            value.VerifyAccess();
            value._parent = this._owner;
            UIElement.PropagateFlags(this._owner, UIElement.Flags.IsSubtreeDirtyForRender);
            UIElement.PropagateFlags(value, UIElement.Flags.IsSubtreeDirtyForRender);
            value._flags |= UIElement.Flags.IsDirtyForRender;
            this._items[index] = value;
            this._version++;
            UIElement.PropagateResumeLayout(value);
            this._owner.OnChildrenChanged(value, null, index);
        }

        public bool Contains(UIElement element)
        {
            if (element != null)
            {
                return (element._parent == this._owner);
            }
            for (int i = 0; i < this._size; i++)
            {
                if (this._items[i] == null)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if ((index < 0) || ((array.Length - index) < this._size))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            Array.Copy(this._items, 0, array, index, this._size);
        }

        public virtual void CopyTo(UIElement[] array, int index)
        {
            this.CopyTo((Array) array, index);
        }

        private void DisconnectChild(int index)
        {
            UIElement v = this._items[index];
            this._items[index] = null;
            v._parent = null;
            UIElement.PropagateFlags(this._owner, UIElement.Flags.IsSubtreeDirtyForRender);
            this._version++;
            UIElement.PropagateSuspendLayout(v);
            v._parent.OnChildrenChanged(null, v, index);
        }

        private void EnsureCapacity(int min)
        {
            if (this.Capacity < min)
            {
                this.Capacity = Math.Max(min, this.Capacity * 2);
            }
        }

        public int IndexOf(UIElement element)
        {
            if ((element == null) || (element._parent == this._owner))
            {
                for (int i = 0; i < this._size; i++)
                {
                    if (this._items[i] == element)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public void Insert(int index, UIElement element)
        {
            this._owner.VerifyAccess();
            if ((index < 0) || (index > this._size))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            if ((element._parent != null) || element.GetIsRootElement())
            {
                throw new ArgumentException("element has parent");
            }
            if ((this._items == null) || (this._size == this._items.Length))
            {
                this.EnsureCapacity(this._size + 1);
            }
            for (int i = this._size - 1; i >= index; i--)
            {
                UIElement element2 = this._items[i];
                this._items[i + 1] = element2;
            }
            this._items[index] = null;
            this._size++;
            this.ConnectChild(index, element);
            this._owner.InvalidateMeasure();
        }

        public void Remove(UIElement element)
        {
            int num = -1;
            this._owner.VerifyAccess();
            if (element != null)
            {
                if (element._parent != this._owner)
                {
                    return;
                }
                this.DisconnectChild(num = this.IndexOf(element));
            }
            else
            {
                for (int i = 0; i < this._size; i++)
                {
                    if (this._items[i] == null)
                    {
                        num = i;
                        break;
                    }
                }
            }
            if (num != -1)
            {
                this._size--;
                for (int i = num; i < this._size; i++)
                {
                    this._items[i] = this._items[i + 1];
                }
                this._items[this._size] = null;
            }
            this._owner.InvalidateMeasure();
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this._size))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            this.Remove(this._items[index]);
        }

        public void RemoveRange(int index, int count)
        {
            this._owner.VerifyAccess();
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((this._size - index) < count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count > 0)
            {
                for (int i = index; i < (index + count); i++)
                {
                    if (this._items[i] != null)
                    {
                        this.DisconnectChild(i);
                        this._items[i] = null;
                    }
                }
                this._size -= count;
                for (int j = index; j < this._size; j++)
                {
                    this._items[j] = this._items[j + count];
                    this._items[j + count] = null;
                }
                this._version++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public virtual int Count
        {
            get
            {
                return this._size;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public virtual int Capacity
        {
            get
            {
                if (this._items == null)
                {
                    return 0;
                }
                return this._items.Length;
            }
            set
            {
                int capacity = this.Capacity;
                if (value != capacity)
                {
                    if (value < this._size)
                    {
                        throw new ArgumentOutOfRangeException("value", "not enough capacity");
                    }
                    if (value > 0)
                    {
                        UIElement[] destinationArray = new UIElement[value];
                        if (this._size > 0)
                        {
                            Array.Copy(this._items, 0, destinationArray, 0, this._size);
                        }
                        this._items = destinationArray;
                    }
                    else
                    {
                        this._items = null;
                    }
                }
            }
        }

        public UIElement this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this._size))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return this._items[index];
            }
            set
            {
                this._owner.VerifyAccess();
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if ((index < 0) || (index >= this._size))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                UIElement element = this._items[index];
                if (element != value)
                {
                    if ((value == null) && (element != null))
                    {
                        this.DisconnectChild(index);
                    }
                    else if (value != null)
                    {
                        if ((value._parent != null) || value.GetIsRootElement())
                        {
                            throw new ArgumentException("element has parent");
                        }
                        this.ConnectChild(index, value);
                    }
                    this._owner.InvalidateMeasure();
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator, ICloneable
        {
            private UIElementCollection _collection;
            private int _index;
            private int _version;
            private UIElement _currentElement;
            internal Enumerator(UIElementCollection collection)
            {
                this._collection = collection;
                this._index = -1;
                this._version = this._collection._version;
                this._currentElement = null;
            }

            public object Clone()
            {
                return base.MemberwiseClone();
            }

            public bool MoveNext()
            {
                if (this._version != this._collection._version)
                {
                    throw new InvalidOperationException("collection changed");
                }
                if ((this._index > -2) && (this._index < (this._collection._size - 1)))
                {
                    this._index++;
                    this._currentElement = this._collection[this._index];
                    return true;
                }
                this._currentElement = null;
                this._index = -2;
                return false;
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
            public UIElement Current
            {
                get
                {
                    if (this._index >= 0)
                    {
                        return this._currentElement;
                    }
                    if (this._index == -1)
                    {
                        throw new InvalidOperationException("not started");
                    }
                    throw new InvalidOperationException("reached end");
                }
            }
            public void Reset()
            {
                if (this._version != this._collection._version)
                {
                    throw new InvalidOperationException("collection changed");
                }
                this._index = -1;
            }
        }
    }
}

