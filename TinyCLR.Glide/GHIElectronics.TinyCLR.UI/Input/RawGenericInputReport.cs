namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public class RawGenericInputReport : InputReport
    {
        public readonly UIElement Target;
        public readonly GenericEvent InternalEvent;

        public RawGenericInputReport(PresentationSource inputSource, GenericEvent genericEvent) : base(inputSource, genericEvent.Time)
        {
            this.InternalEvent = genericEvent;
            this.Target = null;
        }

        public RawGenericInputReport(PresentationSource inputSource, GenericEvent genericEvent, UIElement destTarget) : base(inputSource, genericEvent.Time)
        {
            this.InternalEvent = genericEvent;
            this.Target = destTarget;
        }
    }
}

