namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public sealed class PreProcessInputEventArgs : ProcessInputEventArgs
    {
        internal bool _canceled;

        internal PreProcessInputEventArgs(StagingAreaInputItem input) : base(input)
        {
            this._canceled = false;
        }

        public void Cancel()
        {
            this._canceled = true;
        }

        public bool Canceled
        {
            get
            {
                return this._canceled;
            }
        }
    }
}

