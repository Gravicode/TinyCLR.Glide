namespace GHI.Pins
{
    
    using System;

    public static class FEZCerbuinoNet
    {
        public const int DebugLed = ((int) 0x12);
        public const int SdCardDetect = ((int) 0x22);
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 12;

        public static class EthernetInterface
        {
            public const int ChipSelect = 13;
            public const int ExternalInterrupt = 14;
            public const int Reset = ((int) 0x1a);
            public const string SpiModule = "SPI1";
        }

        public static class Headers
        {
            public static class AnalogInput
            {
                public const int A0 = ((int) 10);
                public const int A1 = ((int) 8);
                public const int A2 = ((int) 9);
                public const int A3 = 7;
                public const int A4 = 4;
                public const int A5 = 5;
            }

            public static class AnalogOutput
            {
                public const int A1 = 1;
                public const int A5 = 0;
            }

            public static class Gpio
            {
                public const int A0 = ((int) 0x11);
                public const int A1 = 5;
                public const int A2 = ((int) 0x10);
                public const int A3 = ((int) 0x23);
                public const int A4 = ((int) 0x21);
                public const int A5 = 4;
                public const int D0 = ((int) 0x1b);
                public const int D1 = ((int) 0x1a);
                public const int D10 = 15;
                public const int D11 = ((int) 0x15);
                public const int D12 = ((int) 20);
                public const int D13 = ((int) 0x13);
                public const int D2 = ((int) 0x1c);
                public const int D3 = ((int) 0x2e);
                public const int D4 = ((int) 0x2f);
                public const int D5 = 8;
                public const int D6 = 10;
                public const int D7 = ((int) 0x24);
                public const int D8 = ((int) 0x1d);
                public const int D9 = 9;
            }

            public static class PwmOutput
            {
                public const int A0 = 5;
                public const int A2 = 4;
                public const int D0 = ((int) 9);
                public const int D1 = ((int) 10);
                public const int D11 = 6;
                public const int D12 = 7;
                public const int D13 = ((int) 8);
                public const int D5 = 3;
                public const int D6 = ((int) 11);
                public const int D9 = ((int) 12);
            }

            public static class SerialPort
            {
                public const string Com3 = "COM3";
            }

            public static class SpiBus
            {
                public const string Spi1 = "SPI1";
            }
        }

        public static class Socket1
        {
            public const int Pin3 = ((int) 0x2d);
            public const int Pin4 = ((int) 0x26);
            public const int Pin5 = ((int) 0x27);
            public const int Pin6 = ((int) 0x10);
            public const int Pin7 = ((int) 0x15);
            public const int Pin8 = ((int) 20);
            public const int Pin9 = ((int) 0x13);
            public const int Pwm7 = 6;
            public const int Pwm8 = 7;
            public const int Pwm9 = ((int) 8);
            public const string SerialPortName = "COM6";
            public const string SpiModule = "SPI1";
        }

        public static class Socket2
        {
            public const int AnalogInput3 = 0;
            public const int AnalogInput4 = 1;
            public const int AnalogInput5 = 2;
            public const int Pin3 = 6;
            public const int Pin4 = 2;
            public const int Pin5 = 3;
            public const int Pin6 = 1;
            public const int Pin7 = 0;
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
            public const string SerialPortName = "COM2";
        }

        public static class Socket3
        {
            public const int AnalogInput3 = 3;
            public const int AnalogInput4 = 4;
            public const int AnalogInput5 = 5;
            public const int AnalogOutput = 0;
            public const int Pin3 = ((int) 0x20);
            public const int Pin4 = ((int) 0x21);
            public const int Pin5 = 4;
            public const int Pin6 = ((int) 0x25);
            public const int Pin7 = ((int) 0x18);
            public const int Pin8 = 7;
            public const int Pin9 = ((int) 0x19);
            public const int Pwm7 = ((int) 14);
            public const int Pwm8 = 1;
            public const int Pwm9 = ((int) 15);
        }
    }
}

