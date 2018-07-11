namespace GHIElectronics.TinyCLR.UI.Controls
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Media;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class Text : UIElement
    {
        protected System.Drawing.Font _font;
        private GHIElectronics.TinyCLR.UI.Media.Color _foreColor;
        protected string _text;
        private bool _textWrap;
        private TextTrimming _trimming;
        private GHIElectronics.TinyCLR.UI.Media.TextAlignment _alignment;

        public Text() : this(null, null)
        {
        }

        public Text(string content) : this(null, content)
        {
        }

        public Text(System.Drawing.Font font, string content)
        {
            this._trimming = TextTrimming.WordEllipsis;
            this._text = content;
            this._font = font;
            this._foreColor = Colors.Black;
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            if (((this._font == null) || (this._text == null)) || (this._text.Length <= 0))
            {
                desiredWidth = 0;
                desiredHeight = 0;
                if (this._font != null)
                {
                    desiredHeight = this._font.Height;
                }
            }
            else
            {
                uint dtFlags = 0x11;
                switch (this._alignment)
                {
                    case GHIElectronics.TinyCLR.UI.Media.TextAlignment.Left:
                        dtFlags |= 0;
                        break;

                    case GHIElectronics.TinyCLR.UI.Media.TextAlignment.Center:
                        dtFlags |= 2;
                        break;

                    case GHIElectronics.TinyCLR.UI.Media.TextAlignment.Right:
                        dtFlags |= 0x20;
                        break;

                    default:
                        throw new NotSupportedException();
                }
                switch (this._trimming)
                {
                    case TextTrimming.CharacterEllipsis:
                        dtFlags |= 0x40;
                        break;

                    case TextTrimming.WordEllipsis:
                        dtFlags |= 8;
                        break;
                }
                this._font.ComputeTextInRect(this._text, out desiredWidth, out desiredHeight, 0, 0, availableWidth, 0, dtFlags);
                if (!this._textWrap)
                {
                    desiredHeight = this._font.Height;
                }
            }
        }

        public override void OnRender(DrawingContext dc)
        {
            if ((this._font != null) && (this._text != null))
            {
                int height = this._textWrap ? base._renderHeight : this._font.Height;
                string text = this._text;
                dc.DrawText(ref text, this._font, this._foreColor, 0, 0, base._renderWidth, height, this._alignment, this._trimming);
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

        public GHIElectronics.TinyCLR.UI.Media.Color ForeColor
        {
            get
            {
                return this._foreColor;
            }
            set
            {
                base.VerifyAccess();
                this._foreColor = value;
                base.Invalidate();
            }
        }

        public string TextContent
        {
            get
            {
                return this._text;
            }
            set
            {
                base.VerifyAccess();
                if (this._text != value)
                {
                    this._text = value;
                    base.InvalidateMeasure();
                }
            }
        }

        public TextTrimming Trimming
        {
            get
            {
                return this._trimming;
            }
            set
            {
                base.VerifyAccess();
                this._trimming = value;
                base.Invalidate();
            }
        }

        public GHIElectronics.TinyCLR.UI.Media.TextAlignment TextAlignment
        {
            get
            {
                return this._alignment;
            }
            set
            {
                base.VerifyAccess();
                this._alignment = value;
                base.Invalidate();
            }
        }

        public int LineHeight
        {
            get
            {
                if (this._font == null)
                {
                    return 0;
                }
                return (this._font.Height + this._font.ExternalLeading);
            }
        }

        public bool TextWrap
        {
            get
            {
                return this._textWrap;
            }
            set
            {
                base.VerifyAccess();
                this._textWrap = value;
                base.InvalidateMeasure();
            }
        }
    }
}

