namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomatedCar.SystemComponents.Packets;

    public class ParkingPilot : SystemComponent
    {
        private EngineController Engine { get; set; }

        private ISteeringController Steering { get; set; }

        private IReadOnlyHMIPacket HMIPacket { get; set; }

        private IReadOnlyPowerTrainPacket PowerTrainPacket { get; set; }

        private IReadOnlyRadarSensorPacket RadarSensorPacket { get; set; }

        private IReadOnlyDebugPacket DebugPacket { get; set; }

        public ParkingPilot(VirtualFunctionBus functionBus)
          : base(functionBus)
        {
            this.Engine = new EngineController();
            this.Steering = new SteeringController();
            this.PowerTrainPacket = this.virtualFunctionBus.PowerTrainPacket;
            this.HMIPacket = this.virtualFunctionBus.HMIPacket;
            this.RadarSensorPacket = this.virtualFunctionBus.RadarSensorPacket;
            this.DebugPacket = this.virtualFunctionBus.DebugPacket;
        }

        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}
