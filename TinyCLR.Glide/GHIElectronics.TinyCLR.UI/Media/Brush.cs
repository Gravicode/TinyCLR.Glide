namespace GHIElectronics.TinyCLR.UI.Media
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public abstract class Brush
    {
        private ushort _opacity = 0xff;

        protected Brush()
        {
        }

        internal virtual void RenderEllipse(Bitmap bmp, Pen outline, int x, int y, int xRadius, int yRadius)
        {
            throw new NotSupportedException("RenderEllipse is not supported with this brush.");
        }

        internal virtual void RenderPolygon(Bitmap bmp, Pen outline, int[] pts)
        {
            throw new NotSupportedException("RenderPolygon is not supported with this brush.");
        }

        internal abstract void RenderRectangle(Bitmap bmp, Pen outline, int x, int y, int width, int height);

        public ushort Opacity
        {
            get
            {
                return this._opacity;
            }
            set
            {
                if (value > 0xff)
                {
                    value = 0xff;
                }
                this._opacity = value;
            }
        }
    }
}

