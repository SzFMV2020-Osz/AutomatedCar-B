using AutomatedCar.SystemComponents;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;
using Avalonia;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Tests.SystemComponents.Packets
{
    public class ComputeVector
    {
        [Fact]
        public void ComputeVector_from22_43()
        {
            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(4,3,"",0,0,new List<List<Avalonia.Point>>());
            NoticedObject no = new NoticedObject();
            no.PrevX = 2;
            no.PrevY = 2;
            no.worldObject = car;

            Radar R = new Radar();

            R.computeVector(no);

            Assert.Equal(2, no.Vector.Value.X);
            Assert.Equal(1, no.Vector.Value.Y);
        }

        [Fact]
        public void ComputeVector_from22_10()
        {
            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(1,0,"",0,0,new List<List<Avalonia.Point>>());
            NoticedObject no = new NoticedObject();
            no.PrevX = 2;
            no.PrevY = 2;
            no.worldObject = car;

            Radar R = new Radar();

            R.computeVector(no);

            Assert.Equal(-1, no.Vector.Value.X);
            Assert.Equal(-2, no.Vector.Value.Y);
        }
    }
}