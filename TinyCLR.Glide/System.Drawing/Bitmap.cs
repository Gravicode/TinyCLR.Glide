namespace System.Drawing
{
    using System;
    using System.Drawing.Internal;
    using System.IO;

    public class Bitmap : Image
    {
        public System.Drawing.Internal.Bitmap GetInternalBitmap()
        {
            return base.data.GetInternalBitmap();
        }
        private Bitmap(System.Drawing.Internal.Bitmap bmp)
        {
            base.data = new Graphics(bmp, IntPtr.Zero);
        }

        public Bitmap(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            byte[] buffer = new byte[(int) stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            base.data = new Graphics(buffer);
        }

        public Bitmap(int width, int height)
        {
            base.data = new Graphics(width, height);
        }

        public Color GetPixel(int x, int y)
        {
            return Color.FromArgb((int) base.data.surface.GetPixel(x, y));
        }

        public void SetPixel(int x, int y, Color color)
        {
            base.data.surface.SetPixel(x, y, (uint) color.ToRgb());
        }
    }
}

