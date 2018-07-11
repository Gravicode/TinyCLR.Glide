//using GTI = Gadgeteer.SocketInterfaces;
using GHIElectronics.TinyCLR.Devices.Adc;
using GHIElectronics.TinyCLR.Devices.Dac;
using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.Pwm;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A BreakoutTB10 module for Microsoft .NET Gadgeteer.</summary>
	public class BreakoutTB10 : GTM.Module {
		//private Socket socket;

		/// <summary>The mainboard socket which this module is plugged into.</summary>
		//public Socket Socket { get { return this.socket; } }
        int[] DigitalPins { set; get; }
        int[] AnalogPins { set; get; }
		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The mainboard socket that has the module plugged into it.</param>
		public BreakoutTB10() {
			//this.socket = Socket.GetSocket(socketNumber, true, this, null);
		}
        public BreakoutTB10(int[] DigitalPins, int[] AnalogPins)
        {
            this.DigitalPins = DigitalPins;
            this.AnalogPins = AnalogPins;
        }

        public int GetDigitalPins(int PinNumber) {
            if (PinNumber < 0) return DigitalPins[0];
            return DigitalPins[PinNumber - 1];
        }
        public int GetAnalogPins(int PinNumber)
        {
            if (PinNumber < 0) return AnalogPins[0];
            return AnalogPins[PinNumber - 1];
        }
        /// <summary>Creates a digital input on the given pin.</summary>
        /// <param name="DigitalPin">The pin to create the interface on.</param>
        /// <returns>The new interface.</returns>
        public GpioPin CreateDigitalInput(int DigitalPin) {
            var controller = GpioController.GetDefault();
            var ThisPin = controller.OpenPin(DigitalPin);
            ThisPin.SetDriveMode(GpioPinDriveMode.Input);

            return ThisPin;
        }

        /// <summary>Creates a digital output on the given pin.</summary>
        /// <param name="DigitalPin">The pin to create the interface on.</param>
        /// <param name="initialState">The initial state for the interface.</param>
        /// <returns>The new interface.</returns>
        public GpioPin CreateDigitalOutput(int DigitalPin, bool initialState) {
            var controller = GpioController.GetDefault();
            var ThisPin = controller.OpenPin(DigitalPin);
            ThisPin.SetDriveMode(GpioPinDriveMode.Output);
            ThisPin.Write(initialState ? GpioPinValue.High : GpioPinValue.Low);
            return ThisPin;
        }
        

		/// <summary>Creates an interrupt input on the given pin.</summary>
		/// <param name="DigitalPin">The pin to create the interface on.</param>
		/// <param name="isPullUp">Input pull up ?.</param>
		/// <returns>The new interface.</returns>
		public GpioPin CreateInterruptInput(int DigitalPin, bool IsPullUp) {
            var controller = GpioController.GetDefault();
            var ThisPin = controller.OpenPin(DigitalPin);
            ThisPin.SetDriveMode(IsPullUp? GpioPinDriveMode.InputPullUp:GpioPinDriveMode.InputPullDown);

            return ThisPin;
        }

		/// <summary>Creates an analog input on the given pin.</summary>
		/// <param name="AnalogPin">The pin to create the interface on.</param>
		/// <returns>The new interface.</returns>
		public AdcChannel CreateAnalogInput(int AnalogPin) {
            var AnalogController = AdcController.GetDefault();
            var ThisAnalogPin = AnalogController.OpenChannel(AnalogPin);
           
            return ThisAnalogPin;
        }

		/// <summary>Creates an analog output on the given pin.</summary>
		/// <param name="AnalogPin">The pin to create the interface on.</param>
		/// <returns>The new interface.</returns>
		public DacChannel CreateAnalogOutput(int AnalogPin) {
            var AnalogController = DacController.GetDefault();
            var ThisAnalogPin = AnalogController.OpenChannel(AnalogPin);
            return ThisAnalogPin;
		}

        /// <summary>Creates a pwm output on the given pin.</summary>
        /// <param name="PwmId">The ID PWM to create the interface on.</param>
        /// <param name="PWMPin">The pwm pin number.</param>
        /// <returns>The new interface.</returns>
        public PwmPin CreatePwmOutput(string PwmId, int PWMPin) {
            //socket.EnsureTypeIsSupported('P', this);
            var PwmController1 = PwmController.FromId(PwmId);
            var pwm = PwmController1.OpenPin(PWMPin);
            return pwm;
        }
	}
}