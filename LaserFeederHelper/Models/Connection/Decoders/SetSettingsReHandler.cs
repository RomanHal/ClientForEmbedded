using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    internal class SetSettingsReHandler : DecodingHandler
    {
        public SetSettingsReHandler(Func<byte> getData, Action<byte[]> send, Action<byte> confirmResponse) : base(getData, send)
        {
            ConfirmResponse = confirmResponse;
        }

        public Action<byte> ConfirmResponse { get; }

        public override void Handle(byte data)
        {
            throw new NotImplementedException();
        }
    }
}
