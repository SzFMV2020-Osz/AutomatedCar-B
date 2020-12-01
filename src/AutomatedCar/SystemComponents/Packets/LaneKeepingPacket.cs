using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    public class LaneKeepingPacket
    {
        public bool Active { get; set; }
        public double Steering { get; set; }

    }
}
