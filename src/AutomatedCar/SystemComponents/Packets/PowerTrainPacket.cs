using ReactiveUI;

namespace AutomatedCar.SystemComponents.Packets
{
    public class PowerTrainPacket : ReactiveObject, IReadOnlyPowerTrainPacket
    {
        double velocity;
        int rpm;
        Gears gearShifterPostion;
        DriveGear driveGear;

        public void UpdatePowerTrainPacket(double speed, int rpm, Gears position, DriveGear driveGear)
        {
            this.Velocity = speed;
            this.RPM = rpm;
            this.GearShifterPostion = position;
            this.DriveGear = driveGear;
        }

        public double Velocity 
        { get => this.velocity; 
            set => this.RaiseAndSetIfChanged(ref this.velocity, value); }

        public int RPM { get => this.rpm; set => this.RaiseAndSetIfChanged(ref this.rpm, value); }

        public Gears GearShifterPostion { get => this.gearShifterPostion; set => this.RaiseAndSetIfChanged(ref this.gearShifterPostion, value); }

        public DriveGear DriveGear { get => this.driveGear; set => this.RaiseAndSetIfChanged(ref this.driveGear, value); }
    }
}
