namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class TouchEvents
    {
        public static readonly RoutedEvent TouchDownEvent = new RoutedEvent("TouchDownEvent", RoutingStrategy.Tunnel, typeof(TouchEventArgs));
        public static readonly RoutedEvent TouchMoveEvent = new RoutedEvent("TouchMoveEvent", RoutingStrategy.Tunnel, typeof(TouchEventArgs));
        public static readonly RoutedEvent TouchUpEvent = new RoutedEvent("TouchUpEvent", RoutingStrategy.Tunnel, typeof(TouchEventArgs));
    }
}

