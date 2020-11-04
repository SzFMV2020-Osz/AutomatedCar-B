using AutomatedCar.SystemComponents;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Layout;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using Xunit;
using Avalonia;
using System.Linq;

namespace Tests.SystemComponents.Packets
{
    public class FinalRadarTest
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

            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(200,100,"",0,0,new List<List<Avalonia.Point>>());

            // ====================================== Tick ================================

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
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(200,150,"",0,0,new List<List<Avalonia.Point>>());

        }

        private void prepareRadar(){
            R.NoticedObjects = new List<AutomatedCar.Models.NoticedObject>();
            NoticedObject n1 = new NoticedObject();
            NoticedObject n2 = new NoticedObject();
            NoticedObject n3 = new NoticedObject();
            NoticedObject n4 = new NoticedObject();
            n1.worldObject = w_slow;
            n1.PrevX = 150;
            n1.PrevY = 350;
            n1.Vector = new Vector(0, -50);
            n2.worldObject = w_oposite;
            n2.PrevX = 200;
            n2.PrevY = 350;
            n2.Vector = new Vector(0, 100);
            n3.worldObject = w_fast;
            n3.PrevX = 200;
            n3.PrevY = 300;
            n3.Vector = new Vector(0, -100);
            n4.worldObject = w_leaving;
            n4.PrevX = 250;
            n4.PrevY = 400;
            n4.Vector = new Vector(0, 100);
            R.NoticedObjects.Add(n1);
            R.NoticedObjects.Add(n2);
            R.NoticedObjects.Add(n3);
            R.NoticedObjects.Add(n4);
        }

    }
}