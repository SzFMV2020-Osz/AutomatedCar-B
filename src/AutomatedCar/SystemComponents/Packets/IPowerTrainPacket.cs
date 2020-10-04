namespace AutomatedCar.SystemComponents.Packets
{
    public enum GearShifterPosition
    {
        D,
        N,
        R,
        P
    }

    public interface IPowerTrainPacket
    {
        public int GasPedal { get; set; }

        public int BrakePedal { get; set; }

        public int SteeringWheel { get; set; }

        public GearShifterPosition GearShifterPosition { get; set; }
    }
}
