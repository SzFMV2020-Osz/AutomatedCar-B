using Avalonia;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Models
{
    public class NoticedObject
    {
        public WorldObject worldObject;
        private int? prevY;
        private int? prevX;
        private bool _new;
        private bool seen;
        private double distanceFromCar;
        private Vector? vector;

        public int? PrevY { get => prevY; set => prevY = value; }
        public int? PrevX { get => prevX; set => prevX = value; }
        public bool New { get => _new; set => _new = value; }
        public bool Seen { get => seen; set => seen = value; }
        public double DistanceFromCar_inMeter { get => distanceFromCar; set => distanceFromCar = value; }
        public Vector? Vector { get => vector; set => vector = value; }
    }
}
