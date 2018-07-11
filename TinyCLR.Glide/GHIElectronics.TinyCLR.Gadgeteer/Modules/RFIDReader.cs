//using Microsoft.SPOT;
using GHIElectronics.TinyCLR.Devices.SerialCommunication;
using GHIElectronics.TinyCLR.Storage.Streams;
using System;
using System.Text;
using System.Threading;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>An RFIDReader module for Microsoft .NET Gadgeteer</summary>
	public class RFIDReader : GTM.Module {
        private TimeSpan TimerInterval;

        private const int MESSAGE_LENGTH = 13;
        private SerialDevice port;
        private IOutputStream _outStream;
        private IInputStream _inStream;
        private DataReader SerialReader;
        private DataWriter SerialWriter;
        private Timer timer;
        private AutoResetEvent autoEvent;

        private byte[] buffer;
		private uint read;
		private int checksum;
		private IdReceivedEventHandler onIdReceived;

		private MalformedIdReceivedEventHandler onMalformedIdReceived;

		/// <summary>The delegate that is used to handle the id received event.</summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The event arguments.</param>
		public delegate void IdReceivedEventHandler(RFIDReader sender, string e);

		/// <summary>The delegate that is used to handle the bad checksum event.</summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The event arguments.</param>
		public delegate void MalformedIdReceivedEventHandler(RFIDReader sender, EventArgs e);

		/// <summary>Raised when the module receives an id.</summary>
		public event IdReceivedEventHandler IdReceived;

		/// <summary>Raised when the module receives an id with an incorrect checksum.</summary>
		public event MalformedIdReceivedEventHandler MalformedIdReceived;

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public RFIDReader(string ComId)
        {
            port = SerialDevice.FromId(ComId);
            port.BaudRate = 9600;
            port.Parity = SerialParity.None;
            port.StopBits = SerialStopBitCount.Two;
            port.DataBits = 8;
            port.Handshake = SerialHandshake.None;
            _outStream = port.OutputStream;
            _inStream = port.InputStream;
            SerialWriter = new DataWriter(_outStream);
            SerialReader = new DataReader(_inStream);
            this.port.ReadTimeout = new TimeSpan(0, 0, 0, 0, 10);
            //this.port.WriteTimeout = new TimeSpan(0, 0, 0, 0, 500);

            this.buffer = new byte[RFIDReader.MESSAGE_LENGTH];
			this.read = 0;
			this.checksum = 0;

            //this.port = GTI.SerialFactory.Create(socket, 9600, GTI.SerialParity.None, GTI.SerialStopBits.Two, 8, GTI.HardwareFlowControl.NotRequired, this);
            //this.port.ReadTimeout = 10;
            //this.port.Open();
            TimerInterval = new TimeSpan(0, 0, 0, 0, 100);
            autoEvent = new AutoResetEvent(false);
            StartTimer();
            //this.timer = new GT.Timer(100);
			//this.timer.Tick += this.DoWork;
			//this.timer.Start();
		}
        void StartTimer()
        {
            this.timer = new Timer(new TimerCallback((a) => { this.DoWork(null); }), autoEvent, new TimeSpan(0, 0, 0), TimerInterval);
        }
        private int ASCIIToNumber(byte upper, byte lower) {
			var high = upper - 48 - (upper >= 'A' ? 7 : 0);
			var low = lower - 48 - (lower >= 'A' ? 7 : 0);

			return (high << 4) | low;
		}

		private void DoWork(object o) {
            //this.read += this.port.Read(this.buffer, this.read, RFIDReader.MESSAGE_LENGTH - this.read);
            var justread = SerialReader.Load(RFIDReader.MESSAGE_LENGTH);
            this.read += justread;

            if (this.read != RFIDReader.MESSAGE_LENGTH)
                return;

            if (justread > 0)
            {
                SerialReader.ReadBytes(buffer);
            }


            for (int i = 1; i < 10; i += 2)
                this.checksum ^= this.ASCIIToNumber(this.buffer[i], this.buffer[i + 1]);

            if (this.buffer[0] == 0x02 && this.buffer[12] == 0x03 && this.checksum == this.buffer[11])
            {
                this.OnIdReceived(this, new string(Encoding.UTF8.GetChars(this.buffer, 1, 10)));
            }
            else
            {
                this.SerialReader.DetachBuffer();

                this.OnMalformedIdReceived(this, null);
            }

            this.read = 0;
            this.checksum = 0;

        }

		private void OnIdReceived(RFIDReader sender, string e) {
			if (this.onIdReceived == null)
				this.onIdReceived = this.OnIdReceived;

			//if (Program.CheckAndInvoke(this.IdReceived, this.onIdReceived, sender, e))
			if(this.IdReceived!=null)	    
                this.IdReceived(sender, e);
		}

		private void OnMalformedIdReceived(RFIDReader sender, EventArgs e) {
			if (this.onMalformedIdReceived == null)
				this.onMalformedIdReceived = this.OnMalformedIdReceived;

            //if (Program.CheckAndInvoke(this.MalformedIdReceived, this.onMalformedIdReceived, sender, e))
            if(this.onMalformedIdReceived!=null)
                this.MalformedIdReceived(sender, e);
		}
	}
}