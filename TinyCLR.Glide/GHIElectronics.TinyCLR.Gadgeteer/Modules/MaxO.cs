using GHIElectronics.TinyCLR.Devices.Enumeration;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.Spi;
using System;
using System.Threading;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A MaxO module for Microsoft .NET Gadgeteer</summary>
	public class MaxO : GTM.Module {
		private SpiDevice spi;
		private GpioPin enable;
		private GpioPin clr;
		private byte[] data;
		private int boards;

		/// <summary>The number modules chained together.</summary>
		public int Boards {
			get {
				return this.boards;
			}

			set {
				if (this.data != null) throw new InvalidOperationException("You may only set boards once.");
				if (value <= 0) throw new ArgumentOutOfRangeException("value", "value must be positive.");

				this.boards = value;
				this.data = new byte[value * 4];
			}
		}

		/// <summary>The size of the array to be filled.</summary>
		public int ArraySize {
			get {
				return this.data.Length;
			}
		}

		/// <summary>Sets the state of the module output.</summary>
		public bool OutputEnabled {
			get {
				return !(this.enable.Read()==GpioPinValue.High);
			}

			set {
				this.enable.Write((!value)?GpioPinValue.High:GpioPinValue.Low);
			}
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public MaxO(string SPIControllerName,int DigitalPin3, int DigitalPin4, int CSPin5) {
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            //socket.EnsureTypeIsSupported('S', this);
            /*
			this.spi = SpiDeviceFactory.Create(socket, new SpiDeviceConfiguration(false, 0, 0, false, true, 1000), SpiDeviceSharing.Shared, socket, Socket.Pin.Five, this);
			this.enable = GpioPinFactory.Create(socket, Socket.Pin.Three, false, this);
			this.clr = GpioPinFactory.Create(socket, Socket.Pin.Four, true, this);
			*/
            var settings = new SpiConnectionSettings(CSPin5);
            settings.ClockFrequency = 1000000; //1000Khz                             
            settings.Mode = SpiMode.Mode1; //clock polarity = false, clock phase = true                                  
            settings.SharingMode = SpiSharingMode.Shared;
            settings.DataBitLength = 8;
           
            string spiAqs = SpiDevice.GetDeviceSelector(SPIControllerName);
            var devicesInfo = DeviceInformation.FindAll(spiAqs);
            spi = SpiDevice.FromId(devicesInfo[0].Id, settings);

            var controller = GpioController.GetDefault();
            this.enable = controller.OpenPin(DigitalPin3);
            this.enable.SetDriveMode(GpioPinDriveMode.Output);
            this.enable.Write(GpioPinValue.Low);

            this.clr = controller.OpenPin(DigitalPin4);
            this.clr.SetDriveMode(GpioPinDriveMode.Output);
            this.clr.Write(GpioPinValue.High);

            this.boards = 0;
			this.data = null;

        }

		/// <summary>Clears all registers.</summary>
		public void Clear() {
			if (this.data == null) throw new InvalidOperationException("You must set Boards first.");

			this.enable.Write(GpioPinValue.High);
			this.clr.Write(GpioPinValue.Low);

			Thread.Sleep(10);

			this.spi.Write(new byte[] { 0 });

			this.clr.Write(GpioPinValue.High);
			this.enable.Write(GpioPinValue.Low);

			for (int i = 0; i < this.data.Length; i++)
				this.data[i] = 0x0;
		}

		/// <summary>Writes the buffer to the modules.</summary>
		/// <param name="buffer">The buffer to write.</param>
		public void Write(byte[] buffer) {
			if (this.data == null) throw new InvalidOperationException("You must set Boards first.");
			if (buffer.Length != this.data.Length) throw new ArgumentException("array", "array.Length must be the same size as ArraySize.");

			this.enable.Write(GpioPinValue.High);

			byte[] reversed = new byte[buffer.Length];
			for (int i = 0; i < reversed.Length; i++)
				reversed[i] = buffer[reversed.Length - i - 1];

			this.spi.Write(reversed);

			if (this.data != buffer)
				Array.Copy(buffer, this.data, buffer.Length);

			this.enable.Write(GpioPinValue.Low);
		}

		/// <summary>Sets the state of the specified pin on the specified board.</summary>
		/// <param name="board">The board to write to.</param>
		/// <param name="pin">The pin to write.</param>
		/// <param name="value">The value to write to the pin.</param>
		public void SetPin(int board, int pin, bool value) {
			if (this.data == null) throw new InvalidOperationException("You must set Boards first.");
			if (board * 4 > this.data.Length) throw new ArgumentException("board", "The board is out of range.");

			int index = (board - 1) * 4 + pin / 8;

			if (value) {
				this.data[index] = (byte)(this.data[index] | (1 << (pin % 8)));
			}
			else {
				this.data[index] = (byte)(this.data[index] & ~(1 << (pin % 8)));
			}

			this.Write(this.data);
		}

		/// <summary>The data currently on the modules.</summary>
		/// <returns>The data.</returns>
		public byte[] Read() {
			if (this.data == null) throw new InvalidOperationException("You must set Boards first.");

			byte[] buffer = new byte[this.data.Length];

			Array.Copy(this.data, buffer, this.data.Length);

			return this.data;
		}
	}
}