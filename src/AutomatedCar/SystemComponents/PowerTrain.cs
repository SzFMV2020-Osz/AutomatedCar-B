using AutomatedCar.SystemComponents.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents
{
    public class PowerTrain : SystemComponent
    {
        public PowerTrain(VirtualFunctionBus functionBus)
            : base(functionBus)
        {
            this.Engine = new EngineController();
        }

        private EngineController Engine { get; set; }

        private IPowerTrainPacket Packet { get; set; }

        public override void Process()
        {
            //world.instance.controlledcar -> itt elerni, IMovable implementacio utan SetPosition / MoveObject-tel mozgatni
            throw new NotImplementedException();
        }
    }
}
