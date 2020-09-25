﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace AutomatedCar.Models {
    using System;
    using Avalonia;
    using Avalonia.Media;

    public class World : ReactiveObject, IWorld {

        private static System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());

        public ObservableCollection<IWorldObject> WorldObjects { get; } = new ObservableCollection<IWorldObject> ();

        private AutomatedCar _controlledCar;
        public AutomatedCar ControlledCar {
            get => _controlledCar;
            set => this.RaiseAndSetIfChanged (ref _controlledCar, value);
        }

        public int Width { get; set; }
        public int Height { get; set; }

        private World () { }
        public void addObject (IWorldObject worldObject) {
            WorldObjects.Add (worldObject);
        }

        public World GetInstance()
        {
            return lazySingleton.Value;
        }

        public List<IWorldObject> SearchInRange(List<Point> points)
        {
            List<IWorldObject> result = new List<IWorldObject>();
            PolylineGeometry geometry = new PolylineGeometry();
            geometry.Points = new Points();
            points.ForEach(p => geometry.Points.Add(p));
            foreach (IWorldObject worldObject in this.WorldObjects)
            {
                if (geometry.FillContains(worldObject.PositionPoint))
                {
                    result.Add(worldObject);
                }
            }

            return result;
        }

        public AutomatedCar GetControlledCar()
        {
            return this.GetInstance().ControlledCar;
        }

        public List<IWorldObject> GetNPCs()
        {
            List<IWorldObject> result = new List<IWorldObject>();
            for (int i = 0; i < this.WorldObjects.Count; i++)
            {
                try
                {
                    if (this.WorldObjects[i] is IMoveable)
                    {
                        result.Add(this.WorldObjects[i]);
                    }
                }
                catch (InvalidCastException ex)
                {}
               
            }
            return result;
        }
    }
}