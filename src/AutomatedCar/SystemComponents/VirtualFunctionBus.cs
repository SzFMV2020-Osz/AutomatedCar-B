namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.SystemComponents.Packets;
    using System.Collections.Generic;

    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadOnlyDummyPacket DummyPacket { get; set; }

        public IReadOnlyHMIPacket HMIPacket { get; set; }

        // public IReadOnlyDebugPacket DebugPacket { get; set; } // TODO remove

        public IReadOnlyPowerTrainPacket PowerTrainPacket { get; set; }

        public VirtualFunctionBus()
        {
            this.PowerTrainPacket = new PowerTrainPacket();
        }

        public void RegisterComponent(SystemComponent component)
        {
            this.components.Add(component);
        }

        protected override void Tick()
        {
            foreach (SystemComponent component in this.components)
            {
                component.Process();
            }
        }
    }
}