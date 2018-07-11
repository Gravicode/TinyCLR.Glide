namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class InputProvider
    {
        private readonly InputProviderSite buttonSite;
        private readonly Application application;

        public InputProvider(Application a)
        {
            this.buttonSite = InputManager.CurrentInputManager.RegisterInputProvider(this);
            this.application = a;
        }

        public void RaiseButton(HardwareButton button, bool state, DateTime time)
        {
            RawButtonInputReport report = new RawButtonInputReport(null, time, button, state ? RawButtonActions.ButtonUp : RawButtonActions.ButtonDown);
            InputReportArgs args = new InputReportArgs(InputManager.CurrentInputManager.ButtonDevice, report);
            this.application.Dispatcher.BeginInvoke(_ => this.buttonSite.ReportInput(args.Device, args.Report), null);
        }

        public void RaiseTouch(int x, int y, TouchMessages which, DateTime time)
        {
            TouchEvent ev = new TouchEvent {
                Time = time,
                EventMessage = (byte) which
            };
            TouchInput[] inputArray1 = new TouchInput[1];
            TouchInput input1 = new TouchInput {
                X = x,
                Y = y
            };
            inputArray1[0] = input1;
            ev.Touches = inputArray1;
            Application.Current.OnEvent(ev);
        }
    }
}

