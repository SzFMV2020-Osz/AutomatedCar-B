namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Media;

    public class Ultrasound : Sensor
    {
        private List<Type> types = new List<Type>()
        {
            typeof(Tree),
            typeof(Sign),
            typeof(NpcPedestrian),
            typeof(NpcCar),
            typeof(Garage),
        };

        private double dif;

        public double Distance { get; set; }

        public WorldObject LastSeenObject { get; set; }

        public SolidColorBrush LastSeenObjectBrush { get; set; }

        public Ultrasound(VirtualFunctionBus virtualFunction, int offsetX, int offsetY, int rotate)
            : base(virtualFunction)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.rotate = rotate - 90;
            this.range = 150;
            this.angleOfView = 100;
            this.Brush = SolidColorBrush.Parse("green");
            this.dif = this.range * Math.Tan((double)this.angleOfView / 2 * (Math.PI / 180));
            this.maxReach = Math.Sqrt(Math.Pow(this.dif, 2) + Math.Pow(this.range, 2));
        }

        public override void Process()
        {
            this.Points = this.CalculatePoints();
            this.WorldObjects = this.GetWorldObjectsInRange();
            this.WorldObjects = this.WorldObjectSort();
            if (this.WorldObjects.Count > 0)
            {
                this.Distance = this.CalculateDistance();
            }
            else if (this.LastSeenObject != null)
            {
                this.ResetLastSeenObjectBrush();
            }
        }

        public double CalculateDistance()
        {
            double distance = this.maxReach;
            double actual;
            WorldObject obj = null;
            foreach (WorldObject item in this.WorldObjects)
            {
                actual = Math.Sqrt(Math.Pow(this.Points[0].X - item.X, 2) + Math.Pow(this.Points[0].Y - item.Y, 2));
                if (actual <= distance)
                {
                    distance = actual;
                    obj = item;
                }
            }

            if (obj != null)
            {
                this.SetClosestWorldObjectBrustToHighlighted(obj);
                return distance;
            }

            return this.maxReach + 1;
        }

        private List<WorldObject> WorldObjectSort()
        {
            List<WorldObject> wos = new List<WorldObject>();
            foreach (WorldObject obj in this.WorldObjects)
            {
                if (this.types.Contains(obj.GetType()))
                {
                    wos.Add(obj);
                }
            }

            return wos;
        }

        public void SetClosestWorldObjectBrustToHighlighted(WorldObject obj)
        {
            if (this.LastSeenObject != obj && this.LastSeenObject != null)
            {
                this.ResetLastSeenObjectBrush();
            }

            this.LastSeenObject = obj;
            this.LastSeenObjectBrush = obj.Brush;
            obj.Brush = new SolidColorBrush(Color.Parse("red"));
        }

        public void ResetLastSeenObjectBrush()
        {
            this.LastSeenObject.Brush = this.LastSeenObjectBrush;
        }
    }
}
