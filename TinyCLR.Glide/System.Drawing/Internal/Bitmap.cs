namespace System.Drawing.Internal
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Bitmap : MarshalByRefObject, IDisposable
    {
        private object m_bitmap;
        public const ushort OpacityOpaque = 0xff;
        public const ushort OpacityTransparent = 0;
        public const int SRCCOPY = 1;
        public const int PATINVERT = 2;
        public const int DSTINVERT = 3;
        public const int BLACKNESS = 4;
        public const int WHITENESS = 5;
        public const int DSTGRAY = 6;
        public const int DSTLTGRAY = 7;
        public const int DSTDKGRAY = 8;
        public const int SINGLEPIXEL = 9;
        public const int RANDOM = 10;
        public const uint DT_None = 0;
        public const uint DT_WordWrap = 1;
        public const uint DT_TruncateAtBottom = 4;
        [Obsolete("Use DT_TrimmingWordEllipsis or DT_TrimmingCharacterEllipsis to specify the type of trimming needed.", false)]
        public const uint DT_Ellipsis = 8;
        public const uint DT_IgnoreHeight = 0x10;
        public const uint DT_AlignmentLeft = 0;
        public const uint DT_AlignmentCenter = 2;
        public const uint DT_AlignmentRight = 0x20;
        public const uint DT_AlignmentMask = 0x22;
        public const uint DT_TrimmingNone = 0;
        public const uint DT_TrimmingWordEllipsis = 8;
        public const uint DT_TrimmingCharacterEllipsis = 0x40;
        public const uint DT_TrimmingMask = 0x48;

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Bitmap(byte[] imageData, BitmapImageType type);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Bitmap(int width, int height);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Clear();
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void CreateInstantFromResources(uint buffer, uint size, uint assembly);
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Dispose(bool disposing);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void DrawEllipse(uint colorOutline, int thicknessOutline, int x, int y, int xRadius, int yRadius, uint colorGradientStart, int xGradientStart, int yGradientStart, uint colorGradientEnd, int xGradientEnd, int yGradientEnd, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void DrawImage(int xDst, int yDst, System.Drawing.Internal.Bitmap bitmap, int xSrc, int ySrc, int width, int height, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void DrawLine(uint color, int thickness, int x0, int y0, int x1, int y1);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void DrawRectangle(uint colorOutline, int thicknessOutline, int x, int y, int width, int height, int xCornerRadius, int yCornerRadius, uint colorGradientStart, int xGradientStart, int yGradientStart, uint colorGradientEnd, int xGradientEnd, int yGradientEnd, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void DrawText(string text, Font font, uint color, int x, int y);
        public void DrawTextInRect(string text, int x, int y, int width, int height, uint dtFlags, Color color, Font font)
        {
            int xRelStart = 0;
            int yRelStart = 0;
            this.DrawTextInRect(ref text, ref xRelStart, ref yRelStart, x, y, width, height, dtFlags, (uint) (color.value & 0xffffffL), font);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern bool DrawTextInRect(ref string text, ref int xRelStart, ref int yRelStart, int x, int y, int width, int height, uint dtFlags, uint color, Font font);
        ~Bitmap()
        {
            this.Dispose(false);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Flush(IntPtr hdc);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Flush(int x, int y, int width, int height);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern byte[] GetBitmap();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern uint GetPixel(int xPos, int yPos);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetSizeForLcdFromHdc(IntPtr hdc, out int width, out int height);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void MakeTransparent(uint color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void RotateImage(int angle, int xDst, int yDst, System.Drawing.Internal.Bitmap bitmap, int xSrc, int ySrc, int width, int height, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void Scale9Image(int xDst, int yDst, int widthDst, int heightDst, System.Drawing.Internal.Bitmap bitmap, int leftBorder, int topBorder, int rightBorder, int bottomBorder, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetClippingRectangle(int x, int y, int width, int height);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void SetPixel(int xPos, int yPos, uint color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void StretchImage(int xDst, int yDst, System.Drawing.Internal.Bitmap bitmap, int width, int height, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void StretchImage(int xDst, int yDst, int widthDst, int heightDst, System.Drawing.Internal.Bitmap bitmap, int xSrc, int ySrc, int widthSrc, int heightSrc, ushort opacity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern void TileImage(int xDst, int yDst, System.Drawing.Internal.Bitmap bitmap, int width, int height, ushort opacity);

        public int Width { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public int Height { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        public enum BitmapImageType : byte
        {
            TinyCLRBitmap = 0,
            Gif = 1,
            Jpeg = 2,
            Bmp = 3
        }
    }
}

