// Decompiled with JetBrains decompiler
// Type: System.Drawing.Pen
// Assembly: GHIElectronics.TinyCLR.Drawing, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FB94FDB-AA94-43DE-9FD5-9C0BE64863BE
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.Drawing.dll

using System.Drawing.Drawing2D;

namespace System.Drawing
{
    public sealed class Pen : MarshalByRefObject, ICloneable, IDisposable
    {
        public float Width { get; set; }

        public Color Color { get; set; }

        public PenType PenType { get; }

        public Brush Brush
        {
            get
            {
                return (Brush)new SolidBrush(this.Color);
            }
            set
            {
                SolidBrush solidBrush;
                if ((solidBrush = value as SolidBrush) == null)
                    throw new NotSupportedException();
                this.Color = solidBrush.Color;
            }
        }

        public Pen(Color color)
          : this(color, 1f)
        {
        }

        public Pen(Brush brush)
          : this(brush, 1f)
        {
        }

        public Pen(Color color, float width)
        {
            this.Width = width;
            this.Color = color;
            this.PenType = PenType.SolidColor;
        }

        public Pen(Brush brush, float width)
        {
            this.Width = width;
            this.Brush = brush;
            this.PenType = PenType.SolidColor;
        }

        public void Dispose()
        {
        }

        public object Clone()
        {
            return (object)new Pen(this.Color, this.Width);
        }
    }
}
