using Xunit;
using AutomatedCar.SystemComponents;

namespace Tests.SystemComponents
{
    public class GearShifterTest
    {
        [Theory]
        [InlineData(2900, 101, GearShifterPosition.D, 5, 6)]
        [InlineData(2900, 100, GearShifterPosition.D, 5, 5)]
        [InlineData(4000, -100, GearShifterPosition.D, 4, 4)]
        [InlineData(1100, -101, GearShifterPosition.D, 4, 3)]
        public void SetDriveGear_ShiftingRPMBoundaryTest(int currentRPM, int deltaRPM, GearShifterPosition position, int currentDriveGear, int expected)
        {
            GearShifter gs = new GearShifter();
            gs.Position = position;
            gs.CurrentDriveGear = gs.DriveGears[currentDriveGear];
            gs.SetDriveGear(currentRPM, deltaRPM);

            Assert.Equal<int>(expected, gs.CurrentDriveGear.SequenceNumber);
        }

        [Theory]
        [InlineData(3000, 101, GearShifterPosition.D, 6, 6)]
        [InlineData(1000, -100, GearShifterPosition.D, 1, 1)]
        public void SetDriveGear_ShiftingDriveGearBoundaryTest(int currentRPM, int deltaRPM, GearShifterPosition position, int currentDriveGear, int expected)
        {
            GearShifter gs = new GearShifter();
            gs.Position = position;
            gs.CurrentDriveGear = gs.DriveGears[currentDriveGear];
            gs.SetDriveGear(currentRPM, deltaRPM);

            Assert.Equal<int>(expected, gs.CurrentDriveGear.SequenceNumber);
        }

        [Theory]
        [InlineData(3000, 101, GearShifterPosition.N, 6, 0)]
        [InlineData(1000, -100, GearShifterPosition.P, 5, 0)]
        [InlineData(1000, -100, GearShifterPosition.R, 3, 0)]
        public void SetDriveGear_ShiftingWhileNotInDrivePosition(int currentRPM, int deltaRPM, GearShifterPosition position, int currentDriveGear, int expected)
        {
            GearShifter gs = new GearShifter();
            gs.Position = position;
            gs.CurrentDriveGear = gs.DriveGears[currentDriveGear];
            gs.SetDriveGear(currentRPM, deltaRPM);

            Assert.Equal<int>(expected, gs.CurrentDriveGear.SequenceNumber);
        }

        [Theory]
        [InlineData(3000, 101, GearShifterPosition.D, 5, ChangeState.Upshift)]
        [InlineData(3000, 101, GearShifterPosition.D, 6, ChangeState.None)]
        [InlineData(1000, -101, GearShifterPosition.D, 5, ChangeState.Downshift)]
        [InlineData(1000, -101, GearShifterPosition.D, 1, ChangeState.None)]
        [InlineData(3000, 101, GearShifterPosition.P, 5, ChangeState.None)]
        public void SetDriveGear_ChangeStateTest(int currentRPM, int deltaRPM, GearShifterPosition position, int currentDriveGear, ChangeState expected)
        {
            GearShifter gs = new GearShifter();
            gs.Position = position;
            gs.CurrentDriveGear = gs.DriveGears[currentDriveGear];
            gs.SetDriveGear(currentRPM, deltaRPM);

            Assert.Equal<ChangeState>(expected, gs.DriveGearChangeState);
        }
    }
}
