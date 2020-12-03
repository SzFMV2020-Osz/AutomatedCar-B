using AutomatedCar.Models;
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
        static VirtualFunctionBus vfb;

        public InputProcessTests()
        {
            vfb = new VirtualFunctionBus();
            vfb.AEBActionPacket = new AEBAction() { Active = false };
            hmi = new HumanMachineInterface(vfb);
        }

        #region GearShiftTests
        [Theory]
        [InlineData(0, Gears.P)]
        [InlineData(1, Gears.R)]
        [InlineData(2, Gears.N)]
        [InlineData(3, Gears.D)]
        [InlineData(4, Gears.D)]
        [InlineData(500, Gears.D)]
        public void GearIncreasesTillDrivemodeWithNumerousGearshiftsUpward(int gearshiftsUp, Gears gear)
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
        public void GearDecreasesTillParkingmodeWithNumerousGearshiftsDownward(int gearshiftsDown, Gears gear)
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
        public void GearDoesNotChangeItsValueWithBothGearUpAndGearDownBeingTrue(int indx, Gears gear)
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
        public void GearDoesNotChangeItsValueWithBothGearUpAndGearDownBeingFalse(int indx, Gears gear)
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
        public void AccSetDoesNotChangeTheStateOfAccWithNoInput()
        {
            var acc = ((HMIPacket)hmi.HmiPacket).Acc;
            hmi.AccSet(false);

            Assert.Equal(acc, ((HMIPacket)hmi.HmiPacket).Acc);
        }

        [Fact]
        public void AccSpeedSetDoesNotChangeTheDesiredSpeedWithNoInput()
        {
            var speed = ((HMIPacket)hmi.HmiPacket).AccSpeed;
            hmi.AccSpeedSet(false, false);

            Assert.Equal(speed, ((HMIPacket)hmi.HmiPacket).AccSpeed);
        }

        [Fact]
        public void AccDistenceSetDoesNotChangeTheDesiredDistanceWithNoInput()
        {
            var distance = ((HMIPacket)hmi.HmiPacket).AccDistance;
            hmi.AccDistanceSet(false);

            Assert.Equal(distance, ((HMIPacket)hmi.HmiPacket).AccDistance);
        }

        [Fact]
        public void AccTurnsOnAndOffWithVariousInput()
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
        public void AccDistanceGoesToMaxValueThenBackToMinWithNumerousTrueInput(int indx, double value)
        {
            for (int i = 0; i < indx; i++)
            {
                hmi.AccDistanceSet(true);
            }
            Assert.Equal(((HMIPacket)hmi.HmiPacket).AccDistance, value);
        }

        [Fact]
        public void AccSpeedsValueGoesUpToMaxValueWithNoumerusAccSpeedPlusInput()
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
        public void AccSpeedsValueGoesDownToMinValueWithNoumerusAccSpeedMinusInput()
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
        public void AccSpeedsValueDoesNotChangeWithBothAccSpeedMinusAndAccSpeedPlusBeingTrue()
        {
            var value = ((HMIPacket)hmi.HmiPacket).AccSpeed = 70;
            for (int i = 0; i < 10; i++)
            {
                hmi.AccSpeedSet(true, true);
                Assert.Equal(value, ((HMIPacket)hmi.HmiPacket).AccSpeed);
            }
        }

        [Fact]
        public void AccSpeedsValueDoesNotChangeWithBothAccSpeedMinusAndAccSpeedPlusBeingFalse()
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
        public void HandleGasPedalReturns0WithNoInput()
        {
            hmi.HandleGasPedal(false);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Gaspedal);
        }

        [Fact]
        public void HandleBrakePedalReturns0WithNoInput()
        {
            hmi.HandleBrakePedal(false);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Breakpedal);
        }

        [Fact]
        public void HandleSteeringReturns0WithNoInput()
        {
            hmi.HandleSteering(false, false);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Steering);
        }

        [Fact]
        public void HandleSteeringActsAsNoInputWasGivenWithWrongInput()
        {
            hmi.HandleSteering(true, true);
            Assert.Equal(0, ((HMIPacket)hmi.HmiPacket).Steering);
        }

        [Fact]
        public void HandleGasPedalGouesUpTo100ThenWithGoesDownTo0WithNoumerousTrueInput()
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
        public void HandleBrakePedalGouesUpTo100ThenWithGoesDownTo0WithNoumerousTrueInput()
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
        public void HandleSteeringGouesUpTo100ThenWithGoesDownTo0WithNoumerousTrueFalseInput()
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
        public void HandleSteeringGouesDownToMinus100ThenWithGoesUpTo0WithNoumerousFalseTrueInput()
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
        public void TurnSignalRightSetDoesNotChangeTheStateOfTurnSignalRightWithNoInputTurn()
        {
            var tsright = ((HMIPacket)hmi.HmiPacket).TurnSignalRight;
            hmi.TurnSignalRightSet(false);

            Assert.Equal(tsright, ((HMIPacket)hmi.HmiPacket).TurnSignalRight);
        }

        [Fact]
        public void TurnSignalLeftSetDoesNotChangeTheStateOfTurnSignalLeftWithNoInput()
        {
            var tsleft = ((HMIPacket)hmi.HmiPacket).TurnSignalLeft;
            hmi.TurnSignalLeftSet(false);

            Assert.Equal(tsleft, ((HMIPacket)hmi.HmiPacket).TurnSignalLeft);
        }

        [Fact]
        public void LaneKeepingSetDoesNotChangeTheStateOfLaneKeepingWithNoInput()
        {
            var lanekeep = ((HMIPacket)hmi.HmiPacket).LaneKeeping;
            hmi.LaneKeepingSet(false);

            Assert.Equal(lanekeep, ((HMIPacket)hmi.HmiPacket).LaneKeeping);
        }

        [Fact]
        public void ParkingPilotSetDoesNotChangeTheStateOfParkingPilotWithNoInput()
        {
            var parkingPilot = ((HMIPacket)hmi.HmiPacket).ParkingPilot;
            hmi.ParkingPilotSet(false);

            Assert.Equal(parkingPilot, ((HMIPacket)hmi.HmiPacket).ParkingPilot);
        }

        [Fact]
        public void TurnSignalRightSwitchBetweenOnAndOffWithNumerousInput()
        {
            hmi.TurnSignalRightSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).TurnSignalRight);
            hmi.TurnSignalRightSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).TurnSignalRight);
        }

        [Fact]
        public void TurnSignalLeftSwitchBetweenOnAndOffWithNumerousInput()
        {
            hmi.TurnSignalLeftSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).TurnSignalLeft);
            hmi.TurnSignalLeftSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).TurnSignalLeft);
        }

        [Fact]
        public void LaneKeepingSwitchBetweenOnAndOffWithNumerousInput()
        {
            hmi.LaneKeepingSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).LaneKeeping);
            hmi.LaneKeepingSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).LaneKeeping);
        }

        [Fact]
        public void ParkingPilotSwitchBetweenOnAndOffWithNumerousInput()
        {
            hmi.ParkingPilotSet(true);
            Assert.True(((HMIPacket)hmi.HmiPacket).ParkingPilot);
            hmi.ParkingPilotSet(false);
            Assert.False(((HMIPacket)hmi.HmiPacket).ParkingPilot);
        }

        [Fact]
        public void PolygonSetDoesNotChangeTheStateOfPolygonWithNoInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0,0,"",5,5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            var polygon = car.PolygonVisible;
            hmi.PolygonSet(false);

            Assert.Equal(polygon, car.PolygonVisible);
        }

        [Fact]
        public void PolygonSetSwitchBetweenOnAndOffWithNumerousInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            hmi.PolygonSet(true);
            Assert.True(car.PolygonVisible);
            hmi.PolygonSet(false);
            Assert.False(car.PolygonVisible);
        }

        [Fact]
        public void UtrasoundSensorSetDoesNotChangeTheStateOfUtrasoundSensorWithNoInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            var ultrasound = car.UltraSoundVisible;
            hmi.UtrasoundSensorSet(false);

            Assert.Equal(ultrasound, car.UltraSoundVisible);
        }

        [Fact]
        public void UtrasoundSensorSetSwitchBetweenOnAndOffWithNumerousInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            hmi.UtrasoundSensorSet(true);
            Assert.True(car.UltraSoundVisible);
            hmi.UtrasoundSensorSet(false);
            Assert.False(car.UltraSoundVisible);
        }

        [Fact]
        public void RadarSensorSetDoesNotChangeTheStateOfRadarSensorWithNoInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            var radar = car.RadarVisible;
            hmi.RadarSensorSet(false);

            Assert.Equal(radar, car.RadarVisible);
        }

        [Fact]
        public void RadarSensorSetSwitchBetweenOnAndOffWithNumerousInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            hmi.RadarSensorSet(true);
            Assert.True(car.RadarVisible);
            hmi.RadarSensorSet(false);
            Assert.False(car.RadarVisible);
        }

        [Fact]
        public void BoardCameraSetDoesNotChangeTheStateOfBoardCameraWithNoInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            var camera = car.CameraVisible;
            hmi.BoardCameraSet(false);

            Assert.Equal(camera, car.CameraVisible);
        }

        [Fact]
        public void BoardCameraSetSwitchBetweenOnAndOffWithNumerousInput()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(0, 0, "", 5, 5, new List<List<Avalonia.Point>>());
            AutomatedCar.Models.AutomatedCar car = World.Instance.ControlledCar;
            hmi.BoardCameraSet(true);
            Assert.True(car.CameraVisible);
            hmi.BoardCameraSet(false);
            Assert.False(car.CameraVisible);
        }

        public void Dispose()
        {
            hmi = new HumanMachineInterface(new VirtualFunctionBus());
        }
        #endregion
    }
}