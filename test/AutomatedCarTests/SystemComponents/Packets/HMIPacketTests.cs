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
    }
}