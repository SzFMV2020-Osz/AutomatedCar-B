using AutomatedCar.SystemComponents.Packets;
using Xunit;

namespace Tests.SystemComponents.Packets
{
    public class DebugPacketTests
    {
        DebugPacket debugPacket;

        public DebugPacketTests()
        {
            debugPacket = new DebugPacket();
        }

        [Fact]
        public void UltrasoundSensorExsits()
        {            
            Assert.IsType<bool>(debugPacket.UtrasoundSensor);
        }

        [Fact]
        public void BoardCameraExsits()
        {
            Assert.IsType<bool>(debugPacket.BoardCamera);
        }

        [Fact]
        public void RadarSensorExsits()
        {
            Assert.IsType<bool>(debugPacket.RadarSensor);
        }

        [Fact]
        public void WithNoInputUtrasoundSensorSetDoesentChangeTheStateOfUtrasoundSensor()
        {
            var ultrasound = debugPacket.UtrasoundSensor;
            debugPacket.UtrasoundSensorSet(false);

            Assert.Equal(ultrasound, debugPacket.UtrasoundSensor);
        }
        
        [Fact]
        public void WithNoumerusInputUtrasoundSensorSetSwitchBetweenOnAndOff()
        {
            var ultrasound = debugPacket.UtrasoundSensor;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(ultrasound, debugPacket.UtrasoundSensor);
                debugPacket.UtrasoundSensorSet(true);
                ultrasound = !ultrasound;
            }
        }

        [Fact]
        public void WithNoInputRadarSensorSetDoesentChangeTheStateOfRadarSensor()
        {
            var radar = debugPacket.RadarSensor;
            debugPacket.RadarSensorSet(false);

            Assert.Equal(radar, debugPacket.RadarSensor);
        }

        [Fact]
        public void WithNoumerusInputRadarSensorSetSwitchBetweenOnAndOff()
        {
            var radar = debugPacket.RadarSensor;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(radar, debugPacket.RadarSensor);
                debugPacket.RadarSensorSet(true);
                radar = !radar;
            }
        }

        [Fact]
        public void WithNoInputBoardCameraSetDoesentChangeTheStateOfBoardCamera()
        {
            var camera = debugPacket.BoardCamera;
            debugPacket.BoardCameraSet(false);

            Assert.Equal(camera, debugPacket.BoardCamera);
        }

        [Fact]
        public void WithNoumerusInputBoardCameraSetSwitchBetweenOnAndOff()
        {
            var camera = debugPacket.BoardCamera;
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(camera, debugPacket.BoardCamera);
                debugPacket.BoardCameraSet(true);
                camera = !camera;
            }
        }
    }
}
