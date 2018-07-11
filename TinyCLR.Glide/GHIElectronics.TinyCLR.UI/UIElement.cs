// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.UIElement
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Media;
using GHIElectronics.TinyCLR.UI.Threading;
using System;
using System.Collections;

namespace GHIElectronics.TinyCLR.UI
{
    public abstract class UIElement : DispatcherObject
    {
        internal const int MAX_ELEMENTS_IN_ROUTE = 256;
        internal UIElement _parent;
        internal UIElementCollection _logicalChildren;
        internal UIElement.Flags _flags;
        private Visibility _visibility;
        internal UIElement.Pair _requestedSize;
        internal UIElement.Pair _anchorInfo;
        private int _marginLeft;
        private int _marginTop;
        private int _marginRight;
        private int _marginBottom;
        protected HorizontalAlignment _horizontalAlignment;
        protected VerticalAlignment _verticalAlignment;
        internal int _finalX;
        internal int _finalY;
        internal int _finalWidth;
        internal int _finalHeight;
        internal int _offsetX;
        internal int _offsetY;
        internal int _renderWidth;
        internal int _renderHeight;
        internal int _previousAvailableWidth;
        internal int _previousAvailableHeight;
        private int _desiredWidth;
        private int _desiredHeight;
        internal int _unclippedWidth;
        internal int _unclippedHeight;
        private static Hashtable _classEventHandlersStore;
        private Hashtable _instanceEventHandlersStore;
        private PropertyChangedEventHandler _isEnabledChanged;
        private PropertyChangedEventHandler _isVisibleChanged;

        protected UIElement()
        {
            UIElement.EnsureClassHandlers();
            this._flags = UIElement.Flags.Enabled | UIElement.Flags.NeverMeasured | UIElement.Flags.NeverArranged;
            this._visibility = Visibility.Visible;
            this._verticalAlignment = VerticalAlignment.Stretch;
            this._horizontalAlignment = HorizontalAlignment.Stretch;
        }

        private static void AddRoutedEventHandler(Hashtable table, RoutedEvent routedEvent, RoutedEventHandler handler, bool handledEventsToo)
        {
            if (routedEvent == null || handler == null)
                throw new ArgumentNullException();
            RoutedEventHandlerInfo eventHandlerInfo = new RoutedEventHandlerInfo(handler, handledEventsToo);
            ArrayList arrayList = (ArrayList)table[(object)routedEvent];
            if (arrayList == null)
                table[(object)routedEvent] = (object)(arrayList = new ArrayList());
            arrayList.Add((object)eventHandlerInfo);
        }

        private static void EnsureClassHandlers()
        {
            if (UIElement._classEventHandlersStore != null)
                return;
            UIElement._classEventHandlersStore = new Hashtable();
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, Buttons.PreviewButtonDownEvent, new RoutedEventHandler(UIElement.OnPreviewButtonDownThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, Buttons.ButtonDownEvent, new RoutedEventHandler(UIElement.OnButtonDownThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, Buttons.PreviewButtonUpEvent, new RoutedEventHandler(UIElement.OnPreviewButtonUpThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, Buttons.ButtonUpEvent, new RoutedEventHandler(UIElement.OnButtonUpThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, Buttons.GotFocusEvent, new RoutedEventHandler(UIElement.OnGotFocusThunk), true);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, Buttons.LostFocusEvent, new RoutedEventHandler(UIElement.OnLostFocusThunk), true);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, TouchEvents.TouchDownEvent, new RoutedEventHandler(UIElement.OnTouchDownThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, TouchEvents.TouchUpEvent, new RoutedEventHandler(UIElement.OnTouchUpThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, TouchEvents.TouchMoveEvent, new RoutedEventHandler(UIElement.OnTouchMoveThunk), false);
            UIElement.AddRoutedEventHandler(UIElement._classEventHandlersStore, GenericEvents.GenericStandardEvent, new RoutedEventHandler(UIElement.OnGenericEventThunk), true);
        }

        private static void OnPreviewButtonDownThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnPreviewButtonDown((ButtonEventArgs)e);
        }

        private static void OnButtonDownThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnButtonDown((ButtonEventArgs)e);
        }

        private static void OnPreviewButtonUpThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnPreviewButtonUp((ButtonEventArgs)e);
        }

        private static void OnButtonUpThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnButtonUp((ButtonEventArgs)e);
        }

