using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Commands
{
    class ParameterChanger : Sender
    {

        public ParameterChanger(Action errorHandling,Action<byte[]> send) 
            : base(errorHandling,send,SerialCodeRsponse.SetSettingsOk)
        {
        }

        private void ChangeParameters(uint amount,double timeS)
        {
            var time_ds = Convert.ToUInt32(timeS * 10 + 0.5);
            byte header = SerialCodeCommand.SetSettings;
            byte bAmountL = (byte)amount;
            byte bAmountH = (byte)(amount >> 8);
            byte btimeL = (byte)(time_ds );
            byte btimeH = (byte)(time_ds >> 8);
            Send( new byte[] { header, bAmountL, bAmountH, btimeL, btimeH });
            WaitingForResponse();
        }

        public override void SendCommand(SerialCommands command, object data)
        {
            if(command==SerialCommands.SetSettings)
            {
                (uint, double) tuple = ((uint, double))data;
                ChangeParameters(tuple.Item1, tuple.Item2);
            }


        }
    }
}
