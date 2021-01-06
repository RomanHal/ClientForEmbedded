using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    public class DecoderController
    {
        readonly List<DecodingHandler> decoders = new List<DecodingHandler>();
        private readonly Func<byte> _getData;

        public double TimeS { get; private set; }
        public uint Amount { get; private set; }
        public DeviceStatus Status { get; private set; }
        public DecoderController(Func<byte>getData,Action<byte[]>send,Action<byte>confirmResponse,Action errorHandler)
        {
            decoders.Add(new GetSettingsHandler(getData, send, (data) => { TimeS = data.timeS; Amount = data.amount; }));
            decoders.Add(new GetStatusHandler(getData, send, e => Status = e));
            decoders.Add(new ResopnseHandler(getData, send, confirmResponse));
            decoders.Add(new ShootHandler(getData, send, errorHandler));
            _getData = getData;
        }

        public void HandleByte(byte command)
        {
            decoders.ForEach(d => d.Handle(command));
        }
        

        
    }
}
