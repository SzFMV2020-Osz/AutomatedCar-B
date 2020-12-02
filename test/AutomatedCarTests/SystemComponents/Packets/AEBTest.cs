namespace Tests.SystemComponents.Packets
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia;
    using Xunit;
    
    public class AEBTest {
        AEB aeb = new AEB();
        int m = 50;
        int km = 1000*50;
        int h = 60*60;

        /**
        Convert km/h into pixel/sec
        */
        private int kmh_into_pxs(int kmh){
            return (kmh*km)/h;
        }

        [Fact]
        public void IsAEBUseable_carSpeed0(){
            aeb.controlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Speed = kmh_into_pxs(0);

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed10(){
            aeb.controlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Speed = kmh_into_pxs(10);

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed69(){
            aeb.controlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Speed = kmh_into_pxs(69);

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed71(){
            aeb.controlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Speed = kmh_into_pxs(71);

            Assert.Equal(false, aeb.IsUseable());
        }

        [Fact]
        public void getStoppingDistanceTo_carPosition_speed0_x0_y0_worldobjectPosition_speed0_x0_y0() {
            aeb.controlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            WorldObject wo = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());

            double distance = aeb.getStoppingDistanceTo_inPixels(wo);

            Assert.Equal(0, distance);
        }

        [Fact]
        public void getStoppingDistanceTo_carPosition_speed9ms_x0m_y0_worldobjectPosition_speed0_x0_y0() {
            aeb.controlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Speed = 9*50; //9ms
            WorldObject wo = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());

            double distance = aeb.getStoppingDistanceTo_inPixels(wo);

            Assert.Equal(9*50, distance);
        }

        [Fact]
        public void getStoppingDistanceTo_carPosition_speed9ms_x100m_y0_worldobjectPosition_speed0_x0_y0() {
            aeb.controlledCar = new AutomatedCar(100*50, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Speed = 9*50; //9ms
            WorldObject wo = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());

            double distance = aeb.getStoppingDistanceTo_inPixels(wo);

            Assert.Equal(91*50, distance);
        }

       
        public void thereIsAnObjectInRadar()
        {
            aeb.controlledCar = new AutomatedCar(100 * 50, 0, "", 0, 0, new List<List<Avalonia.Point>>());            
            WorldObject wo = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            aeb.controlledCar.Radar.LastSeenObject = wo;
            Assert.True(aeb.isThereAnObjectInRadar());
        }

        [Fact]
        public void thereIsNoObjectInRadar()
        {
            aeb.controlledCar = new AutomatedCar(100 * 50, 0, "", 0, 0, new List<List<Avalonia.Point>>());          
            Assert.False(aeb.isThereAnObjectInRadar());
        }

         [Fact]
        public void setAEB_Warning() {
            VirtualFunctionBus virtualFunctionBus = new VirtualFunctionBus();
            aeb = new AEB(virtualFunctionBus);

            aeb.SetWarning("Text");

            Assert.Equal("Text", virtualFunctionBus.AEBActionPacket.Message);
        }

         [Fact]
        public void setAEB_Warning_over70() {
            VirtualFunctionBus virtualFunctionBus = new VirtualFunctionBus();
            aeb = new AEB(virtualFunctionBus);
            aeb.controlledCar = new AutomatedCar(100 * 50, 0, "", 0, 0, new List<List<Avalonia.Point>>()); 
            aeb.controlledCar.Speed = kmh_into_pxs(71);

            aeb.Run();

            Assert.Equal("AEB off", virtualFunctionBus.AEBActionPacket.Message);
        }

        [Fact]
        public void setAEB_Warning_lessthan70() {
            VirtualFunctionBus virtualFunctionBus = new VirtualFunctionBus();
            aeb = new AEB(virtualFunctionBus);
            aeb.controlledCar = new AutomatedCar(100 * 50, 0, "", 0, 0, new List<List<Avalonia.Point>>()); 
            aeb.controlledCar.Speed = kmh_into_pxs(69);

            aeb.Run();

            Assert.Equal("", virtualFunctionBus.AEBActionPacket.Message);
        }
    }
}