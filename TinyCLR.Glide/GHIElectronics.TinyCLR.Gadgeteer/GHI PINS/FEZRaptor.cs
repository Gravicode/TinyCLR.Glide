namespace GHI.Pins
{
    
    using System;

    public static class FEZRaptor
    {
        public const int DebugLed = ((int) 0x63);
        public const int Ldr0 = ((int) 0x18);
        public const int Ldr1 = 4;
        public const int SupportedAnalogInputPrecision = 10;

        public static class Socket1
        {
            public const int Pin3 = ((int)0x20);
            public const int Pin4 = 7;
            public const int Pin5 = 8;
            public const int Pin6 = ((int) 0x25);
            public const int Pin7 = ((int) 0x16);
            public const int Pin8 = ((int) 0x15);
            public const int Pin9 = ((int) 0x17);
            public const string SerialPortName = "COM4";
            public const string SpiModule = "SPI2";
        }

        public static class Socket10
        {
            public const int CanChannel = 1;
            public const int Pin3 = ((int) 0x1d);
            public const int Pin4 = 10;
            public const int Pin5 = 9;
            public const int Pin6 = ((int) 0x1a);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
            public const string SerialPortName = "COM1";
        }

        public static class Socket11
        {
            public const int CanChannel = 2;
            public const int Pin3 = ((int) 90);
            public const int Pin4 = 5;
            public const int Pin5 = 6;
            public const int Pin6 = ((int) 0x24);
            public const string SerialPortName = "COM3";
            public const string SpiModule = "SPI1";
        }

        public static class Socket12
        {
            public const int Pin3 = ((int) 0x5f);
            public const int Pin4 = ((int) 80);
            public const int Pin5 = ((int) 0x51);
            public const int Pin6 = ((int) 0x22);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
            public const string SerialPortName = "COM6";
        }

        public static class Socket13
        {
            public const int AnalogInput3 = 6;
            public const int AnalogInput4 = 7;
            public const int AnalogInput5 = ((int) 8);
            public const int Pin3 = ((int) 0x31);
            public const int Pin4 = ((int) 0x26);
            public const int Pin5 = ((int) 0x27);
            public const int Pin6 = ((int) 0x56);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
        }

        public static class Socket14
        {
            public const int AnalogInput3 = 0;
            public const int AnalogInput4 = 1;
            public const int AnalogInput5 = 2;
            public const int Pin3 = ((int) 0x2b);
            public const int Pin4 = ((int) 0x2c);
            public const int Pin5 = ((int) 0x2d);
            public const int Pin6 = ((int) 0x61);
            public const int Pin7 = ((int) 0x62);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
        }

        public static class Socket15
        {
            public const int Pin3 = ((int) 0x4b);
            public const int Pin4 = ((int) 0x4c);
            public const int Pin5 = ((int) 0x4d);
            public const int Pin6 = ((int) 0x4e);
            public const int Pin7 = ((int) 0x4f);
            public const int Pin8 = ((int) 0x5b);
            public const int Pin9 = ((int) 0x5c);
        }

        public static class Socket16
        {
            public const int Pin3 = ((int) 0x45);
            public const int Pin4 = ((int) 70);
            public const int Pin5 = ((int) 0x47);
            public const int Pin6 = ((int) 0x48);
            public const int Pin7 = ((int) 0x49);
            public const int Pin8 = ((int) 0x4a);
            public const int Pin9 = ((int) 0x55);
        }

        public static class Socket17
        {
            public const int Pin3 = ((int) 0x40);
            public const int Pin4 = ((int) 0x41);
            public const int Pin5 = ((int) 0x42);
            public const int Pin6 = ((int) 0x43);
            public const int Pin7 = ((int) 0x44);
            public const int Pin8 = ((int) 0x5d);
            public const int Pin9 = ((int) 0x5e);
        }

        public static class Socket18
        {
            public const int AnalogInput3 = ((int) 9);
            public const int AnalogInput4 = ((int) 10);
            public const int AnalogInput5 = ((int) 11);
            public const int Pin3 = ((int) 40);
            public const int Pin4 = ((int) 0x29);
            public const int Pin5 = ((int) 0x2a);
            public const int Pin6 = ((int) 0x58);
            public const int Pin7 = ((int) 0x52);
            public const int Pin8 = ((int) 0x53);
            public const int Pin9 = ((int) 0x54);
            public const int Pwm7 = 0;
            public const int Pwm8 = 1;
            public const int Pwm9 = 2;
            public static class PwmPin
            {
                public static class Controller0
                {
                    public const string Id = "CON0";
                    public const int PC18 = 0;
                }
                public static class Controller1
                {
                    public const string Id = "CON1";
                    public const int PC19 = 0;
                }
                public static class Controller2
                {
                    public const string Id = "CON2";
                    public const int PC20 = 0;
                }
                public static class Controller3
                {
                    public const string Id = "CON3";
                    public const int PC21 = 0;
                }
            }
        }

        public static class Socket2
        {
            public const int AnalogInput3 = 3;
            public const int AnalogInput4 = 4;
            public const int AnalogInput5 = 5;
            public const int Pin3 = ((int) 0x2e);
            public const int Pin4 = ((int) 0x2f);
            public const int Pin5 = ((int) 0x30);
            public const int Pin6 = ((int) 50);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
        }

        public static class Socket3
        {
            public const int Pin3 = ((int) 0x21);
            public const int Pin4 = ((int) 0x57);
            public const int Pin5 = ((int) 0x23);
            public const int Pin6 = ((int) 0x1c);
            public const string SpiModule = "SPI1";
        }

        public static class Socket4
        {
            public const int Pin3 = ((int) 0x1b);
            public const int Pin4 = 0;
            public const int Pin5 = 1;
            public const int Pin6 = 2;
            public const int Pin7 = 3;
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
            public const string SerialPortName = "COM2";
        }

        public static class Socket6
        {
            public const int Pin3 = ((int) 100);
            public const int Pin6 = ((int) 0x18);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
        }

        public static class Socket7
        {
            public const int Pin3 = ((int) 0x19);
            public const int Pin6 = 4;
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
        }

        public static class Socket8
        {
            public const int Pin3 = ((int) 0x66);
            public const int Pin6 = ((int) 0x60);
            public const int Pin7 = ((int) 0x65);
            public const int Pin8 = ((int) 30);
            public const int Pin9 = ((int) 0x1f);
        }

        public static class Socket9
        {
            public const int Pin3 = ((int) 0x67);
            public const int Pin4 = 15;
            public const int Pin5 = ((int) 0x12);
            public const int Pin6 = ((int) 0x10);
            public const int Pin7 = ((int) 0x13);
            public const int Pin8 = ((int) 20);
            public const int Pin9 = ((int) 0x11);
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

