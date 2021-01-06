using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaserFeederHelper.Resources;

namespace LaserFeederHelper.Models.Connection.Commands
{
    internal class StatusGetter : Sender
    {
        public StatusGetter(Action errorHandling, Action<byte[]> send) 
            : base(errorHandling, send,SerialCodeRsponse.Status)
        {

        }
        public void GetStatus()
        {
            Send(SerialCodeCommand.GetStatus);
            WaitingForResponse();
        }

        public override void SendCommand(SerialCommands command, object data)
        {
            if (command == SerialCommands.GetStatus)
                GetStatus();
        }
    }
}
