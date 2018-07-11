namespace GHIElectronics.TinyCLR.UI.Media
{
    using GHIElectronics.TinyCLR.Devices.Display;
    using GHIElectronics.TinyCLR.UI;
    using GHIElectronics.TinyCLR.UI.Threading;
    using System;
    using System.Collections;
    using System.Drawing;

    internal class MediaContext : DispatcherObject, IDisposable
    {
        private int _screenW;
        private int _screenH;
        private int _dirtyX0;
        private int _dirtyY0;
        private int _dirtyX1;
        private int _dirtyY1;
        private DispatcherOperation _currentRenderOp;
        private DispatcherOperationCallback _renderMessage;
        private bool _isRendering;
        private ArrayList _invokeOnRenderCallbacks;
        private UIElement _target;
        private GHIElectronics.TinyCLR.UI.Bitmap _screen;

        internal MediaContext()
        {
            this._renderMessage = new DispatcherOperationCallback(this.RenderMessageHandler);
            DisplayController displayController = WindowManager.Instance.DisplayController;
            this._target = WindowManager.Instance;
            this._screen = new GHIElectronics.TinyCLR.UI.Bitmap(Graphics.FromHdc(displayController.Hdc));
            this._screenW = (int) displayController.ActiveSettings.Width;
            this._screenH = (int) displayController.ActiveSettings.Height;
            this._dirtyX0 = (int) displayController.ActiveSettings.Width;
            this._dirtyY0 = (int) displayController.ActiveSettings.Height;
        }

        internal void AddDirtyArea(int x, int y, int w, int h)
        {
            if (x < 0)
            {
                x = 0;
            }
            if ((x + w) > this._screenW)
            {
                w = this._screenW - x;
            }
            if (w > 0)
            {
                if (y < 0)
                {
                    y = 0;
                }
                if ((y + h) > this._screenH)
                {
                    h = this._screenH - y;
                }
                if (h > 0)
                {
                    int num = x + w;
                    int num2 = y + h;
                    if (x < this._dirtyX0)
                    {
                        this._dirtyX0 = x;
                    }
                    if (y < this._dirtyY0)
                    {
                        this._dirtyY0 = y;
                    }
                    if (num > this._dirtyX1)
                    {
                        this._dirtyX1 = num;
                    }
                    if (num2 > this._dirtyY1)
                    {
                        this._dirtyY1 = num2;
                    }
                }
            }
        }

        internal void BeginInvokeOnRender(DispatcherOperationCallback callback, object arg)
        {
            if (this._invokeOnRenderCallbacks == null)
            {
                MediaContext context = this;
                lock (context)
                {
                    if (this._invokeOnRenderCallbacks == null)
                    {
                        this._invokeOnRenderCallbacks = new ArrayList();
                    }
                }
            }
            ArrayList list = this._invokeOnRenderCallbacks;
            lock (list)
            {
                this._invokeOnRenderCallbacks.Add(new InvokeOnRenderCallback(callback, arg));
            }
            this.PostRender();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this._screen.Dispose();
        }

        internal static MediaContext From(Dispatcher dispatcher)
        {
            MediaContext context = dispatcher._mediaContext;
            if (context == null)
            {
                Type type = typeof(GlobalLock);
                lock (type)
                {
                    context = dispatcher._mediaContext;
                    if (context == null)
                    {
                        context = new MediaContext();
                        dispatcher._mediaContext = context;
                    }
                }
            }
            return context;
        }

        internal void PostRender()
        {
            base.VerifyAccess();
            if (!this._isRendering && (this._currentRenderOp == null))
            {
                this._currentRenderOp = base.Dispatcher.BeginInvoke(this._renderMessage, null);
            }
        }

        internal object RenderMessageHandler(object arg)
        {
            try
            {
                this._isRendering = true;
                if (this._invokeOnRenderCallbacks != null)
                {
                    int num5 = 0;
                    for (int i = this._invokeOnRenderCallbacks.Count; i > 0; i = this._invokeOnRenderCallbacks.Count)
                    {
                        InvokeOnRenderCallback[] callbackArray;
                        num5++;
                        if (num5 > 0x99)
                        {
                            throw new InvalidOperationException("infinite loop");
                        }
                        ArrayList list = this._invokeOnRenderCallbacks;
                        lock (list)
                        {
                            i = this._invokeOnRenderCallbacks.Count;
                            callbackArray = new InvokeOnRenderCallback[i];
                            this._invokeOnRenderCallbacks.CopyTo(callbackArray);
                            this._invokeOnRenderCallbacks.Clear();
                        }
                        for (int j = 0; j < i; j++)
                        {
                            callbackArray[j].DoWork();
                        }
                    }
                }
                DrawingContext dc = new DrawingContext(this._screen);
                int x = this._dirtyX0;
                int y = this._dirtyY0;
                int width = this._dirtyX1 - this._dirtyX0;
                int height = this._dirtyY1 - this._dirtyY0;
                this._dirtyX0 = this._screenW;
                this._dirtyY0 = this._screenH;
                this._dirtyX1 = this._dirtyY1 = 0;
                try
                {
                    if ((width > 0) && (height > 0))
                    {
                        dc.PushClippingRectangle(x, y, width, height);
                        this._target.RenderRecursive(dc);
                        dc.PopClippingRectangle();
                    }
                }
                finally
                {
                    dc.Close();
                    if ((width > 0) && (height > 0))
                    {
                        this._screen.Flush(x, y, width, height);
                    }
                }
            }
            finally
            {
                this._currentRenderOp = null;
                this._isRendering = false;
            }
            return null;
        }

        private class GlobalLock
        {
        }

        private class InvokeOnRenderCallback
        {
            private DispatcherOperationCallback _callback;
            private object _arg;

            public InvokeOnRenderCallback(DispatcherOperationCallback callback, object arg)
            {
                this._callback = callback;
                this._arg = arg;
            }

            public void DoWork()
            {
                this._callback(this._arg);
            }
        }
    }
}

