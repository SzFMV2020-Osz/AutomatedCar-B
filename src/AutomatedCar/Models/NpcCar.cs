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
    using Avalonia.Media;
    using Newtonsoft.Json;

    public class NpcCar : WorldObject, INPC
    {
        public NpcCar(string filename, int width, int height, List<List<Point>> polyPoints, string jsonRoute)
        : base(0, 0, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polyPoints)
        {
            this.JsonRoute = jsonRoute;
            speedLimit = 20; //remove when adaptive speedLimit done!
            this.ReadCarRoute();
            this.toReach = CarRoutes[1];
            index = 1;
        }

        private string JsonRoute;

        private int index;
        private List<NpcRoute> CarRoutes;
        private NpcRoute toReach;
        private int speedLimit;
        public int Rotation { get; set; }

        public int Speed { get; set; }

        public void ReadCarRoute()
        {
            this.CarRoutes = LoadJson(this.JsonRoute);
        }

        public void SetStartPosition()
        {
            this.X = (this.CarRoutes.First().x);
            this.Y = (this.CarRoutes.First().y);
        }

       /*public void Moveeee(object sender, EventArgs args)
        {
             if (index == this.CarRoutes.Count())
                index = 0;

            int deltaX = this.CarRoutes[index].x - this.X;

            if (deltaX < 0)
                this.X -= 10;
            else if (deltaX > 0)
                this.X += 10;

            int deltaY =this.CarRoutes[index].y - this.Y;

            if (deltaY < 0)
                this.Y -= 10;
            else if (deltaY > 0)
                this.Y += 10;

            if (deltaX == 0 && deltaY == 0)
                index++;
        }*/

        public void Move(object sender, EventArgs args)
        {
            Vector2 movementDirection = new Vector2(toReach.x - X, toReach.y - Y);
            Vector2 moveWith = movementDirection / movementDirection.Length() * Convert.ToSingle(speedLimit);
            
            if(movementDirection.Length() == 0 || movementDirection.Length() < moveWith.Length())
            {
                if(toReach.tag == "finish")
                {
                    index = -1;
                }

                toReach = CarRoutes[++index];
                movementDirection.X = toReach.x - X;
                movementDirection.Y = toReach.y - Y;
                moveWith = movementDirection / movementDirection.Length() * Convert.ToSingle(speedLimit);
            }
            Angle = CalculateNpcAngle(movementDirection);

            Move(moveWith);
        }

        private double CalculateNpcAngle(Vector2 direction)
        {
            return (double)Math.PI;
        }

        public void Move(Vector2 with)
        {
            this.X = this.X + (int)with.X;
            this.Y = this.Y + (int)with.Y;
        }

        public void MoveX(int x)
        {
            throw new NotImplementedException();
        }

        public void MoveY(int y)
        {
            throw new NotImplementedException();
        }

        public void SetNextPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        private int CheckSpeedLimit(int currentLimit)
        {
            List<Point> viewTrianglePoints = CalculateNpcViewTriangle();
            List<WorldObject> visibleObjects = World.Instance.GetWorldObjectsInsideTriangle(viewTrianglePoints);

            if(visibleObjects.Where(o => o is Sign).Where(o => o.Filename.Contains("roadsign_speed")) != null)
            {
                string speedLimitSignName = visibleObjects.Where(o => o.Filename.Contains("roadsign_speed")).First().Filename;
                int speedLimit = SpeedLimitFromSignName(speedLimitSignName, currentLimit);
            }
            return currentLimit;
        }

        private int SpeedLimitFromSignName(string type, int currentLimit)
        { 
            switch(type)
                {
                    case "roadsign_speed_40":
                        Console.WriteLine("Case 1");
                        return 40;
                    case "roadsign_speed_50":
                        Console.WriteLine("Case 2");
                        return 50;
                    case "roadsign_speed_60":
                        Console.WriteLine("Case 2");
                        return 60;
                    default:
                        Console.WriteLine("Default case");
                        return currentLimit;
                }
        }

        private List<Point> CalculateNpcViewTriangle()
        {
            Vector2 carPosition = new Vector2(this.X,this.Y);

            Vector2 carDirection = new Vector2((float)Math.Cos(this.Angle), (float)Math.Sin(this.Angle));
            Vector2 carNormalRight = new Vector2((float)Math.Cos(this.Angle - (Math.PI / 2)), (float)Math.Sin(this.Angle - (Math.PI / 2)));
            Vector2 carNormalLeft = new Vector2((float)Math.Cos(this.Angle + (Math.PI / 2)), (float)Math.Sin(this.Angle + (Math.PI / 2)));

            carDirection = carDirection / carDirection.Length();
            carNormalRight = carNormalRight / carNormalRight.Length();
            carNormalLeft = carNormalLeft/ carNormalLeft.Length();

            float triangleHeight = 200;
            float trinagleBaseHalf = 231 / 2;

            List<Point> trianglePoints = new List<Point>();
            Vector2 trianglePoint1 = carPosition + (Vector2.Multiply(carDirection,triangleHeight)) + (Vector2.Multiply(carNormalRight,trinagleBaseHalf));
            Vector2 trianglePoint2 = carPosition + (Vector2.Multiply(carDirection,triangleHeight)) + (Vector2.Multiply(carNormalLeft,trinagleBaseHalf));

            trianglePoints.Add(new Point(this.X,this.Y));
            trianglePoints.Add(new Point(trianglePoint1.X,trianglePoint1.Y));
            trianglePoints.Add(new Point(trianglePoint2.X,trianglePoint2.Y));

            return trianglePoints;
        }

        public static List<NpcRoute> LoadJson(string path)
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
