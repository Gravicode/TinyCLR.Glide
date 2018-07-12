namespace System.Drawing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public sealed class Font : MarshalByRefObject, ICloneable, IDisposable
    {
        private object m_font;
        private const int DefaultKerning = 0x400;

        private Font()
        {
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int CharWidth(char c);
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void ComputeExtent(string text, out int width, out int height)
        {
            this.ComputeExtent(text, out width, out height, 0x400);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void ComputeExtent(string text, out int width, out int height, int kerning);
        public void ComputeTextInRect(string text, out int renderWidth, out int renderHeight)
        {
            this.ComputeTextInRect(text, out renderWidth, out renderHeight, 0, 0, 0x10000, 0, 0x11);
        }

        public void ComputeTextInRect(string text, out int renderWidth, out int renderHeight, int availableWidth)
        {
            this.ComputeTextInRect(text, out renderWidth, out renderHeight, 0, 0, availableWidth, 0, 0x11);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern void ComputeTextInRect(string text, out int renderWidth, out int renderHeight, int xRelStart, int yRelStart, int availableWidth, int availableHeight, uint dtFlags);
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void CreateInstantFromResources(uint buffer, uint size, uint assembly);
        public void Dispose()
        {
        }

        public GraphicsUnit Unit
        {
            get
            {
                return GraphicsUnit.Pixel;
            }
        }

        public int Height { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal int AverageWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal int MaxWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal int Ascent { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal int Descent { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal int InternalLeading { [MethodImpl(MethodImplOptions.InternalCall)] get; }

        internal int ExternalLeading { [MethodImpl(MethodImplOptions.InternalCall)] get; }
    }
}

