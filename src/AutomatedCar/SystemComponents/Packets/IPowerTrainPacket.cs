namespace AutomatedCar.SystemComponents.Packets
{
    public interface IPowerTrainPacket
    {
         public double VelocityPixelsPerSecond { get; }
        public int Rpm { get; }
        public Gears GearShifterPosition { get; }
        public DriveGear DriveGear { get;}
        public void UpdateHMIPacket(double speed, int rpm, Gears position, DriveGear driveGear);
    }
}
