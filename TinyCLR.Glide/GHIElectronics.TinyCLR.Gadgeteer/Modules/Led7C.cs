using GHIElectronics.TinyCLR.Devices.Gpio;
using GTM = Gadgeteer.Modules;

// ReSharper disable once CheckNamespace
namespace Gadgeteer.Modules.GHIElectronics
{

    public class Led7C : Module
    {
        private readonly GpioPin _red;
        private readonly GpioPin _green;
        private readonly GpioPin _blue;

        public enum Color
        {
            Red=0x4,
            Green=0x2,
            Blue=0x1,
            Yellow=0x6,
            Cyan=0x3,
            Magenta=0x5,
            White=0x7,
            Black=0x0,
            Off=0x0
        }

        public Led7C(int digitalPin3, int digitalPin4,int digitalPin5)
        {
            var controller = GpioController.GetDefault();
            _red = controller.OpenPin(digitalPin3);
            _red.SetDriveMode(GpioPinDriveMode.Output);
            _green = controller.OpenPin(digitalPin4);
            _green.SetDriveMode(GpioPinDriveMode.Output);
            _blue = controller.OpenPin(digitalPin5);
            _blue.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void SetColor(Color color)
        {
            int c = (int) color;
            _red.Write((c & 4) == 0 ? GpioPinValue.Low : GpioPinValue.High);
            _green.Write((c & 2) == 0 ? GpioPinValue.Low : GpioPinValue.High);
            _blue.Write((c & 1) == 0 ? GpioPinValue.Low : GpioPinValue.High);
        }
    }
}
