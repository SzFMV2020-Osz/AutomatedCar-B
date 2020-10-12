namespace AutomatedCar.Models
{
    using System;
    using Avalonia.Controls.Shapes;
    using ReactiveUI;
    using System.Collections.Generic;
    using System.Numerics;
    using Avalonia;

    public abstract class WorldObject : ReactiveObject
    {
        private int _x;
        private int _y;

        public RotationMatrix RotMatrix { get; set; }

        public WorldObject(int x, int y, string filename, bool iscolliding, RotationMatrix rotmatrix)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.IsColliding = iscolliding;
            this.ZIndex = 1;
            this.Polygons = new List<Polygon>();
            this.RotMatrix = rotmatrix;
            this.Angle = Math.Atan2(rotmatrix.N12, rotmatrix.N11) * 180 / Math.PI;
        }

        private double _angle;

        public double Angle
        {
            get { return _angle; }
            set => this.RaiseAndSetIfChanged(ref this._angle, value);
        }

        public bool IsColliding { get; set; }

        public List<Polygon> Polygons { get; set; }

        public int ZIndex { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int X
        {
            get => this._x;
            protected set => this.RaiseAndSetIfChanged(ref this._x, value);
        }

        public int Y
        {
            get => this._y;
            protected set => this.RaiseAndSetIfChanged(ref this._y, value);
        }

        public int referenceOffsetX { get; set; }

        public int referenceOffsetY { get; set; }

        public string Filename { get; set; }
    }
}