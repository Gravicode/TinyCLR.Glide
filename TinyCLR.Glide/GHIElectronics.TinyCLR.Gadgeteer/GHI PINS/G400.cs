namespace GHI.Pins
{
    
    using System;

    [Obsolete("Use GHI.Pins.G400S or GHI.Pins.G400D instead.")]
    public static class G400
    {
        public const int PA0 = 0;
        public const int PA1 = 1;
        public const int PA10 = 10;
        public const int PA15 = 15;
        public const int PA16 = ((int) 0x10);
        public const int PA17 = ((int) 0x11);
        public const int PA18 = ((int) 0x12);
        public const int PA19 = ((int) 0x13);
        public const int PA2 = 2;
        public const int PA20 = ((int) 20);
        public const int PA21 = ((int) 0x15);
        public const int PA22 = ((int) 0x16);
        public const int PA23 = ((int) 0x17);
        public const int PA24 = ((int) 0x18);
        public const int PA25 = ((int) 0x19);
        public const int PA26 = ((int) 0x1a);
        public const int PA27 = ((int) 0x1b);
        public const int PA28 = ((int) 0x1c);
        public const int PA29 = ((int) 0x1d);
        public const int PA3 = 3;
        public const int PA30 = ((int) 30);
        public const int PA31 = ((int) 0x1f);
        public const int PA4 = 4;
        public const int PA5 = 5;
        public const int PA6 = 6;
        public const int PA7 = 7;
        public const int PA8 = 8;
        public const int PA9 = 9;
        public const int PB0 = ((int) 0x20);
        public const int PB1 = ((int) 0x21);
        public const int PB10 = ((int) 0x2a);
        public const int PB11 = ((int) 0x2b);
        public const int PB12 = ((int) 0x2c);
        public const int PB13 = ((int) 0x2d);
        public const int PB14 = ((int) 0x2e);
        public const int PB15 = ((int) 0x2f);
        public const int PB16 = ((int) 0x30);
        public const int PB17 = ((int) 0x31);
        public const int PB18 = ((int) 50);
        public const int PB2 = ((int) 0x22);
        public const int PB3 = ((int) 0x23);
        public const int PB4 = ((int) 0x24);
        public const int PB5 = ((int) 0x25);
        public const int PB6 = ((int) 0x26);
        public const int PB7 = ((int) 0x27);
        public const int PB8 = ((int) 40);
        public const int PB9 = ((int) 0x29);
        public const int PC0 = ((int) 0x40);
        public const int PC1 = ((int) 0x41);
        public const int PC10 = ((int) 0x4a);
        public const int PC11 = ((int) 0x4b);
        public const int PC12 = ((int) 0x4c);
        public const int PC13 = ((int) 0x4d);
        public const int PC14 = ((int) 0x4e);
        public const int PC15 = ((int) 0x4f);
        public const int PC16 = ((int) 80);
        public const int PC17 = ((int) 0x51);
        public const int PC18 = ((int) 0x52);
        public const int PC19 = ((int) 0x53);
        public const int PC2 = ((int) 0x42);
        public const int PC20 = ((int) 0x54);
        public const int PC21 = ((int) 0x55);
        public const int PC22 = ((int) 0x56);
        public const int PC23 = ((int) 0x57);
        public const int PC24 = ((int) 0x58);
        public const int PC26 = ((int) 90);
        public const int PC27 = ((int) 0x5b);
        public const int PC28 = ((int) 0x5c);
        public const int PC29 = ((int) 0x5d);
        public const int PC3 = ((int) 0x43);
        public const int PC30 = ((int) 0x5e);
        public const int PC31 = ((int) 0x5f);
        public const int PC4 = ((int) 0x44);
        public const int PC5 = ((int) 0x45);
        public const int PC6 = ((int) 70);
        public const int PC7 = ((int) 0x47);
        public const int PC8 = ((int) 0x48);
        public const int PC9 = ((int) 0x49);
        public const int PD0 = ((int) 0x60);
        public const int PD1 = ((int) 0x61);
        public const int PD10 = ((int) 0x6a);
        public const int PD11 = ((int) 0x6b);
        public const int PD12 = ((int) 0x6c);
        public const int PD13 = ((int) 0x6d);
        public const int PD14 = ((int) 110);
        public const int PD15 = ((int) 0x6f);
        public const int PD16 = ((int) 0x70);
        public const int PD17 = ((int) 0x71);
        public const int PD18 = ((int) 0x72);
        public const int PD2 = ((int) 0x62);
        public const int PD3 = ((int) 0x63);
        public const int PD4 = ((int) 100);
        public const int PD5 = ((int) 0x65);
        public const int PD6 = ((int) 0x66);
        public const int PD7 = ((int) 0x67);
        public const int PD8 = ((int) 0x68);
        public const int PD9 = ((int) 0x69);
        public const int SupportedAnalogInputPrecision = 10;

        public static class AnalogInput
        {
            public const int PB10 = ((int) 11);
            public const int PB11 = 0;
            public const int PB12 = 1;
            public const int PB13 = 2;
            public const int PB14 = 3;
            public const int PB15 = 4;
            public const int PB16 = 5;
            public const int PB17 = 6;
            public const int PB6 = 7;
            public const int PB7 = ((int) 8);
            public const int PB8 = ((int) 9);
            public const int PB9 = ((int) 10);
        }

        public static class CanBus
        {
            public const int Can1 = 1;
            public const int Can2 = 2;
        }

        public static class PwmOutput
        {
            public const int PC18 = 0;
            public const int PC19 = 1;
            public const int PC20 = 2;
            public const int PC21 = 3;
        }

        public static class SerialPort
        {
            public const string Com1 = "COM1";
            public const string Com2 = "COM2";
            public const string Com3 = "COM3";
            public const string Com4 = "COM4";
            public const string Com5 = "COM5";
            public const string Com6 = "COM6";
        }

        public static class SpiBus
        {
            public const string Spi1 = "SPI1";
            public const string Spi2 = "SPI2";
        }
    }
}

