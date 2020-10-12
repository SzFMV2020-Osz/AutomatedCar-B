namespace AutomatedCar.SystemComponents.Packets
{
    public class HMIPacket : IReadOnlyHMIPacket
    {
        private double gaspedal;
        private double breakpedal;
        private double steering;
        private Gears gear;
        private bool acc;
        private double accDistance;
        private int accSpeed;
        private bool laneKeeping;
        private bool parkingPilot;
        private bool turnSignalRight;
        private bool turnSignalLeft;
        private string sign;

        public double Gaspedal { get => this.gaspedal; set => this.gaspedal = value; }

        public double Breakpedal { get => this.breakpedal; set => this.breakpedal = value; }

        public double Steering { get => this.steering; set => this.steering = value; }

        public Gears Gear { get => this.gear; set => this.gear = value; }

        public bool ACC { get => this.acc; set => this.acc = value; }

        public double ACCDistance { get => this.accDistance; set => this.accDistance = value; }

        public int ACCSpeed { get => this.accSpeed; set => this.accSpeed = value; }

        public bool LaneKeeping { get => this.laneKeeping; set => this.laneKeeping = value; }

        public bool ParkingPilot { get => this.parkingPilot; set => this.parkingPilot = value; }

        public bool TurnSignalRight { get => this.turnSignalRight; set => this.turnSignalRight = value; }

        public bool TurnSignalLeft { get => this.turnSignalLeft; set => this.turnSignalLeft = value; }

        public string Sign { get => this.sign; set => this.sign = value; }
    }
}