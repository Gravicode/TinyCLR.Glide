// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Controls.TextBox
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Media;
using System;

namespace GHIElectronics.TinyCLR.UI.Controls
{
    public class TextBox : Control
    {
        private string text = string.Empty;
        private int width;

        public TextBox()
        {
            this.Background = (Brush)new SolidColorBrush(Colors.White);
        }

        public event TextChangedEventHandler TextChanged;

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.InvalidateMeasure();
                TextChangedEventArgs e = new TextChangedEventArgs(new RoutedEvent("TextChangedEvent", RoutingStrategy.Bubble, typeof(TextChangedEventHandler)), (object)this);
                TextChangedEventHandler textChanged = this.TextChanged;
                if (textChanged == null)
                    return;
                textChanged((object)this, e);
            }
        }

        internal bool ForOnScreenKeyboard { get; set; }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            if (this.ForOnScreenKeyboard)
                return;
            Application.Current.ShowOnScreenKeyboardFor(this);
        }

        protected override void MeasureOverride(int availableWidth, int availableHeight, out int desiredWidth, out int desiredHeight)
        {
            this._font.ComputeExtent(this.text, out desiredWidth, out desiredHeight);
            this.width = desiredWidth;
        }

        public override void OnRender(DrawingContext dc)
        {
            SolidColorBrush foreground;
            if ((foreground = this.Foreground as SolidColorBrush) == null)
                throw new NotSupportedException();
            base.OnRender(dc);
            string text = this.text;
            int x = this._renderWidth - this.width;
            if (x > 0)
                dc.DrawText(ref text, this._font, foreground.Color, 0, 0, this._renderWidth, this._font.Height, TextAlignment.Left, TextTrimming.CharacterEllipsis);
            else
                dc.DrawText(ref text, this._font, foreground.Color, x, 0, this._renderWidth + this.width, this._font.Height, TextAlignment.Left, TextTrimming.CharacterEllipsis);
        }
    }
}
