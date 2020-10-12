using AutomatedCar.SystemComponents;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Layout;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System.ComponentModel;
using Xunit;

namespace Tests.SystemComponents.Packets
{
    public class HMIPacketTests
    {
        HMIPacket hmiPacket;
        HumanMachineInterface hmi;

        public  HMIPacketTests()
        {
            hmiPacket = new HMIPacket();
            hmi = new HumanMachineInterface(new VirtualFunctionBus());
        }

        #region Existence tests
        [Fact]
        public void GaspedalExsits()
        {
            Assert.IsType<double>(hmiPacket.Gaspedal);
        }

        [Fact]
        public void BreakpedalExsits()
        {
            Assert.IsType<double>(hmiPacket.Breakpedal);
        }

        [Fact]
        public void SteeringExsits()
        {
            Assert.IsType<double>(hmiPacket.Steering);
        }
        [Fact]
        public void GearExsits()
        {
            Assert.IsType<Gears>(hmiPacket.Gear);
        }

        [Fact]
        public void ACCExsist()
        {
            Assert.IsType<bool>(hmiPacket.Acc);
        }

        [Fact]
        public void ACCDistanceExsits()
        {
            Assert.IsType<double>(hmiPacket.AccDistance);
        }

        [Fact]
        public void ACCSpeedExsits()
        {
            Assert.IsType<int>(hmiPacket.AccSpeed);
        }
        [Fact]
        public void LaneKeepingExsits()
        {
            Assert.IsType<bool>(hmiPacket.LaneKeeping);
        }

        [Fact]
        public void ParkingPilotExsits()
        {
            Assert.IsType<bool>(hmiPacket.ParkingPilot);
        }

        [Fact]
        public void TurnSignalRightExsits()
        {
            Assert.IsType<bool>(hmiPacket.TurnSignalRight);
        }

        [Fact]
        public void TurnSignalLeftExsits()
        {
            Assert.IsType<bool>(hmiPacket.TurnSignalLeft);
        }

