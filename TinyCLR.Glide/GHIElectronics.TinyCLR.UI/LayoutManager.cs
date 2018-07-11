namespace GHIElectronics.TinyCLR.UI
{
    using GHIElectronics.TinyCLR.UI.Media;
    using GHIElectronics.TinyCLR.UI.Threading;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    internal class LayoutManager : DispatcherObject
    {
        private bool _isUpdating;
        private bool _gotException;
        private bool _layoutRequestPosted;
        private UIElement _forceLayoutElement;
        private LayoutQueue _arrangeQueue;
        private LayoutQueue _measureQueue;
        private DispatcherOperationCallback _updateLayoutBackground;
        private DispatcherOperationCallback _updateCallback;

        private LayoutManager()
        {
            this._updateLayoutBackground = new DispatcherOperationCallback(this.UpdateLayoutBackground);
            this._updateCallback = new DispatcherOperationCallback(this.UpdateLayoutCallback);
        }

        public static LayoutManager From(Dispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentException();
            }
            if (dispatcher._layoutManager == null)
            {
                Type type = typeof(SingletonLock);
                lock (type)
                {
                    if (dispatcher._layoutManager == null)
                    {
                        dispatcher._layoutManager = new LayoutManager();
                    }
                }
            }
            return dispatcher._layoutManager;
        }

        private void getProperArrangeRect(UIElement element, out int x, out int y, out int width, out int height)
        {
            x = element._finalX;
            y = element._finalY;
            width = element._finalWidth;
            height = element._finalHeight;
            if (element.Parent == null)
            {
                int num;
                int num2;
                x = y = 0;
                element.GetDesiredSize(out num, out num2);
                if (element._previousAvailableWidth == 0x7ffff)
                {
                    width = num;
                }
                if (element._previousAvailableHeight == 0x7ffff)
                {
                    height = num2;
                }
            }
        }

        private void invalidateTreeIfRecovering()
        {
            if ((this._forceLayoutElement != null) || this._gotException)
            {
                if (this._forceLayoutElement != null)
                {
                    UIElement rootUIElement = this._forceLayoutElement.RootUIElement;
                    this.markTreeDirtyHelper(rootUIElement);
                    this.MeasureQueue.Add(rootUIElement);
                }
                this._forceLayoutElement = null;
                this._gotException = false;
            }
        }

        private bool LimitExecution(ref long loopStartTime)
        {
            if (loopStartTime == 0)
            {
                loopStartTime = DateTime.Now.Ticks;
            }
            else if ((DateTime.Now.Ticks - loopStartTime) > 0x2eb120L)
            {
                base.Dispatcher.BeginInvoke(this._updateLayoutBackground, this);
                return true;
            }
            return false;
        }

        private void markTreeDirtyHelper(UIElement e)
        {
            if (e != null)
            {
                e._flags |= UIElement.Flags.InvalidArrange | UIElement.Flags.InvalidMeasure;
                UIElementCollection elements = e._logicalChildren;
                if (elements != null)
                {
                    int count = elements.Count;
                    while (count-- > 0)
                    {
                        this.markTreeDirtyHelper(elements[count]);
                    }
                }
            }
        }

        private void NeedsRecalc()
        {
            if (!this._layoutRequestPosted && !this._isUpdating)
            {
                this._layoutRequestPosted = true;
                MediaContext.From(base.Dispatcher).BeginInvokeOnRender(this._updateCallback, this);
            }
        }

        public void UpdateLayout()
        {
            base.VerifyAccess();
            if (!this._isUpdating)
            {
                this._isUpdating = true;
                WindowManager.Instance.Invalidate();
                LayoutQueue measureQueue = this.MeasureQueue;
                LayoutQueue arrangeQueue = this.ArrangeQueue;
                int num = 0;
                bool flag = true;
                UIElement parent = null;
                try
                {
                    this.invalidateTreeIfRecovering();
                    while (!this.MeasureQueue.IsEmpty || !this.ArrangeQueue.IsEmpty)
                    {
                        if (++num > 0x99)
                        {
                            base.Dispatcher.BeginInvoke(this._updateLayoutBackground, this);
                            parent = null;
                            flag = false;
                            return;
                        }
                        int num2 = 0;
                        long loopStartTime = 0L;
                    Label_0070:
                        if (++num2 > 0x99)
                        {
                            num2 = 0;
                            if (this.LimitExecution(ref loopStartTime))
                            {
                                parent = null;
                                flag = false;
                                return;
                            }
                        }
                        parent = measureQueue.GetTopMost();
                        if (parent != null)
                        {
                            parent.Measure(parent._previousAvailableWidth, parent._previousAvailableHeight);
                            measureQueue.RemoveOrphans(parent);
                            goto Label_0070;
                        }
                        num2 = 0;
                        loopStartTime = 0L;
                    Label_00C7:
                        if (++num2 > 0x99)
                        {
                            num2 = 0;
                            if (this.LimitExecution(ref loopStartTime))
                            {
                                parent = null;
                                flag = false;
                                return;
                            }
                        }
                        parent = arrangeQueue.GetTopMost();
                        if (parent != null)
                        {
                            int num4;
                            int num5;
                            int num6;
                            int num7;
                            this.getProperArrangeRect(parent, out num4, out num5, out num6, out num7);
                            parent.Arrange(num4, num5, num6, num7);
                            arrangeQueue.RemoveOrphans(parent);
                            goto Label_00C7;
                        }
                    }
                    parent = null;
                    flag = false;
                }
                finally
                {
                    this._isUpdating = false;
                    this._layoutRequestPosted = false;
                    if (flag)
                    {
                        this._gotException = true;
                        this._forceLayoutElement = parent;
                        base.Dispatcher.BeginInvoke(this._updateLayoutBackground, this);
                    }
                }
            }
        }

        private object UpdateLayoutBackground(object arg)
        {
            this.NeedsRecalc();
            return null;
        }

        private object UpdateLayoutCallback(object arg)
        {
            this.UpdateLayout();
            return null;
        }

        public LayoutQueue ArrangeQueue
        {
            get
            {
                if (this._arrangeQueue == null)
                {
                    Type type = typeof(SingletonLock);
                    lock (type)
                    {
                        if (this._arrangeQueue == null)
                        {
                            this._arrangeQueue = new LayoutQueue(this);
                        }
                    }
                }
                return this._arrangeQueue;
            }
        }

        public static LayoutManager CurrentLayoutManager
        {
            get
            {
                return From(Dispatcher.CurrentDispatcher);
            }
        }

        public LayoutQueue MeasureQueue
        {
            get
            {
                if (this._measureQueue == null)
                {
                    Type type = typeof(SingletonLock);
                    lock (type)
                    {
                        if (this._measureQueue == null)
                        {
                            this._measureQueue = new LayoutQueue(this);
                        }
                    }
                }
                return this._measureQueue;
            }
        }

        public class LayoutQueue
        {
            private LayoutManager _layoutManager;
            private ArrayList _elements;

            public LayoutQueue(LayoutManager layoutManager)
            {
                this._layoutManager = layoutManager;
                this._elements = new ArrayList();
            }

            public void Add(UIElement e)
            {
                if (!this._elements.Contains(e))
                {
                    this.RemoveOrphans(e);
                    this._elements.Add(e);
                }
                this._layoutManager.NeedsRecalc();
            }

            public UIElement GetTopMost()
            {
                UIElement element = null;
                int num = 0x7fffffff;
                int count = this._elements.Count;
                for (int i = 0; i < count; i++)
                {
                    UIElement element2 = (UIElement) this._elements[i];
                    UIElement element3 = element2._parent;
                    int num4 = 0;
                    while ((element3 != null) && (num4 < num))
                    {
                        num4++;
                        element3 = element3._parent;
                    }
                    if (num4 < num)
                    {
                        num = num4;
                        element = element2;
                    }
                }
                return element;
            }

            public void Remove(UIElement e)
            {
                this._elements.Remove(e);
            }

            public void RemoveOrphans(UIElement parent)
            {
                for (int i = this._elements.Count - 1; i >= 0; i--)
                {
                    if (((UIElement) this._elements[i])._parent == parent)
                    {
                        this._elements.RemoveAt(i);
                    }
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return (this._elements.Count == 0);
                }
            }
        }

        private class SingletonLock
        {
        }
    }
}

