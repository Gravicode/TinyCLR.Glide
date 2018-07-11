using System;
using GHIElectronics.TinyCLR.Devices.Gpio;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Spi;
using System.Drawing;

// ReSharper disable once CheckNamespace
namespace Gadgeteer.Modules.GHIElectronics
{
    /// <summary>A CharacterDisplay module for Microsoft .NET Gadgeteer</summary>
    public class DisplayN18 : Module
    {
        #region constants
        private const byte St7735Madctl = 0x36;
        private const byte MadctlMy = 0x80;
        private const byte MadctlMx = 0x40;
        private const byte MadctlMv = 0x20;
        private const byte MadctlBgr = 0x08;
        #endregion

        #region fields
        private readonly SpiDevice _dev;

        private readonly GpioPin _resetPin;
        private readonly GpioPin _backlightPin;
        private readonly GpioPin _rsPin;

        private readonly byte[] _byteArray;
        private readonly ushort[] _shortArray;
        private bool _isBgr;

        private readonly DisplayOrientation _orientation;
        #endregion

        #region property
        public int Height { get; }

        public int Width { get; }

        /// <summary>Whether or not the backlight is enabled.</summary>
        public bool BacklightEnabled
        {
            get => _backlightPin.Read() == GpioPinValue.High;
            set => _backlightPin.Write(value ? GpioPinValue.High : GpioPinValue.Low);
        }
        #endregion

        #region constructor
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="spiControllerName"></param>
        /// <param name="digitalResetPin3"> Pin3 of S socket which is the reset pin</param>
        /// <param name="digitalBackLightPin4">Pin4 of S socket which is the backlight pin</param>
        /// <param name="digitalRsPin5">Pin5 of S socket which is the RS pin</param>
        /// <param name="digitalCsPin6"></param>
        /// <param name="isBrg">Set to true if color must be in BRG order, false for RGB order mode</param>
        public DisplayN18(string spiControllerName, int digitalResetPin3, int digitalBackLightPin4, int digitalRsPin5, int digitalCsPin6, bool isBrg = false)
        {
            _orientation = DisplayOrientation.Normal;

            _byteArray = new byte[1];
            _shortArray = new ushort[2];
            _isBgr = isBrg;

            var controller = GpioController.GetDefault();

            _resetPin = controller.OpenPin(digitalResetPin3);
            _resetPin.SetDriveMode(GpioPinDriveMode.Output);

            _backlightPin = controller.OpenPin(digitalBackLightPin4);
            _backlightPin.SetDriveMode(GpioPinDriveMode.Output);
            BacklightEnabled = false;

            _rsPin = controller.OpenPin(digitalRsPin5);
            _rsPin.SetDriveMode(GpioPinDriveMode.Output);

            var settings = new SpiConnectionSettings(digitalCsPin6)
            {
                ClockFrequency = 12000000, // 12 000 kHz
                Mode = SpiMode.Mode0,
                SharingMode = SpiSharingMode.Exclusive,
                DataBitLength = 8
            };

            _dev = SpiDevice.FromId(spiControllerName, settings);

            Reset();

            ConfigureDisplay();

            Height = 160;
            Width = 128;
            Clear();
            BacklightEnabled = true;
        }
        #endregion

        #region methods
        private void Clear()
        {
            var data = new byte[64 * 80 * 2];

            if (_orientation == DisplayOrientation.Normal || _orientation == DisplayOrientation.UpsideDown)
            {
                DrawRaw(data, 0, 0, 64, 80);
                DrawRaw(data, 64, 0, 64, 80);
                DrawRaw(data, 0, 80, 64, 80);
                DrawRaw(data, 64, 80, 64, 80);
            }
            else
            {
                DrawRaw(data, 0, 0, 80, 64);
                DrawRaw(data, 80, 0, 80, 64);
                DrawRaw(data, 0, 64, 80, 64);
                DrawRaw(data, 80, 64, 80, 64);
            }
        }

