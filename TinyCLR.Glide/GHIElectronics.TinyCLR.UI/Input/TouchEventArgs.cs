namespace GHIElectronics.TinyCLR.UI.Input
{
    using GHIElectronics.TinyCLR.UI;
    using System;
    using System.Runtime.InteropServices;

    public class TouchEventArgs : InputEventArgs
    {
        public TouchInput[] Touches;

        public TouchEventArgs(InputDevice inputDevice, DateTime timestamp, TouchInput[] touches) : base(inputDevice, timestamp)
        {
            this.Touches = touches;
        }

        public void GetPosition(UIElement relativeTo, int touchIndex, out int x, out int y)
        {
            x = this.Touches[touchIndex].X;
            y = this.Touches[touchIndex].Y;
            relativeTo.PointToClient(ref x, ref y);
        }
    }
}

