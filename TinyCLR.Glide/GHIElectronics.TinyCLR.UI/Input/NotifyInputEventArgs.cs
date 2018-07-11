namespace GHIElectronics.TinyCLR.UI.Input
{
    using System;

    public class NotifyInputEventArgs : EventArgs
    {
        public readonly StagingAreaInputItem StagingItem;

        internal NotifyInputEventArgs(StagingAreaInputItem input)
        {
            this.StagingItem = input;
        }
    }
}

