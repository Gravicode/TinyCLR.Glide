namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class InputReportEventArgs : InputEventArgs
    {
        public readonly InputReport Report;

        public InputReportEventArgs(InputDevice inputDevice, InputReport report) : base(inputDevice, (report != null) ? report.Timestamp : DateTime.MinValue)
        {
            this.Report = report ?? throw new ArgumentNullException("report");
        }
    }
}

