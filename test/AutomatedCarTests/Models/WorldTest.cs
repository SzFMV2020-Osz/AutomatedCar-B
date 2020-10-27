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

        /**
         * Init Test Constructor
         */
        public WorldTest()
        {
            this.random = new Random();
            this.testRotMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            this.world = World.Instance;
            InitWorld();
        }

        /**
         * Negative test for triangle detection
         */
        [Fact]
        public void NegativeTestTriangleDetection()
        {
            List<Point> testPoints = CreateTestPointList(0, 0, 1, 0, 1, 1, 0, 0);
            Assert.Empty(this.world.GetWorldObjectsInsideTriangle(testPoints));
        }

        /**
         * Positive test for triangle detection
         */
        [Fact]
        public void PositiveTestTriangleDetection()
        {
            List<Point> testPoints = CreateTestPointList(50, 50, 55, 54, 60, 60, 50, 50);
            Assert.NotEmpty(this.world.GetWorldObjectsInsideTriangle(testPoints));
        }

        private List<Point> CreateTestPointList(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            List<Point> testPoints = new List<Point>();
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y3);
            Point p3 = new Point(x3, y3);
            Point p4 = new Point(x4, y4);
            testPoints.Add(p1);
            testPoints.Add(p2);
            testPoints.Add(p3);
            testPoints.Add(p4);
            return testPoints;
        }

        /**
         * Create Test Road Object
         */
        private WorldObject CreateRoad()
        {
            return new Road(10, 10, null, 10, 10, 0, 0,
                testRotMatrix, CreatePolyPoints());
        }

        /**
         * Create Test Sign Object
         */
        private WorldObject CreateSign()
        {
            return new Sign(10, 5, null, 5, 5, 0, 0,
                testRotMatrix, CreatePolyPoints());
        }

        /**
         * Create Test Tree Object
         */
        private WorldObject CreateTree()
        {
            return new Tree(20, 10, null, 5, 5, 0, 0,
                testRotMatrix, CreatePolyPoints());
        }

        /**
         * Create Test Parking Spot Object
         */
        private WorldObject CreateParkingSpot()
        {
            return new ParallelParking(30, 30, null, 20, 10, 0, 0,
                testRotMatrix, CreatePolyPoints());
        }

        /**
         * Create Test Garage Object
         */
        private WorldObject CreateGarage()
        {
            return new Garage(50, 20, null, 20, 20, 0, 0,
                testRotMatrix, CreatePolyPoints());
        }

        /**
         * Create Test Poly Points List Object
         */
        private List<List<Point>> CreatePolyPoints()
        {
            List<List<Point>> polyPoints = new List<List<Point>>();

            Point p1 = new Point(50, 55);
            Point p2 = new Point(60, 60);
            List<Point> points = new List<Point>();
            points.Add(p1);
            points.Add(p2);
            
            polyPoints.Add(points);
            return polyPoints;
        }
        
        /**
         * Adds the Test Objects into the World
         */
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