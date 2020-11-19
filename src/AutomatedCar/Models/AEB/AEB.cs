using AutomatedCar.SystemComponents;

namespace AutomatedCar.Models
{
    public class AEB
    {
        public AEB()
        {
        }

        public bool IsUseable(){
            return isCarFasterThan(70);
        }

        private bool isCarFasterThan(double speed){
            return World.Instance.ControlledCar.Speed <= speed;
        }
    }
}