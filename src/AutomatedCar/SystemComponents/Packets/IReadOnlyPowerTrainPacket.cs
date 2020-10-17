﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadOnlyPowerTrainPacket
    {
        public double Velocity { get; }
        public int RPM { get; }
        public GearShifterPosition GearShifterPostion { get; }
        public Gears DriveGear { get; }

        public void UpdatePowerTrainPacket(double speed, int rpm, Gears position, DriveGear driveGear);
    }
}
