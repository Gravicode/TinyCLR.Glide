namespace GHIElectronics.TinyCLR.UI.Threading
{
    using System;
    using System.Threading;

    public abstract class DispatcherObject
    {
        public readonly GHIElectronics.TinyCLR.UI.Threading.Dispatcher Dispatcher;

        protected DispatcherObject()
        {
            this.Dispatcher = GHIElectronics.TinyCLR.UI.Threading.Dispatcher.CurrentDispatcher;
        }

        internal DispatcherObject(bool canBeUnbound)
        {
            if (canBeUnbound)
            {
                this.Dispatcher = GHIElectronics.TinyCLR.UI.Threading.Dispatcher.FromThread(Thread.CurrentThread);
            }
            else
            {
                this.Dispatcher = GHIElectronics.TinyCLR.UI.Threading.Dispatcher.CurrentDispatcher;
            }
        }

        public bool CheckAccess()
        {
            bool flag = true;
            if (this.Dispatcher != null)
            {
                flag = this.Dispatcher.CheckAccess();
            }
            return flag;
        }

        public void VerifyAccess()
        {
            if (this.Dispatcher != null)
            {
                this.Dispatcher.VerifyAccess();
            }
        }
    }
}

