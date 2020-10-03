﻿using AutomatedCar.Models;
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
            this.Width = 500;
            this.Height = 500;

            // new positionComputeObject(realWorld.controlledCar)
            // positionComputeObject.getScreenSquare(width, height)
            // realWorld.getWorldsObjectsInRectangle(int a, int b, int c, int d): List

            this.VisibleWorldObjects.Add(realWorld.WorldObjects[0]);
            this.VisibleWorldObjects.Add(realWorld.WorldObjects[1]);
        }

        public void AddVisibleObject(WorldObject worldObject)
        {
            this.VisibleWorldObjects.Add(worldObject);
        }
    }
}