        private void ConfigureDisplay()
        {
            WriteCommand(0x11);//Sleep exit
            Thread.Sleep(120);

            //ST7735R Frame Rate
            WriteCommand(0xB1);
            WriteData(0x01); WriteData(0x2C); WriteData(0x2D);
            WriteCommand(0xB2);
            WriteData(0x01); WriteData(0x2C); WriteData(0x2D);
            WriteCommand(0xB3);
            WriteData(0x01); WriteData(0x2C); WriteData(0x2D);
            WriteData(0x01); WriteData(0x2C); WriteData(0x2D);

            WriteCommand(0xB4); //Column inversion
            WriteData(0x07);

            //ST7735R Power Sequence
            WriteCommand(0xC0);
            WriteData(0xA2); WriteData(0x02); WriteData(0x84);
            WriteCommand(0xC1); WriteData(0xC5);
            WriteCommand(0xC2);
            WriteData(0x0A); WriteData(0x00);
            WriteCommand(0xC3);
            WriteData(0x8A); WriteData(0x2A);
            WriteCommand(0xC4);
            WriteData(0x8A); WriteData(0xEE);

            WriteCommand(0xC5); //VCOM
            WriteData(0x0E);

            SetOrientationOverride(DisplayOrientation.Normal);

            //ST7735R Gamma Sequence
            WriteCommand(0xe0);
            WriteData(0x0f); WriteData(0x1a);
            WriteData(0x0f); WriteData(0x18);
            WriteData(0x2f); WriteData(0x28);
            WriteData(0x20); WriteData(0x22);
            WriteData(0x1f); WriteData(0x1b);
            WriteData(0x23); WriteData(0x37); WriteData(0x00);

            WriteData(0x07);
            WriteData(0x02); WriteData(0x10);
            WriteCommand(0xe1);
            WriteData(0x0f); WriteData(0x1b);
            WriteData(0x0f); WriteData(0x17);
            WriteData(0x33); WriteData(0x2c);
            WriteData(0x29); WriteData(0x2e);
            WriteData(0x30); WriteData(0x30);
            WriteData(0x39); WriteData(0x3f);
            WriteData(0x00); WriteData(0x07);
            WriteData(0x03); WriteData(0x10);

            WriteCommand(0x2a);
            WriteData(0x00); WriteData(0x00);
            WriteData(0x00); WriteData(0x7f);
            WriteCommand(0x2b);
            WriteData(0x00); WriteData(0x00);
            WriteData(0x00); WriteData(0x9f);

            WriteCommand(0xF0); //Enable test command
            WriteData(0x01);
            WriteCommand(0xF6); //Disable ram power save mode
            WriteData(0x00);

            WriteCommand(0x3A); //65k mode
            WriteData(0x05);

            WriteCommand(0x29); //Display on
        }

        /// <summary>Draws an image to the screen.</summary>
        /// <param name="bitmap">The bitmap to be drawn to the screen</param>
        /// <param name="x">Starting X position of the image.</param>
        /// <param name="y">Starting Y position of the image.</param>
        public void DrawBitmap(Bitmap bitmap, int x, int y)
        {
            var vram = new byte[bitmap.Width * bitmap.Height * 2];
            NativeBitmapConverter(bitmap, vram);
            DrawRaw(vram, x, y, bitmap.Width, bitmap.Height);
        }

        /// <summary>
        /// Draws a string to the screen
        /// </summary>
        /// <param name="str">String to display</param>
        /// <param name="font">Font used to display text</param>
        /// <param name="x">Starting X position of the text</param>
        /// <param name="y">Starting Y position of the text</param>
        /// <param name="color">Color used to display text</param>
        public void DrawString(string str, Font font, int x, int y, Color color)
        {
            var image=new Bitmap(12*str.Length,15);
            var grap = Graphics.FromImage(image);
            grap.DrawString(str,font,new SolidBrush(color), 0,0 );
            DrawBitmap(image,x,y);
        }

        /// <summary>
        /// Draw a fill rectangle on display
        /// </summary>
        /// <param name="x">x coordinate of pixel</param>
        /// <param name="y">y coordinate of pixel</param>
        /// <param name="width">width of rectangle</param>
        /// <param name="height">height of rectangle</param>
        /// <param name="color">color of rectangle</param>
        public void DrawFillRect(int x, int y, int width, int height, Color color)
        {
            // Prepare data
            byte[] data = new byte[width * height * 2];
            byte[] a = color.To565ByteArray();
            for (int i = 0; i < width * height; i++)
            {
                data[i * 2] = a[0];
                data[i * 2 + 1] = a[1];
            }
            DrawRaw(data, x, y, width, height);
        }

        /// <summary>
        /// Draw a pixel on display
        /// </summary>
        /// <param name="x">x coordinate of pixel</param>
        /// <param name="y">y coordinate of pixel</param>
        /// <param name="color">color of pixel</param>
        public void DrawPixel(int x, int y, Color color)
        {
            byte[] a = color.To565ByteArray();
            DrawRaw(a, x, y, 1, 1);
        }

