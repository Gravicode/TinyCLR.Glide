// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Application
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.UI.Controls;
using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Threading;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GHIElectronics.TinyCLR.UI
{
    public class Application : DispatcherObject
    {
        private readonly DisplayController display;
        private OnScreenKeyboard onScreenKeyboard;
        private static bool _isShuttingDown;
        private static bool _appCreatedInThisAppDomain;
        private static Application _appInstance;
        private WindowCollection _appWindowList;
        private WindowCollection _nonAppWindowList;
        private Window _mainWindow;
        private bool _ownDispatcherStarted;
        private bool _appIsShutdown;
        private ShutdownMode _shutdownMode;
        private EventHandler _startupEventHandler;
        private EventHandler _exitEventHandler;
        private static DispatcherOperationCallback _reportInputMethod;
        private static InputManager _inputManager;
        private InputProviderSite _inputProviderSite;
        private static int _stylusMaxX;
        private static int _stylusMaxY;

        public InputProvider InputProvider { get; }

        public Application()
          : this(DisplayController.GetDefault())
        {
        }

        public Application(DisplayController display)
        {
            DisplayController displayController = display;
            if (displayController == null)
                throw new ArgumentException();
            this.display = displayController;
            WindowManager.EnsureInstance(this.display);
            Application._stylusMaxX = (int)this.display.ActiveSettings.Width;
            Application._stylusMaxY = (int)this.display.ActiveSettings.Height;
            lock (typeof(Application.GlobalLock))
            {
                if (Application._appCreatedInThisAppDomain)
                    throw new InvalidOperationException("application is a singleton");
                Application._appInstance = this;
                Application.IsShuttingDown = false;
                Application._appCreatedInThisAppDomain = true;
            }
            this.InitializeForEventSource();
            this.InputProvider = new InputProvider(this);
            Dispatcher.SetFinalDispatcherExceptionHandler(new DispatcherExceptionEventHandler(Application.DefaultContextExceptionHandler));
            this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(this.StartupCallback), (object)null);
        }

        private object StartupCallback(object unused)
        {
            this.OnStartup(new EventArgs());
            return (object)null;
        }

        public void Run()
        {
            this.Run((Window)null);
        }

        public void Run(Window window)
        {
            this.VerifyAccess();
            if (this._appIsShutdown)
                throw new InvalidOperationException("cannot call Run multiple times");
            WindowManager.EnsureInstance(this.display);
            if (window != null)
            {
                if (!window.CheckAccess())
                    throw new ArgumentException("window must be on same dispatcher");
                if (!this.WindowsInternal.HasItem(window))
                    this.WindowsInternal.Add(window);
                if (this.MainWindow == null)
                    this.MainWindow = window;
                if (window.Visibility != Visibility.Visible)
                    this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(this.ShowWindow), (object)window);
            }
            this._ownDispatcherStarted = true;
            Dispatcher.Run();
        }

        public void Shutdown()
        {
            this.VerifyAccess();
            if (Application.IsShuttingDown)
                return;
            Application.IsShuttingDown = true;
            this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(this.ShutdownCallback), (object)null);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void InitializeForEventSource()
        {
            if (Application._inputManager != null)
                return;
            Application._inputManager = InputManager.CurrentInputManager;
            this._inputProviderSite = Application._inputManager.RegisterInputProvider((object)this);
            Application._reportInputMethod = (DispatcherOperationCallback)(o =>
            {
                InputReportArgs inputReportArgs = (InputReportArgs)o;
                return (object)this._inputProviderSite.ReportInput(inputReportArgs.Device, inputReportArgs.Report);
            });
        }

        public bool OnEvent(BaseEvent ev)
        {
            InputReport inputReport = (InputReport)null;
            InputDevice inputDevice = (InputDevice)null;
            TouchEvent touchEvent;
            if ((touchEvent = ev as TouchEvent) != null)
            {
                UIElement target = TouchCapture.Captured;
                if (target != null && touchEvent.EventMessage == (byte)1)
                {
                    int x1 = 0;
                    int y1 = 0;
                    int x2 = touchEvent.Touches[0].X;
                    int y2 = touchEvent.Touches[0].Y;
                    target.PointToScreen(ref x1, ref y1);
                    int width;
                    int height;
                    target.GetRenderSize(out width, out height);
                    if ((x1 > x2 || x2 > x1 + width || (y1 > y2 || y2 > y1 + height)) && (x2 <= (int)this.display.ActiveSettings.Width && y2 <= (int)this.display.ActiveSettings.Height))
                        target = (UIElement)null;
                }
                if (target == null)
                    target = WindowManager.Instance.GetPointerTarget(touchEvent.Touches[0].X, touchEvent.Touches[0].Y);
                if (target != null)
                    Application._inputManager.TouchDevice.SetTarget(target);
                else
                    Application._inputManager.TouchDevice.SetTarget((UIElement)this.MainWindow);
                inputReport = (InputReport)new RawTouchInputReport((PresentationSource)null, touchEvent.Time, touchEvent.EventMessage, touchEvent.Touches);
                inputDevice = (InputDevice)Application._inputManager._touchDevice;
            }
            else
            {
                GenericEvent genericEvent;
                if ((genericEvent = ev as GenericEvent) != null)
                {
                    UIElement target = TouchCapture.Captured ?? WindowManager.Instance.GetPointerTarget(genericEvent.X, genericEvent.Y);
                    if (target != null)
                        Application._inputManager.GenericDevice.SetTarget(target);
                    else
                        Application._inputManager.GenericDevice.SetTarget((UIElement)this.MainWindow);
                    inputReport = (InputReport)new RawGenericInputReport((PresentationSource)null, genericEvent);
                    inputDevice = (InputDevice)Application._inputManager._genericDevice;
                }
            }
            this.Dispatcher.BeginInvoke(Application._reportInputMethod, (object)new InputReportArgs((object)inputDevice, (object)inputReport));
            return true;
        }

        public static Application Current
        {
            get
            {
                lock (typeof(Application.GlobalLock))
                    return Application._appInstance;
            }
        }

        public WindowCollection Windows
        {
            get
            {
                return this.WindowsInternal.Clone();
            }
        }

        public Window MainWindow
        {
            get
            {
                return this._mainWindow;
            }
            set
            {
                this.VerifyAccess();
                if (value == this._mainWindow)
                    return;
                this._mainWindow = value;
            }
        }

        public ShutdownMode ShutdownMode
        {
            get
            {
                return this._shutdownMode;
            }
            set
            {
                this.VerifyAccess();
                if (!Application.IsValidShutdownMode(value))
                    throw new ArgumentOutOfRangeException(nameof(value), "enum");
                if (Application.IsShuttingDown || this._appIsShutdown)
                    throw new InvalidOperationException();
                this._shutdownMode = value;
            }
        }

        public event EventHandler Startup
        {
            add
            {
                this.VerifyAccess();
                this._startupEventHandler += value;
            }
            remove
            {
                this.VerifyAccess();
                this._startupEventHandler -= value;
            }
        }

        public event EventHandler Exit
        {
            add
            {
                this.VerifyAccess();
                this._exitEventHandler += value;
            }
            remove
            {
                this.VerifyAccess();
                this._exitEventHandler -= value;
            }
        }

        protected virtual void OnStartup(EventArgs e)
        {
            EventHandler startupEventHandler = this._startupEventHandler;
            if (startupEventHandler == null)
                return;
            startupEventHandler((object)this, e);
        }

        protected virtual void OnExit(EventArgs e)
        {
            EventHandler exitEventHandler = this._exitEventHandler;
            if (exitEventHandler == null)
                return;
            exitEventHandler((object)this, e);
        }

        internal void ShowOnScreenKeyboardFor(TextBox textBox)
        {
            this.onScreenKeyboard = this.onScreenKeyboard ?? new OnScreenKeyboard();
            this.onScreenKeyboard.ShowFor(textBox);
            this.onScreenKeyboard.Visibility = Visibility.Visible;
            this.onScreenKeyboard.Topmost = true;
            this.onScreenKeyboard.UpdateLayout();
        }

        internal void CloseOnScreenKeyboard()
        {
            this.onScreenKeyboard.Visibility = Visibility.Hidden;
            this.onScreenKeyboard.UpdateLayout();
        }

        internal virtual void DoShutdown()
        {
            lock (typeof(Application.GlobalLock))
                this._appWindowList = (WindowCollection)null;
            EventArgs e = new EventArgs();
            try
            {
                this.OnExit(e);
            }
            finally
            {
                lock (typeof(Application.GlobalLock))
                {
                    Application._appInstance = (Application)null;
                    this._nonAppWindowList = (WindowCollection)null;
                }
                this._mainWindow = (Window)null;
                this._appIsShutdown = true;
            }
        }

        private object ShowWindow(object obj)
        {
            (obj as Window).Visibility = Visibility.Visible;
            return (object)null;
        }

        internal WindowCollection WindowsInternal
        {
            get
            {
                lock (typeof(Application.GlobalLock))
                {
                    if (this._appWindowList == null)
                        this._appWindowList = new WindowCollection();
                    return this._appWindowList;
                }
            }
        }

        internal WindowCollection NonAppWindowsInternal
        {
            get
            {
                lock (typeof(Application.GlobalLock))
                {
                    if (this._nonAppWindowList == null)
                        this._nonAppWindowList = new WindowCollection();
                    return this._nonAppWindowList;
                }
            }
        }

        internal static bool IsShuttingDown
        {
            get
            {
                lock (typeof(Application.GlobalLock))
                    return Application._isShuttingDown;
            }
            set
            {
                lock (typeof(Application.GlobalLock))
                    Application._isShuttingDown = value;
            }
        }

        private object ShutdownCallback(object arg)
        {
            try
            {
                this.DoShutdown();
            }
            finally
            {
                if (this._ownDispatcherStarted)
                    this.Dispatcher.InvokeShutdown();
            }
            return (object)null;
        }

        private static bool DefaultContextExceptionHandler(object sender, Exception e)
        {
            Trace.WriteLine("[Default DispatcherException Handler] Exception caught: " + ((object)e).GetType().FullName);
            return true;
        }

        private static bool IsValidShutdownMode(ShutdownMode value)
        {
            if (value != ShutdownMode.OnExplicitShutdown && value != ShutdownMode.OnLastWindowClose)
                return value == ShutdownMode.OnMainWindowClose;
            return true;
        }

        private class GlobalLock
        {
        }
    }
}
