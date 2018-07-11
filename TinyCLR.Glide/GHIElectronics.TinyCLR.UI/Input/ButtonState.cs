namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    [Flags]
    public enum ButtonState : byte
    {
        None = 0,
        Down = 1,
        Held = 2
    }
}

