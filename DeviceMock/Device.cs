using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DeviceMock
{
    class Device
    {
        byte[] _setting;
        byte _status;
        SerialPort _port;
        Queue<byte> _queue = new Queue<byte>();
        System.Timers.Timer _timer;
        System.Timers.Timer _timeBetween;
        private uint _amount;

        public Device()
        {
            _port = new SerialPort("COM3");
            _port.DataReceived += _port_DataReceived;
            _port.Open();
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
            _timeBetween = new System.Timers.Timer(1000);
            _timeBetween.Elapsed += _timeBetween_Elapsed;
            _setting = new byte[] { 0x00, 0x01, 0x02, 0x03 };
            _status = 0x00;
        }

        private void _timeBetween_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendShoot();
            _amount--;
            Console.WriteLine("Amount to do "+_amount);
        }

        private void SendShoot()
        {
            Send(0x07);
        }
        private void Send(byte byteData)
        {
            Send(new byte[] { byteData });
        }

        private void Send(byte[] bytes)
        {
            _port.Write(bytes, 0, bytes.Length);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            HandleCommunication();
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytes = new byte[_port.BytesToRead];
            _port.Read(bytes, 0, bytes.Length);
            bytes.ToList().ForEach(b => _queue.Enqueue(b));
        }
        public bool IsData()
        {
            return _queue.Count > 0;
        }
        public byte GetData()
        {
            while (!IsData()) Thread.Sleep(500);
            return _queue.Dequeue();
        }
        public void HandleCommunication()
        {
            while(IsData())
            {
                byte data = GetData();
                switch(data)
                {
                    case 0x0f: HelloHandle();
                        break;
                    case 0x01: GetStatusHandle();
                        break;
                    case 0x02: GetSettingsHandle();
                        break;
                    case 0x22: SetSettingsHandle();
                        break;
                    case 0x03:StartHandle();
                        break;
                    case 0x04:StopHandle();
                        break;
                    case 0x17:ShootResponseHandle();
                        break;
                    default: throw new Exception("Bad Data");
                }
            }
        }

        private void ShootResponseHandle()
        {
            Console.WriteLine("received reShoot");
        }

        private void StopHandle()
        {
            _timeBetween.Stop();
            Console.WriteLine("received Stop");
            Send(0x14);
            _status = 3;
        }

        private void StartHandle()
        {
            Console.WriteLine("received Start");
            _timeBetween.Start();
            Send(0x13);
            _status = 2;
        }

        private void SetSettingsHandle()
        {
            _setting[0] = _queue.Dequeue();
            _setting[1] = _queue.Dequeue();
            _setting[2] = _queue.Dequeue();
            _setting[3] = _queue.Dequeue();
            _amount = (uint)(_setting[1] << 8) + (uint)_setting[0];
            Console.WriteLine("received setSetting, amount: "+_amount);
            Send(0x32);
            _status = 1;
        }

        private void GetSettingsHandle()
        {
            byte[] response = new byte[5];
            response[0] = 0x12;
            _setting[0] = (byte)_amount;
            _setting[1] = (byte)(_amount>>8);

            _setting.CopyTo(response, 1);
            Console.WriteLine("received getSetting");
            Send(response);
        }

        private void GetStatusHandle()
        {
            Console.WriteLine("received getStatus");

            Send(new byte[] {0x11,_status });
        }

        private void HelloHandle()
        {
            Console.WriteLine("received getStatus");
            Send(0xf0);
        }
    }
}
