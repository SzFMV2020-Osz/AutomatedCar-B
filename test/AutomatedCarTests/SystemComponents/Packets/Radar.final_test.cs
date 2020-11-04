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

        private void changeWorldObjectPosition(){
            w_slow = new AutomatedCar.Models.AutomatedCar(150,375,"",0,0,new List<List<Avalonia.Point>>());
            w_oposite = new AutomatedCar.Models.AutomatedCar(200,250,"",0,0,new List<List<Avalonia.Point>>());
            w_fast = new AutomatedCar.Models.AutomatedCar(200,400,"",0,0,new List<List<Avalonia.Point>>());
            w_leaving = new AutomatedCar.Models.AutomatedCar(250,300,"",0,0,new List<List<Avalonia.Point>>());
            w_new = new AutomatedCar.Models.AutomatedCar(250,450,"",0,0,new List<List<Avalonia.Point>>());
        }

        private void set1TickList(){
            this.list.Add(w_slow);
            this.list.Add(w_oposite);
            this.list.Add(w_fast);
            this.list.Add(w_leaving);
        }

        private void set2TickList(){
            this.list.Add(w_slow);
            this.list.Add(w_oposite);
            this.list.Add(w_fast);
            this.list.Add(w_new);
        }

        private void set1TickCar(){
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(200,100,"",0,0,new List<List<Avalonia.Point>>());
            World.Instance.ControlledCar.Angle = 180;
        }

        private void set2TickCar(){
            World.Instance.ControlledCar.Y = 150;
        }

        private void prepareRadar(){
            RadarTriangleComputer RTC = new RadarTriangleComputer();
            RTC.offset = 120;
            RTC.distance = 200;
            RTC.angle = 60 / 2;
            RTC.rotate = (int)World.Instance.ControlledCar.Angle;
            RTC.carX = (int)World.Instance.ControlledCar.X;
            RTC.carY = (int)World.Instance.ControlledCar.Y;
        }

    }
}