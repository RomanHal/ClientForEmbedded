using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaserFeederHelper.Resources;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    internal class ResopnseHandler : DecodingHandler
    {
        private readonly Action<byte> _confirmResponse;

        public ResopnseHandler(Func<byte> getData, Action<byte[]> send,Action<byte>confirmResponse) : base(getData, send)
        {
            _confirmResponse = confirmResponse;
        }

        public override void Handle(byte data)
        {
            _confirmResponse(data);
        }
    }
}
