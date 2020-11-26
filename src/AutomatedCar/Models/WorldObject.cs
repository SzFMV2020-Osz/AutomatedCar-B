namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Avalonia;
    using Avalonia.Media;
    using NetTopologySuite.Geometries;
    using ReactiveUI;
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public abstract class WorldObject : ReactiveObject
    {
        private int _mass;
        private int _x;
        private int _y;
        private SolidColorBrush brush;

        public SolidColorBrush Brush { get => this.brush; set => this.RaiseAndSetIfChanged(ref this.brush, value); }

        public Matrix RotMatrix { get; set; }

        public List<List<Point>> BasePoints { get; private set; }

        public WorldObject(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints)
        {
            BasePoints = polyPoints;
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.Width = width;
            this.Height = height;
            this.referenceOffsetX = referenceOffsetX;
            this.referenceOffsetY = referenceOffsetY;
            this.Angle = Math.Atan2(rotmatrix.M12, rotmatrix.M11) * 180 / Math.PI;
            this.RotMatrix = rotmatrix;
            this.NetPolygons = this.GenerateNetPolygons(polyPoints);
            this.ZIndex = 1;
            this.Brush = new SolidColorBrush(Color.Parse("blue"));
        }

        private double _angle;

        public double Angle
        {
            get
            {
                return this._angle;
            }

            set
            {
                var radAngle = this._angle * Math.PI / 180;
                this.RotMatrix = new Matrix(Math.Cos(radAngle), -Math.Sin(radAngle), Math.Sin(radAngle),Math.Cos(radAngle),0,0);
                this.UpdatePolygons();
                this.RaiseAndSetIfChanged(ref this._angle, value);
            }
        }

        public bool IsColliding { get; set; }

        public List<Polygon> Polygons { get; set; }

        public List<LineString> NetPolygons { get; set; }

        public int ZIndex { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int X
        {
            get => this._x;
            protected set
            {
                this.UpdatePolygons();
                this.RaiseAndSetIfChanged(ref this._x, value);
            }
        }

        public int Y
        {
            get => this._y;
            protected set
            {
                this.UpdatePolygons();
                this.RaiseAndSetIfChanged(ref this._y, value);
            }
        }

        private void UpdatePolygons()
        {
           this.NetPolygons = this.GenerateNetPolygons(this.BasePoints);
        }

        public int referenceOffsetX { get; set; }

        public int referenceOffsetY { get; set; }

        public string Filename { get; set; }

        private List<List<Point>> RotatePoints(List<List<Point>> polyPoints)
        {
            List<List<Point>> rotatedPointsList = new List<List<Point>>();
            foreach (List<Point> points in polyPoints)
            {
                List<Point> rotatedPoints = new List<Point>();
                foreach (Point point in points)
                {
                    Point tempPoint = new Point(point.X + this.referenceOffsetX, point.Y + this.referenceOffsetY);
                    rotatedPoints.Add(new Point(tempPoint.Transform(this.RotMatrix).X - this.referenceOffsetX, tempPoint.Transform(this.RotMatrix).Y - this.referenceOffsetY));
                }

                rotatedPointsList.Add(rotatedPoints);
            }

            return rotatedPointsList;
        }

        private List<Polygon> GeneratePolygons(List<List<Point>> polyPoints)
        {
            List<Polygon> objectPolygons = new List<Polygon>();
            foreach (List<Point> points in polyPoints)
            {
                objectPolygons.Add(new Polygon() { Points = points });
            }

            return objectPolygons;
        }

        public List<LineString> GenerateNetPolygons(List<List<Point>> polyPoints)
        {
            polyPoints = RotatePoints(polyPoints);
            List<LineString> objectLineStrings = new List<LineString>();
            foreach (List<Point> points in polyPoints)
            {
                var coordinates = points.Select(point => new Coordinate(point.X + this.referenceOffsetX + this.X, point.Y + this.referenceOffsetY + this.Y)).ToArray();
                objectLineStrings.Add(new LineString(coordinates));
            }

            return objectLineStrings;
        }
    }
}