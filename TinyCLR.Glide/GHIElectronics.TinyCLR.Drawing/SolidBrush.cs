namespace System.Drawing
{
    using System;
    using System.Runtime.CompilerServices;

    public class SolidBrush : Brush
    {
        public SolidBrush(System.Drawing.Color color)
        {
            this.Color = color;
        }

        public override object Clone()
        {
            return new SolidBrush(this.Color);
        }

        public System.Drawing.Color Color { get; set; }
    }
}

