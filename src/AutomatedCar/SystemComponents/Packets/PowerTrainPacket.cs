namespace AutomatedCar.SystemComponents.Packets
{
    class PowerTrainPacket : IPowerTrainPacket
    {
        public int GasPedal { get; private set; }

        public int BrakePedal { get; private set; }

        public int SteeringWheel { get; private set; }

        public GearShifterPosition GearShifterPosition { get; private set; }

        public PowerTrainPacket()
        {
            this.GasPedal = 0;
            this.BrakePedal = 0;
            this.GearShifterPosition = GearShifterPosition.N;
            this.SteeringWheel = 0;
        }

        public void UpdatePowerTrainPacket(int gasPedal, int brakePedal, int steeringWheel, GearShifterPosition position)
        {
            this.GasPedal = gasPedal;
            this.BrakePedal = brakePedal;
            this.SteeringWheel = steeringWheel;
            this.GearShifterPosition = position;
        }
    }
}
