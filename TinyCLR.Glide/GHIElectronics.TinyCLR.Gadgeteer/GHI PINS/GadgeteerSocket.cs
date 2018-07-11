using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace TinyApp.GHI_PINS
{
    public class GadgeteerSocket
    {
        public int[] Pins { set; get; }
        public char[] SocketLabel { set; get; }
        public int SocketNumber { set; get; }
        public string SPI_Name { set; get; }
        public string UART_Name { set; get; }
        public string I2C_Name { set; get; }
        public string CAN_Name { set; get; }
        public GadgeteerSocket()
        {   //pin 1 = 3.3, pin2 = 5, pin 10 = GND
            Pins = new int[10];
        }

        GadgeteerSocket(int SocketNumber, char[] SocketLabels, int[] Pins3To9):this()
        {
            for (int i = 2; i < 9; i++)
            {
                if (Pins3To9.Length - 1 >= i - 2)
                    Pins[i] = Pins3To9[i - 2];
                else
                    Pins[i] = -999;
            }
            this.SocketNumber = SocketNumber;
            SocketLabel = SocketLabels;
        }

        public bool EnsureType(char[] SocketTypes)
        {   
            foreach(var c in SocketTypes)
            {
                foreach(var x in SocketLabel)
                {
                    if (x == c) return true;
                }
            }
            return false;
        }
    }
}
