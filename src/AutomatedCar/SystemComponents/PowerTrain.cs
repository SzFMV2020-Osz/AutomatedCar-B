namespace AutomatedCar.SystemComponents
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;

    public class PowerTrain : SystemComponent
    {
        public PowerTrain(VirtualFunctionBus functionBus,int start_x, int start_y)
            : base(functionBus)
        {
            this.Engine = new EngineController();
            this.Steering = new SteeringController();
            this.PowerTrainPacket = new PowerTrainPacket();
            this.HMIPacket = this.virtualFunctionBus.HMIPacket;
            this.AEBActionPacket = virtualFunctionBus.AEBActionPacket;
            this.virtualFunctionBus.PowerTrainPacket = this.PowerTrainPacket;
            this.Steering.SetStartPosition(start_x, start_y);
        }

        public  EngineController Engine { get; private set; }

        private ISteeringController Steering { get; set; }

        private IReadOnlyHMIPacket HMIPacket { get; set; }

        private PowerTrainPacket PowerTrainPacket { get; set; }

        private IReadOnlyAEBAction AEBActionPacket { get; set; }

        public override void Process()
        {
            this.Engine.UpdateEngineProperties(this.HMIPacket,this.AEBActionPacket);
            this.Steering.UpdateSteeringProperties(this.HMIPacket);
            this.UpdateCarPosition();
            this.UpdatePowerTrainPacket();
        }

        private void UpdateCarPosition()
        {
            World.Instance.ControlledCar.Move(this.Steering.NewCarPosition);
            World.Instance.ControlledCar.Angle = this.Steering.NewCarAngle;
            World.Instance.ControlledCar.speed = (int)this.Engine.VelocityPixelsPerSecond;
        }

        private void UpdatePowerTrainPacket()
            => this.PowerTrainPacket.UpdatePowerTrainPacket(
                this.Engine.VelocityPixelsPerSecond,
                this.Engine.RPM,
                this.Engine.GearShifter.Position,
                this.Engine.GearShifter.CurrentDriveGear.Label);
    }
}
