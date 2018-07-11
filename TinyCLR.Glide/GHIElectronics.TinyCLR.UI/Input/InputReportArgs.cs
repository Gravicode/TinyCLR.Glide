namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class InputReportArgs
    {
        public readonly InputDevice Device;
        public readonly InputReport Report;

        public InputReportArgs(object dev, object report)
        {
            this.Device = (InputDevice) dev;
            this.Report = (InputReport) report;
        }
    }
}

