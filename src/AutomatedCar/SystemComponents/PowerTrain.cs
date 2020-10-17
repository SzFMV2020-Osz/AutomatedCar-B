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
            this.Steering = new TestSteering();
            this.Packet = this.virtualFunctionBus.HMIPacket;
        }

        private EngineController Engine { get; set; }

        private ISteeringController Steering { get; set; }

        private IReadOnlyHMIPacket Packet { get; set; }

        public override void Process()
        {
            this.Engine.UpdateEngineProperties(this.Packet);
            this.Steering.UpdateSteeringProperties(this.Packet);
            this.UpdateCarPosition();
            //this.UpdateHMIPacket();
        }

        private void UpdateCarPosition()
        {
            World.Instance.ControlledCar.Move(this.Steering.NewCarPosition);
            World.Instance.ControlledCar.Angle = this.Steering.NewCarAngle;
            World.Instance.ControlledCar.Speed = (int)this.Engine.VelocityPixelsPerSecond;
        }

        //private void UpdateHMIPacket()
        //{
        //    this.virtualFunctionBus.HMIPacket.UpdateHMIPacket(this.Engine.VelocityPixelsPerSecond, this.Engine.RPM, this.Engine.GearShifter.Position, this.Engine.GearShifter.CurrentDriveGear.Label);
        //}
    }
}
