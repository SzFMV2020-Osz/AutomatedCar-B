using AutomatedCar.Models;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents.Packets
{
    public class RadarSensorPacket : IReadOnlyRadarSensorPacket
    {
        public List<NoticedObject> DangerousObjects { get; private set; }

        public void Update(List<NoticedObject> dangerousObjects)
        {
            this.DangerousObjects = dangerousObjects;
        }
    }
}
