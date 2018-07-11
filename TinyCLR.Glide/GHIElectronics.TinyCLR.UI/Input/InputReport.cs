namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public abstract class InputReport
    {
        public readonly PresentationSource InputSource;
        public readonly DateTime Timestamp;

        protected InputReport(PresentationSource inputSource, DateTime timestamp)
        {
            this.InputSource = inputSource;
            this.Timestamp = timestamp;
        }
    }
}

