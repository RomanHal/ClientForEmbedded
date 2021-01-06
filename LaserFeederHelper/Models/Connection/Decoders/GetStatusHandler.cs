using LaserFeederHelper.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    internal class GetStatusHandler : DecodingHandler
    {
        private readonly Action<DeviceStatus> _getStatus;

        public DeviceStatus Status { get; private set; }
        public GetStatusHandler(Func<byte> getData, Action<byte[]> send, Action<DeviceStatus> getStatus) : base(getData, send)
        {
            _getStatus = getStatus;
        }

        public override void Handle(byte data)
        {
            if (data == SerialCodeRsponse.Status)
            {
                Status = (DeviceStatus)GetData();
                _getStatus(Status);
            }
        }
    }
}
