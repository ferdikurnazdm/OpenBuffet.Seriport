using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBuffet.Seriport.Core.Configurations {
    /// <summary>
    /// this class configure the flashtech seriport
    /// </summary>
    public sealed class SeriportConfiguration {
        /// <summary>
        /// this field keeps the portname
        /// </summary>
        private string _portName = "COM1";
        /// <summary>
        /// this property keeps the portname
        /// </summary>
        public string PortName {
            get { return _portName; }
            set { _portName = value; }
        }
        /// <summary>
        /// this field keeps the baudrate
        /// </summary>
        private int _baudrate = 9600;
        /// <summary>
        /// this property keeps the baudrate
        /// </summary>
        public int Baudrate {
            get { return _baudrate; }
            set { _baudrate = value; }
        }
        /// <summary>
        /// this field keeps the databit
        /// </summary>
        private int _databit = 8;
        /// <summary>
        /// this property keeps the databit
        /// </summary>
        public int Databit {
            get { return _databit; }
            set { _databit = value; }
        }
        /// <summary>
        /// this field keeps the parity
        /// </summary>
        private Parity _parity = Parity.None;
        /// <summary>
        /// this property keeps the parity
        /// </summary>
        public Parity Paritybit {
            get { return _parity; }
            set { _parity = value; }
        }
        /// <summary>
        /// this field keeps the stopbit
        /// </summary>
        private StopBits _stopbits = StopBits.One;
        /// <summary>
        /// this property keeps the stopbit
        /// </summary>
        public StopBits Stopbit {
            get { return _stopbits; }
            set { _stopbits = value; }
        }
        /// <summary>
        /// this field keeps the transfer buffer size
        /// </summary>
        private int _transferBufferSize = 1024;
        /// <summary>
        /// this property keeps the transfer buffer size
        /// </summary>
        public int TransferBufferSize {
            get { return _transferBufferSize; }
            set { _transferBufferSize = value; }
        }
        /// <summary>
        /// this field keeps the receive buffer size
        /// </summary>
        private int _receiveBufferSize = 1024;
        /// <summary>
        /// this property keeps the receive buffer size
        /// </summary>
        public int ReceiveBufferSize {
            get { return _receiveBufferSize; }
            set { _receiveBufferSize = value; }
        }
        /// <summary>
        /// this field keeps the transfer timeout
        /// </summary>
        private int _transferTimeout = 1000;
        /// <summary>
        /// this property keeps the transfer timeout
        /// </summary>
        public int TransferTimeout {
            get { return _transferTimeout; }
            set { _transferTimeout = value; }
        }
        /// <summary>
        /// this field keeps the receive timeout
        /// </summary>
        private int _receiveTimeout = 1000;
        /// <summary>
        /// this property keeps the receive timeout
        /// </summary>
        public int ReceiveTimeout {
            get { return _receiveTimeout; }
            set { _receiveTimeout = value; }
        }
    }
}
