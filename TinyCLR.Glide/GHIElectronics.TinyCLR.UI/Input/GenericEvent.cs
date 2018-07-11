namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class GenericEvent : BaseEvent
    {
        public byte EventCategory;
        public uint EventData;
        public int X;
        public int Y;
        public DateTime Time;
    }
}

