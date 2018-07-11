//using Microsoft.SPOT;
using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.Devices.Gpio;
using System;
using System.Drawing;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A DisplayTE35 module for Microsoft .NET Gadgeteer.</summary>
	public class DisplayT35 : GTM.Module {
		private GpioPin backlightPin;
        public const int ScreenWidth = 320;     // 0- 319
        public const int ScreenHeight = 240;    // 0 - 239

        /// <summary>Whether or not the backlight is enabled.</summary>
        public bool BacklightEnabled {
			get {
				return this.backlightPin.Read()==GpioPinValue.High;
			}

			set {
				this.backlightPin.Write(value?GpioPinValue.High:GpioPinValue.Low);
			}
		}

        public Graphics Screen { set; get; }

        /// <summary>Constructs a new instance.</summary>
        /// <param name="DigitalPin9onGSocket">Pin 9 on Socket G.</param>
        public DisplayT35(int DigitalPin9onGSocket) {
            var controller = GpioController.GetDefault();
            this.backlightPin = controller.OpenPin(DigitalPin9onGSocket);
            this.backlightPin.SetDriveMode(GpioPinDriveMode.Output);
            this.BacklightEnabled = true;
            var displayController = DisplayController.GetDefault(); //Currently returns the hardware LCD controller by default
                                                                    
            //Enables the display
            displayController.ApplySettings(new ParallelDisplayControllerSettings
            {
                Width = ScreenWidth,
                Height = ScreenHeight,
                PixelClockRate = 16625,
                PixelPolarity = true,
                OutputEnablePolarity = true,
                OutputEnableIsFixed = true,
                HorizontalFrontPorch = 51,
                HorizontalBackPorch = 29, // Was 27 causing 2 pixel shift x axis
                HorizontalSyncPulseWidth = 41,
                HorizontalSyncPolarity = false,
                VerticalFrontPorch = 16,
                VerticalBackPorch = 3, // Was 8
                VerticalSyncPulseWidth = 10,
                VerticalSyncPolarity = false,
            });

            Screen = Graphics.FromHdc(displayController.Hdc); //Calling flush on the object returned will flush to the display represented by Hdc. Only one active display is supported at this time.
          
        }

		/// <summary>Constructs a new instance.</summary>
		/// <param name="rSocketNumber">The mainboard socket that has the display's R socket connected to it.</param>
		/// <param name="gSocketNumber">The mainboard socket that has the display's G socket connected to it.</param>
		/// <param name="bSocketNumber">The mainboard socket that has the display's B socket connected to it.</param>
		/// <param name="tSocketNumber">The mainboard socket that has the display's T socket connected to it.</param>
		

		/// <summary>Renders display data on the display device.</summary>
		/// <param name="bitmap">The bitmap object to render on the display.</param>
		/// <param name="x">The start x coordinate of the dirty area.</param>
		/// <param name="y">The start y coordinate of the dirty area.</param>
		/// <param name="width">The width of the dirty area.</param>
		/// <param name="height">The height of the dirty area.</param>
		public void Paint(Bitmap bitmap, int x, int y, int width, int height) {
			try {
                Screen.DrawImage(bitmap, x, y);
                Screen.Flush();
            }
			catch {
				this.ErrorPrint("Painting error");
			}
		}
	}
}