namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Input;
    using System;
    using System.Runtime.InteropServices;

    public class ScrollViewer : ContentControl
    {
        private int _previousHorizontalOffset;
        private int _previousVerticalOffset;
        private int _horizontalOffset;
        private int _verticalOffset;
        private int _extentWidth;
        private int _extentHeight;
        private int _scrollableWidth;
        private int _scrollableHeight;
        private int _lineHeight = 1;
        private int _lineWidth = 1;
        private GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle _scrollingStyle;
        private ScrollChangedEventHandler _scrollChanged;

        public event ScrollChangedEventHandler ScrollChanged
        {
            add
            {
                base.VerifyAccess();
                this._scrollChanged = (ScrollChangedEventHandler) Delegate.Combine(this._scrollChanged, value);
            }
            remove
            {
                base.VerifyAccess();
                this._scrollChanged = (ScrollChangedEventHandler) Delegate.Remove(this._scrollChanged, value);
            }
        }

        public ScrollViewer()
        {
            base.HorizontalAlignment = HorizontalAlignment.Left;
            base.VerticalAlignment = VerticalAlignment.Stretch;
        }

        protected override void ArrangeOverride(int arrangeWidth, int arrangeHeight)
        {
            UIElement child = base.Child;
            if (child != null)
            {
                this._scrollableWidth = Math.Max(0, this.ExtentWidth - arrangeWidth);
                this._scrollableHeight = Math.Max(0, this.ExtentHeight - arrangeHeight);
                this._horizontalOffset = Math.Min(this._horizontalOffset, this._scrollableWidth);
                this._verticalOffset = Math.Min(this._verticalOffset, this._scrollableHeight);
                child.Arrange(-this._horizontalOffset, -this._verticalOffset, Math.Max(arrangeWidth, this.ExtentWidth), Math.Max(arrangeHeight, this.ExtentHeight));
            }
            else
            {
                this._horizontalOffset = this._verticalOffset = 0;
            }
            this.InvalidateScrollInfo();
        }

        private void InvalidateScrollInfo()
        {
            if (this._scrollChanged != null)
            {
                int offsetChangeX = this._horizontalOffset - this._previousHorizontalOffset;
                int offsetChangeY = this._verticalOffset - this._previousVerticalOffset;
                this._scrollChanged(this, new ScrollChangedEventArgs(this._horizontalOffset, this._verticalOffset, offsetChangeX, offsetChangeY));
            }
            this._previousHorizontalOffset = this._horizontalOffset;
            this._previousVerticalOffset = this._verticalOffset;
        }

        public void LineDown()
        {
            this.VerticalOffset += this._lineHeight;
        }

        public void LineLeft()
        {
            this.HorizontalOffset -= this._lineWidth;
        }

        public void LineRight()
        {
            this.HorizontalOffset += this._lineWidth;
        }

        public void LineUp()
        {
            this.VerticalOffset -= this._lineHeight;
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            UIElement child = base.Child;
            if ((child != null) && (child.Visibility != Visibility.Collapsed))
            {
                child.Measure((base.HorizontalAlignment == HorizontalAlignment.Stretch) ? 0x7ffff : availableWidth, (base.VerticalAlignment == VerticalAlignment.Stretch) ? 0x7ffff : availableHeight);
                child.GetDesiredSize(out desiredWidth, out desiredHeight);
                this._extentHeight = child._unclippedHeight;
                this._extentWidth = child._unclippedWidth;
            }
            else
            {
                desiredWidth = desiredHeight = 0;
                this._extentHeight = this._extentWidth = 0;
            }
        }

        protected override void OnButtonDown(ButtonEventArgs e)
        {
            switch (e.Button)
            {
                case HardwareButton.Down:
                    if (this._scrollingStyle != GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First)
                    {
                        this.PageDown();
                        break;
                    }
                    this.LineDown();
                    break;

                case HardwareButton.Up:
                    if (this._scrollingStyle != GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First)
                    {
                        this.PageUp();
                        break;
                    }
                    this.LineUp();
                    break;

                case HardwareButton.Left:
                    if (this._scrollingStyle != GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First)
                    {
                        this.PageLeft();
                        break;
                    }
                    this.LineLeft();
                    break;

                case HardwareButton.Right:
                    if (this._scrollingStyle != GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First)
                    {
                        this.PageRight();
                        break;
                    }
                    this.LineRight();
                    break;

                default:
                    return;
            }
            if ((this._previousHorizontalOffset != this._horizontalOffset) || (this._previousVerticalOffset != this._verticalOffset))
            {
                e.Handled = true;
            }
        }

        public void PageDown()
        {
            this.VerticalOffset += base.ActualHeight;
        }

        public void PageLeft()
        {
            this.HorizontalOffset -= base.ActualWidth;
        }

        public void PageRight()
        {
            this.HorizontalOffset += base.ActualWidth;
        }

        public void PageUp()
        {
            this.VerticalOffset -= base.ActualHeight;
        }

        public int HorizontalOffset
        {
            get
            {
                return this._horizontalOffset;
            }
            set
            {
                base.VerifyAccess();
                if (value < 0)
                {
                    value = 0;
                }
                else if (((base._flags & UIElement.Flags.NeverArranged) == UIElement.Flags.None) && (value > this._scrollableWidth))
                {
                    value = this._scrollableWidth;
                }
                if (this._horizontalOffset != value)
                {
                    this._horizontalOffset = value;
                    base.InvalidateArrange();
                }
            }
        }

        public int VerticalOffset
        {
            get
            {
                return this._verticalOffset;
            }
            set
            {
                base.VerifyAccess();
                if (value < 0)
                {
                    value = 0;
                }
                else if (((base._flags & UIElement.Flags.NeverArranged) == UIElement.Flags.None) && (value > this._scrollableHeight))
                {
                    value = this._scrollableHeight;
                }
                if (this._verticalOffset != value)
                {
                    this._verticalOffset = value;
                    base.InvalidateArrange();
                }
            }
        }

        public int ExtentHeight
        {
            get
            {
                return this._extentHeight;
            }
        }

        public int ExtentWidth
        {
            get
            {
                return this._extentWidth;
            }
        }

        public int LineWidth
        {
            get
            {
                return this._lineWidth;
            }
            set
            {
                base.VerifyAccess();
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("LineWidth");
                }
                this._lineWidth = value;
            }
        }

        public int LineHeight
        {
            get
            {
                return this._lineHeight;
            }
            set
            {
                base.VerifyAccess();
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("LineHeight");
                }
                this._lineHeight = value;
            }
        }

        public GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle ScrollingStyle
        {
            get
            {
                return this._scrollingStyle;
            }
            set
            {
                base.VerifyAccess();
                if ((value < GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.First) || (value > GHIElectronics.TinyCLR.UI.Controls.ScrollingStyle.PageByPage))
                {
                    throw new ArgumentOutOfRangeException("ScrollingStyle", "Invalid Enum");
                }
                this._scrollingStyle = value;
            }
        }
    }
}

