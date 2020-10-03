using AutomatedCar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutomatedCar.Visualization
{
    public class VisualizationWorld: World
    {
        public ObservableCollection<WorldObject> VisibleWorldObjects { get; } = new ObservableCollection<WorldObject>();

        public VisualizationWorld(World realWorld)
        {

        }

        public void AddVisibleObject(WorldObject worldObject)
        {
            this.VisibleWorldObjects.Add(worldObject);
        }
    }
}
