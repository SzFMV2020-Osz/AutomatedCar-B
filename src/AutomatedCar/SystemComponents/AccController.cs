namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Linq;
    using System.Numerics;
    using Models;
    using Packets;

    public class AccController: SystemComponent
    {
        public AccController(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
        }

        public override void Process()
        {
            var hmiPacket = (HMIPacket)virtualFunctionBus.HMIPacket;
            var powerTrainPacket = (PowerTrainPacket)virtualFunctionBus.PowerTrainPacket;
            var speed = kmh_to_pxs(hmiPacket.AccSpeed);

            if (hmiPacket.Acc)
            {
                if (hmiPacket.Sign != null && hmiPacket.Sign != "")
                {
                    var lastSeenSign = int.Parse(hmiPacket.Sign);
                    if (kmh_to_pxs(hmiPacket.AccSpeed) > kmh_to_pxs(lastSeenSign))
                    {
                        speed = kmh_to_pxs(lastSeenSign);
                    }
                }

                if (powerTrainPacket.Velocity < speed)
                {
                    hmiPacket.Gaspedal = 40;
                }
                else if (powerTrainPacket.Velocity > speed)
                {
                    hmiPacket.Gaspedal = 0;
                }
            }
        }

        private static int kmh_to_pxs(int AccSpeed)
        {
            return AccSpeed * (50 * 1000) / (60 * 60);
        }
    }
}