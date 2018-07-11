// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Input.InputManager
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Threading;
using System;
using System.Collections;

namespace GHIElectronics.TinyCLR.UI.Input
{
    public sealed class InputManager : DispatcherObject
    {
        public static readonly RoutedEvent PreviewInputReportEvent = new RoutedEvent("PreviewInputReport", RoutingStrategy.Tunnel, typeof(InputReportEventHandler));
        public static readonly RoutedEvent InputReportEvent = new RoutedEvent("InputReport", RoutingStrategy.Bubble, typeof(InputReportEventHandler));
        private ArrayList _inputProviders = new ArrayList();
        private ArrayList _inputProviderSites = new ArrayList();
        private DispatcherOperationCallback _continueProcessingStagingAreaCallback;
        private ArrayList _frameStagingArea;
        public InputManager.DeviceEvents[] InputDeviceEvents;
        internal Stack _currentStagingStack;
        internal Queue _stagingArea;
        private InputDevice _mostRecentInputDevice;
        internal ButtonDevice _buttonDevice;
        internal TouchDevice _touchDevice;
        internal GenericDevice _genericDevice;

        public static InputManager CurrentInputManager
        {
            get
            {
                Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
                if (currentDispatcher == null)
                    throw new InvalidOperationException("no dispatcher");
                if (currentDispatcher._inputManager == null)
                {
                    lock (typeof(InputManager.GlobalLock))
                    {
                        if (currentDispatcher._inputManager == null)
                            currentDispatcher._inputManager = new InputManager();
                    }
                }
                return currentDispatcher._inputManager;
            }
        }

        private InputManager()
        {
            this._stagingArea = new Queue();
            this._currentStagingStack = new Stack();
            this._frameStagingArea = new ArrayList();
            this.InputDeviceEvents = new InputManager.DeviceEvents[3];
            for (int index = 0; index < 3; ++index)
                this.InputDeviceEvents[index] = new InputManager.DeviceEvents();
            this._continueProcessingStagingAreaCallback = new DispatcherOperationCallback(this.ProcessStagingArea);
            this._buttonDevice = new ButtonDevice(this);
            this._touchDevice = new TouchDevice(this);
            this._genericDevice = new GenericDevice(this);
        }

        public ButtonDevice ButtonDevice
        {
            get
            {
                return this._buttonDevice;
            }
        }

        public TouchDevice TouchDevice
        {
            get
            {
                return this._touchDevice;
            }
        }

        public GenericDevice GenericDevice
        {
            get
            {
                return this._genericDevice;
            }
        }

        public InputProviderSite RegisterInputProvider(object inputProvider)
        {
            this.VerifyAccess();
            InputProviderSite inputProviderSite = new InputProviderSite(this, inputProvider);
            int index = this._inputProviders.IndexOf(inputProvider);
            if (index < 0)
            {
                this._inputProviders.Add(inputProvider);
                this._inputProviderSites.Add((object)inputProviderSite);
            }
            else
            {
                this._inputProviders[index] = inputProvider;
                this._inputProviderSites[index] = (object)inputProviderSite;
            }
            return inputProviderSite;
        }

        internal void UnregisterInputProvider(object inputProvider)
        {
            int index = this._inputProviders.IndexOf(inputProvider);
            if (index < 0)
                return;
            this._inputProviders.RemoveAt(index);
            this._inputProviderSites.RemoveAt(index);
        }

        public ICollection InputProviders
        {
            get
            {
                this.VerifyAccess();
                return (ICollection)this._inputProviders;
            }
        }

        public InputDevice MostRecentInputDevice
        {
            get
            {
                this.VerifyAccess();
                return this._mostRecentInputDevice;
            }
            set
            {
                this.VerifyAccess();
                this._mostRecentInputDevice = value;
            }
        }

        public bool ProcessInput(InputEventArgs input)
        {
            this.VerifyAccess();
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            Stack stack = new Stack();
            stack.Push((object)new StagingAreaInputItem(input, (StagingAreaInputItem)null));
            this._stagingArea.Enqueue((object)stack);
            DispatcherFrame currentFrame = this.Dispatcher.CurrentFrame;
            if (currentFrame != null && !this._frameStagingArea.Contains((object)currentFrame))
            {
                this._frameStagingArea.Add((object)currentFrame);
                this.Dispatcher.BeginInvoke(this._continueProcessingStagingAreaCallback, (object)currentFrame);
            }
            return true;
        }

