// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Threading.DispatcherOperation
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using System;
using System.Threading;

namespace GHIElectronics.TinyCLR.UI.Threading
{
    public sealed class DispatcherOperation
    {
        private Dispatcher _dispatcher;
        internal DispatcherOperationCallback _method;
        internal object _args;
        internal object _result;
        internal DispatcherOperationStatus _status;

        internal DispatcherOperation(Dispatcher dispatcher, DispatcherOperationCallback method, object args)
        {
            this._dispatcher = dispatcher;
            this._method = method;
            this._args = args;
        }

        public Dispatcher Dispatcher
        {
            get
            {
                return this._dispatcher;
            }
        }

        public DispatcherOperationStatus Status
        {
            get
            {
                return this._status;
            }
            internal set
            {
                this._status = value;
            }
        }

        public DispatcherOperationStatus Wait()
        {
            return this.Wait(new TimeSpan(-10000L));
        }

        public DispatcherOperationStatus Wait(TimeSpan timeout)
        {
            if ((this._status == DispatcherOperationStatus.Pending || this._status == DispatcherOperationStatus.Executing) && timeout.Ticks != 0L)
            {
                if (this._dispatcher.Thread == Thread.CurrentThread)
                {
                    if (this._status == DispatcherOperationStatus.Executing)
                        throw new InvalidOperationException();
                    Dispatcher.PushFrame((DispatcherFrame)new DispatcherOperation.DispatcherOperationFrame(this, timeout));
                }
                else
                    new DispatcherOperation.DispatcherOperationEvent(this, timeout).WaitOne();
            }
            return this._status;
        }

        public bool Abort()
        {
            int num = this._dispatcher.Abort(this) ? 1 : 0;
            if (num == 0)
                return num != 0;
            this._status = DispatcherOperationStatus.Aborted;
            // ISSUE: reference to a compiler-generated field
            GHIElectronics.TinyCLR.UI.EventHandler aborted = this.Aborted;
            if (aborted == null)
                return num != 0;
            aborted((object)this, EventArgs.Empty);
            return num != 0;
        }

        public object Result
        {
            get
            {
                return this._result;
            }
        }

        public event GHIElectronics.TinyCLR.UI.EventHandler Aborted;

        public event GHIElectronics.TinyCLR.UI.EventHandler Completed;

        internal void OnCompleted()
        {
            // ISSUE: reference to a compiler-generated field
            GHIElectronics.TinyCLR.UI.EventHandler completed = this.Completed;
            if (completed == null)
                return;
            completed((object)this, EventArgs.Empty);
        }

        private class DispatcherOperationFrame : DispatcherFrame, IDisposable
        {
            private DispatcherOperation _operation;
            private Timer _waitTimer;

            public DispatcherOperationFrame(DispatcherOperation op, TimeSpan timeout)
              : base(false)
            {
                this._operation = op;
                this._operation.Aborted += new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
                this._operation.Completed += new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
                if (timeout.Ticks > 0L)
                    this._waitTimer = new Timer(new TimerCallback(this.OnTimeout), (object)null, timeout, new TimeSpan(-10000L));
                if (this._operation._status == DispatcherOperationStatus.Pending)
                    return;
                this.Exit();
            }

            private void OnCompletedOrAborted(object sender, EventArgs e)
            {
                this.Exit();
            }

            private void OnTimeout(object arg)
            {
                this.Exit();
            }

            private void Exit()
            {
                this.Continue = false;
                if (this._waitTimer != null)
                    this._waitTimer.Dispose();
                this._operation.Aborted -= new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
                this._operation.Completed -= new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
            }

            public virtual void Close()
            {
                this.Dispose();
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize((object)this);
            }

            protected virtual void Dispose(bool disposing)
            {
                this._waitTimer.Dispose();
            }
        }

        private class DispatcherOperationEvent : IDisposable
        {
            private DispatcherOperation _operation;
            private TimeSpan _timeout;
            private AutoResetEvent _event;
            private Timer _waitTimer;

            public DispatcherOperationEvent(DispatcherOperation op, TimeSpan timeout)
            {
                this._operation = op;
                this._timeout = timeout;
                this._event = new AutoResetEvent(false);
                this._operation.Aborted += new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
                this._operation.Completed += new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
                if (this._operation._status == DispatcherOperationStatus.Pending || this._operation._status == DispatcherOperationStatus.Executing)
                    return;
                this._event.Set();
            }

            private void OnCompletedOrAborted(object sender, EventArgs e)
            {
                this._event.Set();
            }

            public void WaitOne()
            {
                this._waitTimer = new Timer(new TimerCallback(this.OnTimeout), (object)null, this._timeout, new TimeSpan(-10000L));
                this._event.WaitOne();
                this._waitTimer.Dispose();
                this._operation.Aborted -= new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
                this._operation.Completed -= new GHIElectronics.TinyCLR.UI.EventHandler(this.OnCompletedOrAborted);
            }

            private void OnTimeout(object arg)
            {
                this._event.Set();
            }

            public virtual void Close()
            {
                this.Dispose();
            }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize((object)this);
            }

            protected virtual void Dispose(bool disposing)
            {
                this._waitTimer.Dispose();
            }
        }
    }
}
