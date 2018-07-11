using GHIElectronics.TinyCLR.Devices.SerialCommunication;
using GHIElectronics.TinyCLR.Storage.Streams;
using System;
using GT = Gadgeteer;
//using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics {
	/// <summary>A XBeeAdapter module for Microsoft .NET Gadgeteer</summary>
	public class XBeeAdapter : GTM.Module {
		private SerialDevice serialPort;
        public IOutputStream _outStream;
        public IInputStream _inStream;
        public DataReader SerialReader;
        public DataWriter SerialWriter;
        private string ComId { set; get; }

		//private Socket socket;

		/// <summary>The underlying serial port object.</summary>
		public SerialDevice Port {
			get {
				if (this.serialPort == null) throw new InvalidOperationException("You must call Configure first.");

				return this.serialPort;
			}
		}

		/// <summary>Constructs a new instance.</summary>
		/// <param name="socketNumber">The socket that this module is plugged in to.</param>
		public XBeeAdapter(string ComId) {
            this.ComId = ComId;
			//this.socket = Socket.GetSocket(socketNumber, true, this, null);
			//this.socket.EnsureTypeIsSupported('U', this);
			this.serialPort = null;
		}

		/// <summary>Initializes the serial port with the parameters 115200 baud, 8N1, with no flow control.</summary>
		public void Configure(string ComId) {
            //this.Configure(115200, GTI.SerialParity.None, GTI.SerialStopBits.One, 8, GTI.HardwareFlowControl.NotRequired);
            this.ComId = ComId;
            serialPort = SerialDevice.FromId(ComId);
            serialPort.BaudRate = 115200;
            serialPort.Parity = SerialParity.None;
            serialPort.StopBits = SerialStopBitCount.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = SerialHandshake.None;
            _outStream = serialPort.OutputStream;
            _inStream = serialPort.InputStream;
            SerialWriter = new DataWriter(_outStream);
            SerialReader = new DataReader(_inStream);

            //this.serialPort.ReadTimeout = new TimeSpan(0, 0, 0, 0, 500);
            //this.serialPort.WriteTimeout = new TimeSpan(0, 0, 0, 0, 500);
        }

		/// <summary>Initializes the serial port with the given parameters.</summary>
		/// <param name="baudRate">The baud rate to use.</param>
		/// <param name="parity">The parity to use.</param>
		/// <param name="stopBits">The stop bits to use.</param>
		/// <param name="dataBits">The number of data bits to use.</param>
		/// <param name="flowControl">The flow control to use.</param>
		public void Configure(uint baudRate, SerialParity parity, SerialStopBitCount stopBits, ushort dataBits, SerialHandshake flowControl) {
			if (this.serialPort != null) throw new InvalidOperationException("Configure can only be called once.");

            serialPort = SerialDevice.FromId(ComId);
            serialPort.BaudRate = baudRate;
            serialPort.Parity = parity;
            serialPort.StopBits = stopBits;
            serialPort.DataBits = dataBits;
            serialPort.Handshake = flowControl;
            _outStream = serialPort.OutputStream;
            _inStream = serialPort.InputStream;
            SerialWriter = new DataWriter(_outStream);
            SerialReader = new DataReader(_inStream);
        }
	}
}