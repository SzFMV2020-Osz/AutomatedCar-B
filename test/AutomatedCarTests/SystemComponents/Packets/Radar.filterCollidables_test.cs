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
        [Fact]
        public void FilterList()
        {

            List<WorldObject> wo = new List<WorldObject>();
            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Avalonia.Point>>());
            car.IsColliding = true;
            wo.Add(car);
            wo.Add(new AutomatedCar.Models.Road(0,0,"",0,0,0,0, new Avalonia.Matrix(), new List<List<Avalonia.Point>>()));

            Radar radar = new Radar();

            List<NoticedObject> nwo = radar.filterCollidables(wo);


            Assert.Equal(1, nwo.Count);
        }

        [Fact]
        public void UseGivenWorldObject()
        {

            List<WorldObject> wo = new List<WorldObject>();
            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(0,0,"car",0,0,new List<List<Avalonia.Point>>());
            car.IsColliding = true;
            wo.Add(new AutomatedCar.Models.Road(0,0,"",0,0,0,0, new Avalonia.Matrix(), new List<List<Avalonia.Point>>()));
            wo.Add(car);

            Radar radar = new Radar();

            List<NoticedObject> nwo = radar.filterCollidables(wo);

            Assert.Equal(car, nwo[0].worldObject);
        }
    }
}