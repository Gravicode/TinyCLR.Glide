//using Microsoft.SPOT;
//using Microsoft.SPOT.Hardware;
using GHIElectronics.TinyCLR.Devices.Display;
using GHIElectronics.TinyCLR.Devices.Enumeration;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.I2c;
using System;
using System.Drawing;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics
{
    /// <summary>A DisplayNHVN module for Microsoft .NET Gadgeteer.</summary>
    public class DisplayNHVN : GTM.Module
    {

        public enum DisplayTypes { Display7inch, Display43Inch }

        private DisplayTypes DisplayType;
        private GpioPin backlightPin;

        private delegate void NullParamsDelegate();

        /// <summary>The delegate that is used to handle the capacitive touch events.</summary>
        /// <param name="sender">The DisplayNHVN object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void CapacitiveTouchEventHandler(DisplayNHVN sender, TouchEventArgs e);

        /// <summary>Raised when the module detects a capacitive press.</summary>
        public event CapacitiveTouchEventHandler CapacitiveScreenPressed;

        /// <summary>Raised when the module detects a capacitive release.</summary>
        public event CapacitiveTouchEventHandler CapacitiveScreenReleased;


        /// <summary>Constructs a new instance.</summary>
        /// <param name="Pin9OnGSocket">Backlight pin.</param>
        /// <param name="Pin3OnISocket">Interrupt pin for capacitive touch panel (i2c).</param>
        public DisplayNHVN(string I2CBus, int Pin9OnGSocket, int Pin3OnISocket, DisplayTypes DisplayType)
        {
            this.DisplayType = DisplayType;
            this.I2CBus = I2CBus;
            var controller = GpioController.GetDefault();
            this.backlightPin = controller.OpenPin(Pin9OnGSocket);
            this.backlightPin.SetDriveMode(GpioPinDriveMode.Output);
            BacklightEnabled = true;
            if (Pin3OnISocket > 0)
            {
                SetupCapacitiveTouchController(Pin3OnISocket);
            }
            ConfigureDisplay();
            //this.backlightPin = GpioPinFactory.Create(gSocket, Socket.Pin.Nine, true, this);


        }

        /// <summary>Whether or not the backlight is enabled.</summary>
        public bool BacklightEnabled
        {
            get
            {
                return this.backlightPin.Read() == GpioPinValue.High;
            }

            set
            {
                this.backlightPin.Write(value ? GpioPinValue.High : GpioPinValue.Low);
            }
        }

        public Graphics Screen { set; get; }

        /// <summary>Constructs a new instance.</summary>
        /// <param name="DigitalPin9onGSocket">Pin 9 on Socket G.</param>
        public void ConfigureDisplay()
        {

            var displayController = DisplayController.GetDefault(); //Currently returns the hardware LCD controller by default

            if (DisplayType == DisplayTypes.Display7inch)
            {
                //Enables the display
                displayController.ApplySettings(new ParallelDisplayControllerSettings
                {
                    Width = 800,
                    Height = 480,
                    PixelClockRate = 20000,
                    PixelPolarity = false,
                    OutputEnablePolarity = true,
                    OutputEnableIsFixed = false,
                    HorizontalFrontPorch = 40,
                    HorizontalBackPorch = 88,
                    HorizontalSyncPulseWidth = 48,
                    HorizontalSyncPolarity = false,
                    VerticalFrontPorch = 13,
                    VerticalBackPorch = 32,
                    VerticalSyncPulseWidth = 3,
                    VerticalSyncPolarity = false,
                });
                /*
                UsesCommonSyncPin = false, //not the proper property, but we needed it for OutputEnableIsFixed
				CommonSyncPinIsActiveHigh = true, //not the proper property, but we needed it for OutputEnablePolarity
				PixelDataIsValidOnClockRisingEdge = false,
				MaximumClockSpeed = 20000,
				HorizontalSyncPulseIsActiveHigh = false,
				HorizontalSyncPulseWidth = 48,
				HorizontalBackPorch = 88,
				HorizontalFrontPorch = 40,
				VerticalSyncPulseIsActiveHigh = false,
				VerticalSyncPulseWidth = 3,
				VerticalBackPorch = 32,
				VerticalFrontPorch = 13,
                 800, 480
                 */
            }
            else if (DisplayType == DisplayTypes.Display43Inch)
            {
                displayController.ApplySettings(new ParallelDisplayControllerSettings
                {
                    Width = 480,
                    Height = 272,
                    PixelClockRate = 20000,
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
                /*
                UsesCommonSyncPin = false, //not the proper property, but we needed it for OutputEnableIsFixed
				CommonSyncPinIsActiveHigh = true, //not the proper property, but we needed it for OutputEnablePolarity
				PixelDataIsValidOnClockRisingEdge = false,
				MaximumClockSpeed = 20000,
				HorizontalSyncPulseIsActiveHigh = false,
				HorizontalSyncPulseWidth = 41,
				HorizontalBackPorch = 2,
				HorizontalFrontPorch = 2,
				VerticalSyncPulseIsActiveHigh = false,
				VerticalSyncPulseWidth = 10,
				VerticalBackPorch = 2,
				VerticalFrontPorch = 2,
                480, 272,   
             */
            }
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
                this.ErrorPrint("Painting error");
            }
        }






        /// <summary>
        /// Event arguments for the capacitive touch events.
        /// </summary>
        public class TouchEventArgs
        {
            /// <summary>
            /// The X coordinate of the touch event.
            /// </summary>
            public int X { get; set; }

            /// <summary>
            /// The Y coordinate of the touch event.
            /// </summary>
            public int Y { get; set; }

            internal TouchEventArgs(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        #region TouchController
        //this is i2c touch driver by Dave McLaughin, and I have ported to TinyCLR, thanks mate ;) 
        private GpioPin touchInterrupt;
        private I2cDevice i2cBus;
        //private I2cDevice.I2CTransaction[] transactions;
        private byte[] addressBuffer;
        private byte[] resultBuffer;



        private string I2CBus { set; get; }
        const int I2C_ADDRESS = 0x38;


        private void SetupCapacitiveTouchController(int portId)
        {
            var Devices = DeviceInformation.FindAll(I2CBus);

            // Device I2C1 Slave address
            I2cConnectionSettings Setting = new I2cConnectionSettings(I2C_ADDRESS);
            Setting.BusSpeed = I2cBusSpeed.FastMode; // 400kHz
            i2cBus = I2cDevice.FromId(Devices[0].Id, Setting);
            resultBuffer = new byte[1];
            addressBuffer = new byte[1];
            //i2cBus = new I2cDevice(new I2cDevice.Configuration(0x38, 400));

            var controller = GpioController.GetDefault();
            this.touchInterrupt = controller.OpenPin(portId);
            this.touchInterrupt.SetDriveMode(GpioPinDriveMode.Input);
            //touchInterrupt = new InterruptPort(portId, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            touchInterrupt.ValueChanged += TouchInterrupt_ValueChanged;
         
            //+= (object sender, GpioPinValueChangedEventArgs e) =>

            //touchInterrupt.OnInterrupt += (a, b, c) => this.OnTouchEvent();


        }

        private void TouchInterrupt_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            this.OnTouchEvent();
        }

        private void OnTouchEvent()
        {
            for (var i = 0; i < 5; i++)
            {
                var first = this.ReadRegister((byte)(3 + i * 6));
                var x = ((first & 0x0F) << 8) + this.ReadRegister((byte)(4 + i * 6));
                var y = ((this.ReadRegister((byte)(5 + i * 6)) & 0x0F) << 8) + this.ReadRegister((byte)(6 + i * 6));

                if (x == 4095 && y == 4095)
                    break;
                if (((first & 0xC0) >> 6) == 1)
                {
                    if (this.CapacitiveScreenReleased != null)
                        this.CapacitiveScreenReleased(this, new TouchEventArgs(x, y));
                }
                else
                {
                    if (this.CapacitiveScreenPressed != null)
                        this.CapacitiveScreenPressed(this, new TouchEventArgs(x, y));
                }
                /*
                if (((first & 0xC0) >> 6) == 1)
                    GlideTouch.RaiseTouchUpEvent(null, new GHI.Glide.TouchEventArgs(new Point(x, y)));
                else
                    GlideTouch.RaiseTouchDownEvent(null, new GHI.Glide.TouchEventArgs(new Point(x, y)));
                */
            }
        }

        private byte ReadRegister(byte address)
        {
            this.addressBuffer[0] = address;

            //this.transactions[0] = I2cDevice.CreateWriteTransaction(this.addressBuffer);
            //this.transactions[1] = I2cDevice.CreateReadTransaction(this.resultBuffer);
            //, 1000
            this.i2cBus.WriteRead(this.addressBuffer, this.resultBuffer);

            return this.resultBuffer[0];
        }
        #endregion
    }
}