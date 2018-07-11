//using Microsoft.SPOT;
using GHIElectronics.TinyCLR.Devices.Enumeration;
using GHIElectronics.TinyCLR.Devices.I2c;
using System;
using System.Threading;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A Gyro module for Microsoft .NET Gadgeteer.</summary>
	public class Gyro : GTM.Module {
		private Timer timer;
		private I2cDevice i2c;
		private byte[] readBuffer1;
		private byte[] writeBuffer1;
		private byte[] writeBuffer2;
		private byte[] readBuffer8;
		private double offsetX;
		private double offsetY;
		private double offsetZ;
        const int I2C_ADDRESS = 0x68;
        TimerBehaviors Behavior = TimerBehaviors.RunOnce;
        private bool IsRunning = false;
        public enum TimerBehaviors { RunContinuously , RunOnce}
        private TimeSpan TimerInterval;
        private AutoResetEvent autoEvent;

        private MeasurementCompleteEventHandler onMeasurementComplete;

		/// <summary>Represents the delegate used for the MeasurementComplete event.</summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The event arguments.</param>
		public delegate void MeasurementCompleteEventHandler(Gyro sender, MeasurementCompleteEventArgs e);

		/// <summary>Raised when a measurement reading is complete.</summary>
		public event MeasurementCompleteEventHandler MeasurementComplete;

		/// <summary>
		/// The low pass filter configuration. Note that setting the low pass filter to 256Hz results in a maximum internal sample rate of 8kHz. Any other setting results in a maximum sample rate of
		/// 1kHz. The sample rate can be further divided by using SampleRateDivider.
		/// </summary>
		public Bandwidth LowPassFilter {
			get {
				return (Bandwidth)(this.Read(Register.Scale) & 0x7);
			}
			set {
				this.Write(Register.Scale, (byte)((byte)value | 0x18));
			}
		}

		/// <summary>
		/// the internal sample rate divider. The gyro outputs are sampled internally at either 8kHz (if the LowPassFilter is set to 256Hz) or 1kHz for any other LowPassFilter settings. This setting
		/// can be used to further divide the sample rate.
		/// </summary>
		public byte SampleRateDivider {
			get {
				return this.Read(Register.SampleRateDivider);
			}
			set {
				this.Write(Register.SampleRateDivider, value);
			}
		}

		/// <summary>The interval at which measurements are taken.</summary>
		public TimeSpan MeasurementInterval {
			get {
				return this.TimerInterval;
			}
			set {
				var wasRunning = this.IsRunning;

                if(timer!=null)
                timer.Dispose();
                this.TimerInterval = value;

                if (wasRunning)
                    StartTimer();
			}
		}

		/// <summary>Whether or not the driver is currently taking measurements.</summary>
		public bool IsTakingMeasurements {
			get {
				return this.IsRunning;
			}
		}

		/// <summary>Available low pass filter bandwidth settings.</summary>
		public enum Bandwidth {
			/// <summary>256Hz</summary>
			Hertz256 = 0,
			/// <summary>188Hz</summary>
			Hertz188 = 1,
			/// <summary>98Hz</summary>
			Hertz98 = 2,
			/// <summary>42Hz</summary>
			Hertz42 = 3,
			/// <summary>20Hz</summary>
			Hertz20 = 4,
			/// <summary>10Hz</summary>
			Hertz10 = 5,
			/// <summary>5Hz</summary>
			Hertz5 = 6
		}

		private enum Register : byte {
			SampleRateDivider = 0x15,
			Scale = 0x16,
			TemperatureOutHigh = 0x1B,
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public Gyro(string I2CBus) {
			//Socket socket = Socket.GetSocket(socketNumber, true, this, null);
			//socket.EnsureTypeIsSupported('I', this);

			this.readBuffer1 = new byte[1];
			this.writeBuffer1 = new byte[1];
			this.writeBuffer2 = new byte[2];
			this.readBuffer8 = new byte[8];

			this.offsetX = 0;
			this.offsetY = 0;
			this.offsetZ = 0;
                                                                                                                                                     //this.timer.Tick += (a) => this.TakeMeasurement();
            var Devices = DeviceInformation.FindAll(I2CBus);

            // Device I2C1 Slave address
            I2cConnectionSettings Setting = new I2cConnectionSettings(I2C_ADDRESS);
            Setting.BusSpeed = I2cBusSpeed.StandardMode; // 100kHz
            i2c = I2cDevice.FromId(Devices[0].Id, Setting);

            //this.i2c = I2cDeviceFactory.Create(socket, 0x68, 100, this);

            this.SetFullScaleRange();
            TimerInterval = new TimeSpan(0, 0, 0, 0, 200);
            autoEvent = new AutoResetEvent(false);
            //StartTimer(); //new GT.Timer(200);

        }

        void StartTimer()
        {
            
            this.timer = new Timer(new TimerCallback((a) => { this.TakeMeasurement(); }), autoEvent, new TimeSpan(0, 0, 0),TimerInterval);
        }

		/// <summary>Calibrates the gyro values. Ensure that the sensor is not moving when calling this method.</summary>
		public void Calibrate() {
			this.Read(Register.TemperatureOutHigh, this.readBuffer8);
			int rawT = (this.readBuffer8[0] << 8) | this.readBuffer8[1];
			int rawX = (this.readBuffer8[2] << 8) | this.readBuffer8[3];
			int rawY = (this.readBuffer8[4] << 8) | this.readBuffer8[5];
			int rawZ = (this.readBuffer8[6] << 8) | this.readBuffer8[7];

			rawT = (((rawT >> 15) == 1) ? -32767 : 0) + (rawT & 0x7FFF);
			rawX = (((rawX >> 15) == 1) ? -32767 : 0) + (rawX & 0x7FFF);
			rawY = (((rawY >> 15) == 1) ? -32767 : 0) + (rawY & 0x7FFF);
			rawZ = (((rawZ >> 15) == 1) ? -32767 : 0) + (rawZ & 0x7FFF);

			this.offsetX = rawX / -14.375;
			this.offsetY = rawY / -14.375;
			this.offsetZ = rawZ / -14.375;
		}

		/// <summary>Obtains a single measurement and raises the event when complete.</summary>
		public void RequestSingleMeasurement() {
            
			if (this.Behavior==TimerBehaviors.RunContinuously && this.IsTakingMeasurements) throw new InvalidOperationException("You cannot request a single measurement while continuous measurements are being taken.");
            
            this.Behavior = TimerBehaviors.RunOnce; //Timer.BehaviorType.RunOnce;
            StartTimer();
            IsRunning = true;
            autoEvent.WaitOne();
            this.timer.Dispose();
            IsRunning = false;
			//this.timer.Start();
		}

		/// <summary>Starts taking measurements and fires MeasurementComplete when a new measurement is available.</summary>
		public void StartTakingMeasurements() {
            if (timer != null) { timer.Dispose();IsRunning = false; }
            this.Behavior = TimerBehaviors.RunContinuously;
            StartTimer();
            IsRunning = true;
			//this.timer.Start();
		}

		/// <summary>Stops taking measurements.</summary>
		public void StopTakingMeasurements() {
            this.timer.Dispose();
            IsRunning = false;
            //this.timer.Stop();
		}

		private void SetFullScaleRange() {
			this.Write(Register.Scale, (byte)(this.Read(Register.Scale) | 0x18));
		}

		private byte Read(Register register) {
			this.writeBuffer1[0] = (byte)register;
			this.i2c.WriteRead(this.writeBuffer1, this.readBuffer1);
			return this.readBuffer1[0];
		}

		private void Read(Register register, byte[] readBuffer) {
			this.writeBuffer1[0] = (byte)register;
			this.i2c.WriteRead(this.writeBuffer1, readBuffer);
		}

		private void Write(Register register, byte value) {
			this.writeBuffer2[0] = (byte)register;
			this.writeBuffer2[1] = (byte)value;
			this.i2c.Write(this.writeBuffer2);
		}

		private void TakeMeasurement() {
			this.Read(Register.TemperatureOutHigh, this.readBuffer8);

			int rawT = (this.readBuffer8[0] << 8) | this.readBuffer8[1];
			int rawX = (this.readBuffer8[2] << 8) | this.readBuffer8[3];
			int rawY = (this.readBuffer8[4] << 8) | this.readBuffer8[5];
			int rawZ = (this.readBuffer8[6] << 8) | this.readBuffer8[7];

			rawT = (((rawT >> 15) == 1) ? -32767 : 0) + (rawT & 0x7FFF);
			rawX = (((rawX >> 15) == 1) ? -32767 : 0) + (rawX & 0x7FFF);
			rawY = (((rawY >> 15) == 1) ? -32767 : 0) + (rawY & 0x7FFF);
			rawZ = (((rawZ >> 15) == 1) ? -32767 : 0) + (rawZ & 0x7FFF);

			double x = (rawX / 14.375) + this.offsetX;
			double y = (rawY / 14.375) + this.offsetY;
			double z = (rawZ / 14.375) + this.offsetZ;
			double t = (rawT + 13200) / 280.0 + 35;
			this.OnMeasurementComplete(this, new MeasurementCompleteEventArgs(x, y, z, t));
            autoEvent.Set();

        }

        private void OnMeasurementComplete(Gyro sender, MeasurementCompleteEventArgs e) {
			if (this.onMeasurementComplete == null)
				this.onMeasurementComplete = this.OnMeasurementComplete;

            //if (Program.CheckAndInvoke(this.MeasurementComplete, this.onMeasurementComplete, sender, e))
            if(this.MeasurementComplete!=null)
                this.MeasurementComplete(sender, e);
		}

		/// <summary>Event arguments for the MeasurementComplete event.</summary>
		public class MeasurementCompleteEventArgs {
			/// <summary>Angular rate around the X axis (roll) in degree per second.</summary>
			public double X { get; private set; }

			/// <summary>Angular rate around the Y axis (pitch) in degree per second.</summary>
			public double Y { get; private set; }

			/// <summary>Angular rate around the Z axis (yaw) in degree per second.</summary>
			public double Z { get; private set; }

			/// <summary>Temperature in degree celsius.</summary>
			public double Temperature { get; private set; }

			internal MeasurementCompleteEventArgs(double x, double y, double z, double temperature) {
				this.X = x;
				this.Y = y;
				this.Z = z;
				this.Temperature = temperature;
			}

			/// <summary>Provides a string representation of the instance.</summary>
			/// <returns>A string describing the values contained in the object.</returns>
			public override string ToString() {
				return "X: " + X.ToString("f2") + " Y: " + Y.ToString("f2") + " Z: " + Z.ToString("f2") + " Temperature: " + Temperature.ToString("f2");
			}
		}
	}
}