// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Input.ButtonDevice
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using System;

namespace GHIElectronics.TinyCLR.UI.Input
{
    public sealed class ButtonDevice : InputDevice
    {
        private byte[] _buttonState = new byte[2];
        private object _tagNonRedundantActions = new object();
        private object _tagButton = new object();
        private InputManager _inputManager;
        private bool _isActive;
        private UIElement _focus;
        private UIElement _focusRootUIElement;
        private HardwareButton _previousButton;
        private PropertyChangedEventHandler _isEnabledOrVisibleChangedEventHandler;

        internal ButtonDevice(InputManager inputManager)
        {
            this._inputManager = inputManager;
            this._inputManager.InputDeviceEvents[0].PreNotifyInput += new NotifyInputEventHandler(this.PreNotifyInput);
            this._inputManager.InputDeviceEvents[0].PostProcessInput += new ProcessInputEventHandler(this.PostProcessInput);
            this._isEnabledOrVisibleChangedEventHandler = new PropertyChangedEventHandler(this.OnIsEnabledOrVisibleChanged);
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
                return InputManager.InputDeviceType.Button;
            }
        }

        public UIElement Focus(UIElement obj)
        {
            this.VerifyAccess();
            bool flag1 = false;
            bool flag2 = true;
            bool flag3 = true;
            if (obj != null)
            {
                flag2 = obj.IsEnabled;
                flag3 = obj.IsVisible;
                if (flag2 & flag3 & flag1)
                {
                    obj = (UIElement)null;
                    flag2 = true;
                    flag3 = true;
                }
            }
            if (flag2 & flag3 && obj != this._focus)
                this.ChangeFocus(obj, DateTime.UtcNow);
            return this._focus;
        }

        public bool IsButtonDown(HardwareButton button)
        {
            return this.GetButtonState(button) == ButtonState.Down;
        }

        public bool IsButtonUp(HardwareButton button)
        {
            return this.GetButtonState(button) == ButtonState.None;
        }

        public bool IsButtonHeld(HardwareButton button)
        {
            return this.GetButtonState(button) == ButtonState.Held;
        }

        public ButtonState GetButtonState(HardwareButton button)
        {
            if (HardwareButton.LastSystemDefinedButton <= button || button <= HardwareButton.None)
                throw new ArgumentOutOfRangeException(nameof(button), "invalid enum");
            return (ButtonState)((int)this._buttonState[(int)button / 4] >> (int)button % 4 & 3);
        }

        internal void SetButtonState(HardwareButton button, ButtonState state)
        {
            this.VerifyAccess();
            if (HardwareButton.LastSystemDefinedButton <= button || button <= HardwareButton.None)
                throw new ArgumentOutOfRangeException(nameof(button), "invalid enum");
            int index = (int)button / 4;
            int num1 = (int)button % 4;
            byte num2 = (byte)((uint)(byte)((uint)this._buttonState[index] & (uint)~(byte)(3 << num1)) | (uint)(byte)((uint)state << num1));
            this._buttonState[index] = num2;
        }

        private void ChangeFocus(UIElement focus, DateTime timestamp)
        {
            if (focus == this._focus)
                return;
            UIElement focus1 = this._focus;
            this._focus = focus;
            this._focusRootUIElement = focus?.RootUIElement;
            if (focus1 != null)
            {
                focus1.IsEnabledChanged -= this._isEnabledOrVisibleChangedEventHandler;
                focus1.IsVisibleChanged -= this._isEnabledOrVisibleChangedEventHandler;
            }
            if (focus != null)
            {
                focus.IsEnabledChanged += this._isEnabledOrVisibleChangedEventHandler;
                focus.IsVisibleChanged += this._isEnabledOrVisibleChangedEventHandler;
            }
            if (focus1 != null)
            {
                FocusChangedEventArgs changedEventArgs = new FocusChangedEventArgs(this, timestamp, focus1, focus);
                changedEventArgs.RoutedEvent = Buttons.LostFocusEvent;
                changedEventArgs.Source = (object)focus1;
                this._inputManager.ProcessInput((InputEventArgs)changedEventArgs);
            }
            if (focus == null)
                return;
            FocusChangedEventArgs changedEventArgs1 = new FocusChangedEventArgs(this, timestamp, focus1, focus);
            changedEventArgs1.RoutedEvent = Buttons.GotFocusEvent;
            changedEventArgs1.Source = (object)focus;
            this._inputManager.ProcessInput((InputEventArgs)changedEventArgs1);
        }

        private void OnIsEnabledOrVisibleChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Focus(this._focus.Parent);
        }

        private void PreNotifyInput(object sender, NotifyInputEventArgs e)
        {
            RawButtonInputReport buttonInputReport = this.ExtractRawButtonInputReport(e, InputManager.PreviewInputReportEvent);
            if (buttonInputReport != null)
            {
                this.CheckForDisconnectedFocus();
                if ((buttonInputReport.Actions & RawButtonActions.Activate) == RawButtonActions.Activate)
                {
                    for (int index = 0; index < this._buttonState.Length; ++index)
                        this._buttonState[index] = (byte)0;
                    this._isActive = true;
                }
                if ((buttonInputReport.Actions & RawButtonActions.ButtonDown) == RawButtonActions.ButtonDown)
                {
                    RawButtonActions rawButtonActions = this.GetNonRedundantActions(e) | RawButtonActions.ButtonDown;
                    e.StagingItem.SetData(this._tagNonRedundantActions, (object)rawButtonActions);
                    e.StagingItem.SetData(this._tagButton, (object)buttonInputReport.Button);
                    ButtonState buttonState = this.GetButtonState(buttonInputReport.Button);
                    ButtonState state = (buttonState & ButtonState.Down) != ButtonState.Down ? buttonState | ButtonState.Down : ButtonState.Down | ButtonState.Held;
                    this.SetButtonState(buttonInputReport.Button, state);
                    if (this._inputManager != null && this._inputManager.MostRecentInputDevice != this)
                        this._inputManager.MostRecentInputDevice = (InputDevice)this;
                }
                if ((buttonInputReport.Actions & RawButtonActions.ButtonUp) == RawButtonActions.ButtonUp)
                {
                    RawButtonActions rawButtonActions = this.GetNonRedundantActions(e) | RawButtonActions.ButtonUp;
                    e.StagingItem.SetData(this._tagNonRedundantActions, (object)rawButtonActions);
                    e.StagingItem.SetData(this._tagButton, (object)buttonInputReport.Button);
                    ButtonState buttonState = this.GetButtonState(buttonInputReport.Button);
                    ButtonState state = (buttonState & ButtonState.Down) != ButtonState.Down ? buttonState | ButtonState.Held : buttonState & ButtonState.Held;
                    this.SetButtonState(buttonInputReport.Button, state);
                    if (this._inputManager != null && this._inputManager.MostRecentInputDevice != this)
                        this._inputManager.MostRecentInputDevice = (InputDevice)this;
                }
            }
            if (e.StagingItem.Input.RoutedEvent == Buttons.PreviewButtonDownEvent)
            {
                this.CheckForDisconnectedFocus();
                ButtonEventArgs input = (ButtonEventArgs)e.StagingItem.Input;
                if (this._previousButton == input.Button)
                {
                    input._isRepeat = true;
                }
                else
                {
                    this._previousButton = input.Button;
                    input._isRepeat = false;
                }
            }
            else
            {
                if (e.StagingItem.Input.RoutedEvent != Buttons.PreviewButtonUpEvent)
                    return;
                this.CheckForDisconnectedFocus();
                ((ButtonEventArgs)e.StagingItem.Input)._isRepeat = false;
                this._previousButton = HardwareButton.None;
            }
        }

        private void PostProcessInput(object sender, ProcessInputEventArgs e)
        {
            if (e.StagingItem.Input.RoutedEvent == Buttons.PreviewButtonDownEvent)
            {
                this.CheckForDisconnectedFocus();
                if (!e.StagingItem.Input.Handled)
                {
                    ButtonEventArgs input = (ButtonEventArgs)e.StagingItem.Input;
                    ButtonEventArgs buttonEventArgs1 = new ButtonEventArgs(this, input.InputSource, input.Timestamp, input.Button);
                    buttonEventArgs1._isRepeat = input.IsRepeat;
                    buttonEventArgs1.RoutedEvent = Buttons.ButtonDownEvent;
                    ButtonEventArgs buttonEventArgs2 = buttonEventArgs1;
                    e.PushInput((InputEventArgs)buttonEventArgs2, e.StagingItem);
                }
            }
            if (e.StagingItem.Input.RoutedEvent == Buttons.PreviewButtonUpEvent)
            {
                this.CheckForDisconnectedFocus();
                if (!e.StagingItem.Input.Handled)
                {
                    ButtonEventArgs input = (ButtonEventArgs)e.StagingItem.Input;
                    ButtonEventArgs buttonEventArgs1 = new ButtonEventArgs(this, input.InputSource, input.Timestamp, input.Button);
                    buttonEventArgs1.RoutedEvent = Buttons.ButtonUpEvent;
                    ButtonEventArgs buttonEventArgs2 = buttonEventArgs1;
                    e.PushInput((InputEventArgs)buttonEventArgs2, e.StagingItem);
                }
            }
            RawButtonInputReport buttonInputReport = this.ExtractRawButtonInputReport((NotifyInputEventArgs)e, InputManager.InputReportEvent);
            if (buttonInputReport == null)
                return;
            this.CheckForDisconnectedFocus();
            if (!e.StagingItem.Input.Handled)
            {
                int redundantActions = (int)this.GetNonRedundantActions((NotifyInputEventArgs)e);
                if ((redundantActions & 1) == 1)
                {
                    HardwareButton data = (HardwareButton)e.StagingItem.GetData(this._tagButton);
                    if (data != HardwareButton.None)
                    {
                        ButtonEventArgs buttonEventArgs1 = new ButtonEventArgs(this, buttonInputReport.InputSource, buttonInputReport.Timestamp, data);
                        buttonEventArgs1.RoutedEvent = Buttons.PreviewButtonDownEvent;
                        ButtonEventArgs buttonEventArgs2 = buttonEventArgs1;
                        e.PushInput((InputEventArgs)buttonEventArgs2, e.StagingItem);
                    }
                }
                if ((redundantActions & 2) == 2)
                {
                    HardwareButton data = (HardwareButton)e.StagingItem.GetData(this._tagButton);
                    if (data != HardwareButton.None)
                    {
                        ButtonEventArgs buttonEventArgs1 = new ButtonEventArgs(this, buttonInputReport.InputSource, buttonInputReport.Timestamp, data);
                        buttonEventArgs1.RoutedEvent = Buttons.PreviewButtonUpEvent;
                        ButtonEventArgs buttonEventArgs2 = buttonEventArgs1;
                        e.PushInput((InputEventArgs)buttonEventArgs2, e.StagingItem);
                    }
                }
            }
            if ((buttonInputReport.Actions & RawButtonActions.Deactivate) != RawButtonActions.Deactivate || !this._isActive)
                return;
            this._isActive = false;
            this.ChangeFocus((UIElement)null, e.StagingItem.Input.Timestamp);
        }

        private RawButtonActions GetNonRedundantActions(NotifyInputEventArgs e)
        {
            object data = e.StagingItem.GetData(this._tagNonRedundantActions);
            return data == null ? (RawButtonActions)0 : (RawButtonActions)data;
        }

        private bool CheckForDisconnectedFocus()
        {
            bool flag = false;
            if (this._focus != null && this._focus.RootUIElement != this._focusRootUIElement)
            {
                flag = true;
                this.Focus(this._focusRootUIElement);
            }
            return flag;
        }

        private RawButtonInputReport ExtractRawButtonInputReport(NotifyInputEventArgs e, RoutedEvent Event)
        {
            RawButtonInputReport buttonInputReport = (RawButtonInputReport)null;
            InputReportEventArgs input;
            if ((input = e.StagingItem.Input as InputReportEventArgs) != null && input.Report is RawButtonInputReport && input.RoutedEvent == Event)
                buttonInputReport = (RawButtonInputReport)input.Report;
            return buttonInputReport;
        }
    }
}
