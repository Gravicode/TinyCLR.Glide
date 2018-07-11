// Decompiled with JetBrains decompiler
// Type: System.Drawing.Graphics
// Assembly: GHIElectronics.TinyCLR.Drawing, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FB94FDB-AA94-43DE-9FD5-9C0BE64863BE
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.Drawing.dll

namespace System.Drawing
{
    public sealed class Graphics : MarshalByRefObject, IDisposable
    {
        internal System.Drawing.Internal.Bitmap surface;
        private bool disposed;
        internal bool callFromImage;
        private IntPtr hdc;

        internal int Width
        {
            get
            {
                return this.surface.Width;
            }
        }

        internal int Height
        {
            get
            {
                return this.surface.Height;
            }
        }

        public GraphicsUnit PageUnit { get; } = GraphicsUnit.Pixel;

        internal Graphics(byte[] buffer)
          : this(new System.Drawing.Internal.Bitmap(buffer, System.Drawing.Internal.Bitmap.BitmapImageType.Bmp), IntPtr.Zero)
        {
        }

        internal Graphics(int width, int height)
          : this(width, height, IntPtr.Zero)
        {
        }

        private Graphics(int width, int height, IntPtr hdc)
          : this(new System.Drawing.Internal.Bitmap(width, height), hdc)
        {
        }

        internal Graphics(System.Drawing.Internal.Bitmap bmp, IntPtr hdc)
        {
            this.surface = bmp;
            this.hdc = hdc;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed || this.callFromImage)
                return;
            this.surface?.Dispose();
            this.surface = (System.Drawing.Internal.Bitmap)null;
            this.disposed = true;
        }

