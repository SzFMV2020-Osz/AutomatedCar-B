using System;
using AutomatedCar.SystemComponents;
using Avalonia;

namespace AutomatedCar.Models
{
    public class AEB
    {
        public AEB()
        {
        }

        public bool IsUseable(){
            return !isCarFasterThanKmh(70);
        }

        private bool isCarFasterThanKmh(double speed) {
            return World.Instance.ControlledCar.Speed > kmh_into_pxs(speed);
        }

        private double kmh_into_pxs(double kmh) {
            return kmh_into_ms(kmh)*50;
        }

        private double kmh_into_ms(double kmh) {
            return (kmh*1000)/(60*60);
        }

        public double getStoppingDistanceTo_inPixels(WorldObject worldObject)  {
            double carSpeed = pxm_into_ms(World.Instance.ControlledCar.Speed);
            double decceleration =  9;

            return computeObjectDistanceFromCar_inPixel(worldObject) - (Math.Pow(carSpeed, 2)/decceleration)*50; 
        }

        private double pxm_into_ms(double pxm){
            return pxm/50;
        }

        private double computeObjectDistanceFromCar_inPixel(WorldObject item){
            double x = item.X-World.Instance.ControlledCar.X;
            double y = item.Y-World.Instance.ControlledCar.Y;
            Vector V = new Vector(x, y);
            return (V.Length-(World.Instance.ControlledCar.Height/2));
        }
    }
}