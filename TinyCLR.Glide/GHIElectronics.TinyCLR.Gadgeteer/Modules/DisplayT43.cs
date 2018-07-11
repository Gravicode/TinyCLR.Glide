using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.Devices.Gpio;
using System.Drawing;

// ReSharper disable once CheckNamespace
namespace Gadgeteer.Modules.GHIElectronics
{
    /// <summary>A DisplayTE43 module for Microsoft .NET Gadgeteer.</summary>
    public class DisplayT43 : Module
    {
        private readonly GpioPin _backlightPin;

        /// <summary>Whether or not the backlight is enabled.</summary>
        public bool BacklightEnabled
        {
            get => _backlightPin.Read() == GpioPinValue.High;

            set => _backlightPin.Write(value ? GpioPinValue.High : GpioPinValue.Low);
        }

        public Graphics Screen { set; get; }

        /// <summary>Constructs a new instance.</summary>
        /// <param name="digitalPin9OnGSocket">Pin 9 on Socket G.</param>
        public DisplayT43(int digitalPin9OnGSocket)
        {
            var controller = GpioController.GetDefault();
            _backlightPin = controller.OpenPin(digitalPin9OnGSocket);
            _backlightPin.SetDriveMode(GpioPinDriveMode.Output);
            BacklightEnabled = true;
            var displayController = DisplayController.GetDefault(); //Currently returns the hardware LCD controller by default

            //Enables the display
            displayController.ApplySettings(new ParallelDisplayControllerSettings
            {
                Width = 480,
                Height = 272,
                PixelClockRate = 22166,
                PixelPolarity = false,
                OutputEnablePolarity = true,
                OutputEnableIsFixed = false,
                HorizontalFrontPorch = 2,
                HorizontalBackPorch = 2,
                HorizontalSyncPulseWidth = 41,
                HorizontalSyncPolarity = false,
                VerticalFrontPorch = 2,
                VerticalBackPorch = 2,
                VerticalSyncPulseWidth = 10,
                VerticalSyncPolarity = false,
            });

            Screen = Graphics.FromHdc(displayController.Hdc); //Calling flush on the object returned will flush to the display represented by Hdc. Only one active display is supported at this time.

        }

        /// <summary>Renders display data on the display device.</summary>
        /// <param name="bitmap">The bitmap object to render on the display.</param>
        /// <param name="x">The start x coordinate of the dirty area.</param>
        /// <param name="y">The start y coordinate of the dirty area.</param>
        /// <param name="width">The width of the dirty area.</param>
        /// <param name="height">The height of the dirty area.</param>
        public void Paint(Bitmap bitmap, int x, int y, int width, int height)
        {
            try
            {
                Screen.DrawImage(bitmap, x, y);
                Screen.Flush();
            }
            catch
            {
                ErrorPrint("Painting error");
            }
        }
    }
}