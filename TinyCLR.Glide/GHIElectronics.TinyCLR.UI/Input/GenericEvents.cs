namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class GenericEvents
    {
        public static readonly RoutedEvent GenericStandardEvent = new RoutedEvent("GenericStandardEvent", RoutingStrategy.Tunnel, typeof(GenericEventArgs));
    }
}

