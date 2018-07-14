using Gadgeteer.Modules.GHIElectronics;
using GHI.Glide;
using GHI.Glide.Display;
using GHI.Pins;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using TestApp.Properties;

namespace TestApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                //TestDisplayNhvn();
                TestGlide();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            Thread.Sleep(-1);
        }
        
        /// <summary>
        /// Testing method for DisplayNHVN module
        /// </summary>
        private static void TestGlide()
        {
            var lcd = new DisplayNHVN(GHIElectronics.TinyCLR.Pins.FEZRaptor.I2cBus.Socket13, FEZRaptor.Socket16.Pin9, FEZRaptor.Socket13.Pin3,
                DisplayNHVN.DisplayTypes.Display7inch);

            Glide.SetupGlide(800, 480, 96, 0,lcd.displayController);
            string GlideXML = @"<Glide Version=""1.0.7""><Window Name=""instance115"" Width=""800"" Height=""480"" BackColor=""dce3e7""><Button Name=""btn"" X=""40"" Y=""60"" Width=""120"" Height=""40"" Alpha=""255"" Text=""Click Me"" Font=""4"" FontColor=""000000"" DisabledFontColor=""808080"" TintColor=""000000"" TintAmount=""0""/><TextBlock Name=""TxtTest"" X=""42"" Y=""120"" Width=""650"" Height=""32"" Alpha=""255"" Text=""TextBlock"" TextAlign=""Left"" TextVerticalAlign=""Top"" Font=""6"" FontColor=""0"" BackColor=""000000"" ShowBackColor=""False""/></Window></Glide>";
           
            //Resources.GetString(Resources.StringResources.Window)
            Window window = GlideLoader.LoadWindow(GlideXML);

            GlideTouch.Initialize();

            GHI.Glide.UI.Button btn = (GHI.Glide.UI.Button)window.GetChildByName("btn");
            GHI.Glide.UI.TextBlock txt = (GHI.Glide.UI.TextBlock)window.GetChildByName("TxtTest");
            btn.TapEvent += (object sender)=>
            {
                txt.Text = "Welcome to Glide for TinyCLR - Cheers from Mif ;)";
                Debug.WriteLine("Button tapped.");
                
                window.Invalidate();
                txt.Invalidate();
            };

            Glide.MainWindow = window;

            lcd.CapacitiveScreenReleased += Lcd_CapacitiveScreenReleased;
            lcd.CapacitiveScreenPressed += Lcd_CapacitiveScreenPressed;

            //Thread.Sleep(Timeout.Infinite);
        }
        
    

        /// <summary>
        /// Testing method for DisplayNHVN module
        /// </summary>
        private static void TestDisplayNhvn()
        {
            var lcd = new DisplayNHVN(GHIElectronics.TinyCLR.Pins.FEZRaptor.I2cBus.Socket13, FEZRaptor.Socket16.Pin9, FEZRaptor.Socket13.Pin3,
                DisplayNHVN.DisplayTypes.Display7inch);
            var background = Resources.GetBitmap(Resources.BitmapResources.car);
            var font = Resources.GetFont(Resources.FontResources.NinaB);
            lcd.Screen.DrawImage(background, 0, 0);
            lcd.Screen.DrawString("Hello, world", font, new SolidBrush(Color.White), 10, 400);
            lcd.Screen.Flush();
            lcd.CapacitiveScreenReleased += Lcd_CapacitiveScreenReleased;
            lcd.CapacitiveScreenPressed += Lcd_CapacitiveScreenPressed;

            //Thread.Sleep(Timeout.Infinite);
        }


        #region Lcd Capacitive Touch Events
        /// <summary>
        /// Function called when released event raises
        /// </summary>
        /// <param name="sender">sender of event</param>
        /// <param name="e">EventArgs of event</param>
        private static void Lcd_CapacitiveScreenReleased(DisplayNHVN sender, DisplayNHVN.TouchEventArgs e)
        {
            Debug.WriteLine("you release the lcd at X:" + e.X + " ,Y:" + e.Y);
            GlideTouch.RaiseTouchDownEvent(e.X, e.Y);
        }

        /// <summary>
        /// Function called when pressed event raises
        /// </summary>
        /// <param name="sender">sender of event</param>
        /// <param name="e">EventArgs of event</param>
        private static void Lcd_CapacitiveScreenPressed(DisplayNHVN sender, DisplayNHVN.TouchEventArgs e)
        {
            Debug.WriteLine("you press the lcd at X:" + e.X + " ,Y:" + e.Y);
            GlideTouch.RaiseTouchUpEvent(e.X, e.Y);
        }
        #endregion
    }
}
