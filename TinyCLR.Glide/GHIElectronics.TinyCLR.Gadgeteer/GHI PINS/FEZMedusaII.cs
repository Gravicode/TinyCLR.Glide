namespace GHI.Pins
{
    
    using System;

    public static class FEZMedusaII
    {
        public const int DebugLed = ((int) 0x24);
        public const int Ldr0 = 15;
        public const int Ldr1 = ((int) 0x2d);
        public const int SupportedAnalogInputPrecision = 12;

        public static class Socket1
        {
            public const int Pin3 = ((int) 30);
            public const int Pin6 = ((int) 0x1d);
            public const int Pin7 = ((int) 0x1f);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
        }

        public static class Socket2
        {
            public const int Pin3 = ((int) 0x20);
            public const int Pin4 = ((int) 40);
            public const int Pin5 = ((int) 0x29);
            public const int Pin6 = ((int) 50);
            public const int Pin7 = ((int) 0x2a);
            public const int Pin8 = ((int) 0x2b);
            public const int Pin9 = ((int) 0x2c);
        }

        public static class Socket3
        {
            public const int Pin3 = ((int) 0x21);
            public const int Pin4 = 9;
            public const int Pin5 = 10;
            public const int Pin6 = ((int) 0x22);
            public const int Pin7 = ((int) 0x1a);
            public const int Pin8 = ((int) 0x17);
            public const int Pin9 = ((int) 0x16);
            public const string SerialPortName = "COM1";
        }

        public static class Socket4
        {
            public const int AnalogInput3 = 7;
            public const int AnalogInput4 = ((int) 8);
            public const int AnalogInput5 = ((int) 9);
            public const int Pin3 = 7;
            public const int Pin4 = ((int) 0x10);
            public const int Pin5 = ((int) 0x11);
            public const int Pin6 = ((int) 0x1c);
            public const int Pin7 = ((int) 0x15);
            public const int Pin8 = ((int) 20);
            public const int Pin9 = ((int) 0x13);
            public const string SpiModule = "SPI1";
        }

        public static class Socket5
        {
            public const int AnalogInput3 = 4;
            public const int AnalogInput4 = 5;
            public const int AnalogInput5 = 6;
            public const int Pin3 = 4;
            public const int Pin4 = 5;
            public const int Pin5 = 6;
            public const int Pin6 = ((int) 0x12);
            public const int Pin7 = 8;
            public const int Pin8 = ((int) 0x18);
            public const int Pin9 = ((int) 0x19);
            public const int Pwm7 = 0;
            public const int Pwm8 = ((int) 13);
            public const int Pwm9 = ((int) 14);
        }

        public static class Socket6
        {
            public const int AnalogInput3 = ((int) 13);
            public const int AnalogInput4 = 2;
            public const int AnalogInput5 = 3;
            public const int Pin3 = ((int) 0x23);
            public const int Pin4 = 2;
            public const int Pin5 = 3;
            public const int Pin6 = 1;
            public const int Pin7 = 0;
            public const int Pin8 = ((int) 0x26);
            public const int Pin9 = ((int) 0x27);
            public const int Pwm7 = 3;
            public const int Pwm8 = 7;
            public const int Pwm9 = ((int) 8);
            public const string SerialPortName = "COM2";
        }
    }
}

