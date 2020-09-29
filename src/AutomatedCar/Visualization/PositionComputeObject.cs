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
    }
}
