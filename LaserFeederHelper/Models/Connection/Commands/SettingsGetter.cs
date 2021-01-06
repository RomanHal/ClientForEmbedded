using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Commands
{
    internal class SettingsGetter : Sender
    {
        public SettingsGetter(Action errorHandling, Action<byte[]> send) : base(errorHandling, send, SerialCodeRsponse.Settings)
        {
        }

        public override void SendCommand(SerialCommands command, object data)
        {
            if(command==SerialCommands.GetSettings)
            {
                Send(SerialCodeCommand.GetSettings);
                WaitingForResponse();
            }
        }
    }
}
