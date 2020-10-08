namespace AutomatedCar.SystemComponents.Packets
{
    public class DebugPacket : IReadOnlyDebugPacket
    {
        private bool utrasoundSensor;
        private bool radarSensor;
        private bool boardCamera;

        public bool UtrasoundSensor { get => this.utrasoundSensor; set => this.utrasoundSensor = value; }

        public bool RadarSensor { get => this.radarSensor; set => this.radarSensor = value; }

        public bool BoardCamera { get => this.boardCamera; set => this.boardCamera = value; }
    }
}