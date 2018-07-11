namespace GHI.Pins
{
    
    using System;

    public static class Generic
    {
        [Obsolete("Use the specific board definition under GHI.Pins.")]
        public static int GetPin(char port, int pinNumber)
        {
            port = port.ToUpper();
            return (int)(((port - 'A') * 0x10) + pinNumber);
            /*
            switch (SystemInfo.SystemID.Model)
            {
                case 10:
                    return (int) (((port - 'A') * 0x10) + pinNumber);

                case 11:
                    return (int) (((port - 'A') * 0x20) + pinNumber);
            }*/
            throw new InvalidOperationException("Please use the provided pin enumerations for our SoMs and SoCs.");
        }
    }
}

