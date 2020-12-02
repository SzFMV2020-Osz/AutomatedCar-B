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
            var speed = hmiPacket.AccSpeed;

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
                foreach (var wo in wos)
                {
                    if (wo is NpcCar)
                    {
                        var wom = (wo.Angle + 90) % 360;
                        var cam = car.Angle % 360;
                        var dif = Math.Abs(wom - cam) % 360;

                        if (dif > 180)
                        {
                            dif = 360 - dif;
                        }

                        if (dif > 60)
                        {
                            var cv = new Vector2(car.X, car.Y);
                            var nv = new Vector2((wo as NpcCar).X, (wo as NpcCar).Y);
                            var dis = Vector2.Distance(cv, nv);

                            if (dis < 50 * Kmh2ms(Pxs2kmh(powerTrainPacket.Velocity)) * hmiPacket.AccDistance)
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