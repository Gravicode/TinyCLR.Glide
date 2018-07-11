using GHIElectronics.TinyCLR.Devices.Gpio;
using GHIElectronics.TinyCLR.Devices.Pwm;
using System;
using System.Threading;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A MotorDriverL298 module for Microsoft .NET Gadgeteer</summary>
	public class MotorDriverL298 : GTM.Module {
		private const int STEP_FACTOR = 250;

		private PwmPin[] pwms;
		private GpioPin[] directions;
		private double[] lastSpeeds;

		/// <summary>Used to set the PWM frequency for the motors because some motors require a certain frequency in order to operate properly. It defaults to 25KHz (25000).</summary>
		public int Frequency { get; set; }

		/// <summary>The possible motors.</summary>
		public enum Motor {

			/// <summary>The motor marked M1.</summary>
			Motor1 = 0,

			/// <summary>The motor marked M2.</summary>
			Motor2 = 1,
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public MotorDriverL298(string PwmId1, string PwmId2, int PwmPinCtl1,int PwmPinCtl2,int DigitalPin6,int DigitalPin9) {
            //Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            //socket.EnsureTypeIsSupported('P', this);
            var pwmcontroller1 = PwmController.FromId(PwmId1);
            var pwmcontroller2 = PwmController.FromId(PwmId2);

            this.pwms = new PwmPin[2]
            {
                pwmcontroller1.OpenPin(PwmPinCtl1),
                pwmcontroller2.OpenPin(PwmPinCtl2)
            //PwmPinFactory.Create(socket, Socket.Pin.Eight, false, this),
            //PwmPinFactory.Create(socket, Socket.Pin.Seven, false, this)
        };
            var controller = GpioController.GetDefault();
            this.directions = new GpioPin[2]
            {
                            controller.OpenPin(DigitalPin6),
                            controller.OpenPin(DigitalPin9)
                // GpioPinFactory.Create(socket, Socket.Pin.Six, false, this),
                // GpioPinFactory.Create(socket, Socket.Pin.Nine, false, this)
            };
            this.directions[0].SetDriveMode(GpioPinDriveMode.Output);
            this.directions[1].SetDriveMode(GpioPinDriveMode.Output);

            this.lastSpeeds = new double[2] { 0, 0 };

			this.Frequency = 25000;

			this.StopAll();
		}

		/// <summary>Stops all motors.</summary>
		public void StopAll() {
			this.SetSpeed(Motor.Motor1, 0);
			this.SetSpeed(Motor.Motor2, 0);
		}

		/// <summary>Sets the given motor's speed.</summary>
		/// <param name="motor">The motor to set the speed for.</param>
		/// <param name="speed">The desired speed of the motor between -1 and 1.</param>
		public void SetSpeed(Motor motor, double speed) {
			if (speed > 1 || speed < -1) new ArgumentOutOfRangeException("speed", "speed must be between -1 and 1.");
			if (motor != Motor.Motor1 && motor != Motor.Motor2) throw new ArgumentException("motor", "You must specify a valid motor.");

			if (speed == 1.0)
				speed = 0.99;

			if (speed == -1.0)
				speed = -0.99;

			this.directions[(int)motor].Write(speed < 0?GpioPinValue.High:GpioPinValue.Low);
            this.pwms[(int)motor].Controller.SetDesiredFrequency((double)this.Frequency);
            this.pwms[(int)motor].SetActiveDutyCyclePercentage(speed < 0 ? 1 + speed : speed);

            this.lastSpeeds[(int)motor] = speed;
		}

		/// <summary>Sets the given motor's speed.</summary>
		/// <param name="motor">The motor to set the speed for.</param>
		/// <param name="speed">The desired speed of the motor between -1 and 1.</param>
		/// <param name="time">How many milliseconds the motor should take to reach the specified speed.</param>
		public void SetSpeed(Motor motor, double speed, int time) {
			if (speed > 1 || speed < -1) new ArgumentOutOfRangeException("speed", "speed must be between -1  and 1.");
			if (motor != Motor.Motor1 && motor != Motor.Motor2) throw new ArgumentException("motor", "You must specify a valid motor.");

			double currentSpeed = this.lastSpeeds[(int)motor];

			if (currentSpeed == speed)
				return;

			int sleep = (int)(time / (Math.Abs(speed - currentSpeed) * MotorDriverL298.STEP_FACTOR));
			double step = 1.0 / MotorDriverL298.STEP_FACTOR;

			if (sleep < 1)
				throw new ArgumentOutOfRangeException("time", "You cannot move to a speed this close to the existing speed in so little time.");

			if (speed < currentSpeed)
				step *= -1;

			while (Math.Abs(speed - currentSpeed) >= 0.01) {
				currentSpeed += step;

				this.SetSpeed(motor, currentSpeed);

				Thread.Sleep(sleep);
			}
		}
	}
}