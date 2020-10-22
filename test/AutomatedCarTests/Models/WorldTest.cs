namespace Tests.Models
{
    using System;
    using AutomatedCar.Logic;
    using AutomatedCar.Models;
    using Xunit;

    public class WorldTest
    {
        private World word;

        public WorldTest()
        {
            JsonParser parser = new JsonParser();
            parser.populateWorldObjects(World.Instance, $"AutomatedCarTests.Assets.test_world.json");
            this.word = World.Instance;
        }

        [Fact]
        public void TestTriangleDetection()
        {
            Console.WriteLine("ASDASD");
        }
    }
}