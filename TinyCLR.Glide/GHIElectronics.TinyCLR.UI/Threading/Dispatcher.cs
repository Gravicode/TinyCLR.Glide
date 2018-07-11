// Decompiled with JetBrains decompiler
// Type: GHIElectronics.TinyCLR.UI.Threading.Dispatcher
// Assembly: GHIElectronics.TinyCLR.UI, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C2EFF46-96E6-45B9-8219-C947515ADF77
// Assembly location: C:\Users\mifma\source\repos\TinyCLRApplication1\TinyCLRApplication1\bin\Debug\GHIElectronics.TinyCLR.UI.dll

using GHIElectronics.TinyCLR.UI.Input;
using GHIElectronics.TinyCLR.UI.Media;
using System;
using System.Collections;
using System.Threading;

namespace GHIElectronics.TinyCLR.UI.Threading
{
    public sealed class Dispatcher
    {
        private static Hashtable _dispatchers = new Hashtable();
        private DispatcherFrame _currentFrame;
        private int _frameDepth;
        internal bool _hasShutdownStarted;
        private bool _hasShutdownFinished;
        private Queue _queue;
        private AutoResetEvent _event;
        private object _instanceLock;
        private static Dispatcher _possibleDispatcher;
        private Thread _thread;
        internal DispatcherExceptionEventHandler _finalExceptionHandler;
        internal LayoutManager _layoutManager;
        internal InputManager _inputManager;
        internal MediaContext _mediaContext;

        public static Dispatcher CurrentDispatcher
        {
            get
            {
                Dispatcher dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
                if (dispatcher == null)
                {
                    lock (typeof(Dispatcher.GlobalLock))
                        dispatcher = Dispatcher.FromThread(Thread.CurrentThread) ?? new Dispatcher();
                }
                return dispatcher;
            }
        }

        public static Dispatcher FromThread(Thread thread)
        {
            Dispatcher dispatcher1 = Dispatcher._possibleDispatcher;
            if (dispatcher1 == null || dispatcher1._thread != thread)
            {
                dispatcher1 = (Dispatcher)null;
                WeakReference dispatcher2 = (WeakReference)Dispatcher._dispatchers[(object)thread.ManagedThreadId];
                if (dispatcher2 != null)
                {
                    dispatcher1 = dispatcher2.Target as Dispatcher;
                    if (dispatcher1 != null)
                    {
                        if (dispatcher1._thread == thread)
                            Dispatcher._possibleDispatcher = dispatcher1;
                        else
                            Dispatcher._dispatchers.Remove((object)thread.ManagedThreadId);
                    }
                }
            }
            return dispatcher1;
        }

        private Dispatcher()
        {
            this._thread = Thread.CurrentThread;
            this._queue = new Queue();
            this._event = new AutoResetEvent(false);
            this._instanceLock = new object();
            Dispatcher._dispatchers[(object)this._thread.ManagedThreadId] = (object)new WeakReference((object)this);
            if (Dispatcher._possibleDispatcher != null)
                return;
            Dispatcher._possibleDispatcher = this;
        }

        public bool CheckAccess()
        {
            return this._thread == Thread.CurrentThread;
        }

        public void VerifyAccess()
        {
            if (this._thread != Thread.CurrentThread)
                throw new InvalidOperationException();
        }

        public Thread Thread
        {
            get
            {
                return this._thread;
            }
        }

        internal bool Abort(DispatcherOperation operation)
        {
            bool flag = false;
            lock (this._instanceLock)
            {
                if (operation.Status == DispatcherOperationStatus.Pending)
                {
                    operation.Status = DispatcherOperationStatus.Aborted;
                    flag = true;
                }
            }
            return flag;
        }

