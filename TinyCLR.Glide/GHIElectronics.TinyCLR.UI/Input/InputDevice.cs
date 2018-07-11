namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Threading;
    using System;

    public abstract class InputDevice : DispatcherObject
    {
        protected InputDevice()
        {
        }

        public abstract UIElement Target { get; }

        public abstract InputManager.InputDeviceType DeviceType { get; }
    }
}

