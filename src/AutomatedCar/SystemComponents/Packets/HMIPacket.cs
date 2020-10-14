namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;

    public class HMIPacket : ReactiveObject, IReadOnlyHMIPacket
    {
        private double gaspedal;
        private double breakpedal;
        private double steering;
        private Gears gear;
        private bool acc;
        private double accDistance = 0.8;
        private int accSpeed = 30;
        private bool laneKeeping;
        private bool parkingPilot;
        private bool turnSignalRight;
        private bool turnSignalLeft;
        private string sign = string.Empty;

        public double Gaspedal { get => this.gaspedal; set => this.RaiseAndSetIfChanged(ref this.gaspedal, value); }

        public double Breakpedal { get => this.breakpedal; set => this.RaiseAndSetIfChanged(ref this.breakpedal, value); }

        public double Steering { get => this.steering; set => this.RaiseAndSetIfChanged(ref this.steering, value); }

        public Gears Gear { get => this.gear; set => this.RaiseAndSetIfChanged(ref this.gear, value); }

        public bool Acc { get => this.acc; set => this.RaiseAndSetIfChanged(ref this.acc, value); }

        public double AccDistance { get => this.accDistance; set => this.RaiseAndSetIfChanged(ref this.accDistance, value); }

        public int AccSpeed { get => this.accSpeed; set => this.RaiseAndSetIfChanged(ref this.accSpeed, value); }

        public bool LaneKeeping { get => this.laneKeeping; set => this.RaiseAndSetIfChanged(ref this.laneKeeping, value); }

        public bool ParkingPilot { get => this.parkingPilot; set => this.RaiseAndSetIfChanged(ref this.parkingPilot, value); }

        public bool TurnSignalRight { get => this.turnSignalRight; set => this.RaiseAndSetIfChanged(ref this.turnSignalRight, value); }

        public bool TurnSignalLeft { get => this.turnSignalLeft; set => this.RaiseAndSetIfChanged(ref this.turnSignalLeft, value); }

        public string Sign { get => this.sign; set => this.RaiseAndSetIfChanged(ref this.sign, value); }
    }
}