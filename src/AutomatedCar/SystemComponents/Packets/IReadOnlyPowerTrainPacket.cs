using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadOnlyPowerTrainPacket
    {
        public double Velocity { get; }
        public int RPM { get; }
        public Gears GearShifterPostion { get; }
        public DriveGear DriveGear { get; }
    }
}
