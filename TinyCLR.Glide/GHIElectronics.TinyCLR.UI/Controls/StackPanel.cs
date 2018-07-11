namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using System;
    using System.Runtime.InteropServices;

    public class StackPanel : Panel
    {
        private GHIElectronics.TinyCLR.UI.Controls.Orientation _orientation;

        public StackPanel() : this(GHIElectronics.TinyCLR.UI.Controls.Orientation.Vertical)
        {
        }

        public StackPanel(GHIElectronics.TinyCLR.UI.Controls.Orientation orientation)
        {
            this.Orientation = orientation;
        }

        protected override void ArrangeOverride(int arrangeWidth, int arrangeHeight)
        {
            bool flag = this.Orientation == GHIElectronics.TinyCLR.UI.Controls.Orientation.Horizontal;
            int finalRectWidth = 0;
            int finalRectX = 0;
            int count = base.Children.Count;
            for (int i = 0; i < count; i++)
            {
                UIElement element = base.Children[i];
                if (element.Visibility != Visibility.Collapsed)
                {
                    int num5;
                    int num6;
                    finalRectX += finalRectWidth;
                    element.GetDesiredSize(out num5, out num6);
                    if (flag)
                    {
                        finalRectWidth = num5;
                        element.Arrange(finalRectX, 0, finalRectWidth, Math.Max(arrangeHeight, num6));
                    }
                    else
                    {
                        finalRectWidth = num6;
                        element.Arrange(0, finalRectX, Math.Max(arrangeWidth, num5), finalRectWidth);
                    }
                }
            }
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            desiredWidth = 0;
            desiredHeight = 0;
            bool flag = this.Orientation == GHIElectronics.TinyCLR.UI.Controls.Orientation.Horizontal;
            int count = base.Children.Count;
            for (int i = 0; i < count; i++)
            {
                UIElement element = base.Children[i];
                if (element.Visibility != Visibility.Collapsed)
                {
                    int num3;
                    int num4;
                    if (flag)
                    {
                        element.Measure(0x7ffff, availableHeight);
                    }
                    else
                    {
                        element.Measure(availableWidth, 0x7ffff);
                    }
                    element.GetDesiredSize(out num3, out num4);
                    if (flag)
                    {
                        desiredWidth += num3;
                        desiredHeight = Math.Max(desiredHeight, num4);
                    }
                    else
                    {
                        desiredWidth = Math.Max(desiredWidth, num3);
                        desiredHeight += num4;
                    }
                }
            }
        }

        public GHIElectronics.TinyCLR.UI.Controls.Orientation Orientation
        {
            get
            {
                return this._orientation;
            }
            set
            {
                base.VerifyAccess();
                this._orientation = value;
                base.InvalidateMeasure();
            }
        }
    }
}

