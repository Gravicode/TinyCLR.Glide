// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Controls.Button
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Media;

namespace GHIElectronics.TinyCLR.UI.Controls
{
    public class Button : ContentControl
    {
        public Button()
        {
            this.Background = (Brush)new SolidColorBrush(Colors.Gray);
        }

        public event RoutedEventHandler Click;

        protected override void OnTouchUp(TouchEventArgs e)
        {
            RoutedEventArgs e1 = new RoutedEventArgs(new RoutedEvent("ClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler)), (object)this);
            // ISSUE: reference to a compiler-generated field
            RoutedEventHandler click = this.Click;
            if (click != null)
                click((object)this, e1);
            e.Handled = e1.Handled;
        }
    }
}
