using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    public interface IHMIPacket
    {
        public double VelocityPixelsPerSecond { get; }
        public int RPM { get; }
        public GearShifterPosition GearShifterPostion { get; }
        public DriveGear DriveGear { get;}

        public void UpdateHMIPacket(double speed, int rpm, GearShifterPosition position, DriveGear driveGear);
    }
}
