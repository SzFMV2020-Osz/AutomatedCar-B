namespace AutomatedCar.SystemComponents
{
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;

    public interface IUltrasound
    {
        public List<Point> Points { get; set; }

        public List<WorldObject> WorldObjects { get; set; }

        public double Distance { get; set; }

        public List<Point> CalculatePoints();

        public List<WorldObject> GetWorldObjectsInRange();

        public double CalculateDistance();

        public void SetClosestWorldObjectBrustToHighlighted(WorldObject obj);
    }
}