        private static void OnGotFocusThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnGotFocus((FocusChangedEventArgs)e);
        }

        private static void OnLostFocusThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnLostFocus((FocusChangedEventArgs)e);
        }

        private static void OnGenericEventThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnGenericEvent((GenericEventArgs)e);
        }

        protected virtual void OnGenericEvent(GenericEventArgs e)
        {
            GenericEvent internalEvent = e.InternalEvent;
            if (internalEvent.EventCategory != (byte)2)
                return;
            TouchGestureEventArgs e1 = new TouchGestureEventArgs()
            {
                Gesture = (TouchGesture)internalEvent.EventMessage,
                X = internalEvent.X,
                Y = internalEvent.Y,
                Arguments = (ushort)internalEvent.EventData
            };
            if (e1.Gesture == TouchGesture.Begin)
                this.OnTouchGestureStarted(e1);
            else if (e1.Gesture == TouchGesture.End)
                this.OnTouchGestureEnded(e1);
            else
                this.OnTouchGestureChanged(e1);
        }

        private static void OnTouchDownThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnTouchDown((TouchEventArgs)e);
        }

        private static void OnTouchUpThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnTouchUp((TouchEventArgs)e);
        }

        private static void OnTouchMoveThunk(object sender, RoutedEventArgs e)
        {
            ((UIElement)sender).OnTouchMove((TouchEventArgs)e);
        }

        protected virtual void OnTouchDown(TouchEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            TouchEventHandler touchDown = this.TouchDown;
            if (touchDown == null)
                return;
            touchDown((object)this, e);
        }

        protected virtual void OnTouchUp(TouchEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            TouchEventHandler touchUp = this.TouchUp;
            if (touchUp == null)
                return;
            touchUp((object)this, e);
        }

        protected virtual void OnTouchMove(TouchEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            TouchEventHandler touchMove = this.TouchMove;
            if (touchMove == null)
                return;
            touchMove((object)this, e);
        }

        protected virtual void OnTouchGestureStarted(TouchGestureEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            TouchGestureEventHandler touchGestureStart = this.TouchGestureStart;
            if (touchGestureStart == null)
                return;
            touchGestureStart((object)this, e);
        }

        protected virtual void OnTouchGestureChanged(TouchGestureEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            TouchGestureEventHandler touchGestureChanged = this.TouchGestureChanged;
            if (touchGestureChanged == null)
                return;
            touchGestureChanged((object)this, e);
        }

        protected virtual void OnTouchGestureEnded(TouchGestureEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            TouchGestureEventHandler touchGestureEnd = this.TouchGestureEnd;
            if (touchGestureEnd == null)
                return;
            touchGestureEnd((object)this, e);
        }

        protected virtual void OnPreviewButtonDown(ButtonEventArgs e)
        {
        }

        protected virtual void OnButtonDown(ButtonEventArgs e)
        {
        }

        protected virtual void OnPreviewButtonUp(ButtonEventArgs e)
        {
        }

        protected virtual void OnButtonUp(ButtonEventArgs e)
        {
        }

        protected virtual void OnGotFocus(FocusChangedEventArgs e)
        {
        }

        protected virtual void OnLostFocus(FocusChangedEventArgs e)
        {
        }

        public event TouchEventHandler TouchDown;

        public event TouchEventHandler TouchUp;

        public event TouchEventHandler TouchMove;

        public event TouchGestureEventHandler TouchGestureStart;

        public event TouchGestureEventHandler TouchGestureChanged;

        public event TouchGestureEventHandler TouchGestureEnd;

        public void GetDesiredSize(out int width, out int height)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                width = 0;
                height = 0;
            }
            else
            {
                width = this._desiredWidth;
                height = this._desiredHeight;
            }
        }

        public void GetMargin(out int left, out int top, out int right, out int bottom)
        {
            left = this._marginLeft;
            top = this._marginTop;
            right = this._marginRight;
            bottom = this._marginBottom;
        }

        public void SetMargin(int length)
        {
            this.VerifyAccess();
            this.SetMargin(length, length, length, length);
        }

        public void SetMargin(int left, int top, int right, int bottom)
        {
            this.VerifyAccess();
            this._marginLeft = left;
            this._marginTop = top;
            this._marginRight = right;
            this._marginBottom = bottom;
            this.InvalidateMeasure();
        }

        public int ActualWidth
        {
            get
            {
                return this._renderWidth;
            }
        }

        public int ActualHeight
        {
            get
            {
                return this._renderHeight;
            }
        }

        public int Height
        {
            get
            {
                int height;
                if (this.IsHeightSet(out height))
                    return height;
                throw new InvalidOperationException("height not set");
            }
            set
            {
                this.VerifyAccess();
                if (value < 0)
                    throw new ArgumentException();
                if (this._requestedSize == null)
                    this._requestedSize = new UIElement.Pair();
                this._requestedSize._second = value;
                this._requestedSize._status |= 2;
                this.InvalidateMeasure();
            }
        }

        public int Width
        {
            get
            {
                int width;
                if (this.IsWidthSet(out width))
                    return width;
                throw new InvalidOperationException("width not set");
            }
            set
            {
                this.VerifyAccess();
                if (value < 0)
                    throw new ArgumentException();
                if (this._requestedSize == null)
                    this._requestedSize = new UIElement.Pair();
                this._requestedSize._first = value;
                this._requestedSize._status |= 1;
                this.InvalidateMeasure();
            }
        }

        private bool IsHeightSet(out int height)
        {
            UIElement.Pair requestedSize = this._requestedSize;
            if (requestedSize != null && (requestedSize._status & 2) != 0)
            {
                height = requestedSize._second;
                return true;
            }
            height = 0;
            return false;
        }

        private bool IsWidthSet(out int width)
        {
            UIElement.Pair requestedSize = this._requestedSize;
            if (requestedSize != null && (requestedSize._status & 1) != 0)
            {
                width = requestedSize._first;
                return true;
            }
            width = 0;
            return false;
        }

        public void GetLayoutOffset(out int x, out int y)
        {
            x = this._offsetX;
            y = this._offsetY;
        }

        public void GetRenderSize(out int width, out int height)
        {
            width = this._renderWidth;
            height = this._renderHeight;
        }

        protected UIElementCollection LogicalChildren
        {
            get
            {
                this.VerifyAccess();
                if (this._logicalChildren == null)
                    this._logicalChildren = new UIElementCollection(this);
                return this._logicalChildren;
            }
        }

        protected internal virtual void OnChildrenChanged(UIElement added, UIElement removed, int indexAffected)
        {
            if ((this._flags & UIElement.Flags.IsVisibleCache) == UIElement.Flags.None)
                return;
            if (removed != null && removed._visibility == Visibility.Visible)
            {
                removed._flags &= ~UIElement.Flags.IsVisibleCache;
                removed.OnIsVisibleChanged(true);
            }
            if (added == null || added._visibility != Visibility.Visible)
                return;
            added._flags |= UIElement.Flags.IsVisibleCache;
            added.OnIsVisibleChanged(false);
        }

        public bool IsFocused
        {
            get
            {
                return Buttons.FocusedElement == this;
            }
        }

        private void ComputeAlignmentOffset(int clientWidth, int clientHeight, int arrangeWidth, int arrangeHeight, out int dx, out int dy)
        {
            HorizontalAlignment horizontalAlignment = this.HorizontalAlignment;
            VerticalAlignment verticalAlignment = this.VerticalAlignment;
            if (horizontalAlignment == HorizontalAlignment.Stretch && arrangeWidth > clientWidth)
                horizontalAlignment = HorizontalAlignment.Left;
            if (verticalAlignment == VerticalAlignment.Stretch && arrangeHeight > clientHeight)
                verticalAlignment = VerticalAlignment.Top;
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Center:
                case HorizontalAlignment.Stretch:
                    dx = (clientWidth - arrangeWidth) / 2;
                    break;
                case HorizontalAlignment.Right:
                    dx = clientWidth - arrangeWidth;
                    break;
                default:
                    dx = 0;
                    break;
            }
            switch (verticalAlignment)
            {
                case VerticalAlignment.Center:
                case VerticalAlignment.Stretch:
                    dy = (clientHeight - arrangeHeight) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    dy = clientHeight - arrangeHeight;
                    break;
                default:
                    dy = 0;
                    break;
            }
        }

        internal static void PropagateSuspendLayout(UIElement v)
        {
            if ((v._flags & UIElement.Flags.IsLayoutSuspended) != UIElement.Flags.None)
                return;
            v._flags |= UIElement.Flags.IsLayoutSuspended;
            UIElementCollection logicalChildren = v._logicalChildren;
            if (logicalChildren == null)
                return;
            int count = logicalChildren.Count;
            for (int index = 0; index < count; ++index)
            {
                UIElement v1 = logicalChildren[index];
                if (v1 != null)
                    UIElement.PropagateSuspendLayout(v1);
            }
        }

        internal static void PropagateResumeLayout(UIElement e)
        {
            if ((e._flags & UIElement.Flags.IsLayoutSuspended) == UIElement.Flags.None)
                return;
            e._flags &= ~UIElement.Flags.IsLayoutSuspended;
            int num = (e._flags & UIElement.Flags.InvalidMeasure) == UIElement.Flags.None ? 0 : ((e._flags & UIElement.Flags.NeverMeasured) == UIElement.Flags.None ? 1 : 0);
            bool flag = (e._flags & UIElement.Flags.InvalidArrange) != UIElement.Flags.None && (e._flags & UIElement.Flags.NeverArranged) == UIElement.Flags.None;
            LayoutManager layoutManager = (num | (flag ? 1 : 0)) != 0 ? LayoutManager.From(e.Dispatcher) : (LayoutManager)null;
            if (num != 0)
                layoutManager.MeasureQueue.Add(e);
            if (flag)
                layoutManager.ArrangeQueue.Add(e);
            UIElementCollection logicalChildren = e._logicalChildren;
            if (logicalChildren == null)
                return;
            int count = logicalChildren.Count;
            for (int index = 0; index < count; ++index)
            {
                UIElement e1 = logicalChildren[index];
                if (e1 != null)
                    UIElement.PropagateResumeLayout(e1);
            }
        }

        public void Measure(int availableWidth, int availableHeight)
        {
            this.VerifyAccess();
            UIElement.Flags flags = this._flags;
            if (this.Visibility == Visibility.Collapsed || (flags & UIElement.Flags.IsLayoutSuspended) != UIElement.Flags.None)
            {
                LayoutManager.CurrentLayoutManager.MeasureQueue.Remove(this);
                this._previousAvailableWidth = availableWidth;
                this._previousAvailableHeight = availableHeight;
            }
            else
            {
                if ((flags & UIElement.Flags.InvalidMeasure) == UIElement.Flags.None && (flags & UIElement.Flags.NeverMeasured) == UIElement.Flags.None && (availableWidth == this._previousAvailableWidth && availableHeight == this._previousAvailableHeight))
                    return;
                this._flags &= ~UIElement.Flags.NeverMeasured;
                int desiredWidth1 = this._desiredWidth;
                int desiredHeight1 = this._desiredHeight;
                this.InvalidateArrange();
                this._flags |= UIElement.Flags.MeasureInProgress;
                int desiredWidth2;
                int desiredHeight2;
                try
                {
                    int num1 = this._marginLeft + this._marginRight;
                    int num2 = this._marginTop + this._marginBottom;
                    int num3 = availableWidth - num1;
                    int num4 = availableHeight - num2;
                    if (this._requestedSize != null)
                    {
                        int num5 = (uint)(this._requestedSize._status & 1) > 0U ? 1 : 0;
                        bool flag = (uint)(this._requestedSize._status & 2) > 0U;
                        if (num5 != 0)
                            num3 = Math.Min(this._requestedSize._first, num3);
                        if (flag)
                            num4 = Math.Min(this._requestedSize._second, num4);
                        this.MeasureOverride(num3, num4, out desiredWidth2, out desiredHeight2);
                        if (num5 != 0)
                            desiredWidth2 = this._requestedSize._first;
                        if (flag)
                            desiredHeight2 = this._requestedSize._second;
                    }
                    else
                        this.MeasureOverride(num3, num4, out desiredWidth2, out desiredHeight2);
                    this._unclippedWidth = desiredWidth2;
                    this._unclippedHeight = desiredHeight2;
                    desiredWidth2 = Math.Min(desiredWidth2, num3);
                    desiredHeight2 = Math.Min(desiredHeight2, num4);
                    desiredWidth2 += num1;
                    desiredHeight2 += num2;
                }
                finally
                {
                    this._flags &= ~UIElement.Flags.MeasureInProgress;
                    this._previousAvailableWidth = availableWidth;
                    this._previousAvailableHeight = availableHeight;
                }
                this._flags &= ~UIElement.Flags.InvalidMeasure;
                LayoutManager.CurrentLayoutManager.MeasureQueue.Remove(this);
                this._desiredWidth = desiredWidth2;
                this._desiredHeight = desiredHeight2;
                if ((this._flags & UIElement.Flags.MeasureDuringArrange) != UIElement.Flags.None || desiredWidth1 == desiredWidth2 && desiredHeight1 == desiredHeight2)
                    return;
                UIElement parent = this._parent;
                if (parent == null || (parent._flags & UIElement.Flags.MeasureInProgress) != UIElement.Flags.None)
                    return;
                parent.OnChildDesiredSizeChanged(this);
            }
        }

        public void Arrange(int finalRectX, int finalRectY, int finalRectWidth, int finalRectHeight)
        {
            this.VerifyAccess();
            if ((this._flags & (UIElement.Flags.InvalidMeasure | UIElement.Flags.NeverMeasured)) != UIElement.Flags.None)
            {
                try
                {
                    this._flags |= UIElement.Flags.MeasureDuringArrange;
                    this.Measure(finalRectWidth, finalRectHeight);
                }
                finally
                {
                    this._flags &= ~UIElement.Flags.MeasureDuringArrange;
                }
            }
            if (this.Visibility == Visibility.Collapsed || (this._flags & UIElement.Flags.IsLayoutSuspended) != UIElement.Flags.None)
            {
                LayoutManager.CurrentLayoutManager.ArrangeQueue.Remove(this);
                this._finalX = finalRectX;
                this._finalY = finalRectY;
                this._finalWidth = finalRectWidth;
                this._finalHeight = finalRectHeight;
            }
            else if ((this._flags & UIElement.Flags.InvalidArrange) == UIElement.Flags.None && (this._flags & UIElement.Flags.NeverArranged) == UIElement.Flags.None && (finalRectWidth == this._finalWidth && finalRectHeight == this._finalHeight))
            {
                if (finalRectX == this._finalX && finalRectY == this._finalY)
                    return;
                this._offsetX = this._offsetX - this._finalX + finalRectX;
                this._offsetY = this._offsetY - this._finalY + finalRectY;
                this._finalX = finalRectX;
                this._finalY = finalRectY;
                if (!this.IsRenderable())
                    return;
                UIElement.PropagateFlags(this, UIElement.Flags.IsSubtreeDirtyForRender);
            }
            else
            {
                this._flags = this._flags & ~UIElement.Flags.NeverArranged | UIElement.Flags.ArrangeInProgress;
                int num1 = this._marginLeft + this._marginRight;
                int num2 = this._marginTop + this._marginBottom;
                int num3 = this.HorizontalAlignment == HorizontalAlignment.Stretch ? finalRectWidth : this._desiredWidth;
                int num4 = this.VerticalAlignment == VerticalAlignment.Stretch ? finalRectHeight : this._desiredHeight;
                int arrangeWidth = num3 - num1;
                int arrangeHeight = num4 - num2;
                if (this._requestedSize != null)
                {
                    if ((this._requestedSize._status & 1) != 0)
                    {
                        int first = this._requestedSize._first;
                        if (first < arrangeWidth)
                            arrangeWidth = first;
                    }
                    if ((this._requestedSize._status & 2) != 0)
                    {
                        int second = this._requestedSize._second;
                        if (second < arrangeHeight)
                            arrangeHeight = second;
                    }
                }
                try
                {
                    this.ArrangeOverride(arrangeWidth, arrangeHeight);
                }
                finally
                {
                    this._flags &= ~UIElement.Flags.ArrangeInProgress;
                }
                int clientWidth = Math.Max(0, finalRectWidth - num1);
                int clientHeight = Math.Max(0, finalRectHeight - num2);
                int dx;
                int dy;
                if (clientWidth != arrangeWidth || clientHeight != arrangeHeight)
                    this.ComputeAlignmentOffset(clientWidth, clientHeight, arrangeWidth, arrangeHeight, out dx, out dy);
                else
                    dx = dy = 0;
                int num5 = dx + (finalRectX + this._marginLeft);
                dy += finalRectY + this._marginTop;
                this._offsetX = num5;
                this._offsetY = dy;
                this._renderWidth = arrangeWidth;
                this._renderHeight = arrangeHeight;
                this._finalX = finalRectX;
                this._finalY = finalRectY;
                this._finalWidth = finalRectWidth;
                this._finalHeight = finalRectHeight;
                this._flags &= ~UIElement.Flags.InvalidArrange;
                LayoutManager.CurrentLayoutManager.ArrangeQueue.Remove(this);
                if (!this.IsRenderable())
                    return;
                UIElement.PropagateFlags(this, UIElement.Flags.IsSubtreeDirtyForRender);
            }
        }

        protected virtual void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            desiredHeight = desiredWidth = 0;
        }

        protected virtual void ArrangeOverride(int arrangeWidth, int arrangeHeight)
        {
            UIElementCollection logicalChildren = this._logicalChildren;
            if (logicalChildren == null)
                return;
            int count = logicalChildren.Count;
            for (int index = 0; index < count; ++index)
                logicalChildren[index].Arrange(0, 0, arrangeWidth, arrangeHeight);
        }

        public void UpdateLayout()
        {
            this.VerifyAccess();
            LayoutManager.CurrentLayoutManager.UpdateLayout();
        }

        public bool IsMeasureValid
        {
            get
            {
                return (this._flags & UIElement.Flags.InvalidMeasure) == UIElement.Flags.None;
            }
        }

        public bool IsArrangeValid
        {
            get
            {
                return (this._flags & UIElement.Flags.InvalidArrange) == UIElement.Flags.None;
            }
        }

        public UIElement ChildElementFromPoint(int x, int y)
        {
            UIElement uiElement1 = (UIElement)null;
            x -= this._offsetX;
            y -= this._offsetY;
            if (x >= 0 && y >= 0 && (x <= this._renderWidth && y <= this._renderHeight))
            {
                uiElement1 = this;
                UIElementCollection logicalChildren = this._logicalChildren;
                if (logicalChildren != null && logicalChildren.Count > 0)
                {
                    for (int index = logicalChildren.Count - 1; index >= 0; --index)
                    {
                        UIElement uiElement2 = logicalChildren[index].ChildElementFromPoint(x, y);
                        if (uiElement2 != null)
                        {
                            uiElement1 = uiElement2;
                            break;
                        }
                    }
                }
            }
            return uiElement1;
        }

        public void GetUnclippedSize(out int width, out int height)
        {
            width = this._unclippedWidth;
            height = this._unclippedHeight;
        }

        public bool ContainsPoint(int x, int y)
        {
            if (x >= this._offsetX && x < this._offsetX + this._renderWidth && y >= this._offsetY)
                return y < this._offsetY + this._renderHeight;
            return false;
        }

        public UIElement GetPointerTarget(int x, int y)
        {
            UIElement uiElement1 = (UIElement)null;
            UIElementCollection logicalChildren = this._logicalChildren;
            while (logicalChildren != null)
            {
                int count = logicalChildren.Count;
                while (--count >= 0)
                {
                    UIElement uiElement2 = logicalChildren[count];
                    if (uiElement2 != null && uiElement2.Visibility == Visibility.Visible && uiElement2.ContainsPoint(x, y))
                    {
                        uiElement1 = uiElement2;
                        logicalChildren = uiElement2._logicalChildren;
                        x -= uiElement1._offsetX;
                        y -= uiElement1._offsetY;
                        break;
                    }
                }
                if (count < 0)
                    break;
            }
            return uiElement1;
        }

        public void PointToScreen(ref int x, ref int y)
        {
            for (UIElement uiElement = this; uiElement != null; uiElement = uiElement._parent)
            {
                x += uiElement._offsetX;
                y += uiElement._offsetY;
            }
        }

        public void PointToClient(ref int x, ref int y)
        {
            for (UIElement uiElement = this; uiElement != null; uiElement = uiElement._parent)
            {
                x -= uiElement._offsetX;
                y -= uiElement._offsetY;
            }
        }

        private bool IsRenderable()
        {
            if ((this._flags & (UIElement.Flags.NeverMeasured | UIElement.Flags.NeverArranged)) != UIElement.Flags.None || this._visibility == Visibility.Collapsed || this._visibility == Visibility.Hidden)
                return false;
            return (this._flags & (UIElement.Flags.InvalidMeasure | UIElement.Flags.InvalidArrange)) == UIElement.Flags.None;
        }

        public void InvalidateMeasure()
        {
            this.VerifyAccess();
            UIElement.Flags flags = this._flags;
            if ((flags & (UIElement.Flags.InvalidMeasure | UIElement.Flags.MeasureInProgress)) != UIElement.Flags.None)
                return;
            this._flags |= UIElement.Flags.InvalidMeasure;
            if ((flags & UIElement.Flags.NeverMeasured) != UIElement.Flags.None)
                return;
            LayoutManager.CurrentLayoutManager.MeasureQueue.Add(this);
        }

        public void InvalidateArrange()
        {
            this.VerifyAccess();
            UIElement.Flags flags = this._flags;
            if ((flags & (UIElement.Flags.InvalidArrange | UIElement.Flags.ArrangeInProgress)) != UIElement.Flags.None)
                return;
            this._flags |= UIElement.Flags.InvalidArrange;
            if ((flags & UIElement.Flags.NeverArranged) != UIElement.Flags.None)
                return;
            LayoutManager.CurrentLayoutManager.ArrangeQueue.Add(this);
        }

        public UIElement Parent
        {
            get
            {
                return this._parent;
            }
        }

        public UIElement RootUIElement
        {
            get
            {
                UIElement uiElement1 = this;
                UIElement uiElement2;
                do
                {
                    uiElement2 = uiElement1;
                    uiElement1 = uiElement2._parent;
                }
                while (uiElement1 != null);
                return uiElement2;
            }
        }

        internal bool GetIsRootElement()
        {
            return (this._flags & UIElement.Flags.ShouldPostRender) > UIElement.Flags.None;
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this._horizontalAlignment;
            }
            set
            {
                this.VerifyAccess();
                this._horizontalAlignment = value;
                this.InvalidateArrange();
            }
        }

        public VerticalAlignment VerticalAlignment
        {
            get
            {
                return this._verticalAlignment;
            }
            set
            {
                this.VerifyAccess();
                this._verticalAlignment = value;
                this.InvalidateArrange();
            }
        }

        protected virtual void OnChildDesiredSizeChanged(UIElement child)
        {
            if (!this.IsMeasureValid)
                return;
            this.InvalidateMeasure();
        }

        public virtual void OnRender(DrawingContext dc)
        {
        }

        public Visibility Visibility
        {
            get
            {
                return this._visibility;
            }
            set
            {
                this.VerifyAccess();
                if (this._visibility == value)
                    return;
                bool wasVisible = (this._flags & UIElement.Flags.IsVisibleCache) > UIElement.Flags.None;
                int num = this._visibility == Visibility.Collapsed ? 1 : (value == Visibility.Collapsed ? 1 : 0);
                this._visibility = value;
                bool flag = false;
                if ((this._parent == null ? 0 : ((this._parent._flags & UIElement.Flags.IsVisibleCache) > UIElement.Flags.None ? 1 : 0)) != 0 && value == Visibility.Visible)
                {
                    this._flags |= UIElement.Flags.IsVisibleCache;
                    flag = true;
                }
                else
                    this._flags &= ~UIElement.Flags.IsVisibleCache;
                if (num != 0 && this._parent != null)
                    this._parent.InvalidateMeasure();
                if (wasVisible == flag)
                    return;
                this.OnIsVisibleChanged(wasVisible);
            }
        }

        private void OnIsVisibleChanged(bool wasVisible)
        {
            PropertyChangedEventHandler isVisibleChanged = this._isVisibleChanged;
            if (isVisibleChanged != null)
                isVisibleChanged((object)this, new PropertyChangedEventArgs("IsVisible", (object)wasVisible, (object)!wasVisible));
            UIElementCollection logicalChildren = this._logicalChildren;
            if (logicalChildren == null)
                return;
            int count = logicalChildren.Count;
            for (int index = 0; index < count; ++index)
            {
                UIElement uiElement = logicalChildren[index];
                if (uiElement._visibility == Visibility.Visible)
                {
                    if (!wasVisible)
                        uiElement._flags |= UIElement.Flags.IsVisibleCache;
                    else
                        uiElement._flags &= ~UIElement.Flags.IsVisibleCache;
                    uiElement.OnIsVisibleChanged(wasVisible);
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                return (this._flags & UIElement.Flags.IsVisibleCache) > UIElement.Flags.None;
            }
        }

        public event PropertyChangedEventHandler IsVisibleChanged
        {
            add
            {
                this.VerifyAccess();
                this._isVisibleChanged += value;
            }
            remove
            {
                this.VerifyAccess();
                this._isVisibleChanged -= value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                UIElement parent = this._parent;
                bool flag = parent == null || parent.IsEnabled;
                if (flag)
                    flag = (this._flags & UIElement.Flags.Enabled) > UIElement.Flags.None;
                return flag;
            }
            set
            {
                this.VerifyAccess();
                bool isEnabled = this.IsEnabled;
                if (value)
                    this._flags |= UIElement.Flags.Enabled;
                else
                    this._flags &= ~UIElement.Flags.Enabled;
                if (this._isEnabledChanged == null || isEnabled == this.IsEnabled)
                    return;
                this._isEnabledChanged((object)this, new PropertyChangedEventArgs(nameof(IsEnabled), (object)isEnabled, (object)!isEnabled));
            }
        }

        public event PropertyChangedEventHandler IsEnabledChanged
        {
            add
            {
                this.VerifyAccess();
                this._isEnabledChanged += value;
            }
            remove
            {
                this.VerifyAccess();
                this._isEnabledChanged -= value;
            }
        }

        protected internal virtual void RenderRecursive(DrawingContext dc)
        {
            dc.Translate(this._offsetX, this._offsetY);
            dc.PushClippingRectangle(0, 0, this._renderWidth, this._renderHeight);
            try
            {
                if (dc.EmptyClipRect)
                    return;
                this.OnRender(dc);
                UIElementCollection logicalChildren = this._logicalChildren;
                if (logicalChildren == null)
                    return;
                int count = logicalChildren.Count;
                for (int index = 0; index < count; ++index)
                {
                    UIElement uiElement = logicalChildren[index];
                    if (uiElement.IsRenderable())
                        uiElement.RenderRecursive(dc);
                }
            }
            finally
            {
                dc.PopClippingRectangle();
                dc.Translate(-this._offsetX, -this._offsetY);
                this._flags &= ~(UIElement.Flags.IsSubtreeDirtyForRender | UIElement.Flags.IsDirtyForRender);
            }
        }

        internal static void PropagateFlags(UIElement e, UIElement.Flags flags)
        {
            for (; e != null && (e._flags & flags) == UIElement.Flags.None; e = e._parent)
            {
                e._flags |= flags;
                if ((e._flags & UIElement.Flags.ShouldPostRender) != UIElement.Flags.None)
                    MediaContext.From(e.Dispatcher).PostRender();
            }
        }

        private void MarkDirtyRect(int x, int y, int w, int h)
        {
            this.PointToScreen(ref x, ref y);
            MediaContext.From(this.Dispatcher).AddDirtyArea(x, y, w, h);
            UIElement.PropagateFlags(this, UIElement.Flags.IsSubtreeDirtyForRender);
        }

        public void InvalidateRect(int x, int y, int w, int h)
        {
            this.VerifyAccess();
            this.MarkDirtyRect(x, y, w, h);
        }

        public void Invalidate()
        {
            this.VerifyAccess();
            this.MarkDirtyRect(0, 0, this._renderWidth, this._renderHeight);
        }

        public void RaiseEvent(RoutedEventArgs args)
        {
            this.VerifyAccess();
            if (args == null)
                throw new ArgumentNullException();
            EventRoute route = new EventRoute(args._routedEvent);
            args.Source = (object)this;
            if (args._routedEvent._routingStrategy == RoutingStrategy.Direct)
            {
                this.AddToEventRouteImpl(route, args);
            }
            else
            {
                int num = 0;
                UIElement uiElement = this;
                while (num++ <= 256)
                {
                    uiElement.AddToEventRouteImpl(route, args);
                    uiElement = uiElement._parent;
                    if (uiElement == null)
                        goto label_8;
                }
                throw new InvalidOperationException();
            }
            label_8:
            route.InvokeHandlers((object)this, args);
            args.Source = args.OriginalSource;
        }

        public void AddToEventRoute(EventRoute route, RoutedEventArgs args)
        {
            this.VerifyAccess();
            if (route == null || args == null)
                throw new ArgumentNullException();
            this.AddToEventRouteImpl(route, args);
        }

        private void AddToEventRouteImpl(EventRoute route, RoutedEventArgs args)
        {
            Hashtable eventHandlersStore = UIElement._classEventHandlersStore;
            RoutedEvent routedEvent = args._routedEvent;
            for (int index1 = 0; index1 < 2; ++index1)
            {
                if (eventHandlersStore != null)
                {
                    ArrayList arrayList = (ArrayList)eventHandlersStore[(object)routedEvent];
                    if (arrayList != null)
                    {
                        int index2 = 0;
                        for (int count = arrayList.Count; index2 < count; ++index2)
                        {
                            RoutedEventHandlerInfo eventHandlerInfo = (RoutedEventHandlerInfo)arrayList[index2];
                            route.Add((object)this, eventHandlerInfo._handler, eventHandlerInfo._handledEventsToo);
                        }
                    }
                }
                eventHandlersStore = this._instanceEventHandlersStore;
            }
        }

        protected Hashtable InstanceEventHandlersStore
        {
            get
            {
                if (this._instanceEventHandlersStore == null)
                    this._instanceEventHandlersStore = new Hashtable();
                return this._instanceEventHandlersStore;
            }
        }

        public void AddHandler(RoutedEvent routedEvent, RoutedEventHandler handler, bool handledEventsToo)
        {
            this.VerifyAccess();
            if (routedEvent == null || handler == null)
                throw new ArgumentNullException();
            UIElement.AddRoutedEventHandler(this.InstanceEventHandlersStore, routedEvent, handler, handledEventsToo);
        }

        [System.Flags]
        internal enum Flags : uint
        {
            None = 0,
            IsSubtreeDirtyForRender = 2,
            IsDirtyForRender = 4,
            Enabled = 32, // 0x00000020
            InvalidMeasure = 64, // 0x00000040
            InvalidArrange = 128, // 0x00000080
            MeasureInProgress = 256, // 0x00000100
            ArrangeInProgress = 512, // 0x00000200
            MeasureDuringArrange = 1024, // 0x00000400
            NeverMeasured = 2048, // 0x00000800
            NeverArranged = 4096, // 0x00001000
            ShouldPostRender = 8192, // 0x00002000
            IsLayoutSuspended = 16384, // 0x00004000
            IsVisibleCache = 32768, // 0x00008000
        }

        internal class Pair
        {
            public const int Flags_First = 1;
            public const int Flags_Second = 2;
            public int _first;
            public int _second;
            public int _status;
        }
    }
}
