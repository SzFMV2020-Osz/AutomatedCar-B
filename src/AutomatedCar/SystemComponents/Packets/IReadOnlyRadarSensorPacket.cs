using AutomatedCar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadOnlyRadarSensorPacket
    {
        public List<WorldObject> DangerousObjects { get; }
    }
}
