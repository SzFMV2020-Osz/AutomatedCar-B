﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace AutomatedCar.Models {
    using System;
    using Avalonia;
    using Avalonia.Media;

    public class World : ReactiveObject, IWorld {

        public static World Instance { get; } = new World ();
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

        public List<IWorldObject> SearchInRange(List<Point> points)
        {
            List<IWorldObject> result = new List<IWorldObject>();
            PolylineGeometry area = new PolylineGeometry();
            area.Points = new Points();
            points.ForEach(p => area.Points.Add(p));
            foreach (IWorldObject worldObject in this.WorldObjects)
            {
                Rect rect = new PolylineGeometry(worldObject.Polygon.Points, true).GetRenderBounds(new Pen());
                if (area.FillContains(rect.TopLeft) || area.FillContains(rect.TopRight) || area.FillContains(rect.BottomLeft) || area.FillContains(rect.BottomRight) || area.FillContains(rect.Center))
                {
                    result.Add(worldObject);
                }
            }

            return result;
        }

        public AutomatedCar GetControlledCar()
        {
            return Instance.ControlledCar;
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