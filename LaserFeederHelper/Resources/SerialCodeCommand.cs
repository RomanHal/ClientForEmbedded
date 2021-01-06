using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Resources
{
    public static class SerialCodeCommand
    {
        public static byte Hello { get => 0x0f; }
        public static byte GetStatus { get => 0x01; }
        public static byte GetSettings { get => 0x02; }
        public static byte SetSettings { get => 0x22; }
        public static byte Start { get => 0x03; }
        public static byte Stop { get => 0x04; }
        public static byte LaserShoot { get => 0x07; }

    }
}
