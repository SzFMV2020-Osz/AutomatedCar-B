using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    class HMIPacket : ReactiveObject, IHMIPacket
    {
        private double velocityPixelsPerSecond;
        private int rpm;
        private GearShifterPosition gearShifterPosition;
        private DriveGear driveGear;

        public void UpdateHMIPacket(double speed, int rpm, GearShifterPosition position, DriveGear driveGear)
        {
            this.VelocityPixelsPerSecond = speed;
            this.RPM = rpm;
            this.GearShifterPostion = position;
            this.DriveGear = driveGear;
        }

        public double VelocityPixelsPerSecond
        {
            get => this.velocityPixelsPerSecond;
            private set => this.RaiseAndSetIfChanged(ref this.velocityPixelsPerSecond, value);
        }

        public int RPM
        {
            get => this.rpm;
            private set => this.RaiseAndSetIfChanged(ref this.rpm, value);
        }

        public GearShifterPosition GearShifterPostion
        {
            get => this.gearShifterPosition;
            private set => this.RaiseAndSetIfChanged(ref this.gearShifterPosition, value);
        }

        public DriveGear DriveGear
        {
            get => this.driveGear;
            private set => this.RaiseAndSetIfChanged(ref this.driveGear, value);
        }
    }
}
