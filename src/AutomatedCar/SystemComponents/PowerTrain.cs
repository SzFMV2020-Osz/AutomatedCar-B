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
            this.PowerTrainPacket = new PowerTrainPacket();
            this.HMIPacket = this.virtualFunctionBus.HMIPacket;
        }

        private EngineController Engine { get; set; }

        private ISteeringController Steering { get; set; }

        private IReadOnlyHMIPacket HMIPacket { get; set; }

        private PowerTrainPacket PowerTrainPacket { get; set; }

        public override void Process()
        {
            this.Engine.UpdateEngineProperties(this.HMIPacket);
            this.Steering.UpdateSteeringProperties(this.HMIPacket);
            this.UpdateCarPosition();
            this.UpdatePowerTrainPacket();
        }

        private void UpdateCarPosition()
        {
            World.Instance.ControlledCar.Move(this.Steering.NewCarPosition);
            World.Instance.ControlledCar.Angle = this.Steering.NewCarAngle;
            World.Instance.ControlledCar.Speed = (int)this.Engine.VelocityPixelsPerSecond;
        }

        private void UpdatePowerTrainPacket()
        {
            this.PowerTrainPacket.UpdatePowerTrainPacket(this.Engine.VelocityPixelsPerSecond, this.Engine.RPM, this.Engine.GearShifter.Position, this.Engine.GearShifter.CurrentDriveGear.Label);
            this.virtualFunctionBus.PowerTrainPacket = this.PowerTrainPacket;
        }
    }
}
