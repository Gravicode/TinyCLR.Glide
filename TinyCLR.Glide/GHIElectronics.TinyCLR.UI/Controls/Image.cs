namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class Image : UIElement
    {
        private ImageSource _bitmap;

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            desiredWidth = desiredHeight = 0;
            if (this._bitmap != null)
            {
                GHIElectronics.TinyCLR.UI.Media.Stretch stretch = this.Stretch;
                if (stretch != GHIElectronics.TinyCLR.UI.Media.Stretch.None)
                {
                    if (stretch != GHIElectronics.TinyCLR.UI.Media.Stretch.Fill)
                    {
                        throw new NotSupportedException();
                    }
                    desiredWidth = base.Width;
                    desiredHeight = base.Height;
                }
                else
                {
                    desiredWidth = this._bitmap.Width;
                    desiredHeight = this._bitmap.Height;
                }
            }
        }

        public override void OnRender(DrawingContext dc)
        {
            if (this._bitmap != null)
            {
                GHIElectronics.TinyCLR.UI.Media.Stretch stretch = this.Stretch;
                if (stretch != GHIElectronics.TinyCLR.UI.Media.Stretch.None)
                {
                    if (stretch != GHIElectronics.TinyCLR.UI.Media.Stretch.Fill)
                    {
                        return;
                    }
                }
                else
                {
                    dc.DrawImage(this._bitmap, 0, 0);
                    return;
                }
                dc.StretchImage(0, 0, base._renderWidth, base._renderHeight, this._bitmap, 0, 0, this._bitmap.Width, this._bitmap.Height, 0xff);
            }
        }

        public GHIElectronics.TinyCLR.UI.Media.Stretch Stretch { get; set; }

        public ImageSource Source
        {
            get
            {
                base.VerifyAccess();
                return this._bitmap;
            }
            set
            {
                base.VerifyAccess();
                this._bitmap = value;
                base.InvalidateMeasure();
            }
        }
    }
}

