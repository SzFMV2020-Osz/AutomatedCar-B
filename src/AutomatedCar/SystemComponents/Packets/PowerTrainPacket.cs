namespace AutomatedCar.SystemComponents.Packets
{
    class PowerTrainPacket : IPowerTrainPacket
    {
        private double velocityPixelsPerSecond;
        private int rpm;
        private Gears gearShifterPosition;
        private DriveGear driveGear;

        public void UpdateHMIPacket(double speed, int rpm, Gears position, DriveGear driveGear)
        {
            this.VelocityPixelsPerSecond = speed;
            this.Rpm = rpm;
            this.GearShifterPosition = position;
            this.DriveGear = driveGear;
        }


        public double VelocityPixelsPerSecond { get => this.velocityPixelsPerSecond; set => this.velocityPixelsPerSecond = value; }
         public int Rpm { get => this.rpm; set => this.rpm = value; }
         public Gears GearShifterPosition  { get => this.gearShifterPosition; set => this.gearShifterPosition = value; }
         public DriveGear DriveGear { get => this.driveGear; set => this.driveGear = value; }



        /*public double VelocityPixelsPerSecond
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
        }*/
    }
}
