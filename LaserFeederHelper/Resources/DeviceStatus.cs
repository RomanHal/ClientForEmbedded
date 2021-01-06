using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserFeederHelper.Resources
{
    public enum DeviceStatus:byte
    {
        IDLE,
        READY,
        WORKING,
        PAUSE,
        ERROR
    }
}
