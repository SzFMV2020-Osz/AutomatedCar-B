namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;
    using System.Text;
    using Avalonia;
    using Newtonsoft.Json;

    public class NpcPedestrian : WorldObject, INPC
    {
        public NpcPedestrian(string filename, int width, int height, List<List<Point>> polyPoints, string jsonRoute)
             : base(0, 0, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polyPoints)
        {
            this.LoadNpcRoute(jsonRoute);
            this.toReach = Route[1];
            routeIndex = 1;
        }

        private int routeIndex;
        private List<NpcRoute> Route;
        private NpcRoute toReach;
        private bool isthreesixty;
        public int Mass { get; set; } = 1;
        private int speedLimit = 2;

        public void LoadNpcRoute(string jsonRoute)
        {
            this.Route = ReadJson(jsonRoute);
        }

        public void SetStartPosition()
        {

            this.X = this.Route.First().x;
            this.Y = this.Route.First().y;

        }
        public void Move(object sender, EventArgs args)
        {
            if (routeIndex == this.Route.Count())
                routeIndex = 0;

            int deltaX = this.Route[routeIndex].x - this.X;

            if (deltaX < 0)
                this.X -= speedLimit;
            else if (deltaX > 0)
                this.X += speedLimit;

            int deltaY = this.Route[routeIndex].y - this.Y;

            if (deltaY < 0)
                this.Y -= speedLimit;
            else if (deltaY > 0)
                this.Y += speedLimit;

            if (deltaX == 0 && deltaY == 0)
                routeIndex++;


            Vector2 movementDirection = new Vector2(deltaX, deltaY);
            this.CalculateNpcAngle(movementDirection);
        }
        private void CalculateNpcAngle(Vector2 direction)
        {
            double angle = Math.Atan2(direction.X, direction.Y * -1) * 180 / Math.PI;
            if (angle < 0)
            {
                angle += 360;
            }

            if (angle < 55)
            {
                isthreesixty = false;
            }

            if (angle > this.Angle && !isthreesixty)
            {
                this.Angle += 90;
            }
            if (angle < this.Angle && !isthreesixty)
            {
                this.Angle -= 180;
            }
            if (this.Angle >= 360 && !isthreesixty)
            {

                isthreesixty = true;
                this.Angle = 0;
            }
        }

        public void Move(Vector2 with)
        {
            this.X = this.X + (int)with.X;
            this.Y = this.Y + (int)with.Y;
        }

        public void SetNextPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static List<NpcRoute> ReadJson(string path)
        {
            using (StreamReader r = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(path)))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<NpcRoute>>(json);

                return items;
            }
        }

    }
}
