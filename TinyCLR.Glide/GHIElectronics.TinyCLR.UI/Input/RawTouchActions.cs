namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public enum RawTouchActions
    {
        TouchDown = 1,
        TouchUp = 2,
        Activate = 4,
        Deactivate = 8,
        TouchMove = 0x10
    }
}

