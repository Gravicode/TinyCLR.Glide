namespace GHIElectronics.TinyCLR.UI.Media
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Threading;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class DrawingContext : DispatcherObject, IDisposable
    {
        internal bool EmptyClipRect;
        private GHIElectronics.TinyCLR.UI.Bitmap _bitmap;
        internal int _x;
        internal int _y;
        private Stack _clippingRectangles = new Stack();

        internal DrawingContext(GHIElectronics.TinyCLR.UI.Bitmap bmp)
        {
            this._bitmap = bmp;
        }

        public void BlendImage(ImageSource source, int destinationX, int destinationY, int sourceX, int sourceY, int sourceWidth, int sourceHeight, ushort opacity)
        {
            base.VerifyAccess();
            this._bitmap.DrawImage(this._x + destinationX, this._y + destinationY, source, sourceX, sourceY, sourceWidth, sourceHeight, opacity);
        }

        public void Clear()
        {
            base.VerifyAccess();
            this._bitmap.Clear();
        }

        internal void Close()
        {
            this._bitmap = null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._bitmap = null;
        }

        public void DrawEllipse(GHIElectronics.TinyCLR.UI.Media.Brush brush, GHIElectronics.TinyCLR.UI.Media.Pen pen, int x, int y, int xRadius, int yRadius)
        {
            base.VerifyAccess();
            if (brush != null)
            {
                brush.RenderEllipse(this._bitmap, pen, this._x + x, this._y + y, xRadius, yRadius);
            }
            else if ((pen != null) && (pen.Thickness > 0))
            {
                this._bitmap.DrawEllipse(pen.Color, pen.Thickness, this._x + x, this._y + y, xRadius, yRadius, Colors.Transparent, 0, 0, Colors.Transparent, 0, 0, 0);
            }
        }

        public void DrawImage(ImageSource source, int x, int y)
        {
            base.VerifyAccess();
            this._bitmap.DrawImage(this._x + x, this._y + y, source, 0, 0, source.Width, source.Height);
        }

        public void DrawImage(ImageSource source, int destinationX, int destinationY, int sourceX, int sourceY, int sourceWidth, int sourceHeight)
        {
            base.VerifyAccess();
            this._bitmap.DrawImage(this._x + destinationX, this._y + destinationY, source, sourceX, sourceY, sourceWidth, sourceHeight);
        }

        public void DrawLine(GHIElectronics.TinyCLR.UI.Media.Pen pen, int x0, int y0, int x1, int y1)
        {
            base.VerifyAccess();
            if (pen != null)
            {
                this._bitmap.DrawLine(pen.Color, pen.Thickness, this._x + x0, this._y + y0, this._x + x1, this._y + y1);
            }
        }

        public void DrawPolygon(GHIElectronics.TinyCLR.UI.Media.Brush brush, GHIElectronics.TinyCLR.UI.Media.Pen pen, int[] pts)
        {
            base.VerifyAccess();
            brush.RenderPolygon(this._bitmap, pen, pts);
            int num = pts.Length / 2;
            for (int i = 0; i < (num - 1); i++)
            {
                this.DrawLine(pen, pts[i * 2], pts[(i * 2) + 1], pts[(i * 2) + 2], pts[(i * 2) + 3]);
            }
            if (num > 2)
            {
                this.DrawLine(pen, pts[(num * 2) - 2], pts[(num * 2) - 1], pts[0], pts[1]);
            }
        }

        public void DrawRectangle(GHIElectronics.TinyCLR.UI.Media.Brush brush, GHIElectronics.TinyCLR.UI.Media.Pen pen, int x, int y, int width, int height)
        {
            base.VerifyAccess();
            if (brush != null)
            {
                brush.RenderRectangle(this._bitmap, pen, this._x + x, this._y + y, width, height);
            }
            else if ((pen != null) && (pen.Thickness > 0))
            {
                this._bitmap.DrawRectangle(pen.Color, pen.Thickness, this._x + x, this._y + y, width, height, 0, 0, Colors.Transparent, 0, 0, Colors.Transparent, 0, 0, 0);
            }
        }

        public void DrawText(string text, Font font, GHIElectronics.TinyCLR.UI.Media.Color color, int x, int y)
        {
            base.VerifyAccess();
            this._bitmap.DrawText(text, font, color, this._x + x, this._y + y);
        }

        public bool DrawText(ref string text, Font font, GHIElectronics.TinyCLR.UI.Media.Color color, int x, int y, int width, int height, TextAlignment alignment, TextTrimming trimming)
        {
            base.VerifyAccess();
            uint flags = 1;
            switch (alignment)
            {
                case TextAlignment.Left:
                    break;

                case TextAlignment.Center:
                    flags |= 2;
                    break;

                case TextAlignment.Right:
                    flags |= 0x20;
                    break;

                default:
                    throw new NotSupportedException();
            }
            if (trimming != TextTrimming.CharacterEllipsis)
            {
                if (trimming == TextTrimming.WordEllipsis)
                {
                    flags |= 8;
                }
            }
            else
            {
                flags |= 0x40;
            }
            int xRelStart = 0;
            int yRelStart = 0;
            return this._bitmap.DrawTextInRect(ref text, ref xRelStart, ref yRelStart, this._x + x, this._y + y, width, height, flags, color, font);
        }

        public void GetClippingRectangle(out int x, out int y, out int width, out int height)
        {
            if (this._clippingRectangles.Count == 0)
            {
                x = 0;
                y = 0;
                width = this._bitmap.Width - this._x;
                height = this._bitmap.Height - this._y;
            }
            else
            {
                ClipRectangle rectangle = (ClipRectangle) this._clippingRectangles.Peek();
                x = rectangle.X - this._x;
                y = rectangle.Y - this._y;
                width = rectangle.Width;
                height = rectangle.Height;
            }
        }

        public void GetTranslation(out int x, out int y)
        {
            base.VerifyAccess();
            x = this._x;
            y = this._y;
        }

        public void PopClippingRectangle()
        {
            base.VerifyAccess();
            int count = this._clippingRectangles.Count;
            if (count > 0)
            {
                ClipRectangle rectangle;
                this._clippingRectangles.Pop();
                if (count == 1)
                {
                    rectangle = new ClipRectangle(0, 0, this._bitmap.Width, this._bitmap.Height);
                }
                else
                {
                    rectangle = (ClipRectangle) this._clippingRectangles.Peek();
                }
                this._bitmap.SetClippingRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                this.EmptyClipRect = (rectangle.Width == 0) && (rectangle.Height == 0);
            }
        }

        public void PushClippingRectangle(int x, int y, int width, int height)
        {
            base.VerifyAccess();
            if ((width < 0) || (height < 0))
            {
                throw new ArgumentException();
            }
            ClipRectangle rectangle = new ClipRectangle(this._x + x, this._y + y, width, height);
            if (this._clippingRectangles.Count > 0)
            {
                ClipRectangle rectangle2 = (ClipRectangle) this._clippingRectangles.Peek();
                int num = Math.Max(rectangle.X, rectangle2.X);
                int num2 = Math.Min((int) (rectangle.X + rectangle.Width), (int) (rectangle2.X + rectangle2.Width));
                int num3 = Math.Max(rectangle.Y, rectangle2.Y);
                int num4 = Math.Min((int) (rectangle.Y + rectangle.Height), (int) (rectangle2.Y + rectangle2.Height));
                rectangle.X = num;
                rectangle.Y = num3;
                rectangle.Width = num2 - num;
                rectangle.Height = num4 - num3;
            }
            this._clippingRectangles.Push(rectangle);
            this._bitmap.SetClippingRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            this.EmptyClipRect = (rectangle.Width <= 0) || (rectangle.Height <= 0);
        }

        public void RotateImage(int angle, int destinationX, int destinationY, ImageSource bitmap, int sourceX, int sourceY, int sourceWidth, int sourceHeight, ushort opacity)
        {
            base.VerifyAccess();
            this._bitmap.RotateImage(angle, this._x + destinationX, this._y + destinationY, bitmap, sourceX, sourceY, sourceWidth, sourceHeight, opacity);
        }

        public void Scale9Image(int xDst, int yDst, int widthDst, int heightDst, ImageSource bitmap, int leftBorder, int topBorder, int rightBorder, int bottomBorder, ushort opacity)
        {
            base.VerifyAccess();
            this._bitmap.Scale9Image(this._x + xDst, this._y + yDst, widthDst, heightDst, bitmap, leftBorder, topBorder, rightBorder, bottomBorder, opacity);
        }

        public void SetPixel(GHIElectronics.TinyCLR.UI.Media.Color color, int x, int y)
        {
            base.VerifyAccess();
            this._bitmap.SetPixel(this._x + x, this._y + y, color);
        }

        public void StretchImage(int xDst, int yDst, int widthDst, int heightDst, ImageSource bitmap, int xSrc, int ySrc, int widthSrc, int heightSrc, ushort opacity)
        {
            base.VerifyAccess();
            this._bitmap.StretchImage(this._x + xDst, this._y + yDst, widthDst, heightDst, bitmap, xSrc, ySrc, widthSrc, heightSrc, opacity);
        }

        public void TileImage(int xDst, int yDst, ImageSource bitmap, int width, int height, ushort opacity)
        {
            base.VerifyAccess();
            this._bitmap.TileImage(this._x + xDst, this._y + yDst, bitmap, width, height, opacity);
        }

        public void Translate(int dx, int dy)
        {
            base.VerifyAccess();
            this._x += dx;
            this._y += dy;
        }

        public int Width
        {
            get
            {
                return this._bitmap.Width;
            }
        }

        public int Height
        {
            get
            {
                return this._bitmap.Height;
            }
        }

        private class ClipRectangle
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;

            public ClipRectangle(int x, int y, int width, int height)
            {
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
            }
        }
    }
}

