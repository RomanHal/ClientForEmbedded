using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Resources
{
    public static class SerialCodeRsponse
    {
        public static byte Hello { get => 0xf0; }
        public static byte Status { get => 0x11; }
        public static byte Settings { get => 0x12; }
        public static byte SetSettingsOk { get => 0x32; }
        public static byte Started { get => 0x13; }
        public static byte Stopped { get => 0x14; }
        public static byte LaserShootOk { get => 0x17; }

    }
}
