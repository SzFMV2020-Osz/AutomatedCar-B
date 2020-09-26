namespace AutomatedCar.Models
{
    using System.Collections.Generic;
    using Avalonia;

    public interface IWorld
    {
        public List<IWorldObject> SearchInRange(List<Point> points);
        public AutomatedCar GetControlledCar();
        public List<IWorldObject> GetNPCs();

    }
}