using AutomatedCar.SystemComponents;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Tests
{
    public class ParkingPilotTests
    {
        ParkingPilot pp = new ParkingPilot();

        [Theory]        
        [InlineData(0, 0, 10, 10, 0, 10)]
        [InlineData(0, 0, 10, 10, 270, 10)]
        [InlineData(0, 0, 10, 10, 90, 10)]
        [InlineData(0, 0, 10, 10, 180, 10)]

        [InlineData(10, 10, 20, 20, 0, 10)]

        [InlineData(0, 0, 30, 30, 0, 30)]

        [InlineData(0, 0, 0, 10, 270, 0)]
        [InlineData(0, 0, 0, 10, 90, 0)]
        [InlineData(0, 0, 0, 10, 0, 10)]
        [InlineData(0, 0, 0, 10, 180, 10)]

        [InlineData(0, 0, 10, 20, 0, 20)]
        public void CalculateDistanceTest(int carX, int carY, int objectX, int objectY, double carAngle, double expectedDistance)
        {
            Vector2 car = new Vector2(carX, carY);
            Vector2 obj = new Vector2(objectX, objectY);

            Assert.Equal(expectedDistance, this.pp.CalculateSpace(car, obj, carAngle), 0);
        }
    }
}
