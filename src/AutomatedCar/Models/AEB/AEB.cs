using System;
using AutomatedCar.SystemComponents;
using Avalonia;
using AutomatedCar.SystemComponents.Packets;

namespace AutomatedCar.Models
{
    public class AEB: SystemComponent
    {
        public AutomatedCar controlledCar;
        VirtualFunctionBus virtualFunctionBus;
        public AEB()
        {
            this.controlledCar = World.Instance.ControlledCar;
        }

        public AEB(VirtualFunctionBus virtualFunctionBus = null): base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            this.controlledCar = World.Instance.ControlledCar;
            virtualFunctionBus.AEBActionPacket = new EABAction();
        }

        public bool IsUseable(){
            return !isCarFasterThanKmh(70);
        }

        private bool isCarFasterThanKmh(double speed) {
            return this.controlledCar.Speed > kmh_into_pxs(speed);
        }

        private double kmh_into_pxs(double kmh) {
            return kmh_into_ms(kmh)*50;
        }

        private double kmh_into_ms(double kmh) {
            return (kmh*1000)/(60*60);
        }

        public double getStoppingDistanceTo_inPixels(WorldObject worldObject)  {
            double carSpeed = pxm_into_ms(this.controlledCar.Speed);
            double decceleration =  9;

            return Math.Abs(computeObjectDistanceFromCar_inPixel(worldObject)-((Math.Pow(carSpeed, 2)/decceleration)*50)); 
        }

        private double pxm_into_ms(double pxm){
            return pxm/50;
        }

        private double computeObjectDistanceFromCar_inPixel(WorldObject item){
            double x = item.X-this.controlledCar.X;
            double y = item.Y-this.controlledCar.Y;
            Vector V = new Vector(x, y);
            return (V.Length-(this.controlledCar.Height/2));
        }

        public override void Process()
        {
            if(isCarFasterThanKmh(70)) {
                this.SetWarning("AEB off");
            }
        }

        public void SetWarning(String message){
            ((EABAction)this.virtualFunctionBus.AEBActionPacket).Message = message;
        }

        public bool isThereAnObjectInRadar()
        {
            if(controlledCar.Radar.LastSeenObject != null)
            {
                return true;
            }
            return false;
        }
    }
}