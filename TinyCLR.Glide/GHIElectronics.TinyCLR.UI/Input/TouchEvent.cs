namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class TouchEvent : BaseEvent
    {
        public DateTime Time;
        public TouchInput[] Touches;
    }
}

