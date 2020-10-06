using Avalonia;
using System;
using AutomatedCar.Models;

namespace AutomatedCar.Visualization
{
    class PositionComputeObject
    {
        AutomatedCar.Models.AutomatedCar centerCar;
        public PositionComputeObject(AutomatedCar.Models.AutomatedCar centerCar )
        {
            this.centerCar = centerCar;
        }

        public Point[] getScreenSquare(AutomatedCar.Models.WorldObject worldObject)
        {
            throw new NotImplementedException();
        }
        public Point getPositionFromCenter(AutomatedCar.Models.WorldObject worldObject)
        {
            return new Point(worldObject.X - this.centerCar.X, worldObject.Y - this.centerCar.Y);
        }
        public Point getPositionFromScreen(AutomatedCar.Models.WorldObject worldObject, int screenWidth, int screenHeight )
        {
            return new Point(worldObject.X - this.centerCar.X + (screenWidth / 2), worldObject.Y - this.centerCar.Y + (screenHeight / 2));
        }
    }
}
