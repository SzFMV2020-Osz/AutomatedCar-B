using AutomatedCar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutomatedCar.Visualization
{
    public class VisualizationWorld: World
    {
        public void AddVisibleObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }
    }
}
