using GHIElectronics.TinyCLR.Devices.Adc;
using GHIElectronics.TinyCLR.Devices.Gpio;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A GasSense module for Microsoft .NET Gadgeteer</summary>
	public class GasSense : GTM.Module {
		private AdcChannel input;
		private GpioPin enable;

		/// <summary>Turns the heating element on or off. This may take up to 10 seconds befre a proper reading is taken.</summary>
		public bool HeatingElementEnabled {
			get {
				return this.enable.Read()==GpioPinValue.High;
			}

			set {
                var PinVal = value ? GpioPinValue.High : GpioPinValue.Low;
				this.enable.Write(PinVal);
			}
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="AnalogPin3">The socket that has analog pin.</param>
		public GasSense(int AnalogPin3, int DigitalPin4) {
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            //socket.EnsureTypeIsSupported('A', this);
            var controller = GpioController.GetDefault();
            this.enable = controller.OpenPin(DigitalPin4);
            this.enable.SetDriveMode(GpioPinDriveMode.Output);

            var AnalogController = AdcController.GetDefault();
            this.input = AnalogController.OpenChannel(AnalogPin3);
            //this.input = GTI.AnalogInputFactory.Create(socket, Socket.Pin.Three, this);
            //this.enable = GTI.DigitalOutputFactory.Create(socket, Socket.Pin.Four, false, this);
        }

		/// <summary>The voltage returned from the sensor.</summary>
		/// <returns>The voltage value between 0.0 and 3.3</returns>
		public double ReadVoltage() {
			return this.input.ReadValue();
		}

		/// <summary>The proportion returned from the sensor.</summary>
		/// <returns>The value between 0.0 and 1.0</returns>
		public double ReadProportion() {
			return this.input.ReadRatio();
		}
	}
}