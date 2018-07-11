namespace GHI.Pins
{
    
    using System;

    public static class FEZHydra
    {
        public const int DebugLed = ((int) 0x72);
        public const int SupportedAnalogInputPrecision = 12;

        public static class Socket10
        {
            public const int Pin3 = ((int) 0x56);
            public const int Pin4 = ((int) 0x57);
            public const int Pin5 = ((int) 0x58);
            public const int Pin6 = ((int) 0x59);
            public const int Pin7 = ((int) 0x54);
            public const int Pin8 = ((int) 0x44);
            public const int Pin9 = ((int) 0x45);
        }

        public static class Socket11
        {
            public const int Pin3 = ((int) 0x4f);
            public const int Pin4 = ((int) 80);
            public const int Pin5 = ((int) 0x51);
            public const int Pin6 = ((int) 0x52);
            public const int Pin7 = ((int) 0x53);
            public const int Pin8 = ((int) 0x55);
            public const int Pin9 = ((int) 0x43);
        }

        public static class Socket12
        {
            public const int Pin3 = ((int) 0x49);
            public const int Pin4 = ((int) 0x4a);
            public const int Pin5 = ((int) 0x4b);
            public const int Pin6 = ((int) 0x4c);
            public const int Pin7 = ((int) 0x4d);
            public const int Pin8 = ((int) 0x47);
            public const int Pin9 = ((int) 70);
        }

        public static class Socket13
        {
            public const int AnalogInput3 = 4;
            public const int AnalogInput4 = 3;
            public const int AnalogInput5 = 1;
            public const int Pin3 = ((int) 0x66);
            public const int Pin4 = ((int) 20);
            public const int Pin5 = ((int) 0x12);
            public const int Pin6 = ((int) 0x21);
            public const int Pin7 = ((int) 60);
            public const int Pin8 = ((int) 0x3a);
            public const int Pin9 = ((int) 0x3d);
        }

        public static class Socket14
        {
            public const int AnalogInput3 = 5;
            public const int AnalogInput4 = 2;
            public const int AnalogInput5 = 0;
            public const int Pin3 = ((int) 0x67);
            public const int Pin4 = ((int) 0x13);
            public const int Pin5 = ((int) 0x11);
            public const int Pin6 = ((int) 0x20);
            public const int Pin7 = ((int) 0x3e);
            public const int Pin8 = ((int) 0x3f);
            public const int Pin9 = ((int) 0x3b);
        }

        public static class Socket2
        {
            public const int Pin3 = ((int) 0x33);
            public const int Pin6 = ((int) 50);
            public const int Pin7 = ((int) 0x36);
        }

        public static class Socket3
        {
            public const int Pin3 = ((int) 40);
            public const int Pin4 = ((int) 0x29);
            public const int Pin5 = ((int) 0x2c);
            public const int Pin6 = ((int) 0x2d);
            public const int Pin7 = ((int) 0x1a);
            public const int Pin8 = ((int) 0x19);
            public const int Pin9 = ((int) 0x1b);
            public const string SpiModule = "SPI1";
        }

        public static class Socket4
        {
            public const int Pin3 = ((int) 0x22);
            public const int Pin4 = 11;
            public const int Pin5 = 12;
            public const int Pin6 = ((int) 0x2e);
            public const int Pin7 = ((int) 0x1a);
            public const int Pin8 = ((int) 0x19);
            public const int Pin9 = ((int) 0x1b);
            public const string SerialPortName = "COM3";
            public const string SpiModule = "SPI1";
        }

        public static class Socket5
        {
            public const int Pin3 = 9;
            public const int Pin4 = ((int) 0x16);
            public const int Pin5 = ((int) 0x15);
            public const int Pin6 = 10;
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x18);
            public const string SerialPortName = "COM1";
        }

        public static class Socket6
        {
            public const int Pin3 = ((int) 0x71);
            public const int Pin4 = 13;
            public const int Pin5 = 14;
            public const int Pin6 = ((int) 0x1d);
            public const int Pin7 = ((int) 30);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x18);
            public const string SerialPortName = "COM4";
        }

        public static class Socket7
        {
            public const int Pin3 = ((int) 0x73);
            public const int Pin4 = 6;
            public const int Pin5 = 7;
            public const int Pin6 = ((int) 0x74);
            public const int Pin7 = ((int) 110);
            public const int Pin8 = ((int) 0x6f);
            public const int Pin9 = ((int) 0x70);
            public const int Pwm7 = 0;
            public const int Pwm8 = 1;
            public const int Pwm9 = 2;
            public const string SerialPortName = "COM2";
        }

        public static class Socket8
        {
            public const int Pin3 = ((int) 0x6b);
            public const int Pin4 = 0;
            public const int Pin5 = 3;
            public const int Pin6 = 1;
            public const int Pin7 = 4;
            public const int Pin8 = 5;
            public const int Pin9 = 2;
        }

        public static class Socket9
        {
            public const int Pin3 = ((int) 0x69);
            public const int Pin4 = ((int) 0x6a);
            public const int Pin5 = ((int) 0x6c);
            public const int Pin6 = ((int) 0x61);
            public const int Pin7 = ((int) 0x63);
            public const int Pin8 = ((int) 100);
            public const int Pin9 = ((int) 0x62);
        }
    }
}

