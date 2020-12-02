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
            var aebActionPacket = (AEBAction)virtualFunctionBus.AEBActionPacket;
            var speed = hmiPacket.AccSpeed;

            if (aebActionPacket.Active)
            {
                World.Instance.ControlledCar.HumanMachineInterface.Acc = false;
            }

            if (hmiPacket.Acc)
            {
                hmiPacket.Gaspedal = 0;
                if (hmiPacket.Sign != null && hmiPacket.Sign != "")
                {
                    var lastSeenSign = int.Parse(hmiPacket.Sign);
                    if (speed > lastSeenSign)
                    {
                        speed = lastSeenSign;
                    }
                }

                if (powerTrainPacket.Velocity < Kmh2pxs(speed))
                {
                    hmiPacket.Gaspedal = 100;
                }

                var car = World.Instance.ControlledCar;
                var radar = car.Radar;
                var wos = radar.WorldObjects;
                foreach (var selectedObjectFromRadar in wos)
                {
                    if (selectedObjectFromRadar is NpcCar)
                    {
                        var mpcAngleModulo = (selectedObjectFromRadar.Angle) % 360;
                        var controlledCarAngleModulo = car.Angle % 360;
                        var angleDifference = Math.Abs(mpcAngleModulo - controlledCarAngleModulo) % 360;

                        if (angleDifference > 180)
                        {
                            angleDifference = 360 - angleDifference;
                        }

                        if (angleDifference < 60)
                        {
                            var controlledCarPosition = new Vector2(car.X, car.Y);
                            var npmPosition = new Vector2((selectedObjectFromRadar as NpcCar).X, (selectedObjectFromRadar as NpcCar).Y);
                            var distance = Vector2.Distance(controlledCarPosition, npmPosition);

                            if (distance < 50 * Kmh2ms(Pxs2kmh(powerTrainPacket.Velocity)) * hmiPacket.AccDistance)
                            {
                                hmiPacket.Gaspedal = 0;
                            }
                        }
                    }
                }
            }
        }

        private static double Map(double from, double oldMax, double oldMin, double newMax, double newMin)
        {
            return ((from - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
        }

        private static double Kmh2pxs(double kmh)
        {
            return kmh * (double)(50 * 1000) / (double)(60 * 60);
        }

        private static double Pxs2kmh(double pxs)
        {
            return pxs * (double)(60 * 60) / (double)(50 * 1000);
        }

        private static double Kmh2ms(double kmh)
        {
            return kmh / 3.6;
        }
    }
}