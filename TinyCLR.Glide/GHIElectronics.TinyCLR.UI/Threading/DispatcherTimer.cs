// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Threading.DispatcherTimer
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using System;
using System.Threading;

namespace GHIElectronics.TinyCLR.UI.Threading
{
    public class DispatcherTimer : IDisposable
    {
        private object _instanceLock = new object();
        private Dispatcher _dispatcher;
        private int _interval;
        private object _tag;
        private bool _isEnabled;
        private Timer _timer;

        public DispatcherTimer()
          : this(Dispatcher.CurrentDispatcher)
        {
        }

        public DispatcherTimer(Dispatcher dispatcher)
        {
            Dispatcher dispatcher1 = dispatcher;
            if (dispatcher1 == null)
                throw new ArgumentNullException(nameof(dispatcher));
            this._dispatcher = dispatcher1;
            this._timer = new Timer(new TimerCallback(this.Callback), (object)null, -1, -1);
        }

        public Dispatcher Dispatcher
        {
            get
            {
                return this._dispatcher;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                lock (this._instanceLock)
                {
                    if (!value && this._isEnabled)
                    {
                        this.Stop();
                    }
                    else
                    {
                        if (!value || this._isEnabled)
                            return;
                        this.Start();
                    }
                }
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return new TimeSpan((long)this._interval * 10000L);
            }
            set
            {
                bool flag = false;
                long ticks = value.Ticks;
                if (ticks < 0L)
                    throw new ArgumentOutOfRangeException(nameof(value), "too small");
                if (ticks > 21474836470000L)
                    throw new ArgumentOutOfRangeException(nameof(value), "too large");
                lock (this._instanceLock)
                {
                    this._interval = (int)(ticks / 10000L);
                    if (this._isEnabled)
                        flag = true;
                }
                if (!flag)
                    return;
                this._timer.Change(this._interval, this._interval);
            }
        }

        public void Start()
        {
            lock (this._instanceLock)
            {
                if (this._isEnabled)
                    return;
                this._isEnabled = true;
                this._timer.Change(this._interval, this._interval);
            }
        }

        public void Stop()
        {
            lock (this._instanceLock)
            {
                if (!this._isEnabled)
                    return;
                this._isEnabled = false;
                this._timer.Change(-1, -1);
            }
        }

        public event GHIElectronics.TinyCLR.UI.EventHandler Tick;

        public object Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
            }
        }

        private void Callback(object state)
        {
            this._dispatcher.BeginInvoke(new DispatcherOperationCallback(this.FireTick), (object)null);
        }

        private object FireTick(object unused)
        {
            // ISSUE: reference to a compiler-generated field
            GHIElectronics.TinyCLR.UI.EventHandler tick = this.Tick;
            if (tick != null)
                tick((object)this, EventArgs.Empty);
            return (object)null;
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
            this._timer.Dispose();
        }
    }
}
