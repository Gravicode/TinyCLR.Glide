namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public class RawButtonInputReport : InputReport
    {
        public readonly HardwareButton Button;
        public readonly RawButtonActions Actions;

        public RawButtonInputReport(PresentationSource inputSource, DateTime timestamp, HardwareButton button, RawButtonActions actions) : base(inputSource, timestamp)
        {
            this.Button = button;
            this.Actions = actions;
        }
    }
}

