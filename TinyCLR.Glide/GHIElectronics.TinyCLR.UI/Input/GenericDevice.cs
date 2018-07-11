namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class GenericDevice : InputDevice
    {
        private UIElement _focus;
        private InputManager _inputManager;

        internal GenericDevice(InputManager inputManager)
        {
            this._inputManager = inputManager;
            this._inputManager.InputDeviceEvents[2].PostProcessInput += new ProcessInputEventHandler(this.PostProcessInput);
        }

        private void PostProcessInput(object sender, ProcessInputEventArgs e)
        {
            InputReportEventArgs args;
            RawGenericInputReport report;
            if ((((args = e.StagingItem.Input as InputReportEventArgs) != null) && (args.RoutedEvent == InputManager.InputReportEvent)) && (((report = args.Report as RawGenericInputReport) != null) && !e.StagingItem.Input.Handled))
            {
                GenericEvent internalEvent = report.InternalEvent;
                GenericEventArgs input = new GenericEventArgs(this, report.InternalEvent) {
                    RoutedEvent = GenericEvents.GenericStandardEvent
                };
                if (report.Target != null)
                {
                    input.Source = report.Target;
                }
                e.PushInput(input, e.StagingItem);
            }
        }

        public void SetTarget(UIElement target)
        {
            this._focus = target;
        }

        public override UIElement Target
        {
            get
            {
                base.VerifyAccess();
                return this._focus;
            }
        }

        public override InputManager.InputDeviceType DeviceType
        {
            get
            {
                return InputManager.InputDeviceType.Generic;
            }
        }
    }
}

