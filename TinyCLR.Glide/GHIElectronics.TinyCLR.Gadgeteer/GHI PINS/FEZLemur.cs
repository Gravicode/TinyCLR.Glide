namespace GHI.Pins
{
    
    using System;

    public static class FEZLemur
    {
        public const int SupportedAnalogInputPrecision = 12;

        public static class AnalogInput
        {
            public const int A0 = 4;
            public const int A1 = 5;
            public const int A2 = 6;
            public const int A3 = 7;
            public const int A4 = ((int) 8);
            public const int A5 = ((int) 9);
            public const int D10 = 3;
            public const int D20 = ((int) 10);
            public const int D21 = ((int) 11);
            public const int D24 = ((int) 12);
            public const int D25 = ((int) 13);
            public const int D32 = ((int) 14);
            public const int D33 = ((int) 15);
            public const int D5 = 1;
            public const int D6 = 0;
            public const int D9 = 2;
        }

        public static class Gpio
        {
            public const int A0 = 4;
            public const int A1 = 5;
            public const int A2 = 6;
            public const int A3 = 7;
            public const int A4 = ((int) 0x10);
            public const int A5 = ((int) 0x11);
            public const int D0 = 10;
            public const int D1 = 9;
            public const int D10 = 3;
            public const int D11 = ((int) 0x15);
            public const int D12 = ((int) 20);
            public const int D13 = ((int) 0x13);
            public const int D2 = ((int) 0x17);
            public const int D20 = ((int) 0x20);
            public const int D21 = ((int) 0x21);
            public const int D22 = 13;
            public const int D23 = 14;
            public const int D24 = ((int) 0x22);
            public const int D25 = ((int) 0x23);
            public const int D26 = ((int) 0x1d);
            public const int D27 = ((int) 30);
            public const int D28 = ((int) 0x1f);
            public const int D29 = ((int) 0x26);
            public const int D3 = ((int) 0x16);
            public const int D30 = ((int) 0x27);
            public const int D31 = ((int) 0x12);
            public const int D32 = ((int) 0x24);
            public const int D33 = ((int) 0x25);
            public const int D4 = ((int) 0x2f);
            public const int D5 = 1;
            public const int D6 = 0;
            public const int D7 = ((int) 0x2e);
            public const int D8 = 8;
            public const int D9 = 2;
            public const int Ldr0 = 15;
            public const int Ldr1 = ((int) 0x2d);
            public const int Led1 = ((int) 0x19);
            public const int Led2 = ((int) 0x18);
            public const int Mod = ((int) 0x1a);
            public const int SdCardDetect = ((int) 0x1c);
        }

        public static class PwmOutput
        {
            public const int D0 = 2;
            public const int D1 = 1;
            public const int D10 = 6;
            public const int D2 = ((int) 12);
            public const int D29 = 7;
            public const int D3 = ((int) 11);
            public const int D30 = ((int) 8);
            public const int D5 = 4;
            public const int D6 = 3;
            public const int D8 = 0;
            public const int D9 = 5;
            public const int Led1 = ((int) 14);
            public const int Led2 = ((int) 13);
        }

        public static class SerialPort
        {
            public const string Com1 = "COM1";
            public const string Com2 = "COM2";
        }

        public static class SpiBus
        {
            public const string Spi1 = "SPI1";
            public const string Spi2 = "SPI2";
        }
    }
}

