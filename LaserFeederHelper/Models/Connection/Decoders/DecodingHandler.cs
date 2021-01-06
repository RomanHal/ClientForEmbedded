using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    abstract class DecodingHandler
    {
        private readonly Func<byte> _getData;
        private readonly Action<byte[]> _send;

        public DecodingHandler(Func<byte> getData,Action<byte[]> send )
        {
            _getData = getData;
            _send = send;
        }
        protected byte GetData()
        {
            return _getData();
        }
        protected void Send(byte[] data)
        {
            _send(data);
        }
        protected void Send(byte data)
        {
            _send(new byte[] { data });
        }
        public abstract void Handle(byte data);
    }
}
