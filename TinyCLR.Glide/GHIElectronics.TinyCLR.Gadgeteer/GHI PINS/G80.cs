namespace GHI.Pins
{
    
    using System;

    public static class G80
    {
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 12;

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
            public const int PB1 = ((int) 9);
            public const int PC0 = ((int) 10);
            public const int PC1 = ((int) 11);
            public const int PC2 = ((int) 12);
            public const int PC3 = ((int) 13);
            public const int PC4 = ((int) 14);
            public const int PC5 = ((int) 15);
        }

        public static class AnalogOutput
        {
            public const int PA4 = 0;
            public const int PA5 = 1;
        }

        public static class CanBus
        {
            public const int Can1 = 1;
            public const int Can2 = 2;
        }

        public static class Gpio
        {
            public const int PA0 = 0;
            public const int PA1 = 1;
            public const int PA10 = 10;
            public const int PA11 = 11;
            public const int PA12 = 12;
            public const int PA13 = 13;
            public const int PA14 = 14;
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
            public const int PB1 = ((int) 0x11);
            public const int PB10 = ((int) 0x1a);
            public const int PB11 = ((int) 0x1b);
            public const int PB12 = ((int) 0x1c);
            public const int PB13 = ((int) 0x1d);
            public const int PB14 = ((int) 30);
            public const int PB15 = ((int) 0x1f);
            public const int PB2 = ((int) 0x12);
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
            public const int PC5 = ((int) 0x25);
            public const int PC6 = ((int) 0x26);
            public const int PC7 = ((int) 0x27);
            public const int PC8 = ((int) 40);
            public const int PC9 = ((int) 0x29);
            public const int PD0 = ((int) 0x30);
            public const int PD1 = ((int) 0x31);
            public const int PD10 = ((int) 0x3a);
            public const int PD11 = ((int) 0x3b);
            public const int PD12 = ((int) 60);
            public const int PD13 = ((int) 0x3d);
            public const int PD14 = ((int) 0x3e);
            public const int PD15 = ((int) 0x3f);
            public const int PD2 = ((int) 50);
            public const int PD3 = ((int) 0x33);
            public const int PD4 = ((int) 0x34);
            public const int PD5 = ((int) 0x35);
            public const int PD6 = ((int) 0x36);
            public const int PD7 = ((int) 0x37);
            public const int PD8 = ((int) 0x38);
            public const int PD9 = ((int) 0x39);
            public const int PE0 = ((int) 0x40);
            public const int PE1 = ((int) 0x41);
            public const int PE10 = ((int) 0x4a);
            public const int PE11 = ((int) 0x4b);
            public const int PE12 = ((int) 0x4c);
            public const int PE13 = ((int) 0x4d);
            public const int PE14 = ((int) 0x4e);
            public const int PE15 = ((int) 0x4f);
            public const int PE2 = ((int) 0x42);
            public const int PE3 = ((int) 0x43);
            public const int PE4 = ((int) 0x44);
            public const int PE5 = ((int) 0x45);
            public const int PE6 = ((int) 70);
            public const int PE7 = ((int) 0x47);
            public const int PE8 = ((int) 0x48);
            public const int PE9 = ((int) 0x49);
        }

        public static class PwmOutput
        {
            public const int PA15 = 4;
            public const int PA2 = ((int) 20);
            public const int PA3 = ((int) 0x15);
            public const int PA6 = ((int) 0x18);
            public const int PA7 = ((int) 0x19);
            public const int PB0 = ((int) 10);
            public const int PB1 = ((int) 11);
            public const int PB10 = 6;
            public const int PB11 = 7;
            public const int PB3 = 5;
            public const int PB4 = ((int) 8);
            public const int PB5 = ((int) 9);
            public const int PB8 = ((int) 0x16);
            public const int PB9 = ((int) 0x17);
            public const int PC6 = ((int) 0x10);
            public const int PC7 = ((int) 0x11);
            public const int PC8 = ((int) 0x12);
            public const int PC9 = ((int) 0x13);
            public const int PD12 = ((int) 12);
            public const int PD13 = ((int) 13);
            public const int PD14 = ((int) 14);
            public const int PD15 = ((int) 15);
            public const int PE11 = 1;
            public const int PE13 = 2;
            public const int PE14 = 3;
            public const int PE9 = 0;
        }

        public static class SerialPort
        {
            public const string Com1 = "COM1";
            public const string Com2 = "COM2";
            public const string Com3 = "COM3";
            public const string Com4 = "COM4";
        }

        public static class SpiBus
        {
            public const string Spi1 = "SPI1";
            public const string Spi2 = "SPI2";
        }
    }
}

