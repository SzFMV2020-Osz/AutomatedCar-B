namespace AutomatedCarTests.Models
{
    using System.Collections.ObjectModel;
    using AutomatedCar.Models;
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using Avalonia.Media.TextFormatting;
    using Xunit;
    using Moq;  


    public class WorldTest
    {
        [Theory]
        [InlineData (20)]
        [InlineData (0)]
        [InlineData (10000)]
        public void GetNPCsTest (int numOfWorldObjects)
        {
            Mock<World> world = new Mock<World>();
            AddWorldObjects(world.Object, numOfWorldObjects);
            Assert.Equal (numOfWorldObjects, world.Object.GetNPCs().Count);
        }

        private void AddWorldObjects(World world, int numOfWorldObjects)
        {
            for (int i = 0; i < numOfWorldObjects; i++)
            {
                NPC npc = new NPC();
                TestWordObject w = new TestWordObject();
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