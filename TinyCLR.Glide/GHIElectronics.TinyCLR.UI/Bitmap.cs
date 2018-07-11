namespace GHIElectronics.TinyCLR.UI
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Drawing;

    public class Bitmap : IDisposable
    {
        private readonly Graphics g;
        public const ushort OpacityOpaque = 0xff;
        public const ushort OpacityTransparent = 0;
        public const uint DT_WordWrap = 1;
        public const uint DT_TruncateAtBottom = 4;
        public const uint DT_IgnoreHeight = 0x10;
        public const uint DT_AlignmentLeft = 0;
        public const uint DT_AlignmentCenter = 2;
        public const uint DT_AlignmentRight = 0x20;
        public const uint DT_TrimmingWordEllipsis = 8;
        public const uint DT_TrimmingCharacterEllipsis = 0x40;

        public Bitmap(Graphics g)
        {
            this.g = g;
        }

        public void Clear()
        {
            this.g.Clear(System.Drawing.Color.Black);
        }

        public void Dispose()
        {
            this.g.Dispose();
        }

        public void DrawEllipse(GHIElectronics.TinyCLR.UI.Media.Color color1, ushort thickness, int v1, int v2, int xRadius, int yRadius, GHIElectronics.TinyCLR.UI.Media.Color color2, int v3, int v4, GHIElectronics.TinyCLR.UI.Media.Color color3, int v5, int v6, ushort v7)
        {
            this.g.surface.DrawEllipse(color1.ToNativeColor(), thickness, v1, v2, xRadius, yRadius, color2.ToNativeColor(), v3, v4, color3.ToNativeColor(), v5, v6, v7);
        }

        public void DrawImage(int v1, int v2, ImageSource source, int v3, int v4, int width, int height)
        {
            this.g.surface.DrawImage(v1, v2, source.graphics.surface, v3, v4, width, height, 0xff);
        }

        public void DrawImage(int x, int y, ImageSource bitmapSource, int v1, int v2, int width, int height, ushort opacity)
        {
            this.g.surface.DrawImage(x, y, bitmapSource.graphics.surface, v1, v2, width, height, opacity);
        }

        public void DrawLine(GHIElectronics.TinyCLR.UI.Media.Color color, int v, int ix1, int y1, int ix2, int y2)
        {
            this.g.surface.DrawLine(color.ToNativeColor(), v, ix1, y1, ix2, y2);
        }

        public void DrawRectangle(GHIElectronics.TinyCLR.UI.Media.Color outlineColor, ushort outlineThickness, int x, int y, int width, int height, int v1, int v2, GHIElectronics.TinyCLR.UI.Media.Color color1, int v3, int v4, GHIElectronics.TinyCLR.UI.Media.Color color2, int v5, int v6, ushort opacity)
        {
            this.g.surface.DrawRectangle(outlineColor.ToNativeColor(), outlineThickness, x, y, width, height, v1, v2, color1.ToNativeColor(), v3, v4, color2.ToNativeColor(), v5, v6, opacity);
        }

        public void DrawText(string text, Font font, GHIElectronics.TinyCLR.UI.Media.Color color, int v1, int v2)
        {
            this.g.surface.DrawText(text, font, color.ToNativeColor(), v1, v2);
        }

        internal bool DrawTextInRect(ref string text, ref int xRelStart, ref int yRelStart, int v1, int v2, int width, int height, uint flags, GHIElectronics.TinyCLR.UI.Media.Color color, Font font)
        {
            return this.g.surface.DrawTextInRect(ref text, ref xRelStart, ref yRelStart, v1, v2, width, height, flags, color.ToNativeColor(), font);
        }

        public void Flush(int x, int y, int width, int height)
        {
            this.g.Flush();
        }

        public void RotateImage(int angle, int v1, int v2, ImageSource bitmap, int sourceX, int sourceY, int sourceWidth, int sourceHeight, ushort opacity)
        {
            this.g.surface.RotateImage(angle, v1, v2, bitmap.graphics.surface, sourceX, sourceY, sourceWidth, sourceHeight, opacity);
        }

        public void Scale9Image(int v1, int v2, int widthDst, int heightDst, ImageSource bitmap, int leftBorder, int topBorder, int rightBorder, int bottomBorder, ushort opacity)
        {
            this.g.surface.Scale9Image(v1, v2, widthDst, heightDst, bitmap.graphics.surface, leftBorder, topBorder, rightBorder, bottomBorder, opacity);
        }

        public void SetClippingRectangle(int x, int y, int width, int height)
        {
            this.g.surface.SetClippingRectangle(x, y, width, height);
        }

        public void SetPixel(int x, int y, GHIElectronics.TinyCLR.UI.Media.Color color)
        {
            this.g.surface.SetPixel(x, y, color.ToNativeColor());
        }

        public void StretchImage(int x, int y, ImageSource bitmapSource, int width, int height, ushort opacity)
        {
            this.g.surface.StretchImage(x, y, bitmapSource.graphics.surface, width, height, opacity);
        }

        public void StretchImage(int v1, int v2, int widthDst, int heightDst, ImageSource bitmap, int xSrc, int ySrc, int widthSrc, int heightSrc, ushort opacity)
        {
            this.g.surface.StretchImage(v1, v2, widthDst, heightDst, bitmap.graphics.surface, xSrc, ySrc, widthSrc, heightSrc, opacity);
        }

        public void TileImage(int v1, int v2, ImageSource bitmap, int width, int height, ushort opacity)
        {
            this.g.surface.TileImage(v1, v2, bitmap.graphics.surface, width, height, opacity);
        }

        public int Height
        {
            get
            {
                return this.g.Height;
            }
        }

        public int Width
        {
            get
            {
                return this.g.Width;
            }
        }
    }
}