        private object ProcessStagingArea(object frame)
        {
            bool flag1 = false;
            try
            {
                label_21:
                if (this._stagingArea.Count > 0)
                {
                    this._currentStagingStack = this._stagingArea.Dequeue() as Stack;
                    do
                    {
                        StagingAreaInputItem stagingAreaInputItem = (StagingAreaInputItem)this._currentStagingStack.Pop();
                        bool flag2 = false;
                        int deviceType = (int)stagingAreaInputItem.Input._inputDevice.DeviceType;
                        if (this.InputDeviceEvents[deviceType]._preProcessInput != null)
                        {
                            PreProcessInputEventArgs processInputEventArgs;
                            this.InputDeviceEvents[deviceType]._preProcessInput((object)this, processInputEventArgs = new PreProcessInputEventArgs(stagingAreaInputItem));
                            flag2 = processInputEventArgs._canceled;
                        }
                        if (!flag2)
                        {
                            NotifyInputEventHandler preNotifyInput = this.InputDeviceEvents[deviceType]._preNotifyInput;
                            if (preNotifyInput != null)
                                preNotifyInput((object)this, new NotifyInputEventArgs(stagingAreaInputItem));
                            InputEventArgs input1 = stagingAreaInputItem.Input;
                            UIElement uiElement = input1._source as UIElement;
                            if (uiElement == null && input1._inputDevice != null)
                                uiElement = input1._inputDevice.Target;
                            uiElement?.RaiseEvent((RoutedEventArgs)input1);
                            NotifyInputEventHandler postNotifyInput = this.InputDeviceEvents[deviceType]._postNotifyInput;
                            if (postNotifyInput != null)
                                postNotifyInput((object)this, new NotifyInputEventArgs(stagingAreaInputItem));
                            ProcessInputEventHandler postProcessInput = this.InputDeviceEvents[deviceType]._postProcessInput;
                            if (postProcessInput != null)
                                postProcessInput((object)this, new ProcessInputEventArgs(stagingAreaInputItem));
                            if (stagingAreaInputItem.Input._routedEvent == InputManager.PreviewInputReportEvent && !stagingAreaInputItem.Input.Handled)
                            {
                                InputReportEventArgs input2 = (InputReportEventArgs)stagingAreaInputItem.Input;
                                InputReportEventArgs inputReportEventArgs = new InputReportEventArgs(input2.Device, input2.Report);
                                inputReportEventArgs.RoutedEvent = InputManager.InputReportEvent;
                                this._currentStagingStack.Push((object)new StagingAreaInputItem((InputEventArgs)inputReportEventArgs, stagingAreaInputItem));
                            }
                            if (input1.Handled)
                                flag1 = true;
                        }
                    }
                    while (this._currentStagingStack.Count > 0);
                    goto label_21;
                }
            }
            finally
            {
                if (this._stagingArea.Count > 0)
                    this.Dispatcher.BeginInvoke(this._continueProcessingStagingAreaCallback, (object)this.Dispatcher.CurrentFrame);
                this._frameStagingArea.Remove(frame);
            }
            return (object)flag1;
        }

        public enum InputDeviceType
        {
            Button,
            Touch,
            Generic,
            Last,
        }

        public class DeviceEvents : DispatcherObject
        {
            internal PreProcessInputEventHandler _preProcessInput;
            internal NotifyInputEventHandler _preNotifyInput;
            internal NotifyInputEventHandler _postNotifyInput;
            internal ProcessInputEventHandler _postProcessInput;

            public event PreProcessInputEventHandler PreProcessInput
            {
                add
                {
                    this.VerifyAccess();
                    this._preProcessInput = value + this._preProcessInput;
                }
                remove
                {
                    this.VerifyAccess();
                    this._preProcessInput -= value;
                }
            }

            public event NotifyInputEventHandler PreNotifyInput
            {
                add
                {
                    this.VerifyAccess();
                    this._preNotifyInput = value + this._preNotifyInput;
                }
                remove
                {
                    this.VerifyAccess();
                    this._preNotifyInput -= value;
                }
            }

            public event NotifyInputEventHandler PostNotifyInput
            {
                add
                {
                    this.VerifyAccess();
                    this._postNotifyInput = value + this._postNotifyInput;
                }
                remove
                {
                    this.VerifyAccess();
                    this._postNotifyInput -= value;
                }
            }

            public event ProcessInputEventHandler PostProcessInput
            {
                add
                {
                    this.VerifyAccess();
                    this._postProcessInput = value + this._postProcessInput;
                }
                remove
                {
                    this.VerifyAccess();
                    this._postProcessInput -= value;
                }
            }
        }

        private class GlobalLock
        {
        }
    }
}
