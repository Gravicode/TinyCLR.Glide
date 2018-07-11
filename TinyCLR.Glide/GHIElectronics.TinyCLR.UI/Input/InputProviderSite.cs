namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class InputProviderSite : IDisposable
    {
        private bool _isDisposed;
        private GHIElectronics.TinyCLR.UI.Input.InputManager _inputManager;
        private object _inputProvider;

        internal InputProviderSite(GHIElectronics.TinyCLR.UI.Input.InputManager inputManager, object inputProvider)
        {
            this._inputManager = inputManager ?? throw new ArgumentNullException("inputManager");
            this._inputProvider = inputProvider;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                this._isDisposed = true;
                if ((this._inputManager != null) && (this._inputProvider != null))
                {
                    this._inputManager.UnregisterInputProvider(this._inputProvider);
                }
                this._inputManager = null;
                this._inputProvider = null;
            }
        }

        public bool ReportInput(InputDevice device, InputReport inputReport)
        {
            if (this._isDisposed)
            {
                throw new InvalidOperationException();
            }
            bool flag = false;
            InputReportEventArgs input = new InputReportEventArgs(device, inputReport) {
                RoutedEvent = GHIElectronics.TinyCLR.UI.Input.InputManager.PreviewInputReportEvent
            };
            if (this._inputManager != null)
            {
                flag = this._inputManager.ProcessInput(input);
            }
            return flag;
        }

        public GHIElectronics.TinyCLR.UI.Input.InputManager InputManager
        {
            get
            {
                return this._inputManager;
            }
        }

        public bool IsDisposed
        {
            get
            {
                return this._isDisposed;
            }
        }
    }
}

