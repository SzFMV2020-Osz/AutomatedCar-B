using AutomatedCar.SystemComponents;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Layout;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Tests.SystemComponents.Packets
{
    public class FilterCollidables
    {
        List<WorldObject> list = new List<WorldObject>();
        WorldObject w_slow = new AutomatedCar.Models.AutomatedCar(150,350,"",0,0,new List<List<Avalonia.Point>>());
        WorldObject w_oposite = new AutomatedCar.Models.AutomatedCar(200,300,"",0,0,new List<List<Avalonia.Point>>());
        WorldObject w_fast = new AutomatedCar.Models.AutomatedCar(200,350,"",0,0,new List<List<Avalonia.Point>>());
        WorldObject w_leaving = new AutomatedCar.Models.AutomatedCar(250,350,"",0,0,new List<List<Avalonia.Point>>());
        WorldObject w_new = new AutomatedCar.Models.AutomatedCar(250,450,"",0,0,new List<List<Avalonia.Point>>());

        Radar R = new Radar();

        [Fact]
        public void FinalTest1()
        {
            

            Assert.Equal(true, true);
        }



    }
}