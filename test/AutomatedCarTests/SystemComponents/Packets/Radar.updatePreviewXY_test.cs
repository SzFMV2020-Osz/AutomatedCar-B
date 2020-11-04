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
    public class UpdatePrewXY
    {
        [Fact]
        public void UpdatePrewXY_test()
        {

            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(20,30,"",0,0,new List<List<Avalonia.Point>>());
            car.IsColliding = true;

            NoticedObject n = new NoticedObject();
            n.worldObject = car;



            Radar radar = new Radar();

            radar.updatePreviewXY(n);

            Assert.Equal(20, n.PrevX);
            Assert.Equal(30, n.PrevY);
        }
    }
}