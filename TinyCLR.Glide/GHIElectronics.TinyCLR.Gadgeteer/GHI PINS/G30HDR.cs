namespace GHI.Pins
{
    
    using System;

    [Obsolete("Use GHI.Pins.G30 instead.")]
    public static class G30HDR
    {
        public const int SupportedAnalogInputPrecision = 12;

        public static class AnalogInput
        {
            public const int PA0 = 0;
            public const int PA1 = 1;
            public const int PA2 = 2;
            public const int PA3 = 3;
            public const int PA4 = 4;
            public const int PA5 = 5;
            public const int PA6 = 6;
            public const int PA7 = 7;
            public const int PB0 = ((int) 8);
            public const int PC0 = ((int) 10);
            public const int PC1 = ((int) 11);
            public const int PC2 = ((int) 12);
            public const int PC3 = ((int) 13);
            public const int PC4 = ((int) 14);
        }

        public static class Gpio
        {
            public const int PA0 = 0;
            public const int PA1 = 1;
            public const int PA10 = 10;
            public const int PA15 = 15;
            public const int PA2 = 2;
            public const int PA3 = 3;
            public const int PA4 = 4;
            public const int PA5 = 5;
            public const int PA6 = 6;
            public const int PA7 = 7;
            public const int PA8 = 8;
            public const int PA9 = 9;
            public const int PB0 = ((int) 0x10);
            public const int PB10 = ((int) 0x1a);
            public const int PB3 = ((int) 0x13);
            public const int PB4 = ((int) 20);
            public const int PB5 = ((int) 0x15);
            public const int PB6 = ((int) 0x16);
            public const int PB7 = ((int) 0x17);
            public const int PB8 = ((int) 0x18);
            public const int PB9 = ((int) 0x19);
            public const int PC0 = ((int) 0x20);
            public const int PC1 = ((int) 0x21);
            public const int PC10 = ((int) 0x2a);
            public const int PC11 = ((int) 0x2b);
            public const int PC12 = ((int) 0x2c);
            public const int PC13 = ((int) 0x2d);
            public const int PC14 = ((int) 0x2e);
            public const int PC15 = ((int) 0x2f);
            public const int PC2 = ((int) 0x22);
            public const int PC3 = ((int) 0x23);
            public const int PC4 = ((int) 0x24);
            public const int PC6 = ((int) 0x26);
            public const int PC7 = ((int) 0x27);
            public const int PC8 = ((int) 40);
            public const int PC9 = ((int) 0x29);
            public const int PD2 = ((int) 50);
        }

        public static class PwmOutput
        {
            public const int PA0 = 3;
            public const int PA1 = 4;
            public const int PA10 = 2;
            public const int PA2 = 5;
            public const int PA3 = 6;
            public const int PA8 = 0;
            public const int PA9 = 1;
            public const int PB6 = ((int) 11);
            public const int PB7 = ((int) 12);
            public const int PB8 = ((int) 13);
            public const int PB9 = ((int) 14);
            public const int PC6 = 7;
            public const int PC7 = ((int) 8);
            public const int PC8 = ((int) 9);
            public const int PC9 = ((int) 10);
        }

        public static class SerialPort
        {
            public const string Com1 = "COM1";
            public const string Com2 = "COM2";
        }

        public static class SpiBus
        {
            public const string Spi1 = "SPI1";
        }
    }
}

