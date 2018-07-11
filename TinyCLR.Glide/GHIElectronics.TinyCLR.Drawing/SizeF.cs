namespace System.Drawing
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SizeF
    {
        public static readonly SizeF Empty;
        public SizeF(SizeF size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
        }

        public SizeF(float width, float height)
        {
            this.Width = width;
            this.Height = height;
        }

        public bool IsEmpty
        {
            get
            {
                return ((this.Width == 0f) && (this.Height == 0f));
            }
        }
        public float Width { get; set; }
        public float Height { get; set; }
        static SizeF()
        {
        }
    }
}

