namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;

    public class FocusChangedEventArgs : InputEventArgs
    {
        public readonly UIElement OldFocus;
        public readonly UIElement NewFocus;

        public FocusChangedEventArgs(ButtonDevice buttonDevice, DateTime timestamp, UIElement oldFocus, UIElement newFocus) : base(buttonDevice, timestamp)
        {
            this.OldFocus = oldFocus;
            this.NewFocus = newFocus;
        }
    }
}

