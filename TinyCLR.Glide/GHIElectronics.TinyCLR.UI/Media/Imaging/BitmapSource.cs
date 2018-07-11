namespace GHIElectronics.TinyCLR.UI.Media.Imaging
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Drawing;

    public abstract class BitmapSource : ImageSource
    {
        protected BitmapSource(Graphics g) : base(g)
        {
        }
    }
}

