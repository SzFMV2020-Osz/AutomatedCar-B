namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.SystemComponents.Packets;
    using System.Collections.Generic;

    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadOnlyHMIPacket HMIPacket { get; set; }

        public IReadOnlyPowerTrainPacket PowerTrainPacket { get; set; }

        public IReadOnlyRadarSensorPacket RadarSensorPacket { get; set; }

        public IReadOnlyAEBAction AEBActionPacket { get; set; } 

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