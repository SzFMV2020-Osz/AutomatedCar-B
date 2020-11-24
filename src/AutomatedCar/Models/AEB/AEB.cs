using AutomatedCar.SystemComponents;

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

        public int getStoppingDistanceTo(WorldObject worldObject){
            return 0;
        }
    }
}