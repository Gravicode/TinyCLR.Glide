namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public sealed class TouchDevice : InputDevice
    {
        private InputManager _inputManager;
        private UIElement _focus;

        internal TouchDevice(InputManager inputManager)
        {
            this._inputManager = inputManager;
            this._inputManager.InputDeviceEvents[1].PostProcessInput += new ProcessInputEventHandler(this.PostProcessInput);
        }

        private void PostProcessInput(object sender, ProcessInputEventArgs e)
        {
            InputReportEventArgs args;
            RawTouchInputReport report;
            if ((!e.StagingItem.Input.Handled && (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)) && (((args = e.StagingItem.Input as InputReportEventArgs) != null) && ((report = args.Report as RawTouchInputReport) != null)))
            {
                TouchEventArgs input = new TouchEventArgs(this, report.Timestamp, report.Touches);
                UIElement target = report.Target;
                if (report.EventMessage == 1)
                {
                    input.RoutedEvent = TouchEvents.TouchDownEvent;
                }
                else if (report.EventMessage == 2)
                {
                    input.RoutedEvent = TouchEvents.TouchUpEvent;
                }
                else
                {
                    if (report.EventMessage != 3)
                    {
                        throw new Exception("Unknown touch event.");
                    }
                    input.RoutedEvent = TouchEvents.TouchMoveEvent;
                }
                input.Source = target ?? this._focus;
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
                return this._focus;
            }
        }

        public override InputManager.InputDeviceType DeviceType
        {
            get
            {
                return InputManager.InputDeviceType.Touch;
            }
        }
    }
}

