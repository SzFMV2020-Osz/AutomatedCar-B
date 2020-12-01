namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Reactive;
    using Avalonia.Controls;
    using Avalonia.Media;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.Geometries.Implementation;
    using ReactiveUI;
    using Geometry = Avalonia.Media.Geometry;
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public class World : ReactiveObject
    {
        /* private static readonly System.Lazy<World> lazySingleton = new System.Lazy<World> (() => new World());
         public static World Instance { get { return lazySingleton.Value; } } */

        private AutomatedCar _controlledCar;

        private bool debugOn = false;

        public delegate void OnTickHangler(object sender, EventArgs args);

        public OnTickHangler OnTick;

        public delegate void CollisonEventHandler(WorldObject worldObject);

        public CollisonEventHandler OnCollideWithLandmark;

        public CollisonEventHandler OnCollideWithNPC;

        public bool DebugOn { get => this.debugOn; }

        public static World Instance { get; } = new World() { VisibleWidth = 960, VisibleHeight = 720};

        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();

        public AutomatedCar ControlledCar
        {
            get => this._controlledCar;
            set => this.RaiseAndSetIfChanged(ref this._controlledCar, value);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public int VisibleWidth { get; set; }

        public int VisibleHeight { get; set; }

        public void Tick()
        {
            OnTick?.Invoke(this, EventArgs.Empty);
        }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }

        private static double GetArea(Point a, Point b, Point c)
        {
            return Math.Abs(((a.X * (b.Y - c.Y)) +
                             (b.X * (c.Y - a.Y)) +
                             (c.X * (a.Y - b.Y))) / 2.0);
        }

        /* A function to check whether point P(x, y) lies
        inside the triangle formed by A(x1, y1),
        B(x2, y2) and C(x3, y3) */
        private static bool IsInside(List<Point> triangle, Point target)
        {
            Point a = triangle[0];
            Point b = triangle[1];
            Point c = triangle[2];

            /* Calculate area of triangle ABC */
            double areaFull = GetArea(a, b, c);

            /* Calculate area of triangle PBC */
            double area1 = GetArea(target, b, c);

            /* Calculate area of triangle PAC */
            double area2 = GetArea(a, target, c);

            /* Calculate area of triangle PAB */
            double area3 = GetArea(a, b, target);

            double dummy = (area1 + area2 + area3);

            /* Check if sum of A1, A2 and A3 is same as A */
            return areaFull == (area1 + area2 + area3);
        }

        public List<WorldObject> GetWorldObjectsInsideTriangle(List<Point> pointsOfTriangle)
        {
            var coordinatePoints = new Coordinate[pointsOfTriangle.Count + 1];
            for (int i = 0; i < pointsOfTriangle.Count; i++)
            {
                coordinatePoints[i] = new Coordinate(pointsOfTriangle[i].X, pointsOfTriangle[i].Y);
            }

            coordinatePoints[coordinatePoints.Length - 1] = new Coordinate(pointsOfTriangle[0].X, pointsOfTriangle[0].Y);
            var lr1 = new LinearRing(new CoordinateArraySequence(coordinatePoints), GeometryFactory.Default);
            NetTopologySuite.Geometries.Polygon triangle = new NetTopologySuite.Geometries.Polygon(lr1);

            List<WorldObject> objectsInside = new List<WorldObject>();

            foreach (WorldObject item in this.WorldObjects)
            {
                if (!(item is AutomatedCar))
                {
                    foreach (LineString polygon in item.NetPolygons)
                    {
                        if (triangle.Intersects(polygon))
                        {
                            objectsInside.Add(item);
                            break;
                        }
                    }
                }
            }

            return objectsInside;
        }

        public void IsColisonWhitWorldObject()
        {
            foreach (WorldObject item in this.WorldObjects.Where(x => x.IsColliding))
            {
                foreach (NetTopologySuite.Geometries.LineString worldObjectpolygon in item.NetPolygons)
                {
                    foreach (NetTopologySuite.Geometries.LineString carPolygon in this._controlledCar.NetPolygons)
                    {
                        if (worldObjectpolygon.Intersects(carPolygon))
                        {
                            if (item is IMoveable)
                            {
                                OnCollideWithNPC?.Invoke(item);
                            }
                            else
                            {
                                OnCollideWithLandmark?.Invoke(item);
                            }
                        }
                    }
                }
            }
        }

        public Road GetRoadUnderCar()
        {
            List<WorldObject> worldObjects = GetWorldObjectsInsideTriangle(ControlledCar.CameraGeometry.Points.ToList());
            List<Road> roads = worldObjects.Where(x => x is Road).Select(x => x as Road).ToList();
            roads.OrderBy(x => this.DistanceBeetwenCarAndRoad(x));
            return roads.First();
        }

        private double DistanceBeetwenCarAndRoad(Road road) => Math.Sqrt(Math.Pow(road.X - this.ControlledCar.X, 2) + Math.Pow(road.Y - this.ControlledCar.Y, 2));
    }
}