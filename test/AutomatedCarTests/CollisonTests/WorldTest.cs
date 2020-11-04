namespace Tests.Models
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Xunit;

    public class CollisonTests
    {
        [Fact]
        public void CollisonNothingTest()
        {
            World world = World.Instance;

            List<WorldObject> objects = new List<WorldObject>();

            int W = 108;
            int H = 240;
            List<List<Point>> polyList = new List<List<Point>>();
            polyList.Add(new List<Point>(new Point[] { new Point(0, 0), new Point(0, 200), new Point(200, 0), new Point(200, 200) }));
            AutomatedCar controlledCar = new AutomatedCar(1000, 1000, "car_1_white", W, H, polyList);

            world.ControlledCar = controlledCar;

            world.OnCollideWithNPC += delegate (WorldObject worldObject)
            {
                objects.Add(worldObject);
            };
            world.OnCollideWithLandmark += delegate (WorldObject worldObject)
            {
                objects.Add(worldObject);
            };

            world.IsColisonWhitWorldObject();

            Assert.Equal(0, objects.Count);
        }

        [Fact]
        public void CollisonLandmarkTest() {
            World world = World.Instance;
            Matrix testRotMatrix = new Matrix(1, 0, 0, 1, 0, 0);

            List<WorldObject> objects = new List<WorldObject>();

            int W = 108;
            int H = 240;
            List<List<Point>> polyList = new List<List<Point>>();
            polyList.Add(new List<Point>(new Point[] { new Point(0,0), new Point(0,200), new Point(200, 0), new Point(200, 200) } ));
            AutomatedCar controlledCar = new AutomatedCar(0, 0, "car_1_white", W, H, polyList);

            world.ControlledCar = controlledCar;

            Tree tree = new Tree(0, 0, null, 5, 5, 0, 0, testRotMatrix, polyList);

            world.AddObject(tree);

            world.OnCollideWithLandmark += delegate (WorldObject worldObject)
            {
                objects.Add(worldObject);
            };

            world.IsColisonWhitWorldObject();

            Assert.Equal(1, objects.Count);
        }

        [Fact]
        public void CollisonNPCTest()
        {
            World world = World.Instance;

            List<WorldObject> objects = new List<WorldObject>();

            int W = 108;
            int H = 240;
            List<List<Point>> polyList = new List<List<Point>>();
            polyList.Add(new List<Point>(new Point[] { new Point(0, 0), new Point(0, 200), new Point(200, 0), new Point(200, 200) }));
            AutomatedCar controlledCar = new AutomatedCar(0, 0, "car_1_white", W, H, polyList);

            world.ControlledCar = controlledCar;

            AutomatedCar controlledCar2 = new AutomatedCar(0, 0, "car_1_white", W, H, polyList);
            controlledCar2.IsColliding = true;
            world.AddObject(controlledCar2);

            world.OnCollideWithNPC += delegate (WorldObject worldObject)
            {
                objects.Add(worldObject);
            };

            world.IsColisonWhitWorldObject();

            Assert.Equal(1, objects.Count);
        }
    }
}