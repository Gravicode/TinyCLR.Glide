namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public class RawTouchInputReport : InputReport
    {
        public readonly UIElement Target;
        public readonly byte EventMessage;
        public readonly TouchInput[] Touches;

        public RawTouchInputReport(PresentationSource inputSource, DateTime timestamp, byte eventMessage, TouchInput[] touches) : base(inputSource, timestamp)
        {
            this.EventMessage = eventMessage;
            this.Touches = touches;
        }

        public RawTouchInputReport(PresentationSource inputSource, DateTime timestamp, byte eventMessage, TouchInput[] touches, UIElement destTarget) : base(inputSource, timestamp)
        {
            this.EventMessage = eventMessage;
            this.Touches = touches;
            this.Target = destTarget;
        }
    }
}

