//using GTI = Gadgeteer.SocketInterfaces;
using GHIElectronics.TinyCLR.Devices.Adc;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A Moisture module for Microsoft .NET Gadgeteer</summary>
	public class Moisture : GTM.Module {
		private AdcChannel input;
		private GpioPin enable;

		/// <summary>Turns sensor on and off.</summary>
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
		public Moisture(int DigitalPin6,int AnalogPin3) {
            var controller = GpioController.GetDefault();
            this.enable = controller.OpenPin(DigitalPin6);
            this.enable.SetDriveMode(GpioPinDriveMode.Output);
            var AnalogController = AdcController.GetDefault();
            this.input = AnalogController.OpenChannel(AnalogPin3);
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            //socket.EnsureTypeIsSupported('A', this);

            //this.input = AdcChannelFactory.Create(socket, Socket.Pin.Three, this);
            //this.enable = GpioPinFactory.Create(socket, Socket.Pin.Six, true, this);
        }

		/// <summary>The moisture reading from the sensor.</summary>
		/// <returns>A value where 0 is fully dry and 1000 (or greater) is completely wet.</returns>
		public int ReadMoisture() {
			return (int)(this.input.ReadRatio() * 1600);
		}
	}
}