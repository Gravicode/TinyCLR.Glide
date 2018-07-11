namespace GHIElectronics.TinyCLR.UI.Shapes
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Media;
    using System;

    public abstract class Shape : UIElement
    {
        private Brush _fill;
        private Pen _stroke;

        protected Shape()
        {
        }

        public Brush Fill
        {
            get
            {
                if (this._fill == null)
                {
                    SolidColorBrush brush1 = new SolidColorBrush(Colors.Black) {
                        Opacity = 0
                    };
                    this._fill = brush1;
                }
                return this._fill;
            }
            set
            {
                this._fill = value;
                base.Invalidate();
            }
        }

        public Pen Stroke
        {
            get
            {
                if (this._stroke == null)
                {
                    this._stroke = new Pen(Colors.White, 0);
                }
                return this._stroke;
            }
            set
            {
                this._stroke = value;
                base.Invalidate();
            }
        }
    }
}

