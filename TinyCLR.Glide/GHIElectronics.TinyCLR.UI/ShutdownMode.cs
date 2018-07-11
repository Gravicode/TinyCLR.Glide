namespace GHIElectronics.TinyCLR.UI
{
    using System;

    public enum ShutdownMode : byte
    {
        OnLastWindowClose = 0,
        OnMainWindowClose = 1,
        OnExplicitShutdown = 2
    }
}

