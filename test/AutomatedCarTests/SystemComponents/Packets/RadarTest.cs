using AutomatedCar.SystemComponents;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Layout;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using Xunit;
using NetTopologySuite.GeometriesGraph;
using System.Linq;
using Avalonia;

namespace Tests.SystemComponents.Packets
{
    public class RadarTest_filterCollidables
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

    public class RadarTest_getDangeriousWorldObjects
    {
        [Fact]
        public void getWorldObjectsList()
        {
            Radar radar = new Radar();
            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.Vector = new Vector(0, 150);
            radar.NoticedObjects.Add(nw);

            List<WorldObject> wos = radar.getDangerousWorldObjects();
            Assert.IsType<List<WorldObject>>(wos);
        }

        [Fact]
        public void getWorldObjectItem()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400,500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400,300,"",0,0, new List<List<Point>>());

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.Vector = new Vector(0, 150);
            radar.NoticedObjects.Add(nw);

            List<WorldObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_twoSlow()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400,500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400,300,"",0,0, new List<List<Point>>());

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            NoticedObject nw2 = new NoticedObject();
            nw.Vector = new Vector(0, 150);
            nw2.Vector = new Vector(0, 100);
            radar.NoticedObjects.Add(nw);
            radar.NoticedObjects.Add(nw2);

            List<WorldObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(2, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_oneSlow_oneFast()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400,500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400,300,"",0,0, new List<List<Point>>());

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            NoticedObject nw2 = new NoticedObject();
            nw.Vector = new Vector(0, 150);
            nw2.Vector = new Vector(0, 200);
            radar.NoticedObjects.Add(nw);
            radar.NoticedObjects.Add(nw2);

            List<WorldObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_opositeDirection()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400,500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400,300,"",0,0, new List<List<Point>>());

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.Vector = new Vector(0, -200);
            radar.NoticedObjects.Add(nw);

            List<WorldObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }
    }
}