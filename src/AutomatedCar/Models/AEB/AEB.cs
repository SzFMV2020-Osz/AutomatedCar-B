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
        private AEBAction AEBActionPacket;
        public AEB()
        {
            this.controlledCar = World.Instance.ControlledCar;
        }

        public AEB(VirtualFunctionBus virtualFunctionBus = null): base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            this.controlledCar = World.Instance.ControlledCar;
            this.AEBActionPacket = new AEBAction();
            virtualFunctionBus.AEBActionPacket = this.AEBActionPacket;
        }

        public bool IsUseable(){
            return !isCarFasterThanKmh(70);
        }

        private bool isCarFasterThanKmh(double speed) {
            double carSpeed = this.controlledCar.Speed;
            return carSpeed > kmh_into_pxs(speed);
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
            this.controlledCar = World.Instance.ControlledCar;
            Run();
        }
    
        public void Run() {
            if(IsUseable()) {
                this.SetWarning("");
                if(controlledCar.PowerTrain.Engine.GearShifter.Position == Gears.D && controlledCar.Radar.LastSeenObject != null){
                    if(getStoppingDistanceTo_inPixels(controlledCar.Radar.LastSeenObject) > 50) {
                        this.SetWarning("Break please!");
                        InactiveAEB();
                    } else {
                        this.SetWarning("AEB active");
                        Stop();
                    }
                } else {
                    this.SetWarning("");
                    InactiveAEB();
                }
            } else {
                this.SetWarning("AEB off");
                InactiveAEB();
            }
        }

        public void SetWarning(String message){
            ((AEBAction)this.virtualFunctionBus.AEBActionPacket).Message = message;
            
        }

        public void Stop(){
            ((AEBAction)this.virtualFunctionBus.AEBActionPacket).Breakpedal = 100;
            ((AEBAction)this.virtualFunctionBus.AEBActionPacket).Active = true;
        }

        public void InactiveAEB(){
            ((AEBAction)this.virtualFunctionBus.AEBActionPacket).Breakpedal = 0;
            ((AEBAction)this.virtualFunctionBus.AEBActionPacket).Active = false;
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