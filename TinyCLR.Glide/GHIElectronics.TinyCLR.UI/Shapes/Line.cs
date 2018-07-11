namespace GHIElectronics.TinyCLR.UI.Shapes
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;

    public class Line : Shape
    {
        private GHIElectronics.TinyCLR.UI.Shapes.Direction _direction;

        public Line() : this(0, 0)
        {
        }

        public Line(int dx, int dy)
        {
            if ((dx < 0) || (dy < 0))
            {
                throw new ArgumentException();
            }
            base.Width = dx + 1;
            base.Height = dy + 1;
        }

        public override void OnRender(DrawingContext dc)
        {
            int num = base._renderWidth;
            int num2 = base._renderHeight;
            if (this._direction == GHIElectronics.TinyCLR.UI.Shapes.Direction.TopToBottom)
            {
                dc.DrawLine(base.Stroke, 0, 0, num - 1, num2 - 1);
            }
            else
            {
                dc.DrawLine(base.Stroke, 0, num2 - 1, num - 1, 0);
            }
        }

        public GHIElectronics.TinyCLR.UI.Shapes.Direction Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this._direction = value;
                base.Invalidate();
            }
        }
    }
}

