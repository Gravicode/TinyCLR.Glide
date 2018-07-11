namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class GenericEventArgs : InputEventArgs
    {
        public readonly GenericEvent InternalEvent;

        public GenericEventArgs(InputDevice inputDevice, GenericEvent genericEvent) : base(inputDevice, genericEvent.Time)
        {
            this.InternalEvent = genericEvent;
        }
    }
}

