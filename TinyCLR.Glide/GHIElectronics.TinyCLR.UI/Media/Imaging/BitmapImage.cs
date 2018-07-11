namespace GHIElectronics.TinyCLR.UI.Media.Imaging
{
    using System;
    using System.Drawing;

    public class BitmapImage : BitmapSource
    {
        private BitmapImage(Graphics g) : base(g)
        {
        }

        public static BitmapImage FromGraphics(Graphics g)
        {
            return new BitmapImage(g);
        }
    }
}

