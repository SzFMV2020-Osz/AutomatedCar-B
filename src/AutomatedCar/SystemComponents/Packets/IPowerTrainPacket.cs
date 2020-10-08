namespace AutomatedCar.SystemComponents.Packets
{
    public interface IPowerTrainPacket
    {
        public int GasPedal { get; }

        public int BrakePedal { get; }

        public int SteeringWheel { get; }

        public GearShifterPosition GearShifterPosition { get; }

        public void UpdatePowerTrainPacket(int gasPedal, int brakePedal, int steeringWheel, GearShifterPosition position);
    }
}
