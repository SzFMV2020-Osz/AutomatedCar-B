using AutomatedCar.SystemComponents;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.SystemComponents
{
    public class InputProcessTests : IDisposable
    {
        static HumanMachineInterface hmi;

        public InputProcessTests()
        {
            hmi = new HumanMachineInterface(new VirtualFunctionBus());
        }
        
        #region GearShiftTests
        [Theory]
        [InlineData(0, Gears.P)]
        [InlineData(1, Gears.R)]
        [InlineData(2, Gears.N)]
        [InlineData(3, Gears.D)]
        [InlineData(4, Gears.D)]
        [InlineData(500, Gears.D)]
        public void WithNumerousGearshiftsUpwardGearIncreasesTillDrivemode(int gearshiftsUp, Gears gear)
        {
            for (int i = 0; i < gearshiftsUp; i++)
            {
                hmi.GearCalculate(true, false);
            }
            Assert.Equal(gear, hmi.HmiPacket.Gear);
        }

        [Theory]
        [InlineData(3, Gears.P)]
        [InlineData(2, Gears.R)]
        [InlineData(1, Gears.N)]
        [InlineData(0, Gears.D)]
        [InlineData(4, Gears.P)]
        [InlineData(500, Gears.P)]
        public void WithNumerousGearshiftsDownwardGearDecreasesTillParkingmode(int gearshiftsDown, Gears gear)
        {
            ((HMIPacket)hmi.HmiPacket).Gear = Gears.D;
            for (int i = 0; i < gearshiftsDown; i++)
            {
                hmi.GearCalculate(false, true);
            }
            Assert.Equal(gear, ((HMIPacket)hmi.HmiPacket).Gear);
        }

        [Theory]
        [InlineData(10, Gears.P)]
        [InlineData(10, Gears.R)]
        [InlineData(10, Gears.N)]
        [InlineData(10, Gears.D)]
        public void WithBothGearUpAndGearDownBeingTrueGearDoesentChangeItsValue(int indx, Gears gear)
        {
            ((HMIPacket)hmi.HmiPacket).Gear = gear;
            for (int i = 0; i < indx; i++)
            {
                hmi.GearCalculate(true, true);
                Assert.Equal(gear, ((HMIPacket)hmi.HmiPacket).Gear);
            }
        }

        [Theory]
        [InlineData(10, Gears.P)]
        [InlineData(10, Gears.R)]
        [InlineData(10, Gears.N)]
        [InlineData(10, Gears.D)]
        public void WithBothGearUpAndGearDownBeingFalseGearDoesentChangeItsValue(int indx, Gears gear)
        {
            ((HMIPacket)hmi.HmiPacket).Gear = gear;
            for (int i = 0; i < indx; i++)
            {
                hmi.GearCalculate(false, false);
                Assert.Equal(gear, ((HMIPacket)hmi.HmiPacket).Gear);
            }
        }

        #endregion

        #region AccTests

        [Fact]
        public void WithNoInputAccSetDoesentChangeTheStateOfAcc()
        {
            var acc = ((HMIPacket)hmi.HmiPacket).Acc;
            hmi.AccSet(false);

            Assert.Equal(acc, ((HMIPacket)hmi.HmiPacket).Acc);
        }

        [Fact]
        public void WithNoInputAccSpeedSetDoesentChangeTheDesiredSpeed()
        {
            var speed = ((HMIPacket)hmi.HmiPacket).AccSpeed;
            hmi.AccSpeedSet(false, false);

            Assert.Equal(speed, ((HMIPacket)hmi.HmiPacket).AccSpeed);
        }

        [Fact]
        public void WithNoInputAccDistenceSetDoesentChangeTheDesiredDistance()
        {
            var distance = ((HMIPacket)hmi.HmiPacket).AccDistance;
            hmi.AccDistanceSet(false);

            Assert.Equal(distance, ((HMIPacket)hmi.HmiPacket).AccDistance);
        }

        [Fact]
        public void WithVariousInputAccTurnsOnAndOff()
        {
            hmi.AccSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).Acc);
            hmi.AccSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).Acc);
        }

        [Theory]
        [InlineData(0, 0.8)]
        [InlineData(1, 1)]
        [InlineData(2, 1.2)]
        [InlineData(3, 1.4)]
        [InlineData(4, 0.8)]
        [InlineData(5, 1)]
        [InlineData(6, 1.2)]
        [InlineData(7, 1.4)]
        [InlineData(8, 0.8)]
        public void WithNumerousTrueInputAccDistanceGoesToMaxValueThenBackToMin(int indx, double value)
        {
            for (int i = 0; i < indx; i++)
            {
                hmi.AccDistanceSet(true);
            }
            Assert.Equal(((HMIPacket)hmi.HmiPacket).AccDistance, value);
        }

        [Fact]
        public void WithNoumerusAccSpeedPlusInputItsValueGoesUpToMaxValue()
        {
            var value = ((HMIPacket)hmi.HmiPacket).AccSpeed;
            for (int i = 0; i < 30; i++)
            {
                if (value != 160)
                {
                    value += 10;
                }
                hmi.AccSpeedSet(true, false);

                Assert.Equal(value, ((HMIPacket)hmi.HmiPacket).AccSpeed);
            }
        }

        [Fact]
        public void WithNoumerusAccSpeedMinusInputItsValueGoesDownToMinValue()
        {
            var value = ((HMIPacket)hmi.HmiPacket).AccSpeed = 160;
            for (int i = 0; i < 30; i++)
            {
                if (value != 30)
                {
                    value -= 10;
                }
                hmi.AccSpeedSet(false, true);

                Assert.Equal(value, ((HMIPacket)hmi.HmiPacket).AccSpeed);
            }
        }

        [Fact]
        public void WithBothAccSpeedMinusAndAccSpeedPlusBeingTrueItsValueDoesentChange()
        {
            var value = ((HMIPacket)hmi.HmiPacket).AccSpeed = 70;
            for (int i = 0; i < 10; i++)
            {
                hmi.AccSpeedSet(true, true);
                Assert.Equal(value, ((HMIPacket)hmi.HmiPacket).AccSpeed);
            }
        }

        [Fact]
        public void WithBothAccSpeedMinusAndAccSpeedPlusBeingFalseItsValueDoesentChange()
        {
            var value = ((HMIPacket)hmi.HmiPacket).AccSpeed = 70;
            for (int i = 0; i < 10; i++)
            {
                hmi.AccSpeedSet(false, false);
                Assert.Equal(value, ((HMIPacket)hmi.HmiPacket).AccSpeed);
            }
        }
        #endregion

        #region GasBreakStearTests
        [Fact]
        public void WithNoInputHandleGasPedalReturns0()
        {
            hmi.HandleGasPedal(false);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Gaspedal);
        }

        [Fact]
        public void WithNoInputHandleBrakePedalReturns0()
        {
            hmi.HandleBrakePedal(false);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Breakpedal);
        }

        [Fact]
        public void WithNoInputHandleSteeringReturns0()
        {
            hmi.HandleSteering(false, false);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Steering);
        }

        [Fact]
        public void WithWrongInputHandleSteeringActsAsNoInputWasGiven()
        {
            hmi.HandleSteering(true, true);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Steering);
        }

        [Fact]
        public void WithNoumerousTrueInputHandleGasPedalGouesUpTo100ThenWithGoesDownTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleGasPedal(true);
            }
            Assert.Equal(100, ((HMIPacket)hmi.HmiPacket).Gaspedal);
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleGasPedal(false);
            }
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Gaspedal);
        }

        [Fact]
        public void WithNoumerousTrueInputHandleBrakePedalGouesUpTo100ThenWithGoesDownTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleBrakePedal(true);
            }
            Assert.Equal(100, ((HMIPacket)hmi.HmiPacket).Breakpedal);
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleBrakePedal(false);
            }
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Breakpedal);
        }

        [Fact]
        public void WithNoumerousTrueFalseInputHandleSteeringGouesUpTo100ThenWithGoesDownTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleSteering(true, false);
            }
            Assert.Equal(100, ((HMIPacket)hmi.HmiPacket).Steering);
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleSteering(false, false);
            }
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Breakpedal);
        }

        [Fact]
        public void WithNoumerousFalseTrueInputHandleSteeringGouesDownToMinus100ThenWithGoesUpTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleSteering(false, true);
            }
            Assert.Equal(-100, ((HMIPacket)hmi.HmiPacket).Steering);
            for (int i = 0; i < 100; i++)
            {
                hmi.HandleSteering(false, false);
            }
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Breakpedal);
        }
        #endregion

        #region ToogleInputTests
        [Fact]
        public void WithNoInputTurnSignalRightSetDoesentChangeTheStateOfTurnSignalRight()
        {
            var tsright = ((HMIPacket)hmi.HmiPacket).TurnSignalRight;
            hmi.TurnSignalRightSet(false);

            Assert.Equal(tsright, ((HMIPacket)hmi.HmiPacket).TurnSignalRight);
        }

        [Fact]
        public void WithNoInputTurnSignalLeftSetDoesentChangeTheStateOfTurnSignalLeftt()
        {
            var tsleft = ((HMIPacket)hmi.HmiPacket).TurnSignalLeft;
            hmi.TurnSignalLeftSet(false);

            Assert.Equal(tsleft, ((HMIPacket)hmi.HmiPacket).TurnSignalLeft);
        }

        [Fact]
        public void WithNoInputLaneKeepingSetDoesentChangeTheStateOfLaneKeeping()
        {
            var lanekeep = ((HMIPacket)hmi.HmiPacket).LaneKeeping;
            hmi.LaneKeepingSet(false);

            Assert.Equal(lanekeep, ((HMIPacket)hmi.HmiPacket).LaneKeeping);
        }

        [Fact]
        public void WithNoInputParkingPilotSetDoesentChangeTheStateOfParkingPilot()
        {
            var parkingPilot = ((HMIPacket)hmi.HmiPacket).ParkingPilot;
            hmi.ParkingPilotSet(false);

            Assert.Equal(parkingPilot, ((HMIPacket)hmi.HmiPacket).ParkingPilot);
        }

        [Fact]
        public void WithNoumerusInputTurnSignalRightSwitchBetweenOnAndOff()
        {
            hmi.TurnSignalRightSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).TurnSignalRight);
            hmi.TurnSignalRightSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).TurnSignalRight);
        }

        [Fact]
        public void WithNoumerusInputTurnSignalLeftSwitchBetweenOnAndOff()
        {
            hmi.TurnSignalLeftSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).TurnSignalLeft);
            hmi.TurnSignalLeftSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).TurnSignalLeft);
        }

        [Fact]
        public void WithNoumerusInputLaneKeepingSwitchBetweenOnAndOff()
        {
            hmi.LaneKeepingSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).LaneKeeping);
            hmi.LaneKeepingSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).LaneKeeping);            
        }

        [Fact]
        public void WithNoumerusInputParkingPilotSwitchBetweenOnAndOff()
        {
            hmi.ParkingPilotSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).ParkingPilot);
            hmi.ParkingPilotSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).ParkingPilot);
        }

        [Fact]
        public void WithNoInputPolygonSetDoesentChangeTheStateOfPolygon()
        {
            var polygon = ((DebugPacket)hmi.DebugPacket).Polygon;
            hmi.PolygonSet(false);

            Assert.Equal(polygon, ((DebugPacket)hmi.DebugPacket).Polygon);
        }

        [Fact]
        public void WithNoumerusInputPolygonSetSwitchBetweenOnAndOff()
        {
            hmi.PolygonSet(true);
            Assert.True(((DebugPacket)hmi.DebugPacket).Polygon);
            hmi.PolygonSet(false);
            Assert.False(((DebugPacket)hmi.DebugPacket).Polygon);            
        }

        [Fact]
        public void WithNoInputUtrasoundSensorSetDoesentChangeTheStateOfUtrasoundSensor()
        {
            var ultrasound = ((DebugPacket)hmi.DebugPacket).UtrasoundSensor;
            hmi.UtrasoundSensorSet(false);

            Assert.Equal(ultrasound, ((DebugPacket)hmi.DebugPacket).UtrasoundSensor);
        }

        [Fact]
        public void WithNoumerusInputUtrasoundSensorSetSwitchBetweenOnAndOff()
        {
            hmi.UtrasoundSensorSet(true);
            Assert.True(((DebugPacket)hmi.DebugPacket).UtrasoundSensor);
            hmi.UtrasoundSensorSet(false);
            Assert.False(((DebugPacket)hmi.DebugPacket).UtrasoundSensor);
        }

        [Fact]
        public void WithNoInputRadarSensorSetDoesentChangeTheStateOfRadarSensor()
        {
            var radar = ((DebugPacket)hmi.DebugPacket).RadarSensor;
            hmi.RadarSensorSet(false);

            Assert.Equal(radar, ((DebugPacket)hmi.DebugPacket).RadarSensor);
        }

        [Fact]
        public void WithNoumerusInputRadarSensorSetSwitchBetweenOnAndOff()
        {
            hmi.RadarSensorSet(true);
            Assert.True(((DebugPacket)hmi.DebugPacket).RadarSensor);
            hmi.RadarSensorSet(false);
            Assert.False(((DebugPacket)hmi.DebugPacket).RadarSensor);
        }

        [Fact]
        public void WithNoInputBoardCameraSetDoesentChangeTheStateOfBoardCamera()
        {
            var camera = ((DebugPacket)hmi.DebugPacket).BoardCamera;
            hmi.BoardCameraSet(false);

            Assert.Equal(camera, ((DebugPacket)hmi.DebugPacket).BoardCamera);
        }

        [Fact]
        public void WithNoumerusInputBoardCameraSetSwitchBetweenOnAndOff()
        {
            hmi.BoardCameraSet(true);
            Assert.True(((DebugPacket)hmi.DebugPacket).BoardCamera);
            hmi.BoardCameraSet(false);
            Assert.False(((DebugPacket)hmi.DebugPacket).BoardCamera);
        }

        public void Dispose()
        {
            hmi = new HumanMachineInterface(new VirtualFunctionBus());
        }
        #endregion
    }
}
