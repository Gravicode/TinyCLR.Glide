namespace GHI.Pins
{
    
    using System;

    public static class G120E
    {
        public const int SupportedAnalogInputPrecision = 12;
        public const int SupportedAnalogOutputPrecision = 10;

        public static class AnalogInput
        {
            public const int P0_12 = 6;
            public const int P0_13 = 7;
            public const int P0_23 = 0;
            public const int P0_24 = 1;
            public const int P0_25 = 2;
            public const int P0_26 = 3;
            public const int P1_30 = 4;
            public const int P1_31 = 5;
        }

        public static class AnalogOutput
        {
            public const int P0_26 = 0;
        }

        public static class CanBus
        {
            public const int Can1 = 1;
            public const int Can2 = 2;
        }

        public static class Gpio
        {
            public const int P0_0 = 0;
            public const int P0_1 = 1;
            public const int P0_10 = 10;
            public const int P0_11 = 11;
            public const int P0_12 = 12;
            public const int P0_13 = 13;
            public const int P0_15 = 15;
            public const int P0_16 = ((int) 0x10);
            public const int P0_17 = ((int) 0x11);
            public const int P0_18 = ((int) 0x12);
            public const int P0_2 = 2;
            public const int P0_22 = ((int) 0x16);
            public const int P0_23 = ((int) 0x17);
            public const int P0_24 = ((int) 0x18);
            public const int P0_25 = ((int) 0x19);
            public const int P0_26 = ((int) 0x1a);
            public const int P0_27 = ((int) 0x1b);
            public const int P0_28 = ((int) 0x1c);
            public const int P0_3 = 3;
            public const int P0_4 = 4;
            public const int P0_5 = 5;
            public const int P0_6 = 6;
            public const int P1_11 = ((int) 0x2b);
            public const int P1_12 = ((int) 0x2c);
            public const int P1_19 = ((int) 0x33);
            public const int P1_2 = ((int) 0x22);
            public const int P1_20 = ((int) 0x34);
            public const int P1_21 = ((int) 0x35);
            public const int P1_22 = ((int) 0x36);
            public const int P1_23 = ((int) 0x37);
            public const int P1_24 = ((int) 0x38);
            public const int P1_25 = ((int) 0x39);
            public const int P1_26 = ((int) 0x3a);
            public const int P1_27 = ((int) 0x3b);
            public const int P1_28 = ((int) 60);
            public const int P1_29 = ((int) 0x3d);
            public const int P1_3 = ((int) 0x23);
            public const int P1_30 = ((int) 0x3e);
            public const int P1_31 = ((int) 0x3f);
            public const int P1_6 = ((int) 0x26);
            public const int P1_7 = ((int) 0x27);
            public const int P2_0 = ((int) 0x40);
            public const int P2_1 = ((int) 0x41);
            public const int P2_10 = ((int) 0x4a);
            public const int P2_12 = ((int) 0x4c);
            public const int P2_13 = ((int) 0x4d);
            public const int P2_2 = ((int) 0x42);
            public const int P2_21 = ((int) 0x55);
            public const int P2_22 = ((int) 0x56);
            public const int P2_23 = ((int) 0x57);
            public const int P2_25 = ((int) 0x59);
            public const int P2_26 = ((int) 90);
            public const int P2_27 = ((int) 0x5b);
            public const int P2_3 = ((int) 0x43);
            public const int P2_30 = ((int) 0x5e);
            public const int P2_31 = ((int) 0x5f);
            public const int P2_4 = ((int) 0x44);
            public const int P2_5 = ((int) 0x45);
            public const int P2_6 = ((int) 70);
            public const int P2_7 = ((int) 0x47);
            public const int P2_8 = ((int) 0x48);
            public const int P2_9 = ((int) 0x49);
            public const int P3_16 = ((int) 0x70);
            public const int P3_17 = ((int) 0x71);
            public const int P3_18 = ((int) 0x72);
            public const int P3_19 = ((int) 0x73);
            public const int P3_20 = ((int) 0x74);
            public const int P3_21 = ((int) 0x75);
            public const int P3_22 = ((int) 0x76);
            public const int P3_24 = ((int) 120);
            public const int P3_25 = ((int) 0x79);
            public const int P3_26 = ((int) 0x7a);
            public const int P3_27 = ((int) 0x7b);
            public const int P3_28 = ((int) 0x7c);
            public const int P3_29 = ((int) 0x7d);
            public const int P3_30 = ((int) 0x7e);
            public const int P3_31 = ((int) 0x7f);
            public const int P4_28 = ((int) 0x9c);
            public const int P4_29 = ((int) 0x9d);
            public const int P4_31 = ((int) 0x9f);
        }

        public static class PwmOutput
        {
            public const int P3_16 = 0;
            public const int P3_17 = 1;
            public const int P3_18 = 2;
            public const int P3_19 = 3;
            public const int P3_20 = 4;
            public const int P3_21 = 5;
            public const int P3_24 = 6;
            public const int P3_25 = 7;
            public const int P3_26 = ((int) 8);
            public const int P3_27 = ((int) 9);
            public const int P3_28 = ((int) 10);
            public const int P3_29 = ((int) 11);
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
        }
    }
}

