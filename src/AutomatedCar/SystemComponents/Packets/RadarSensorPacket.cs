using AutomatedCar.Models;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents.Packets
{
    public class RadarSensorPacket : IReadOnlyRadarSensorPacket
    {
        public List<WorldObject> DangerousObjects { get; private set; }

        public void Update(List<WorldObject> dangerousObjects)
        {
            this.DangerousObjects = dangerousObjects;
        }
    }
}
