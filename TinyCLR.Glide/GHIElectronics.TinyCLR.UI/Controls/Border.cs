namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Runtime.InteropServices;

    public class Border : ContentControl
    {
        private Brush _borderBrush = new SolidColorBrush(Colors.Black);
        private int _borderLeft;
        private int _borderTop;
        private int _borderRight;
        private int _borderBottom;

        public Border()
        {
            this._borderLeft = this._borderTop = this._borderRight = this._borderBottom = 1;
        }

        protected override void ArrangeOverride(int arrangeWidth, int arrangeHeight)
        {
            UIElement child = base.Child;
            if (child != null)
            {
                child.Arrange(this._borderLeft, this._borderTop, (arrangeWidth - this._borderLeft) - this._borderRight, (arrangeHeight - this._borderTop) - this._borderBottom);
            }
        }

        public void GetBorderThickness(out int left, out int top, out int right, out int bottom)
        {
            left = this._borderLeft;
            top = this._borderTop;
            right = this._borderRight;
            bottom = this._borderBottom;
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            UIElement child = base.Child;
            if (child != null)
            {
                int num = this._borderLeft + this._borderRight;
                int num2 = this._borderTop + this._borderBottom;
                child.Measure(availableWidth - num, availableHeight - num2);
                child.GetDesiredSize(out desiredWidth, out desiredHeight);
                desiredWidth += num;
                desiredHeight += num2;
            }
            else
            {
                desiredWidth = desiredHeight = 0;
            }
        }

        public override void OnRender(DrawingContext dc)
        {
            int width = base._renderWidth;
            int height = base._renderHeight;
            dc.DrawRectangle(this._borderBrush, null, 0, 0, width, height);
            if (base._background != null)
            {
                dc.DrawRectangle(base._background, null, this._borderLeft, this._borderTop, (width - this._borderLeft) - this._borderRight, (height - this._borderTop) - this._borderBottom);
            }
        }

        public void SetBorderThickness(int length)
        {
            this.SetBorderThickness(length, length, length, length);
        }

        public void SetBorderThickness(int left, int top, int right, int bottom)
        {
            base.VerifyAccess();
            if (((left < 0) || (right < 0)) || ((top < 0) || (bottom < 0)))
            {
                string[] textArray1 = new string[] { "'", left.ToString(), ",", top.ToString(), ",", right.ToString(), ",", bottom.ToString(), "' is not a valid value 'BorderThickness'" };
                throw new ArgumentException(string.Concat(textArray1));
            }
            this._borderLeft = left;
            this._borderTop = top;
            this._borderRight = right;
            this._borderBottom = bottom;
            base.InvalidateMeasure();
        }

        public Brush BorderBrush
        {
            get
            {
                base.VerifyAccess();
                return this._borderBrush;
            }
            set
            {
                base.VerifyAccess();
                this._borderBrush = value;
                base.Invalidate();
            }
        }
    }
}

