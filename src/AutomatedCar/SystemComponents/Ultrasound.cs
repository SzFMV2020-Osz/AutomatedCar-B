namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;
    using Microsoft.VisualBasic.CompilerServices;
    using ReactiveUI;

    public class Ultrasound : SystemComponent, IUltrasound
    {
        public const int Height = 150;
        public const int Fov = 100;
        private int offsetX;
        private int offsetY;
        private int rotate;
        private double dif = (double)Height * Math.Tan((double)Fov / 2 * (Math.PI / 180));
        Point start;

        public Ultrasound(VirtualFunctionBus virtualFunctionBus, int offsetX, int offsetY, int rotate)
           : base(virtualFunctionBus)
        {
            this.offsetX = -offsetX;
            this.offsetY = -offsetY;
            this.rotate = rotate + 180;

            this.Brush = new SolidColorBrush(Avalonia.Media.Color.Parse("green"));
        }

        public int CHeight { get => Height; }

        public int CFov { get => Fov; }

        public List<Point> Points { get; set; }

        public List<WorldObject> WorldObjects { get; set; }

        public SolidColorBrush Brush { get; set; }

        public double Distance { get; set; }

        public override void Process()
        {
            this.Points = this.CalculatePoints();
            this.WorldObjects = this.GetWorldObjectsInRange();
            if (this.WorldObjects.Count > 0)
            {
                this.Distance = this.CalculateDistance();
            }
        }

        public Point RotatePoint(double centerX, double centerY, double angle, Point p)
        {
            double s = Math.Sin(Math.PI / 180 * angle);
            double c = Math.Cos(Math.PI / 180 * angle);

            double px = p.X;
            double py = p.Y;

            px -= centerX;
            py -= centerY;

            double xnew = (px * c) - (py * s);
            double ynew = (px * s) + (py * c);

            px = xnew + centerX;
            py = ynew + centerY;
            return new Point(px, py);
        }

        public List<Point> CalculatePoints()
        {
            var car = World.Instance.ControlledCar;
            this.StartCalculate();
            Point right = new Point(
                this.start.X + Height,
                this.start.Y + this.dif
                );
            Point left = new Point(
                this.start.X + Height,
                this.start.Y - this.dif
                );
            right = this.RotatePoint(this.start.X, this.start.Y, car.Angle + this.rotate, right);
            left = this.RotatePoint(this.start.X, this.start.Y, car.Angle + this.rotate, left);
            List<Point> newPoints = new List<Point>()
            {
                this.start,
                left,
                right,
            };
            return newPoints;
        }

        public void StartCalculate()
        {
            var car = World.Instance.ControlledCar;
            this.start = this.RotatePoint(car.X, car.Y, car.Angle, new Point(car.X + this.offsetX, car.Y + this.offsetY));
        }

        public List<WorldObject> GetWorldObjectsInRange()
            => World.Instance.GetWorldObjectsInsideTriangle(this.Points);

        public double CalculateDistance()
        {
            double distance = 155.57;
            double actual;
            WorldObject obj = null;
            foreach (WorldObject item in this.WorldObjects)
            {
                // A későbbiekben hozzáadott npc auto és gyalogos majd bekerül még, eddig ezt a 2-t találtam.
                if (item.GetType() == typeof(Sign) || item.GetType() == typeof(Tree))
                {
                    actual = Math.Sqrt(Math.Pow(this.start.X - item.X, 2) + Math.Pow(this.start.Y - item.Y, 2));
                    if (actual < distance)
                    {
                        distance = actual;
                        obj = item;
                    }
                }
            }

            if (obj != null)
            {
                this.SetClosestWorldObjectBrustToHighlighted(obj);
                return distance;
            }

            return 99999;
        }

        public void SetClosestWorldObjectBrustToHighlighted(WorldObject obj)
        {
            obj.Brush = new SolidColorBrush(Avalonia.Media.Color.Parse("red"));
        }
    }
}
