namespace GHIElectronics.TinyCLR.UI.Media
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class LinearGradientBrush : Brush
    {
        public Color StartColor;
        public Color EndColor;
        public BrushMappingMode MappingMode;
        public int StartX;
        public int StartY;
        public int EndX;
        public int EndY;
        public const int RelativeBoundingBoxSize = 0x3e8;

        public LinearGradientBrush(Color startColor, Color endColor) : this(startColor, endColor, 0, 0, 0x3e8, 0x3e8)
        {
        }

        public LinearGradientBrush(Color startColor, Color endColor, int startX, int startY, int endX, int endY)
        {
            this.MappingMode = BrushMappingMode.RelativeToBoundingBox;
            this.StartColor = startColor;
            this.EndColor = endColor;
            this.StartX = startX;
            this.StartY = startY;
            this.EndX = endX;
            this.EndY = endY;
        }

        internal override void RenderRectangle(Bitmap bmp, Pen pen, int x, int y, int width, int height)
        {
            int startX;
            int startY;
            int endX;
            int endY;
            Color outlineColor = (pen != null) ? pen.Color : Colors.Transparent;
            ushort outlineThickness = 0;
            if (pen != null)
            {
                outlineThickness = pen.Thickness;
            }
            if (this.MappingMode == BrushMappingMode.RelativeToBoundingBox)
            {
                startX = x + ((int) (((width - 1) * this.StartX) / 0x3e8L));
                startY = y + ((int) (((height - 1) * this.StartY) / 0x3e8L));
                endX = x + ((int) (((width - 1) * this.EndX) / 0x3e8L));
                endY = y + ((int) (((height - 1) * this.EndY) / 0x3e8L));
            }
            else
            {
                startX = this.StartX;
                startY = this.StartY;
                endX = this.EndX;
                endY = this.EndY;
            }
            bmp.DrawRectangle(outlineColor, outlineThickness, x, y, width, height, 0, 0, this.StartColor, startX, startY, this.EndColor, endX, endY, base.Opacity);
        }
    }
}

