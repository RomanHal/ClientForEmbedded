using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Resources
{
    public enum SerialCommands
    {
         Hello,
         GetStatus,
         GetSettings,
         SetSettings,
         Start,
         Stop,
         LaserShoot
    }
}
