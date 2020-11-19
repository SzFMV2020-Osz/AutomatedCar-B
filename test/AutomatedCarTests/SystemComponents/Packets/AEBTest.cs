namespace Tests.SystemComponents.Packets
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
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
            World.Instance.ControlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Speed = kmh_into_pxs(0);

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed10(){
            World.Instance.ControlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Speed = kmh_into_pxs(10);

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed69(){
            World.Instance.ControlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Speed = kmh_into_pxs(69);

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed71(){
            World.Instance.ControlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Speed = kmh_into_pxs(71);

            Assert.Equal(false, aeb.IsUseable());
        }
    }
}