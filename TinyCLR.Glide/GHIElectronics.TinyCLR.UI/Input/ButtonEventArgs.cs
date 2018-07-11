namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public class ButtonEventArgs : InputEventArgs
    {
        public readonly HardwareButton Button;
        public readonly PresentationSource InputSource;
        internal bool _isRepeat;

        public ButtonEventArgs(ButtonDevice buttonDevice, PresentationSource inputSource, DateTime timestamp, HardwareButton button) : base(buttonDevice, timestamp)
        {
            this.InputSource = inputSource;
            this.Button = button;
        }

        public GHIElectronics.TinyCLR.UI.Input.ButtonState ButtonState
        {
            get
            {
                return ((ButtonDevice) base.Device).GetButtonState(this.Button);
            }
        }

        public bool IsRepeat
        {
            get
            {
                return this._isRepeat;
            }
        }
    }
}