        private uint ToFlags(StringFormat format, float height, bool ignoreHeight, bool truncateAtBottom)
        {
            uint num1 = 0;
            if (ignoreHeight || (double)height == 0.0)
                num1 |= 16U;
            if (truncateAtBottom)
                num1 |= 4U;
            if (format.FormatFlags != (StringFormatFlags)0)
                throw new NotSupportedException();
            uint num2;
            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    num2 = num1 | 0U;
                    break;
                case StringAlignment.Center:
                    num2 = num1 | 2U;
                    break;
                case StringAlignment.Far:
                    num2 = num1 | 32U;
                    break;
                default:
                    throw new ArgumentException();
            }
            switch (format.Trimming)
            {
                case StringTrimming.None:
                    return num2;
                case StringTrimming.Character:
                case StringTrimming.Word:
                case StringTrimming.EllipsisPath:
                    throw new NotSupportedException();
                case StringTrimming.EllipsisCharacter:
                    num2 |= 64U;
                    goto case StringTrimming.None;
                case StringTrimming.EllipsisWord:
                    num2 |= 9U;
                    goto case StringTrimming.None;
                default:
                    throw new ArgumentException();
            }
        }

        ~Graphics()
        {
            this.Dispose(false);
        }

        public SizeF MeasureString(string text, Font font)
        {
            int width;
            int height;
            font.ComputeExtent(text, out width, out height);
            return new SizeF((float)width, (float)height);
        }

        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            int renderWidth;
            int renderHeight;
            font.ComputeTextInRect(text, out renderWidth, out renderHeight, 0, 0, (int)layoutArea.Width, (int)layoutArea.Height, this.ToFlags(stringFormat, layoutArea.Height, false, false));
            return new SizeF((float)renderWidth, (float)renderHeight);
        }

        public void Clear(Color color)
        {
            if (color != Color.Black)
                throw new NotSupportedException();
            this.surface.Clear();
        }

        public static Graphics FromHdc(IntPtr hdc)
        {
            if (hdc == IntPtr.Zero)
                throw new ArgumentNullException(nameof(hdc));
            int width;
            int height;
            if (!System.Drawing.Internal.Bitmap.GetSizeForLcdFromHdc(hdc, out width, out height) || width == 0 || height == 0)
                throw new InvalidOperationException("No screen configured.");
            return new Graphics(width, height, hdc);
        }

        public static Graphics FromImage(Image image)
        {
            image.data.callFromImage = true;
            return image.data;
        }

        public void Flush()
        {
            if (this.hdc == IntPtr.Zero)
                throw new InvalidOperationException("Graphics not for screen.");
            this.surface.Flush(this.hdc);
        }

        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            this.surface.StretchImage(x, y, srcRect.Width, srcRect.Height, image.data.surface, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (ushort)byte.MaxValue);
        }

        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            this.surface.StretchImage(x, y, width, height, image.data.surface, 0, 0, image.Width, image.Height, (ushort)byte.MaxValue);
        }

        public void DrawImage(Image image, int x, int y)
        {
            this.surface.StretchImage(x, y, image.Width, image.Height, image.data.surface, 0, 0, image.Width, image.Height, (ushort)byte.MaxValue);
        }

        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            this.surface.StretchImage(destRect.X, destRect.Y, destRect.Width, destRect.Height, image.data.surface, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, (ushort)byte.MaxValue);
        }

        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            if (pen.Color.A != byte.MaxValue)
                throw new NotSupportedException("Alpha not supported.");
            this.surface.DrawLine((uint)((ulong)pen.Color.value & 16777215UL), (int)pen.Width / 2, x1, y1, x2, y2);
        }

        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            SolidBrush solidBrush;
            if ((solidBrush = brush as SolidBrush) == null)
                throw new NotSupportedException();
            if (solidBrush.Color.A != byte.MaxValue)
                throw new NotSupportedException("Alpha not supported.");
            this.surface.DrawText(s, font, (uint)((ulong)solidBrush.Color.value & 16777215UL), (int)x, (int)y);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            this.DrawString(s, font, brush, layoutRectangle, new StringFormat()
            {
                Trimming = StringTrimming.EllipsisWord,
                Alignment = StringAlignment.Near
            });
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            SolidBrush solidBrush;
            if ((solidBrush = brush as SolidBrush) == null)
                throw new NotSupportedException();
            if (solidBrush.Color.A != byte.MaxValue)
                throw new NotSupportedException("Alpha not supported.");
            this.surface.DrawTextInRect(s, (int)layoutRectangle.X, (int)layoutRectangle.Y, (int)layoutRectangle.Width, (int)layoutRectangle.Height, this.ToFlags(format, layoutRectangle.Height, false, false), solidBrush.Color, font);
        }

        public void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            if (pen.Color.A != byte.MaxValue)
                throw new NotSupportedException("Alpha not supported.");
            uint colorOutline = (uint)(pen.Color.ToArgb() & 16777215);
            width = (width - 1) / 2;
            height = (height - 1) / 2;
            x += width;
            y += height;
            this.surface.DrawEllipse(colorOutline, (int)pen.Width, x, y, width, height, (uint)Color.Transparent.value, x, y, (uint)Color.Transparent.value, x + width * 2, y + height * 2, (ushort)0);
        }

        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            if (pen.Color.A != byte.MaxValue)
                throw new NotSupportedException("Alpha not supported.");
            this.surface.DrawRectangle((uint)(pen.Color.ToArgb() & 16777215), (int)pen.Width, x, y, width, height, 0, 0, (uint)Color.Transparent.value, x, y, (uint)Color.Transparent.value, x + width, y + height, (ushort)0);
        }

        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            SolidBrush solidBrush;
            if ((solidBrush = brush as SolidBrush) == null)
                throw new NotSupportedException();
            uint num = (uint)(solidBrush.Color.ToArgb() & 16777215);
            width = (width - 1) / 2;
            height = (height - 1) / 2;
            x += width;
            y += height;
            this.surface.DrawEllipse(num, 0, x, y, width, height, num, x, y, num, x + width * 2, y + height * 2, (ushort)solidBrush.Color.A);
        }

        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            SolidBrush solidBrush;
            if ((solidBrush = brush as SolidBrush) == null)
                throw new NotSupportedException();
            uint num = (uint)(solidBrush.Color.ToArgb() & 16777215);
            this.surface.DrawRectangle(num, 0, x, y, width, height, 0, 0, num, x, y, num, x + width, y + height, (ushort)solidBrush.Color.A);
        }

        public System.Drawing.Internal.Bitmap GetInternalBitmap()
        {
            return surface;
        }
    }
}
