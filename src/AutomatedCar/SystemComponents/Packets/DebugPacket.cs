using System.Reflection;

namespace AutomatedCar.SystemComponents.Packets
{
    public class DebugPacket : IReadOnlyDebugPacket
    {
        private bool polygon;
        private bool utrasoundSensor;
        private bool radarSensor;
        private bool boardCamera;

        public bool Polygon { get => this.polygon; set => this.polygon = value; }

        public bool UtrasoundSensor { get => this.utrasoundSensor; set => this.utrasoundSensor = value; }

        public bool RadarSensor { get => this.radarSensor; set => this.radarSensor = value; }

        public bool BoardCamera { get => this.boardCamera; set => this.boardCamera = value; }

        public void PolygonSet(bool change)
        {
            if (change)
            {
                this.polygon = !this.polygon;
            }
        }

        public void UtrasoundSensorSet(bool change)
        {
            if (change)
            {
                this.utrasoundSensor = !this.utrasoundSensor;
            }
        }

        public void RadarSensorSet(bool change)
        {
            if (change)
            {
                this.radarSensor = !this.radarSensor;
            }
        }

        public void BoardCameraSet(bool change)
        {
            if (change)
            {
                this.boardCamera = !this.boardCamera;
            }
        }
    }
}