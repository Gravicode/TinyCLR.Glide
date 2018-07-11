namespace GHIElectronics.TinyCLR.UI.Shapes
{
    using GHIElectronics.TinyCLR.UI.Media;
    using System;

    public class Polygon : Shape
    {
        private int[] _pts;

        public Polygon()
        {
        }

        public Polygon(int[] pts)
        {
            this.Points = pts;
        }

        public override void OnRender(DrawingContext dc)
        {
            if (this._pts != null)
            {
                dc.DrawPolygon(base.Fill, base.Stroke, this._pts);
            }
        }

        public int[] Points
        {
            get
            {
                return this._pts;
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    throw new ArgumentException();
                }
                this._pts = value;
                base.InvalidateMeasure();
            }
        }
    }
}

