//using GTI = Gadgeteer.SocketInterfaces;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A RelayX1 module for Microsoft .NET Gadgeteer</summary>
	public class RelayX1 : GTM.Module {
		private GpioPin enable;

		/// <summary>Whether the relay is on or off.</summary>
		public bool Enabled {
			get {
				return this.enable.Read()==GpioPinValue.High;
			}

			set {
				this.enable.Write(value?GpioPinValue.High:GpioPinValue.Low);
			}
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public RelayX1(int DigitalPin5) {
            var controller = GpioController.GetDefault();
            this.enable = controller.OpenPin(DigitalPin5);
            this.enable.SetDriveMode(GpioPinDriveMode.Output);
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);

            //socket.EnsureTypeIsSupported(new char[] { 'X', 'Y' }, this);

            //this.enable = GpioPinFactory.Create(socket, Socket.Pin.Five, false, this);
        }

		/// <summary>Turns the relay on.</summary>
		public void TurnOn() {
			this.Enabled = true;
		}

		/// <summary>Turns the relay off.</summary>
		public void TurnOff() {
			this.Enabled = false;
		}
	}
}