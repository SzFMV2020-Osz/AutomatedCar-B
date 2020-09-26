namespace AutomatedCarTests.Models
{
    using System.Collections.ObjectModel;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using Avalonia.Media.TextFormatting;
    using Xunit;
    using Moq;
    using System.Collections.Generic;

    public class WorldTests
    {
        //World instance is singleton so I can only run one test on this method
        [Theory]
        [InlineData (20)]
        public void GetNPCsTest (int numOfWorldObjects)
        {
            World world = World.Instance;
            AddWorldObjects(world, numOfWorldObjects);
            Assert.Equal (numOfWorldObjects, world.GetNPCs().Count);
        }

        private void AddWorldObjects(World world, int numOfWorldObjects)
        {
            for (int i = 0; i < numOfWorldObjects; i++)
            {
                NPC npc = new NPC();
                npc.Polygon = new Polygon();
                npc.Polygon.Points = new List<Point> { new Point(0, 0), new Point(5, 0), new Point(-5, 0), new Point(-5, 5) };
                TestWordObject w = new TestWordObject();
                w.Polygon = new Polygon();
                w.Polygon.Points = new List<Point> { new Point(0, 0), new Point(5, 0), new Point(-5, 0), new Point(-5, 5) };
                world.addObject(npc);
                world.addObject(w);
            }
        }


    }

    public class TestWordObject: IWorldObject
    {
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point PositionPoint { get; }
        public int ZIndex { get; set; }
        public Point RotationPoint { get; set; }
        public Polygon Polygon { get; set; }
        public bool IsCollidable { get; set; }
        public MatrixTwoByTwo RotationMatrix { get; set; }
        public bool IsHighlighted { get; set; }
    }
}