        [Fact]
        public void SignExsits()
        {
            Assert.Equal(string.Empty, hmiPacket.Sign);
        }
        #endregion

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
                hmiPacket.GearCalculate(true, false); 
            }
            Assert.Equal(gear, hmiPacket.Gear);
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
            hmiPacket.Gear = Gears.D;
            for (int i = 0; i < gearshiftsDown; i++)
            {
                hmiPacket.GearCalculate(false, true);
            }
            Assert.Equal(gear, hmiPacket.Gear);
        }

        [Theory]
        [InlineData(10, Gears.P)]
        [InlineData(10, Gears.R)]
        [InlineData(10, Gears.N)]
        [InlineData(10, Gears.D)]
        public void WithBothGearUpAndGearDownBeingTrueGearDoesentChangeItsValue(int indx, Gears gear)
        {
            hmiPacket.Gear = gear;
            for (int i = 0; i < indx; i++)
            {
                hmiPacket.GearCalculate(true, true);
                Assert.Equal(gear, hmiPacket.Gear);
            }
        }

        [Theory]
        [InlineData(10, Gears.P)]
        [InlineData(10, Gears.R)]
        [InlineData(10, Gears.N)]
        [InlineData(10, Gears.D)]
        public void WithBothGearUpAndGearDownBeingFalseGearDoesentChangeItsValue(int indx, Gears gear)
        {
            hmiPacket.Gear = gear;
            for (int i = 0; i < indx; i++)
            {
                hmiPacket.GearCalculate(false, false);
                Assert.Equal(gear, hmiPacket.Gear);
            }
        }

        #endregion

        #region AccTests

        [Fact]
        public void WithNoInputAccSetDoesentChangeTheStateOfAcc()
        {
            var acc = hmiPacket.Acc;            
            hmiPacket.AccSet(false);
            
            Assert.Equal(acc, hmiPacket.Acc);            
        }

        [Fact]
        public void WithNoInputAccSpeedSetDoesentChangeTheDesiredSpeed()
        {
            var speed = hmiPacket.AccSpeed;           
            hmiPacket.AccSpeedSet(false, false);
            
            Assert.Equal(speed, hmiPacket.AccSpeed);            
        }

        [Fact]
        public void WithNoInputAccDistenceSetDoesentChangeTheDesiredDistance()
        {
            var distance = hmiPacket.AccDistance;
            hmiPacket.AccDistanceSet(false);
            
            Assert.Equal(distance, hmiPacket.AccDistance);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(false, true)]
        public void WithVariousInputAccTurnsOnAndOff(bool acc, bool packet) 
        {
            hmi.Acc = acc;
            hmiPacket.Acc = packet;
            hmiPacket.AccSet(hmi.Acc);

            if (acc)
            {               
                Assert.NotEqual(packet, hmiPacket.Acc);
            }
            else
            {
                Assert.Equal(packet, hmiPacket.Acc);
            }           
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
                hmiPacket.AccDistanceSet(true);
            }
            Assert.Equal(hmiPacket.AccDistance, value);
        }

        [Fact]
        public void WithNoumerusAccSpeedPlusInputItsValueGoesUpToMaxValue()
        {
            var value = hmiPacket.AccSpeed;
            for (int i = 0; i < 30; i++)
            {
                if (value != 160)
                {
                    value += 10;
                }
                hmiPacket.AccSpeedSet(true, false);
                
                Assert.Equal(value, hmiPacket.AccSpeed);
            }
        }

        [Fact]
        public void WithNoumerusAccSpeedMinusInputItsValueGoesDownToMinValue()
        {
            var value = hmiPacket.AccSpeed = 160;
            for (int i = 0; i < 30; i++)
            {
                if (value != 30)
                {
                    value -= 10;
                }
                hmiPacket.AccSpeedSet(false, true);

                Assert.Equal(value, hmiPacket.AccSpeed);
            }
        }

        [Fact]
        public void WithBothAccSpeedMinusAndAccSpeedPlusBeingTrueItsValueDoesentChange()
        {
            var value = hmiPacket.AccSpeed = 70;
            for (int i = 0; i < 10; i++)
            {
                hmiPacket.AccSpeedSet(true, true);
                Assert.Equal(value, hmiPacket.AccSpeed);
            }
        }

        [Fact]
        public void WithBothAccSpeedMinusAndAccSpeedPlusBeingFalseItsValueDoesentChange()
        {
            var value = hmiPacket.AccSpeed = 70;
            for (int i = 0; i < 10; i++)
            {
                hmiPacket.AccSpeedSet(false, false);
                Assert.Equal(value, hmiPacket.AccSpeed);
            }
        }
        #endregion

        #region GasBreakStearTests
        [Fact]
        public void WithNoInputHandleGasPedalReturns0()
        {
            hmiPacket.HandleGasPedal(false);
            Assert.Equal(0, hmiPacket.Gaspedal);
        }

        [Fact]
        public void WithNoInputHandleBrakePedalReturns0()
        {
            hmiPacket.HandleBrakePedal(false);
            Assert.Equal(0, hmiPacket.Breakpedal);
        }

        [Fact]
        public void WithNoInputHandleSteeringReturns0()
        {
            hmiPacket.HandleSteering(false, false);
            Assert.Equal(0, hmiPacket.Steering);
        }

        [Fact]
        public void WithWrongInputHandleSteeringActsAsNoInputWasGiven()
        {
            hmiPacket.HandleSteering(true, true);
            Assert.Equal(0, hmiPacket.Steering);
        }

        [Fact]
        public void WithNoumerousTrueInputHandleGasPedalGouesUpTo100ThenWithGoesDownTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleGasPedal(true); 
            }
            Assert.Equal(100, hmiPacket.Gaspedal);
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleGasPedal(false);
            }
            Assert.Equal(0, hmiPacket.Gaspedal);
        }

        [Fact]
        public void WithNoumerousTrueInputHandleBrakePedalGouesUpTo100ThenWithGoesDownTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleBrakePedal(true);
            }
            Assert.Equal(100, hmiPacket.Breakpedal);
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleBrakePedal(false);
            }
            Assert.Equal(0, hmiPacket.Breakpedal);
        }

        public void WithNoumerousTrueFalseInputHandleSteeringGouesUpTo100ThenWithGoesDownTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleSteering(true, false);
            }
            Assert.Equal(100, hmiPacket.Breakpedal);
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleSteering(false, false);
            }
            Assert.Equal(0, hmiPacket.Breakpedal);
        }

        public void WithNoumerousFalseTrueInputHandleSteeringGouesDownToMinus100ThenWithGoesUpTo0()
        {
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleSteering(false, true);
            }
            Assert.Equal(-100, hmiPacket.Breakpedal);
            for (int i = 0; i < 100; i++)
            {
                hmiPacket.HandleSteering(false, false);
            }
            Assert.Equal(0, hmiPacket.Breakpedal);
        }
        #endregion

        #region ToogleInputTests
        [Fact]
        public void WithNoInputTurnSignalRightSetDoesentChangeTheStateOfTurnSignalRight()
        {
            var tsright = hmiPacket.TurnSignalRight;
            hmiPacket.TurnSignalRightSet(false);

            Assert.Equal(tsright, hmiPacket.TurnSignalRight);
        }

        [Fact]
        public void WithNoInputTurnSignalLeftSetDoesentChangeTheStateOfTurnSignalLeftt()
        {
            var tsleft = hmiPacket.TurnSignalLeft;
            hmiPacket.TurnSignalLeftSet(false);

            Assert.Equal(tsleft, hmiPacket.TurnSignalLeft);
        }

        [Fact]
        public void WithNoInputLaneKeepingSetDoesentChangeTheStateOfLaneKeeping()
        {
            var lanekeep = hmiPacket.LaneKeeping;
            hmiPacket.LaneKeepingSet(false);

            Assert.Equal(lanekeep, hmiPacket.LaneKeeping);
        }

        [Fact]
        public void WithNoInputParkingPilotSetDoesentChangeTheStateOfParkingPilot()
        {
            var parkingPilot = hmiPacket.ParkingPilot;
            hmiPacket.ParkingPilotSet(false);

            Assert.Equal(parkingPilot, hmiPacket.ParkingPilot);
        }
        
        [Fact]
        public void WithNoumerusInputTurnSignalRightSwitchBetweenOnAndOff()
        {
            var tsright = hmiPacket.TurnSignalRight;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(tsright, hmiPacket.TurnSignalRight);
                hmiPacket.TurnSignalRightSet(true);
                tsright = !tsright;
            }
            
        }

        [Fact]
        public void WithNoumerusInputTurnSignalLeftSwitchBetweenOnAndOff()
        {
            var tsleft = hmiPacket.TurnSignalLeft;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(tsleft, hmiPacket.TurnSignalLeft);
                hmiPacket.TurnSignalLeftSet(true);
                tsleft = !tsleft;
            }
        }

        [Fact]
        public void WithNoumerusInputLaneKeepingSwitchBetweenOnAndOff()
        {
            var lanekeep = hmiPacket.LaneKeeping;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(lanekeep, hmiPacket.LaneKeeping);
                hmiPacket.LaneKeepingSet(true);
                lanekeep = !lanekeep;
            }
        }

        [Fact]
        public void WithNoumerusInputParkingPilotSwitchBetweenOnAndOff()
        {
            var parkingPilot = hmiPacket.ParkingPilot;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(parkingPilot, hmiPacket.ParkingPilot);
                hmiPacket.ParkingPilotSet(true);
                parkingPilot = !parkingPilot;
            }
        }
        #endregion
    }
}
