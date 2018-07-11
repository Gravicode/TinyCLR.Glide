namespace GHI.Pins
{
    
    using System;

    public static class FEZReaper
    {
        public const int DebugLed = ((int) 0x12);
        public const int Ldr0 = ((int) 0x43);
        public const int Ldr1 = ((int) 0x44);
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 12;

        public static class Socket1
        {
            public const int Pin3 = ((int) 0x41);
            public const int Pin6 = 13;
            public const int Pin7 = 14;
        }

        public static class Socket10
        {
            public const int Pin3 = ((int) 70);
            public const int Pin4 = ((int) 0x35);
            public const int Pin5 = ((int) 0x36);
            public const int Pin6 = ((int) 0x34);
            public const int Pin7 = ((int) 0x33);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
            public const string SerialPortName = "COM2";
        }

        public static class Socket11
        {
            public const int Pin3 = ((int) 0x49);
            public const int Pin4 = ((int) 40);
            public const int Pin5 = ((int) 0x29);
            public const int Pin6 = ((int) 50);
            public const int Pin7 = ((int) 0x2a);
            public const int Pin8 = ((int) 0x2b);
            public const int Pin9 = ((int) 0x2c);
        }

        public static class Socket12
        {
            public const int CanChannel = 1;
            public const int Pin3 = ((int) 0x4c);
            public const int Pin4 = ((int) 0x31);
            public const int Pin5 = ((int) 0x30);
            public const int Pin6 = ((int) 0x27);
            public const int Pin7 = ((int) 0x3d);
            public const int Pin8 = ((int) 0x3e);
            public const int Pin9 = ((int) 0x3f);
            public const int Pwm7 = ((int) 13);
            public const int Pwm8 = ((int) 14);
            public const int Pwm9 = ((int) 15);
        }

        public static class Socket13
        {
            public const int AnalogInput3 = 2;
            public const int AnalogInput4 = ((int) 8);
            public const int AnalogInput5 = ((int) 9);
            public const int Pin3 = 2;
            public const int Pin4 = ((int) 0x10);
            public const int Pin5 = ((int) 0x11);
            public const int Pin6 = ((int) 0x3a);
        }

        public static class Socket14
        {
            public const int Pin3 = ((int) 0x4f);
            public const int Pin4 = 14;
            public const int Pin5 = ((int) 0x2e);
            public const int Pin6 = ((int) 0x2f);
            public const int Pin8 = 13;
            public const int Pin9 = ((int) 0x12);
        }

        public static class Socket2
        {
            public const int Pin3 = ((int) 0x4a);
        }

        public static class Socket3
        {
            public const int Pin3 = ((int) 0x47);
            public const int Pin4 = ((int) 0x38);
            public const int Pin5 = ((int) 0x39);
            public const int Pin6 = ((int) 60);
            public const int Pin7 = ((int) 0x3b);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
            public const string SerialPortName = "COM3";
        }

        public static class Socket4
        {
            public const int CanChannel = 2;
            public const int Pin3 = ((int) 0x2d);
            public const int Pin4 = ((int) 0x1d);
            public const int Pin5 = ((int) 0x1c);
            public const int Pin6 = 15;
            public const int Pin7 = ((int) 0x4b);
            public const int Pin8 = ((int) 0x4d);
            public const int Pin9 = ((int) 0x4e);
            public const int Pwm7 = 1;
            public const int Pwm8 = 2;
            public const int Pwm9 = 3;
        }

        public static class Socket5
        {
            public const int AnalogInput3 = 3;
            public const int AnalogInput4 = 6;
            public const int AnalogInput5 = 7;
            public const int Pin3 = 3;
            public const int Pin4 = 6;
            public const int Pin5 = 7;
            public const int Pin6 = ((int) 0x48);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
        }

        public static class Socket6
        {
            public const int Pin3 = 8;
            public const int Pin4 = 0;
            public const int Pin5 = 1;
            public const int Pin6 = ((int) 0x42);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
            public const string SerialPortName = "COM4";
        }

        public static class Socket7
        {
            public const int Pin3 = ((int) 0x45);
            public const int Pin4 = 9;
            public const int Pin5 = 10;
            public const int Pin6 = ((int) 0x40);
            public const int Pin7 = ((int) 0x18);
            public const int Pin8 = ((int) 0x19);
            public const int Pin9 = ((int) 0x1b);
            public const int Pwm7 = ((int) 0x16);
            public const int Pwm8 = ((int) 0x17);
            public const int Pwm9 = 7;
            public const string SerialPortName = "COM1";
        }

        public static class Socket8
        {
            public const int AnalogInput3 = ((int) 10);
            public const int AnalogInput4 = ((int) 11);
            public const int AnalogInput5 = 4;
            public const int AnalogOutput = 0;
            public const int Pin3 = ((int) 0x20);
            public const int Pin4 = ((int) 0x21);
            public const int Pin5 = 4;
            public const int Pin6 = ((int) 0x26);
            public const int Pin7 = ((int) 0x15);
            public const int Pin8 = ((int) 20);
            public const int Pin9 = ((int) 0x13);
            public const int Pwm7 = ((int) 9);
            public const int Pwm8 = ((int) 8);
            public const int Pwm9 = 5;
            public const string SpiModule = "SPI1";
        }

        public static class Socket9
        {
            public const int AnalogInput3 = ((int) 14);
            public const int AnalogInput4 = ((int) 15);
            public const int AnalogInput5 = 5;
            public const int AnalogOutput = 1;
            public const int Pin3 = ((int) 0x24);
            public const int Pin4 = ((int) 0x25);
            public const int Pin5 = 5;
            public const int Pin6 = ((int) 0x37);
            public const int Pin7 = ((int) 0x23);
            public const int Pin8 = ((int) 0x22);
            public const int Pin9 = ((int) 0x1a);
            public const string SpiModule = "SPI2";
        }
    }
}

