using GHIElectronics.TinyCLR.Devices.Enumeration;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.I2c;
using System;
using System.Threading;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
    /// <summary>A Compass module for Microsoft .NET Gadgeteer</summary>
    public class Compass : GTM.Module {
        private GpioPin dataReady;
        private I2cDevice i2c;
        private Timer timer;
        private byte[] writeBuffer1;
        private byte[] readBuffer6;
        private TimeSpan TimerInterval;
        private AutoResetEvent autoEvent;
        private bool IsRunning = false;
        private int I2C_ADDRESS = 0x1E;
        TimerBehaviors Behavior = TimerBehaviors.RunOnce;
        public enum TimerBehaviors { RunContinuously, RunOnce }


        private MeasurementCompleteEventHandler onMeasurementComplete;

        /// <summary>Represents the delegate used for the MeasurementComplete event.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void MeasurementCompleteEventHandler(Compass sender, MeasurementCompleteEventArgs e);

        /// <summary>Raised when a measurement reading is complete.</summary>
        public event MeasurementCompleteEventHandler MeasurementComplete;

        /// <summary>The interval at which measurements are taken.</summary>
        public TimeSpan MeasurementInterval
        {
            get
            {
                return this.TimerInterval;
            }
            set
            {
                var wasRunning = this.IsRunning;

                if (timer != null)
                    timer.Dispose();
                this.TimerInterval = value;

                if (wasRunning)
                    StartTimer();
            }
        }

        void StartTimer()
        {

            this.timer = new Timer(new TimerCallback((a) => { this.TakeMeasurement(); }), autoEvent, new TimeSpan(0, 0, 0), TimerInterval);
        }

        /// <summary>Whether or not the driver is currently taking measurements.</summary>
        public bool IsTakingMeasurements {
            get {
                return this.IsRunning;
            }
        }

        /// <summary>Possible sensing gain values.</summary>
        public enum Gain : byte {

            /// <summary>+ /- 0.88 Ga</summary>
            Gain1 = 0x00,

            /// <summary>+ /- 1.2 Ga (default)</summary>
            Gain2 = 0x20,

            /// <summary>+ /- 1.9 Ga</summary>
            Gain3 = 0x40,

            /// <summary>+ /- 2.5 Ga</summary>
            Gain4 = 0x60,

            /// <summary>+ /- 4.0 Ga</summary>
            Gain5 = 0x80,

            /// <summary>+ /- 4.7 Ga</summary>
            Gain6 = 0xA0,

            /// <summary>+ /- 5.6 Ga</summary>
            Gain7 = 0xC0,

            /// <summary>+ /- 8.1 Ga</summary>
            Gain8 = 0xE0,
        }

        private enum Register : byte {
            CRA = 0x00,
            CRB = 0x01,
            MR = 0x02,
            DXRA = 0x03,
            DXRB = 0x04,
            DZRA = 0x05,
            DZRB = 0x06,
            DYRA = 0x07,
            DYRB = 0x08,
            SR = 0x09,
            IRA = 0x0A,
            IRB = 0x0B,
            IRC = 0x0C
        }

        private enum Mode : byte {
            Continous = 0x00,
            SingleMode = 0x01,
            IdleMode = 0x02,
            SleepMode = 0x03
        }

        /// <summary>Constructs a new instance.</summary>
        /// <param name="I2CBus">I2C bus on socket.</param>
        public Compass(string I2CBus,int DigitalPin3) {
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            //socket.EnsureTypeIsSupported('I', this);

            this.writeBuffer1 = new byte[1];
            this.readBuffer6 = new byte[6];

            var Devices = DeviceInformation.FindAll(I2CBus);

            // Device I2C1 Slave address
            I2cConnectionSettings Setting = new I2cConnectionSettings(I2C_ADDRESS);
            Setting.BusSpeed = I2cBusSpeed.StandardMode; // 100kHz
            i2c = I2cDevice.FromId(Devices[0].Id, Setting);

            //this.i2c = I2cDeviceFactory.Create(socket, 0x68, 100, this);

            TimerInterval = new TimeSpan(0, 0, 0, 0, 200);
            autoEvent = new AutoResetEvent(false);
            var controller = GpioController.GetDefault();
            this.dataReady = controller.OpenPin(DigitalPin3);//GTI.InterruptdataReadyFactory.Create(socket, GT.Socket.Pin.Three, GTI.GlitchFilterMode.On, GTI.ResistorMode.PullUp, GTI.InterruptMode.RisingAndFallingEdge, this);
            if (dataReady.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                dataReady.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                dataReady.SetDriveMode(GpioPinDriveMode.Input);
            dataReady.ValueChanged += OnInterrupt;

            //this.dataReady.Interrupt += this.OnInterrupt;
            //this.timer = new Timer(200);
            //this.timer.Tick += (a) => this.TakeMeasurement();

            //this.dataReady = GpioPinFactory.Create(socket, Socket.Pin.Three, GTI.GlitchFilterMode.Off, GTI.ResistorMode.Disabled, GTI.InterruptMode.RisingEdge, this);
            //this.dataReady.Interrupt += this.OnInterrupt;

            //this.i2c = I2cDeviceFactory.Create(socket, 0x1E, 100, this);
        }

        private void OnInterrupt(object sender, GpioPinValueChangedEventArgs e)
        {
            this.writeBuffer1[0] = (byte)Register.DXRA;
            this.i2c.WriteRead(this.writeBuffer1, this.readBuffer6);

            int rawX = (this.readBuffer6[0] << 8) | this.readBuffer6[1];
            int rawZ = (this.readBuffer6[2] << 8) | this.readBuffer6[3];
            int rawY = (this.readBuffer6[4] << 8) | this.readBuffer6[5];

            rawX = ((rawX >> 15) == 1 ? -32767 : 0) + (rawX & 0x7FFF);
            rawZ = ((rawZ >> 15) == 1 ? -32767 : 0) + (rawZ & 0x7FFF);
            rawY = ((rawY >> 15) == 1 ? -32767 : 0) + (rawY & 0x7FFF);

            if (rawX == -4096 || rawY == -4096 || rawZ == -4096)
            {
                this.DebugPrint("Invalid data read. Measurement discarded.");

                return;
            }

            this.OnMeasurementComplete(this, new MeasurementCompleteEventArgs(Math.Atan2((double)rawY, (double)rawX) * (180 / 3.14159265) + 180, rawX, rawY, rawZ));
            autoEvent.Set();

        }

        /// <summary>Sets the sensor gain value.</summary>
        /// <param name="gain">The gain value.</param>
        public void SetGain(Gain gain) {
            byte[] data = new byte[] { (byte)Register.CRB, (byte)gain };
            this.i2c.Write(data);
        }
        /// <summary>Obtains a single measurement and raises the event when complete.</summary>
		public void RequestSingleMeasurement()
        {

            if (this.Behavior == TimerBehaviors.RunContinuously && this.IsTakingMeasurements) throw new InvalidOperationException("You cannot request a single measurement while continuous measurements are being taken.");

            this.Behavior = TimerBehaviors.RunOnce; //Timer.BehaviorType.RunOnce;
            StartTimer();
            IsRunning = true;
            autoEvent.WaitOne();
            this.timer.Dispose();
            IsRunning = false;
            //this.timer.Start();
        }

        /// <summary>Starts taking measurements and fires MeasurementComplete when a new measurement is available.</summary>
        public void StartTakingMeasurements()
        {
            if (timer != null) { timer.Dispose(); IsRunning = false; }
            this.Behavior = TimerBehaviors.RunContinuously;
            StartTimer();
            IsRunning = true;
            //this.timer.Start();
        }

        /// <summary>Stops taking measurements.</summary>
        public void StopTakingMeasurements()
        {
            this.timer.Dispose();
            IsRunning = false;
            //this.timer.Stop();
        }


        private void TakeMeasurement() {
            byte[] data = new byte[] { (byte)Register.MR, (byte)Mode.SingleMode };
            this.i2c.Write(data);
        }

       

        private void OnMeasurementComplete(Compass sender, MeasurementCompleteEventArgs e) {
            if (this.onMeasurementComplete == null)
                this.onMeasurementComplete = this.OnMeasurementComplete;
            
            if (this.MeasurementComplete != null)
                this.MeasurementComplete(sender, e);
        }
        /// <summary>Event arguments for the MeasurementComplete event.</summary>
        public class MeasurementCompleteEventArgs  {

            /// <summary>X-axis sensor data.</summary>
            public int X { get; private set; }

            /// <summary>Y-axis sensor data.</summary>
            public int Y { get; private set; }

            /// <summary>Z-axis sensor data.</summary>
            public int Z { get; private set; }

            /// <summary>Angle of heading in the XY plane in radians.</summary>
            public double Angle { get; private set; }

            internal MeasurementCompleteEventArgs(double angle, int x, int y, int z) {
                this.Angle = angle;
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            /// <summary>Provides a string representation of the instance.</summary>
            /// <returns>A string describing the values contained in the object.</returns>
            public override string ToString() {
                return "Angle: " + Angle.ToString("f2") + " X: " + X.ToString("f2") + " Y: " + Y.ToString("f2") + " Z: " + Z.ToString("f2");
            }
        }
    }
}