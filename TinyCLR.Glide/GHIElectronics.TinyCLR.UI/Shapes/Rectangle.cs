namespace GHIElectronics.TinyCLR.UI.Shapes
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;

    public class Rectangle : Shape
    {
        public Rectangle()
        {
            base.Width = 0;
            base.Height = 0;
        }

        public Rectangle(int width, int height)
        {
            if ((width < 0) || (height < 0))
            {
                throw new ArgumentException();
            }
            base.Width = width;
            base.Height = height;
        }

        public override void OnRender(DrawingContext dc)
        {
            int x = (base.Stroke != null) ? (base.Stroke.Thickness / 2) : 0;
            dc.DrawRectangle(base.Fill, base.Stroke, x, x, base._renderWidth - (2 * x), base._renderHeight - (2 * x));
        }
    }
}

