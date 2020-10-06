namespace AutomatedCar.SystemComponents
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    public class PowerTrain : SystemComponent
    {
        public PowerTrain(VirtualFunctionBus functionBus)
            : base(functionBus)
        {
            this.Engine = new EngineController();
            this.Steering = new SteeringController();
        }

        private EngineController Engine { get; set; }

        private SteeringController Steering { get; set; }

        private IPowerTrainPacket Packet { get; set; }

        public override void Process()
        {
            this.Engine.UpdateEngineProperties(this.Packet);
            this.Steering.UpdateSteeringProperties(this.Packet);
            this.UpdateCarPosition();
        }

        private void UpdateCarPosition()
        {
            World.Instance.ControlledCar.Move(this.Steering.NewCarPosition);
            World.Instance.ControlledCar.Angle = this.Steering.NewCarAngle;
        }
    }
}
