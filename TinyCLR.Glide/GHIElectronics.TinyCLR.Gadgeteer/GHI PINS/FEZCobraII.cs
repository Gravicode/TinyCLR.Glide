namespace GHI.Pins
{
    
    using System;

    public static class FEZCobraII
    {
        public const int DebugLed = ((int) 0x2f);
        public const int Ldr0 = ((int) 0x4a);
        public const int Ldr1 = ((int) 0x16);
        public const int SdCardDetect = ((int) 40);
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 10;

        public static class NetworkInterface
        {
            public const int ChipSelect = ((int) 0x2a);
            public const int ExternalInterrupt = ((int) 0x4b);
            public const int Reset = ((int) 0x29);
            public const string SpiModule = "SPI2";
        }

        public static class Socket1
        {
            public const int Pin3 = ((int) 0x4d);
            public const int Pin4 = ((int) 0x3a);
            public const int Pin5 = ((int) 0x3b);
            public const int Pin6 = ((int) 60);
            public const int Pin7 = ((int) 0x3d);
            public const int Pin8 = ((int) 0x44);
            public const int Pin9 = ((int) 0x42);
        }

        public static class Socket10
        {
            public const int CanChannel = 1;
            public const int Pin3 = 11;
            public const int Pin4 = 1;
            public const int Pin5 = 0;
            public const int Pin6 = 5;
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
        }

        public static class Socket2
        {
            public const int Pin3 = ((int) 0x34);
            public const int Pin4 = ((int) 0x35);
            public const int Pin5 = ((int) 0x36);
            public const int Pin6 = ((int) 0x37);
            public const int Pin7 = ((int) 0x38);
            public const int Pin8 = ((int) 0x39);
            public const int Pin9 = ((int) 0x33);
        }

        public static class Socket3
        {
            public const int Pin3 = ((int) 0x4c);
            public const int Pin4 = ((int) 70);
            public const int Pin5 = ((int) 0x47);
            public const int Pin6 = ((int) 0x48);
            public const int Pin7 = ((int) 0x49);
            public const int Pin8 = ((int) 0x43);
            public const int Pin9 = ((int) 0x45);
        }

        public static class Socket4
        {
            public const int AnalogInput3 = 2;
            public const int AnalogInput4 = 1;
            public const int AnalogInput5 = 0;
            public const int Pin3 = ((int) 0x19);
            public const int Pin4 = ((int) 0x18);
            public const int Pin5 = ((int) 0x17);
            public const int Pin6 = ((int) 0x20);
            public const int Pin7 = ((int) 0x21);
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
        }

        public static class Socket5
        {
            public const int Pin3 = 13;
            public const int Pin4 = 2;
            public const int Pin5 = 3;
            public const int Pin6 = ((int) 0x24);
            public const string SerialPortName = "COM1";
        }

        public static class Socket6
        {
            public const int Pin3 = ((int) 0x55);
            public const int Pin4 = ((int) 0x2e);
            public const int Pin5 = ((int) 0x30);
            public const int Pin6 = ((int) 0x31);
            public const string SpiModule = "SPI2";
        }

        public static class Socket7
        {
            public const int Pin3 = 4;
            public const int Pin4 = ((int) 0x9c);
            public const int Pin5 = ((int) 0x9d);
            public const int Pin6 = ((int) 0x3e);
            public const int Pin7 = ((int) 0x7a);
            public const int Pin8 = ((int) 0x79);
            public const int Pin9 = ((int) 120);
            public const int Pwm7 = ((int) 8);
            public const int Pwm8 = 7;
            public const int Pwm9 = 6;
            public const string SerialPortName = "COM4";
        }

        public static class Socket8
        {
            public const int Pin3 = 10;
            public const int Pin4 = ((int) 0x40);
            public const int Pin5 = ((int) 0x10);
            public const int Pin6 = 6;
            public const int Pin7 = ((int) 0x11);
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
            public const string SerialPortName = "COM2";
        }

        public static class Socket9
        {
            public const int AnalogInput3 = 6;
            public const int AnalogInput4 = 5;
            public const int AnalogInput5 = 3;
            public const int AnalogOutput = 0;
            public const int Pin3 = 12;
            public const int Pin4 = ((int) 0x3f);
            public const int Pin5 = ((int) 0x1a);
            public const int Pin6 = ((int) 0x25);
            public const int Pin7 = ((int) 0x12);
            public const int Pin8 = ((int) 0x11);
            public const int Pin9 = 15;
            public const string SpiModule = "SPI1";
        }
    }
}

