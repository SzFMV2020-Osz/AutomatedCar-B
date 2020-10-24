namespace Tests.Models
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Xunit;

    public class WorldTest
    {
        private World world;

        private Matrix testRotMatrix;

        private Random random;

        public WorldTest()
        {
            this.random = new Random();
            this.testRotMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            this.world = World.Instance;
            InitWorld();
        }

        [Fact]
        public void TestTriangleDetection()
        {
            Console.WriteLine("ASDASD");
        }

        private WorldObject CreateRoad()
        {
            return new Road(10, 10, null, 10, 10, 0, 0,
                testRotMatrix, CreateRandomPolyPoints());
        }

        private WorldObject CreateSign()
        {
            return new Sign(10, 5, null, 5, 5, 0, 0,
                testRotMatrix, CreateRandomPolyPoints());
        }

        private WorldObject CreateTree()
        {
            return new Tree(20, 10, null, 5, 5, 0, 0,
                testRotMatrix, CreateRandomPolyPoints());
        }

        private WorldObject CreateParkingSpot()
        {
            return new ParallelParking(30, 30, null, 20, 10, 0, 0,
                testRotMatrix, CreateRandomPolyPoints());
        }

        private WorldObject CreateGarage()
        {
            return new Garage(50, 20, null, 20, 20, 0, 0,
                testRotMatrix, CreateRandomPolyPoints());
        }

        private List<List<Point>> CreateRandomPolyPoints()
        {
            List<List<Point>> polyPoints = new List<List<Point>>();

            Point p1 = new Point(this.random.Next(1, 101), this.random.Next(1, 101));
            Point p2 = new Point(this.random.Next(1, 101), this.random.Next(1, 101));
            List<Point> points = new List<Point>();
            points.Add(p1);
            points.Add(p2);
            
            polyPoints.Add(points);
            return polyPoints;
        }
        
        private void InitWorld()
        {
            this.world.AddObject(CreateRoad());
            this.world.AddObject(CreateSign());
            this.world.AddObject(CreateTree());
            this.world.AddObject(CreateParkingSpot());
            this.world.AddObject(CreateGarage());
        }
    }
}