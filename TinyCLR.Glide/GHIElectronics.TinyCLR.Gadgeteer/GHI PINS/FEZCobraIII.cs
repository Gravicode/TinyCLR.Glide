namespace GHI.Pins
{
    
    using System;

    public static class FEZCobraIII
    {
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 10;

        public static class AnalogInput
        {
            public const int D14 = 4;
            public const int D15 = 5;
            public const int D16 = 2;
            public const int D17 = 3;
            public const int D18 = 6;
            public const int D19 = 7;
            public const int D52 = 1;
            public const int D54 = 0;
        }

        public static class AnalogOutput
        {
            public const int D17 = 0;
        }

        public static class CanBus
        {
            public const int Can1 = 1;
            public const int Can2 = 2;
        }

        public static class Gpio
        {
            public const int D0 = 3;
            public const int D1 = 2;
            public const int D10 = ((int) 40);
            public const int D11 = ((int) 0x12);
            public const int D12 = ((int) 0x11);
            public const int D13 = 15;
            public const int D14 = ((int) 0x3e);
            public const int D15 = ((int) 0x3f);
            public const int D16 = ((int) 0x19);
            public const int D17 = ((int) 0x1a);
            public const int D18 = 12;
            public const int D19 = 13;
            public const int D2 = ((int) 0x1b);
            public const int D20 = 11;
            public const int D21 = 10;
            public const int D22 = ((int) 0x31);
            public const int D23 = ((int) 0x30);
            public const int D24 = ((int) 0x2f);
            public const int D25 = ((int) 0x20);
            public const int D26 = ((int) 0x24);
            public const int D27 = ((int) 0x21);
            public const int D28 = ((int) 0x10);
            public const int D29 = ((int) 0x40);
            public const int D3 = ((int) 0x1c);
            public const int D30 = ((int) 0x43);
            public const int D31 = ((int) 0x45);
            public const int D32 = ((int) 0x42);
            public const int D33 = ((int) 0x4c);
            public const int D34 = ((int) 70);
            public const int D35 = ((int) 0x47);
            public const int D36 = ((int) 0x48);
            public const int D37 = ((int) 0x49);
            public const int D38 = ((int) 0x44);
            public const int D39 = ((int) 0x34);
            public const int D4 = 0;
            public const int D40 = ((int) 0x35);
            public const int D41 = ((int) 0x36);
            public const int D42 = ((int) 0x37);
            public const int D43 = ((int) 0x38);
            public const int D44 = ((int) 0x39);
            public const int D45 = ((int) 0x4d);
            public const int D46 = ((int) 0x3a);
            public const int D47 = ((int) 0x3b);
            public const int D48 = ((int) 60);
            public const int D49 = ((int) 0x3d);
            public const int D5 = ((int) 0x7a);
            public const int D50 = 4;
            public const int D51 = 5;
            public const int D52 = ((int) 0x18);
            public const int D54 = ((int) 0x17);
            public const int D56 = ((int) 0x25);
            public const int D58 = ((int) 120);
            public const int D59 = ((int) 0x9c);
            public const int D6 = ((int) 0x79);
            public const int D60 = 6;
            public const int D61 = ((int) 0x9d);
            public const int D62 = ((int) 0x55);
            public const int D7 = 1;
            public const int D8 = ((int) 0x2a);
            public const int D9 = ((int) 0x29);
            public const int Ldr0 = ((int) 0x4a);
            public const int Ldr1 = ((int) 0x16);
            public const int Led1 = ((int) 0x2e);
            public const int Led2 = ((int) 0x33);
            public const int Mod = ((int) 0x41);
            public const int SdCardDetect = ((int) 0x4b);
        }

        public static class PwmOutput
        {
            public const int D30 = ((int) 9);
            public const int D31 = ((int) 11);
            public const int D38 = ((int) 10);
            public const int D5 = ((int) 8);
            public const int D56 = 2;
            public const int D58 = 6;
            public const int D6 = 7;
        }

        public static class SerialPort
        {
            public const string Com1 = "COM1";
            public const string Com2 = "COM2";
            public const string Com3 = "COM3";
            public const string Com4 = "COM4";
            public const string Com5 = "COM5";
        }

        public static class SpiBus
        {
            public const string Spi1 = "SPI1";
            public const string Spi2 = "SPI2";
            public const string Spi3 = "SPI3";
        }
    }
}

