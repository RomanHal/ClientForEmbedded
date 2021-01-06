using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaserFeederHelper.Resources;

namespace LaserFeederHelper.Models.Connection.Commands
{
    class CommandsController
    {
        readonly List<Sender> senders = new List<Sender>();
        public CommandsController(Action<byte[]> send,Action errorhandling)
        {
            senders.Add( new Starter(errorhandling, send));
            senders.Add(new Stopper(errorhandling, send));
            senders.Add(new ParameterChanger(errorhandling, send));
            senders.Add(new SettingsGetter(errorhandling, send));
            senders.Add(new StatusGetter(errorhandling, send));

        }

        public void SendCommand(SerialCommands command,object data)
        {
            senders.ForEach(x => x.SendCommand(command, data));
        }
        public void SendConfirmation(byte command)
        {
            senders.ForEach(x => x.ConfirmResponse(command));
        }
    }
}