        private void DrawRaw(byte[] rawData, int x, int y, int width, int height)
        {
            var orientedWidth = Width;
            var orientedHeight = Height;

            if (_orientation == DisplayOrientation.Clockwise90Degrees || _orientation == DisplayOrientation.Counterclockwise90Degrees)
            {
                orientedWidth = Height;
                orientedHeight = Width;
            }

            if (x > orientedWidth || y > orientedHeight)
                return;

            if (x + width > orientedWidth)
                width = orientedWidth - x;

            if (y + height > orientedHeight)
                height = orientedHeight - y;

            SetClippingArea(x, y, width - 1, height - 1);
            WriteCommand(0x2C);
            WriteData(rawData);
        }

        private void NativeBitmapConverter(Bitmap bitmap, byte[] vram)
        {
            // TODO: method must be optimized: for display on full screen in FEZ Spider II it takes around 25 sec.
            int idx = 0;
            for (int j = 0; j < bitmap.Height; j++)
                for (int i = 0; i < bitmap.Width; i++)
                {
                    var ab = bitmap.GetPixel(i, j).To565ByteArray();
                    vram[idx] = ab[0];
                    vram[idx + 1] = ab[1];
                    idx += 2;
                }
        }

        private void Reset()
        {
            _resetPin.Write(GpioPinValue.Low);
            Thread.Sleep(150);
            _resetPin.Write(GpioPinValue.High);
        }
        private void SetClippingArea(int x, int y, int width, int height)
        {
            _shortArray[0] = (ushort)x;
            _shortArray[1] = (ushort)(x + width);
            WriteCommand(0x2A);
            WriteData(_shortArray);

            _shortArray[0] = (ushort)y;
            _shortArray[1] = (ushort)(y + height);
            WriteCommand(0x2B);
            WriteData(_shortArray);

        }

        private void SetFormat(DisplayOrientation orientation, bool isBgr)
        {
            WriteCommand(St7735Madctl);

            var bgr = (byte)(isBgr ? MadctlBgr : 0);
            switch (orientation)
            {
                case DisplayOrientation.Normal: WriteData((byte)(MadctlMx | MadctlMy | bgr)); break;
                case DisplayOrientation.Clockwise90Degrees: WriteData((byte)(MadctlMv | MadctlMy | bgr)); break;
                case DisplayOrientation.UpsideDown: WriteData(bgr); break;
                case DisplayOrientation.Counterclockwise90Degrees: WriteData((byte)(MadctlMv | MadctlMx | bgr)); break;
                default: throw new ArgumentException("orientation");
            }
        }
        private void SetOrientationOverride(DisplayOrientation orientation)
        {
            SetFormat(orientation, _isBgr);
            Thread.Sleep(1);
        }

        /// <summary>Swaps the red and blue channels if your display has them reversed.</summary>
        public void SwapRedBlueChannels()
        {
            _isBgr = !_isBgr;
            SetFormat(_orientation, _isBgr);
        }

        private void WriteCommand(byte command)
        {
            _byteArray[0] = command;
            _rsPin.Write(GpioPinValue.Low);
            _dev.Write(_byteArray);
        }

        private void WriteData(byte data)
        {
            _byteArray[0] = data;
            WriteData(_byteArray);
        }

        private void WriteData(byte[] byteArray)
        {
            _rsPin.Write(GpioPinValue.High);
            _dev.Write(byteArray);
        }

        private void WriteData(ushort[] data)
        {
            _rsPin.Write(GpioPinValue.High);
            foreach (ushort us in data)
            {
                // Data must be sent in reverse order
                byte[] bData = BitConverter.GetBytes(us);
                byte[] dataToSend = new byte[bData.Length];
                for (int i = 0; i < bData.Length; i++)
                {
                    dataToSend[dataToSend.Length - 1 - i] = bData[i];
                }
                _dev.Write(dataToSend);
            }
        }
        #endregion
    }

    // Extension methods to color
    public static class DisplayN18Extension
    {
        public static byte[] To565ByteArray(this Color color)
        {
            byte[] ba = new byte[2];
            var c = color.To565Color();
            ba[0] = (byte)(((c.R & 0x1f) << 3) | ((c.G >> 3) & 0x07));
            ba[1] = (byte)(((c.G & 0x07) << 5) | (c.B & 0x1f));
            return ba;
        }

        private static Color To565Color(this Color color)
        {
            var c = Color.FromArgb((byte)(color.R / 255.0 * 31), (byte)(color.G / 255.0 * 63),
                (byte)(color.B / 255.0 * 31));
            return c;
        }
    }
}
