namespace GHIElectronics.TinyCLR.UI.Threading
{
    using System;

    public class DispatcherFrame
    {
        private bool _exitWhenRequested;
        private bool _continue;
        private Dispatcher _dispatcher;

        public DispatcherFrame() : this(true)
        {
        }

        public DispatcherFrame(bool exitWhenRequested)
        {
            this._exitWhenRequested = exitWhenRequested;
            this._continue = true;
            this._dispatcher = Dispatcher.CurrentDispatcher;
        }

        public bool Continue
        {
            get
            {
                bool flag = this._continue;
                if ((flag && this._exitWhenRequested) && this._dispatcher._hasShutdownStarted)
                {
                    flag = false;
                }
                return flag;
            }
            set
            {
                this._continue = value;
                this._dispatcher.QueryContinueFrame();
            }
        }
    }
}

