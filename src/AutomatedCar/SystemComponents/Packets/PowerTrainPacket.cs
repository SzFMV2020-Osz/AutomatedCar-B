namespace AutomatedCar.SystemComponents.Packets
{
    public class PowerTrainPacket : IReadOnlyPowerTrainPacket
    {
        public void UpdatePowerTrainPacket(double speed, int rpm, Gears position, DriveGear driveGear)
        {
            this.Velocity = speed;
            this.RPM = rpm;
            this.GearShifterPostion = position;
            this.DriveGear = driveGear;
        }

        public double Velocity { get; private set; }

        public int RPM { get; private set; }

        public Gears GearShifterPostion { get; private set; }

        public DriveGear DriveGear { get; private set; }
    }
}