        public void InvokeShutdown()
        {
            this.VerifyAccess();
            if (this._hasShutdownFinished)
                throw new InvalidOperationException();
            try
            {
                if (this._hasShutdownStarted)
                    return;
                // ISSUE: reference to a compiler-generated field
                GHIElectronics.TinyCLR.UI.EventHandler shutdownStarted = this.ShutdownStarted;
                if (shutdownStarted != null)
                    shutdownStarted((object)this, EventArgs.Empty);
                this._hasShutdownStarted = true;
                if (this._frameDepth <= 0)
                    this.ShutdownImpl();
                Dispatcher._dispatchers.Remove((object)this._thread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                if (this._finalExceptionHandler != null && this._finalExceptionHandler((object)this, ex))
                    return;
                throw;
            }
        }

        private void ShutdownImpl()
        {
            // ISSUE: reference to a compiler-generated field
            GHIElectronics.TinyCLR.UI.EventHandler shutdownFinished = this.ShutdownFinished;
            if (shutdownFinished != null)
                shutdownFinished((object)this, EventArgs.Empty);
            this._hasShutdownFinished = true;
            lock (this._instanceLock)
            {
                while (this._queue.Count > 0)
                    ((DispatcherOperation)_queue.Dequeue()).Abort();
            }
        }

        internal void QueryContinueFrame()
        {
            this._event.Set();
        }

        public bool HasShutdownStarted
        {
            get
            {
                return this._hasShutdownStarted;
            }
        }

        public bool HasShutdownFinished
        {
            get
            {
                return this._hasShutdownFinished;
            }
        }

        public event GHIElectronics.TinyCLR.UI.EventHandler ShutdownStarted;

        public event GHIElectronics.TinyCLR.UI.EventHandler ShutdownFinished;

        public static void Run()
        {
            Dispatcher.PushFrame(new DispatcherFrame());
        }

        public static void PushFrame(DispatcherFrame frame)
        {
            if (frame == null)
                throw new ArgumentNullException();
            Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
            if (currentDispatcher._hasShutdownFinished)
                throw new InvalidOperationException();
            currentDispatcher.PushFrameImpl(frame);
        }

        internal DispatcherFrame CurrentFrame
        {
            get
            {
                return this._currentFrame;
            }
        }

        private void PushFrameImpl(DispatcherFrame frame)
        {
            DispatcherFrame currentFrame = this._currentFrame;
            ++this._frameDepth;
            try
            {
                this._currentFrame = frame;
                while (frame.Continue)
                {
                    DispatcherOperation dispatcherOperation = (DispatcherOperation)null;
                    bool flag = false;
                    if (this._queue.Count > 0)
                    {
                        dispatcherOperation = (DispatcherOperation)this._queue.Dequeue();
                        flag = dispatcherOperation.Status == DispatcherOperationStatus.Aborted;
                    }
                    if (dispatcherOperation != null)
                    {
                        if (!flag)
                        {
                            dispatcherOperation._status = DispatcherOperationStatus.Executing;
                            dispatcherOperation._result = (object)null;
                            try
                            {
                                dispatcherOperation._result = dispatcherOperation._method(dispatcherOperation._args);
                            }
                            catch (Exception ex)
                            {
                                if (this._finalExceptionHandler != null)
                                {
                                    if (this._finalExceptionHandler((object)dispatcherOperation, ex))
                                        goto label_11;
                                }
                                throw;
                            }
                            label_11:
                            dispatcherOperation._status = DispatcherOperationStatus.Completed;
                            dispatcherOperation.OnCompleted();
                        }
                    }
                    else
                        this._event.WaitOne();
                }
            }
            finally
            {
                --this._frameDepth;
                this._currentFrame = currentFrame;
                if (this._frameDepth == 0 && this._hasShutdownStarted)
                    this.ShutdownImpl();
            }
        }

        public DispatcherOperation BeginInvoke(DispatcherOperationCallback method, object args)
        {
            if (method == null)
                throw new ArgumentNullException();
            DispatcherOperation dispatcherOperation = (DispatcherOperation)null;
            if (!this._hasShutdownFinished)
            {
                dispatcherOperation = new DispatcherOperation(this, method, args);
                this._queue.Enqueue((object)dispatcherOperation);
                this._event.Set();
            }
            return dispatcherOperation;
        }

        public object Invoke(TimeSpan timeout, DispatcherOperationCallback method, object args)
        {
            if (method == null)
                throw new ArgumentNullException();
            object obj = (object)null;
            DispatcherOperation dispatcherOperation = this.BeginInvoke(method, args);
            if (dispatcherOperation != null)
            {
                int num = (int)dispatcherOperation.Wait(timeout);
                if (dispatcherOperation.Status == DispatcherOperationStatus.Completed)
                    obj = dispatcherOperation.Result;
                else if (dispatcherOperation.Status != DispatcherOperationStatus.Aborted)
                    dispatcherOperation.Abort();
            }
            return obj;
        }

        internal object WrappedInvoke(DispatcherOperationCallback callback, object arg)
        {
            object obj = (object)null;
            try
            {
                obj = callback(arg);
            }
            catch (Exception ex)
            {
                if (this._finalExceptionHandler != null)
                {
                    if (this._finalExceptionHandler((object)this, ex))
                        goto label_5;
                }
                throw;
            }
            label_5:
            return obj;
        }

        internal static void SetFinalDispatcherExceptionHandler(DispatcherExceptionEventHandler handler)
        {
            Dispatcher.CurrentDispatcher.SetFinalExceptionHandler(handler);
        }

        internal void SetFinalExceptionHandler(DispatcherExceptionEventHandler handler)
        {
            this._finalExceptionHandler = handler;
        }

        private class GlobalLock
        {
        }
    }
}
