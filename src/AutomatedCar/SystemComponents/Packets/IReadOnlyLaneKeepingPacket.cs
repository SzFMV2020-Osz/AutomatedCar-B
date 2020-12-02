using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadOnlyLaneKeepingPacket
    {
        public bool Active { get; }
        public double Steering { get; }
    }
}