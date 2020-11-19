namespace Tests.SystemComponents.Packets
{
    using System;
    using System.Collections.Generic;
    using AutomatedCar.Models;
    using Avalonia;
    using Xunit;
    
    public class AEBTest {
        AEB aeb = new AEB();

        [Fact]
        public void IsAEBUseable_carSpeed0(){
            World.Instance.ControlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Speed = 0;

            Assert.Equal(true, aeb.IsUseable());
        }

        [Fact]
        public void IsAEBUseable_carSpeed71(){
            World.Instance.ControlledCar = new AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Speed = 71;

            Assert.Equal(false, aeb.IsUseable());
        }
    }
}