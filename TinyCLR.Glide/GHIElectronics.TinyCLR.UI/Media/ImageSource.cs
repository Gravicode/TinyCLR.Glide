namespace GHIElectronics.TinyCLR.UI.Media
{
    using System;
    using System.Drawing;

    public abstract class ImageSource
    {
        internal readonly Graphics graphics;

        protected ImageSource(Graphics g)
        {
            this.graphics = g;
        }

        public virtual int Width
        {
            get
            {
                return this.graphics.Width;
            }
        }

        public virtual int Height
        {
            get
            {
                return this.graphics.Height;
            }
        }
    }
}

