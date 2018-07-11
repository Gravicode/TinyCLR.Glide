namespace GHIElectronics.TinyCLR.UI
{
    using GHIElectronics.TinyCLR.UI.Input;
    using GHIElectronics.TinyCLR.UI.Threading;
    using System;

    public class PresentationSource : DispatcherObject
    {
        private UIElement _rootUIElement;

        public UIElement RootUIElement
        {
            get
            {
                return this._rootUIElement;
            }
            set
            {
                base.VerifyAccess();
                if (this._rootUIElement != value)
                {
                    int num;
                    int num2;
                    this._rootUIElement = value;
                    UIElement element1 = this._rootUIElement;
                    value.Measure(0x7ffff, 0x7ffff);
                    value.GetDesiredSize(out num, out num2);
                    value.Arrange(0, 0, num, num2);
                    Buttons.Focus(value);
                }
            }
        }
    }
}

