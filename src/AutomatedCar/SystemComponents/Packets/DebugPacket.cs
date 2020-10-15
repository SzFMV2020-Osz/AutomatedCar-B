using ReactiveUI;
using System.Reflection;

namespace AutomatedCar.SystemComponents.Packets
{
    public class DebugPacket : ReactiveObject, IReadOnlyDebugPacket
    {
        private bool polygon;
        private bool utrasoundSensor;
        private bool radarSensor;
        private bool boardCamera;

        public bool Polygon { get => this.polygon; set => this.RaiseAndSetIfChanged(ref this.polygon, value); }

        public bool UtrasoundSensor { get => this.utrasoundSensor; set => this.RaiseAndSetIfChanged(ref this.utrasoundSensor, value); }

        public bool RadarSensor { get => this.radarSensor; set => this.RaiseAndSetIfChanged(ref this.radarSensor, value); }

        public bool BoardCamera { get => this.boardCamera; set => this.RaiseAndSetIfChanged(ref this.boardCamera, value); }
    }
}