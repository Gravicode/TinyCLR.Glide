namespace GHIElectronics.TinyCLR.UI.Shapes
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;

    public class Ellipse : Shape
    {
        public Ellipse(int xRadius, int yRadius)
        {
            if ((xRadius < 0) || (yRadius < 0))
            {
                throw new ArgumentException();
            }
            base.Width = (xRadius * 2) + 1;
            base.Height = (yRadius * 2) + 1;
        }

        public override void OnRender(DrawingContext dc)
        {
            int x = ((base._renderWidth / 2) + base.Stroke.Thickness) - 1;
            int y = ((base._renderHeight / 2) + base.Stroke.Thickness) - 1;
            int xRadius = (base._renderWidth / 2) - ((base.Stroke.Thickness - 1) * 2);
            int yRadius = (base._renderHeight / 2) - ((base.Stroke.Thickness - 1) * 2);
            dc.DrawEllipse(base.Fill, base.Stroke, x, y, xRadius, yRadius);
        }
    }
}

