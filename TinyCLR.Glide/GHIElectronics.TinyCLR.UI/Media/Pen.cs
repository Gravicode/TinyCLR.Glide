namespace GHIElectronics.TinyCLR.UI.Media
{
    using System;

    public class Pen
    {
        public GHIElectronics.TinyCLR.UI.Media.Color Color;
        public ushort Thickness;

        public Pen(GHIElectronics.TinyCLR.UI.Media.Color color) : this(color, 1)
        {
        }

        public Pen(GHIElectronics.TinyCLR.UI.Media.Color color, ushort thickness)
        {
            this.Color = color;
            this.Thickness = thickness;
        }
    }
}

