using Avalonia;
using System;
using System.Collections.Generic;

using System.Text;

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
            throw new NotImplementedException();
        }
        public Point getPositionFromScreen(AutomatedCar.Models.WorldObject worldObject)
        {
            throw new NotImplementedException();
        }
    }
}
