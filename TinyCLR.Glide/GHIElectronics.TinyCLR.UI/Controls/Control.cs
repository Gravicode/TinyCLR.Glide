namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Drawing;

    public class Control : UIElement
    {
        protected internal GHIElectronics.TinyCLR.UI.Media.Brush _background;
        protected internal GHIElectronics.TinyCLR.UI.Media.Brush _foreground = new SolidColorBrush(Colors.Black);
        protected internal System.Drawing.Font _font;

        public override void OnRender(DrawingContext dc)
        {
            if (this._background != null)
            {
                dc.DrawRectangle(this._background, null, 0, 0, base._renderWidth, base._renderHeight);
            }
        }

        public GHIElectronics.TinyCLR.UI.Media.Brush Background
        {
            get
            {
                base.VerifyAccess();
                return this._background;
            }
            set
            {
                base.VerifyAccess();
                this._background = value;
                base.Invalidate();
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                base.VerifyAccess();
                this._font = value;
                base.InvalidateMeasure();
            }
        }

        public GHIElectronics.TinyCLR.UI.Media.Brush Foreground
        {
            get
            {
                base.VerifyAccess();
                return this._foreground;
            }
            set
            {
                base.VerifyAccess();
                this._foreground = value;
                base.Invalidate();
            }
        }
    }
}

