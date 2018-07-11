namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public class InputEventArgs : RoutedEventArgs
    {
        public readonly DateTime Timestamp;
        internal InputDevice _inputDevice;

        public InputEventArgs(InputDevice inputDevice, DateTime timestamp)
        {
            this._inputDevice = inputDevice;
            this.Timestamp = timestamp;
        }

        public InputDevice Device
        {
            get
            {
                return this._inputDevice;
            }
        }
    }
}

