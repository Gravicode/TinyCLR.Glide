using GHIElectronics.TinyCLR.Devices.Gpio;
using System.Threading;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A CharacterDisplay module for Microsoft .NET Gadgeteer</summary>
	public class CharacterDisplay : GTM.Module {
		private const byte DISP_ON = 0x0C;
		private const byte CLR_DISP = 1;
		private const byte CUR_HOME = 2;
		private const byte SET_CURSOR = 0x80;
		private static byte[] ROW_OFFSETS = new byte[4] { 0x00, 0x40, 0x14, 0x54 };
		private GpioPin lcdRS;
		private GpioPin lcdE;

		private GpioPin lcdD4;
		private GpioPin lcdD5;
		private GpioPin lcdD6;
		private GpioPin lcdD7;

		private GpioPin backlight;

		private int currentRow;

		/// <summary>Whether or not the backlight is enabled.</summary>
		public bool BacklightEnabled {
			get {
				return this.backlight.Read()==GpioPinValue.High;
			}

			set {
				this.backlight.Write(value?GpioPinValue.High:GpioPinValue.Low);
			}
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public CharacterDisplay(int DigitalPin3, int DigitalPin4, int DigitalPin5, int DigitalPin6, int DigitalPin7, int DigitalPin8, int DigitalPin9) {
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);

            //socket.EnsureTypeIsSupported('Y', this);
            var controller = GpioController.GetDefault();

            this.lcdRS = controller.OpenPin(DigitalPin4);
            this.lcdRS.SetDriveMode(GpioPinDriveMode.Output);
            //this.lcdRS = GpioPinFactory.Create(socket, GT.Socket.Pin.Four, false, null);

            this.lcdE = controller.OpenPin(DigitalPin3);
            this.lcdE.SetDriveMode(GpioPinDriveMode.Output);
            //this.lcdE = GpioPinFactory.Create(socket, GT.Socket.Pin.Three, false, null);

            this.lcdD4 = controller.OpenPin(DigitalPin5);
            this.lcdD4.SetDriveMode(GpioPinDriveMode.Output);
            //this.lcdD4 = GpioPinFactory.Create(socket, GT.Socket.Pin.Five, false, null);

            this.lcdD5 = controller.OpenPin(DigitalPin7);
            this.lcdD5.SetDriveMode(GpioPinDriveMode.Output);
            //this.lcdD5 = GpioPinFactory.Create(socket, GT.Socket.Pin.Seven, false, null);

            this.lcdD6 = controller.OpenPin(DigitalPin9);
            this.lcdD6.SetDriveMode(GpioPinDriveMode.Output);
            //this.lcdD6 = GpioPinFactory.Create(socket, GT.Socket.Pin.Nine, false, null);

            this.lcdD7 = controller.OpenPin(DigitalPin6);
            this.lcdD7.SetDriveMode(GpioPinDriveMode.Output);
            //this.lcdD7 = GpioPinFactory.Create(socket, GT.Socket.Pin.Six, false, null);

            this.backlight = controller.OpenPin(DigitalPin8);
            this.backlight.SetDriveMode(GpioPinDriveMode.Output);
            //this.backlight = GpioPinFactory.Create(socket, GT.Socket.Pin.Eight, true, null);

			this.currentRow = 0;

			this.SendCommand(0x33);
			this.SendCommand(0x32);
			this.SendCommand(CharacterDisplay.DISP_ON);
			this.SendCommand(CharacterDisplay.CLR_DISP);

			Thread.Sleep(3);
		}

		/// <summary>Prints the passed in string to the screen at the current cursor position. A newline character (\n) will move the cursor to the start of the next row.</summary>
		/// <param name="value">The string to print.</param>
		public void Print(string value) {
			for (int i = 0; i < value.Length; i++)
				this.Print(value[i]);
		}

		/// <summary>Prints a character to the screen at the current cursor position. A newline character (\n) will move the cursor to the start of the next row.</summary>
		/// <param name="value">The character to display.</param>
		public void Print(char value) {
			if (value != '\n') {
				this.WriteNibble((byte)(value >> 4));
				this.WriteNibble((byte)value);
			}
			else {
				this.SetCursorPosition((this.currentRow + 1) % 2, 0);
			}
		}

		/// <summary>Clears the screen.</summary>
		public void Clear() {
			this.SendCommand(CharacterDisplay.CLR_DISP);

			this.currentRow = 0;

			Thread.Sleep(2);
		}

		/// <summary>Places the cursor at the top left of the screen.</summary>
		public void CursorHome() {
			this.SendCommand(CharacterDisplay.CUR_HOME);

			this.currentRow = 0;

			Thread.Sleep(2);
		}

		/// <summary>Moves the cursor to given position.</summary>
		/// <param name="row">The new row.</param>
		/// <param name="column">The new column.</param>
		public void SetCursorPosition(int row, int column) {
			if (column > 15 || column < 0) throw new System.ArgumentOutOfRangeException("column", "column must be between 0 and 15.");
			if (row > 1 || row < 0) throw new System.ArgumentOutOfRangeException("row", "row must be between 0 and 1.");

			this.currentRow = row;

			this.SendCommand((byte)(CharacterDisplay.SET_CURSOR | CharacterDisplay.ROW_OFFSETS[row] | column));
		}

		private void WriteNibble(byte b) {
			this.lcdD7.Write(((b & 0x8) != 0)?GpioPinValue.High:GpioPinValue.Low);
			this.lcdD6.Write(((b & 0x4) != 0)?GpioPinValue.High:GpioPinValue.Low);
			this.lcdD5.Write(((b & 0x2) != 0) ? GpioPinValue.High : GpioPinValue.Low);
			this.lcdD4.Write(((b & 0x1) != 0) ? GpioPinValue.High : GpioPinValue.Low);

			this.lcdE.Write(GpioPinValue.High);
			this.lcdE.Write(GpioPinValue.Low);

			Thread.Sleep(1);
		}

		private void SendCommand(byte command) {
			this.lcdRS.Write( GpioPinValue.Low);

			this.WriteNibble((byte)(command >> 4));
			this.WriteNibble(command);

			Thread.Sleep(2);

			this.lcdRS.Write(GpioPinValue.High);
		}
	}
}