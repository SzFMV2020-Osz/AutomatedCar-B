using AutomatedCar.SystemComponents.Packets;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AutomatedCar.SystemComponents
{
    public interface ISteeringController
    {
        public Vector2 NewCarPosition { get; }

        public double NewCarAngle { get; set; }

        public void UpdateSteeringProperties(IReadOnlyHMIPacket packet);
    }
}
