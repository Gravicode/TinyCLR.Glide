namespace GHI.Pins
{
    
    using System;

    public static class FEZSpiderII
    {
        public const int DebugLed = ((int) 0x3f);
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 10;

        public static class Socket1
        {
            public const int Pin3 = ((int) 90);
            public const int Pin6 = ((int) 0x56);
            public const int Pin7 = ((int) 0x9f);
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
        }

        public static class Socket10
        {
            public const int AnalogInput3 = 6;
            public const int AnalogInput4 = 1;
            public const int AnalogInput5 = 0;
            public const int Pin3 = 12;
            public const int Pin4 = ((int) 0x18);
            public const int Pin5 = ((int) 0x17);
            public const int Pin6 = ((int) 0x7f);
            public const int Pin7 = ((int) 0x57);
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
        }

        public static class Socket11
        {
            public const int Pin3 = ((int) 0x5e);
            public const int Pin4 = 2;
            public const int Pin5 = 3;
            public const int Pin6 = ((int) 0x9d);
            public const int Pin7 = ((int) 120);
            public const int Pin8 = ((int) 0x70);
            public const int Pin9 = ((int) 0x71);
            public const int Pwm7 = 6;
            public const int Pwm8 = 0;
            public const int Pwm9 = 1;
            public const string SerialPortName = "COM1";
        }

        public static class Socket12
        {
            public const int Pin3 = ((int) 0x4d);
            public const int Pin4 = ((int) 0x3a);
            public const int Pin5 = ((int) 0x3b);
            public const int Pin6 = ((int) 60);
            public const int Pin7 = ((int) 0x3d);
            public const int Pin8 = ((int) 0x44);
            public const int Pin9 = ((int) 0x42);
        }

        public static class Socket13
        {
            public const int Pin3 = ((int) 0x34);
            public const int Pin4 = ((int) 0x35);
            public const int Pin5 = ((int) 0x36);
            public const int Pin6 = ((int) 0x37);
            public const int Pin7 = ((int) 0x38);
            public const int Pin8 = ((int) 0x39);
            public const int Pin9 = ((int) 0x55);
        }

        public static class Socket14
        {
            public const int Pin3 = ((int) 0x4c);
            public const int Pin4 = ((int) 70);
            public const int Pin5 = ((int) 0x47);
            public const int Pin6 = ((int) 0x48);
            public const int Pin7 = ((int) 0x49);
            public const int Pin8 = ((int) 0x43);
            public const int Pin9 = ((int) 0x45);
        }

        public static class Socket3
        {
            public const int Pin3 = 5;
            public const int Pin6 = 4;
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
        }

        public static class Socket4
        {
            public const int Pin3 = ((int) 0x5f);
            public const int Pin4 = ((int) 0x40);
            public const int Pin5 = ((int) 0x10);
            public const int Pin6 = 6;
            public const int Pin7 = ((int) 0x72);
            public const int Pin8 = ((int) 0x1b);
            public const int Pin9 = ((int) 0x1c);
            public const string SerialPortName = "COM2";
        }

        public static class Socket5
        {
            public const int Pin3 = ((int) 0x5b);
            public const int Pin4 = ((int) 0x26);
            public const int Pin5 = ((int) 0x27);
            public const int Pin6 = ((int) 0x23);
            public const int Pin7 = ((int) 0x2b);
            public const int Pin8 = ((int) 0x2c);
            public const int Pin9 = ((int) 0x22);
        }

        public static class Socket6
        {
            public const int CanChannel = 1;
            public const int Pin3 = ((int) 0x59);
            public const int Pin4 = 1;
            public const int Pin5 = 0;
            public const int Pin6 = ((int) 0x9c);
            public const string SpiModule = "SPI2";
        }

        public static class Socket8
        {
            public const int Pin3 = ((int) 0x4a);
            public const int Pin4 = 10;
            public const int Pin5 = 11;
            public const int Pin6 = ((int) 0x33);
            public const int Pin7 = ((int) 0x7d);
            public const int Pin8 = ((int) 0x7b);
            public const int Pin9 = ((int) 0x7a);
            public const int Pwm7 = ((int) 11);
            public const int Pwm8 = ((int) 9);
            public const int Pwm9 = ((int) 8);
            public const string SerialPortName = "COM3";
        }

        public static class Socket9
        {
            public const int AnalogInput3 = 7;
            public const int AnalogInput4 = 2;
            public const int AnalogInput5 = 3;
            public const int AnalogOutput = 0;
            public const int Pin3 = 13;
            public const int Pin4 = ((int) 0x19);
            public const int Pin5 = ((int) 0x1a);
            public const int Pin6 = ((int) 0x79);
            public const int Pin7 = ((int) 0x12);
            public const int Pin8 = ((int) 0x11);
            public const int Pin9 = 15;
            public const string SerialPortName = "COM4";
            public const string SpiModule = "SPI1";
        }

        public static class SerialPort
        {
            public const string Com1 = "COM1";
            public const string Com2 = "COM2";
            public const string Com3 = "COM3";
            public const string Com4 = "COM4";
            public const string Com5 = "COM5";
        }
        public static class I2cBus
        {
            public const string I2c1 = "I2C1";
        }
        public static class SpiBus
        {
            public const string Spi1 = "SPI1";
            public const string Spi2 = "SPI2";
        }
        public static class CanBus
        {
            public const string Can1 = "CAN1";
            public const string Can2 = "CAN2";
        }
    }
}

