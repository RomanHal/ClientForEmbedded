using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaserFeederHelper.Resources;

namespace LaserFeederHelper.Models.Connection.Interpreters
{
    internal class GetSettingsHandler : DecodingHandler
    {
        private readonly Action<(uint amount, double timeS)> _getSettings;

        public uint Amount { get; private set; }
        public double TimeS { get; private set; }
        public GetSettingsHandler(Func<byte> getData, Action<byte[]> send, Action<(uint amount, double timeS)> getSettings) : base(getData, send)
        {
            _getSettings = getSettings;
        }

        private void GetSettings(byte[] bytes)
        {
            var amount= (uint)(bytes[0] << 8) + (uint)bytes[1];
            Amount = amount;
            uint time_ds= (uint)(bytes[2] << 8) + (uint)bytes[3];
            double time = Convert.ToDouble(time_ds);
            time /= 10;
            TimeS = time;
            _getSettings((amount, time));
        }

        public override void Handle(byte data)
        {
            if(data==SerialCodeRsponse.Settings)
            {
                GetSettings(new byte[] { GetData(), GetData(), GetData(), GetData() });
            }
        }
    }
}
