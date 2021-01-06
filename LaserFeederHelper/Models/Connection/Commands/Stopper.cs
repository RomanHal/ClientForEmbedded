using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Commands
{
    class Stopper: Sender
    {
        public Stopper(Action errorHandling, Action<byte[]> send) 
            : base(errorHandling, send,LaserFeederHelper.Resources.SerialCodeRsponse.Stopped)
        {
        }

        public override void SendCommand(SerialCommands command, object data)
        {
            if (command == SerialCommands.Stop) SendStop();
        }

        private void SendStop()
        {
            Send(SerialCodeCommand.Stop );
            WaitingForResponse();
        }
     
    }
}
