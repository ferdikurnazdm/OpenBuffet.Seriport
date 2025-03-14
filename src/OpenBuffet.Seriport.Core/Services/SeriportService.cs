using OpenBuffet.Seriport.Core.Configurations;
using OpenBuffet.Seriport.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBuffet.Seriport.Core.Services {
    /// <summary>
    /// this class manage the seriport
    /// communication process
    /// </summary>
    public sealed class SeriportService : ISeriportService {
        /// <summary>
        /// this field keeps the 
        /// seriport configuration
        /// </summary>
        private SeriportConfiguration _seriportConfiguration;
        /// <summary>
        /// this event runs occured error on seriport
        /// </summary>
        public event EventHandler<SerialError> ErrorReceivedEvent;
        /// <summary>
        /// this event runs when received data on seriport
        /// </summary>
        public event EventHandler DataReceivedEvent;
        /// <summary>
        /// this field keeps the seriport
        /// </summary>
        private readonly SerialPort _serialPort;
        /// <summary>
        /// this field keeps the string builder
        /// </summary>
        private readonly StringBuilder _stringBuilder;
        /// <summary>
        /// this field keeps the stopwatch
        /// </summary>
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// this constructor set the 
        /// seriport configuration
        /// </summary>
        /// <param name="seriportConfiguration">seriport configuration</param>
        public SeriportService(SeriportConfiguration seriportConfiguration) {
            _stopwatch = new Stopwatch();
            _serialPort = new SerialPort();
            _stringBuilder = new StringBuilder();
            _seriportConfiguration = seriportConfiguration;
            _serialPort.PortName = _seriportConfiguration.PortName;
            _serialPort.BaudRate = _seriportConfiguration.Baudrate;
            _serialPort.DataBits = _seriportConfiguration.Databit;
            _serialPort.Parity = _seriportConfiguration.Paritybit;
            _serialPort.StopBits = _seriportConfiguration.Stopbit;
            _serialPort.WriteBufferSize = _seriportConfiguration.TransferBufferSize;
            _serialPort.ReadBufferSize = _seriportConfiguration.ReceiveBufferSize;
            _serialPort.WriteTimeout = _seriportConfiguration.TransferTimeout;
            _serialPort.ReadTimeout = _seriportConfiguration.ReceiveTimeout;

            _serialPort.ErrorReceived += _serialPort_ErrorReceived;
            _serialPort.DataReceived += _serialPort_DataReceived;
        }





        /// <summary>
        /// this method runs the data received event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event args</param>
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            this.DataReceivedEvent?.Invoke(this, e);
        }
        /// <summary>
        /// this method runs the error received event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event args</param>
        private void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e) {
            SerialError error = e.EventType;
            this.ErrorReceivedEvent?.Invoke(sender, error);
        }


        /// <summary>
        /// this property return
        /// seriportconfiguration
        /// </summary>
        public SeriportConfiguration GetSeriportConfiguration {
            get {  return _seriportConfiguration; }
        }




        /// <summary>
        /// this method build the seriport paramater
        /// </summary>
        /// <param name="seriportBuilder">seriport builder</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        public bool TryBuildSeriport(Action<SeriportConfiguration> seriportConfiguration, out Exception exception) {
            bool result = false;
            exception = null;
            try {
                _seriportConfiguration = new SeriportConfiguration();
                seriportConfiguration?.Invoke(_seriportConfiguration);
                _serialPort.PortName = _seriportConfiguration.PortName;
                _serialPort.BaudRate = _seriportConfiguration.Baudrate;
                _serialPort.DataBits = _seriportConfiguration.Databit;
                _serialPort.Parity = _seriportConfiguration.Paritybit;
                _serialPort.StopBits = _seriportConfiguration.Stopbit;
                _serialPort.WriteBufferSize = _seriportConfiguration.TransferBufferSize;
                _serialPort.ReadBufferSize = _seriportConfiguration.ReceiveBufferSize;
                _serialPort.WriteTimeout = _seriportConfiguration.TransferTimeout;
                _serialPort.ReadTimeout = _seriportConfiguration.ReceiveTimeout;
                result = true;
            }
            catch (Exception ex) {
                exception = ex;
                exception.Source = this.GetType().FullName;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// this method gets the seriport
        /// connection state
        /// </summary>
        /// <param name="state">seriport connection state</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        public bool TryGetSeriPortState(out bool state, out Exception exception) {
            bool result = false;
            exception = null;
            state = false;
            try {
                state = _serialPort.IsOpen;
                result = true;
            }
            catch (Exception ex) {
                exception = ex;
                exception.Source = this.GetType().Name;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// this method close the seriport
        /// connection
        /// </summary>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        public bool TryCloseSeriport(out Exception exception) {
            bool result = false;
            exception = null;
            try {
                if (!_serialPort.IsOpen) { return true; }
                _serialPort.Close();
                result = true;
            }
            catch (Exception ex) {
                exception = ex;
                exception.Source = this.GetType().Name;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// this method open the seriport
        /// connection
        /// </summary>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        public bool TryOpenSeriport(out Exception exception) {
            bool result = false;
            exception = null;
            try {
                _serialPort.Open();
                result = true;
            }
            catch (Exception ex) {
                exception = ex;
                exception.Source = this.GetType().Name;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// this method read the seriport
        /// </summary>
        /// <param name="readedData">readed data on seriport</param>
        /// <param name="readedBytes">readed bytes on seriport</param>
        /// <param name="timeTaken">taken time on readed process</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        public bool TryReadSeriport(out string readedData, out int readedBytes, out TimeSpan timeTaken, out Exception exception) {
            bool result = false;
            exception = null;
            readedData = string.Empty;
            int receivedByte;
            char convertedChar;
            readedBytes = 0;
            timeTaken = TimeSpan.Zero;
            try {
                _stringBuilder.Clear();
                _stopwatch.Reset();
                _stopwatch.Start();
                while (_serialPort.BytesToRead > 0) {
                    receivedByte = _serialPort.ReadByte();
                    convertedChar = Convert.ToChar(receivedByte);
                    _stringBuilder.Append(convertedChar);
                    readedBytes++;
                }
                _stopwatch.Stop();
                timeTaken = _stopwatch.Elapsed;
                readedBytes = _serialPort.BytesToRead;
                readedData = _stringBuilder.ToString();
                result = true;
            }
            catch (Exception ex) {
                exception = ex;
                exception.Source = this.GetType().Name;
                result = false;
            }
            return result;
        }
        /// <summary>
        /// this method write to seriport
        /// </summary>
        /// <param name="content">sending content</param>
        /// <param name="sendedBytes">sended bytes on seriport</param>
        /// <param name="timeTaken">taken time sending process</param>
        /// <param name="exception">return null if method runned succesfully 
        /// otherwise return occured exception</param>
        /// <returns>return true if method runned succesfully otherwise return false</returns>
        public bool TryWriteSeriport(string content, out int sendedBytes, out TimeSpan timeTaken, out Exception exception) {
            bool result = false;
            exception = null;
            sendedBytes = 0;
            timeTaken = TimeSpan.Zero;
            try {
                bool isContentBad = string.IsNullOrWhiteSpace(content);
                if (isContentBad) {
                    exception = new ArgumentNullException("content is empty");
                    return false;
                }
                _serialPort.DiscardOutBuffer();
                _stopwatch.Reset();
                _stopwatch.Start();
                _serialPort.WriteLine(content);
                _stopwatch.Stop();
                timeTaken = _stopwatch.Elapsed;
                sendedBytes = _serialPort.BytesToWrite;
                _serialPort.DiscardOutBuffer();
                result = true;
            }
            catch (Exception ex) {
                exception = ex;
                exception.Source = this.GetType().Name;
                result = false;
            }
            return result;
        }
    }
}
