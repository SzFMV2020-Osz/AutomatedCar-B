namespace Tests.SystemComponents
{
    using Xunit;
    using AutomatedCar.SystemComponents;
    using AutomatedCar.SystemComponents.Packets;
    using System.Numerics;
    using System;

    public class EngineControllerTest
    {
        [Theory]
        [InlineData(GearShifterPosition.R, 100, 100)]
        [InlineData(GearShifterPosition.R, 50, 50)]
        [InlineData(GearShifterPosition.R, 0, -500)]
        public void SetRPM_CalculateRPMChangeTest(GearShifterPosition position, int gasPedal, int expectedRPM)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            ec.UpdateEngineProperties(packet);

            Assert.Equal<int>(expectedRPM, ec.RPM);
        }

        [Theory]
        [InlineData(GearShifterPosition.R, 6001, 6000)]
        public void SetRPM_MaximumRPMLimitTest(GearShifterPosition position, int gasPedal, int expectedRPM)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            ec.UpdateEngineProperties(packet);

            Assert.Equal<int>(expectedRPM, ec.RPM);
        }

        [Theory]
        [InlineData(GearShifterPosition.N, 50, 550)]
        [InlineData(GearShifterPosition.N, 100, 600)]
        public void SetRPM_NeutralGearIncreaseRegardlessOfPedalTest(GearShifterPosition position, int gasPedal, int expectedRPM)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            ec.UpdateEngineProperties(packet);

            Assert.Equal<int>(expectedRPM, ec.RPM);
        }

        [Theory]
        [InlineData(GearShifterPosition.P, 50, 0)]
        [InlineData(GearShifterPosition.P, 100, 0)]
        public void SetRPM_ParkReturnsZeroRegardlessOfPedalTest(GearShifterPosition position, int gasPedal, int expectedRPM)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            ec.UpdateEngineProperties(packet);

            Assert.Equal<int>(expectedRPM, ec.RPM);
        }

        [Theory]
        [InlineData(GearShifterPosition.D, 2000, 2676)]
        [InlineData(GearShifterPosition.D, 1600, 2141)]
        public void SetRPM_AdjustRPMOnGearChangeUpshiftTest(GearShifterPosition position, int gasPedal, double expectedRPM)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            ec.UpdateEngineProperties(packet);
            ec.UpdateEngineProperties(packet);

            Assert.Equal(expectedRPM, ec.RPM);
        }

        [Theory]
        [InlineData(GearShifterPosition.D, 2000, 1010)]
        [InlineData(GearShifterPosition.D, 1600, 808)]
        public void SetRPM_AdjustRPMOnGearChangeDownshiftTest(GearShifterPosition position, int gasPedal, double expectedRPM)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            ec.UpdateEngineProperties(packet);
            ec.UpdateEngineProperties(packet);
            packet.GasPedal = -gasPedal;
            ec.UpdateEngineProperties(packet);

            Assert.Equal(expectedRPM, ec.RPM);
        }

        [Theory]
        [InlineData(GearShifterPosition.D, 2000, 0, 50)]
        [InlineData(GearShifterPosition.R, 2000, 0, 37)]
        [InlineData(GearShifterPosition.P, 2000, 0, 0)]
        [InlineData(GearShifterPosition.N, 2000, 0, 0)]
        [InlineData(GearShifterPosition.D, 2000, 2, 10)]
        [InlineData(GearShifterPosition.R, 3000, 2, 66)]
        [InlineData(GearShifterPosition.P, 2000, 200, 0)]
        [InlineData(GearShifterPosition.N, 2000, 200, 0)]
        public void SetRPM_SetVelocityTest(GearShifterPosition position, int gasPedal, int brakePedal, double expected)
        {
            EngineController ec = new EngineController();
            DummyPowerTrainPacket packet = new DummyPowerTrainPacket();

            packet.GearShifterPosition = position;
            packet.GasPedal = gasPedal;
            packet.BrakePedal = brakePedal;
            ec.UpdateEngineProperties(packet);

            Assert.Equal(expected, Math.Floor(ec.VelocityPixelsPerSecond));
        }
    }

    public class DummyPowerTrainPacket : IPowerTrainPacket
    {
        public int GasPedal { get; set; }
        public int BrakePedal { get; set; }
        public int SteeringWheel { get; set; }
        public GearShifterPosition GearShifterPosition { get; set; }

        public DummyPowerTrainPacket()
        {

        }
    }
}
