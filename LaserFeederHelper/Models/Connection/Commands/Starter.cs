using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Commands
{
    class Starter : Sender
    {
        public Starter(Action errorHandling, Action<byte[]> send) 
            : base(errorHandling, send,LaserFeederHelper.Resources.SerialCodeRsponse.Started)
        {
        }

        public override void SendCommand(SerialCommands command, object data)
        {
            if (command == SerialCommands.Start)
                SendStart();
        }

        public void SendStart()
        {
            Send(SerialCodeCommand.Start);
            WaitingForResponse();
        }

    }
}
