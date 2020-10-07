using AutomatedCar.Models;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutomatedCar.Visualization
{
    public class VisualizationWorld: World
    {
        World realWorld;
        public ObservableCollection<WorldObject> VisibleWorldObjects { get; } = new ObservableCollection<WorldObject>();
        public static new VisualizationWorld Instance { get; } = new VisualizationWorld();
        private VisualizationWorld()
        {
            this.realWorld = World.Instance;

            this.Width = 960;
            this.Height = 720;

            PositionComputeObject pco = new PositionComputeObject(realWorld.ControlledCar);
            Point[] point = pco.getScreenSquare(this.Width, this.Height);
            List<WorldObject> wos = realWorld.getWorldObjectsInRectangle(new Point(0, 0), new Point(6000, 0), new Point(6000, 4000),
                new Point(0, 4000));


            foreach(WorldObject wo in wos) {
                if (wo != null)
                {
                    this.VisibleWorldObjects.Add(wo);
                }

            }
        }

        public void AddVisibleObject(WorldObject worldObject)
        {
            this.VisibleWorldObjects.Add(worldObject);
        }
    }
}
