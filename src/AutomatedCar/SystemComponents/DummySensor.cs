using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;

namespace AutomatedCar.SystemComponents {
    using Models;
    using Packets;

    /// <summary>This is a dummy sensor for demonstrating the codebase.
    /// It calculates distance per coordinate between the controlled car and the dummy Circle object</summary>
    public class DummySensor : SystemComponent {
        private DummyPacket dummyPacket;
        public DummySensor (VirtualFunctionBus virtualFunctionBus) : base (virtualFunctionBus) {
            this.virtualFunctionBus = virtualFunctionBus;
            virtualFunctionBus.registerComponent (this);

            dummyPacket = new DummyPacket ();
            virtualFunctionBus.DummyPacket = dummyPacket;
        }
        override public void Process () {
            dummyPacket.DistanceX = (int)World.Instance.WorldObjects[0].PositionPoint.X - World.Instance.ControlledCar.X;
            dummyPacket.DistanceY = (int)World.Instance.WorldObjects[0].PositionPoint.Y - World.Instance.ControlledCar.Y;
        }
    }
}