namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;

    public abstract class Sensor : SystemComponent
    {
        public int offsetX;
        public int offsetY;
        public int rotate;
        public int range;
        public int angleOfView;
        public SolidColorBrush Brush;

        public Sensor(VirtualFunctionBus virtualFunction)
            : base(virtualFunction)
        {
        }

        public List<Point> Points { get; set; }

        public List<WorldObject> WorldObjects { get; set; }

        public List<Point> CalculatePoints()
        {
            var car = World.Instance.ControlledCar;
            Point start = this.RotatePoint(car.X, car.Y, car.Angle - 90, new Point(car.X + this.offsetX, car.Y + this.offsetY));
            double dif = (double)this.range * Math.Tan((double)this.angleOfView / 2 * (Math.PI / 180));

            Point right = new Point(
                start.X + this.range,
                start.Y + dif
                );

            Point left = new Point(
                start.X + this.range,
                start.Y - dif
                );

            right = this.RotatePoint(start.X, start.Y, this.rotate + car.Angle, right);
            left = this.RotatePoint(start.X, start.Y, this.rotate + car.Angle, left);
            List<Point> newPoints = new List<Point>()
            {
                start,
                left,
                right,
            };
            return newPoints;
        }

        public List<WorldObject> GetWorldObjectsInRange()
            => World.Instance.GetWorldObjectsInsideTriangle(this.Points);

        private Point RotatePoint(double centerX, double centerY, double angle, Point p)
        {
            double radian = angle * Math.PI / 180;
            double s = Math.Sin(radian);
            double c = Math.Cos(radian);

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
    }
}
