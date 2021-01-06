using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LaserFeederHelper.Models.Connection.Commands
{
    abstract class Sender
    {
        private Timer _timer;
        private readonly Action _errorHandling;
        private readonly Action<byte[]> _send;
        private readonly byte _response;

        public Sender(Action errorHandling, Action<byte[]> send,byte response)
        {
            _timer = new Timer(1000);
            this._errorHandling = errorHandling;
            this._send = send;
            this._response = response;
            _timer.Elapsed += Timer_Elapsed;
            _timer.AutoReset = false;
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _errorHandling(); 
        }

        protected void Send(byte[] bytes)
        {
            _send(bytes);
        }
        protected void Send(byte data)
        {
            _send(new byte[] { data});
        }
        public void ConfirmResponse(byte response)
        {
            if(response==_response)
                _timer.Stop();  
        }
        protected void WaitingForResponse()
        {
            _timer.Start();
        }
        public abstract void SendCommand(SerialCommands command, object data);
    }
}
