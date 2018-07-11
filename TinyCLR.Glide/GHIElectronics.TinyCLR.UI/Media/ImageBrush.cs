namespace GHIElectronics.TinyCLR.UI.Media
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class ImageBrush : Brush
    {
        public GHIElectronics.TinyCLR.UI.Media.ImageSource ImageSource;
        public GHIElectronics.TinyCLR.UI.Media.Stretch Stretch = GHIElectronics.TinyCLR.UI.Media.Stretch.Fill;

        public ImageBrush(GHIElectronics.TinyCLR.UI.Media.ImageSource imagesource)
        {
            this.ImageSource = imagesource;
        }

        internal override void RenderRectangle(Bitmap bmp, Pen pen, int x, int y, int width, int height)
        {
            if (this.Stretch == GHIElectronics.TinyCLR.UI.Media.Stretch.None)
            {
                bmp.DrawImage(x, y, this.ImageSource, 0, 0, this.ImageSource.Width, this.ImageSource.Height, base.Opacity);
            }
            else if ((width == this.ImageSource.Width) && (height == this.ImageSource.Height))
            {
                bmp.DrawImage(x, y, this.ImageSource, 0, 0, width, height, base.Opacity);
            }
            else
            {
                bmp.StretchImage(x, y, this.ImageSource, width, height, base.Opacity);
            }
            if ((pen != null) && (pen.Thickness > 0))
            {
                bmp.DrawRectangle(pen.Color, pen.Thickness, x, y, width, height, 0, 0, Colors.Transparent, 0, 0, Colors.Transparent, 0, 0, 0);
            }
        }
    }
}

