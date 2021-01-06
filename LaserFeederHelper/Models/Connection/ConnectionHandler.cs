using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace LaserFeederHelper.Models.Connection
{
    class ConnectionHandler
    {
        SerialPort _serialPort;
        int _baudRate = 9600;
        public Queue<byte> Data {get=>_data; }
        Queue<byte> _data = new Queue<byte>();
        private readonly Action<Queue<byte>> _getData;

        public ConnectionHandler(Action<Queue<byte>>getData)
        {

            _serialPort = new SerialPort();
            _serialPort.DataReceived += _serialPort_DataReceived;
            _getData = getData;
        }
        public bool IsAvalible()
        {
            return SerialPort.GetPortNames().Length > 0 ? true : false;
        }
        public void Connect()
        {
            _serialPort.BaudRate = _baudRate;
            _serialPort.PortName = SerialPort.GetPortNames()[0];
            _serialPort.Open();
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytesToRead = ((SerialPort)sender).BytesToRead;
            var bytes = new byte[bytesToRead];
            ((SerialPort)sender).Read(bytes,0,bytesToRead);
            foreach(byte _byte in bytes)
            {
                _data.Enqueue(_byte);
            }
            _getData(_data);
        }

        public void Send(byte[] bytes)
        {
            _serialPort.Write(bytes, 0, bytes.Length);
        }

        public void Disconnect()
        {
            _serialPort.Close();
        }

    }
}
