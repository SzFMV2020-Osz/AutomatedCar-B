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
            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(0, 0, "", 0, 0, new List<List<Avalonia.Point>>());
            car.IsColliding = true;
            wo.Add(car);
            wo.Add(new AutomatedCar.Models.Road(0, 0, "", 0, 0, 0, 0, new Avalonia.Matrix(), new List<List<Avalonia.Point>>()));

            Radar radar = new Radar();

            List<NoticedObject> nwo = radar.filterCollidables(wo);

            Assert.Equal(1, nwo.Count);
        }

        [Fact]
        public void UseGivenWorldObject()
        {
            List<WorldObject> wo = new List<WorldObject>();
            AutomatedCar.Models.AutomatedCar car = new AutomatedCar.Models.AutomatedCar(0, 0, "car", 0, 0, new List<List<Avalonia.Point>>());
            car.IsColliding = true;
            wo.Add(new AutomatedCar.Models.Road(0, 0, "", 0, 0, 0, 0, new Avalonia.Matrix(), new List<List<Avalonia.Point>>()));
            wo.Add(car);

            Radar radar = new Radar();

            List<NoticedObject> nwo = radar.filterCollidables(wo);

            Assert.Equal(car, nwo[0].worldObject);
        }
    }

    public class RadarTest_getDangeriousWorldObjects
    {
        [Fact]
        public void getDistanceFromCar()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400, 500);

            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400, 300, "", 100, 100, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 0;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(400, 400,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, 150);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(50/50, wos[0].DistanceFromCar_inMeter);
        }
        [Fact]
        public void getWorldObjectsList()
        {
            Radar radar = new Radar();
            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, 150);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();
            Assert.IsType<List<NoticedObject>>(wos);
        }

        [Fact]
        public void getWorldObjectItem_inList()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400, 500);

            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400, 300, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 0;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, 150);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_twoSlow_ObjectsAreNoticed()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400, 500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400, 300, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 0;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            NoticedObject nw2 = new NoticedObject();
            nw2.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, 150);
            nw2.Vector = new Vector(0, 100);
            radar.NoticedObjects.Add(nw);
            radar.NoticedObjects.Add(nw2);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(2, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_oneSlow_oneFast_ObjectsAreNoticed()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400, 500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400, 300, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 0;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            NoticedObject nw2 = new NoticedObject();
            nw2.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, 150);
            nw2.Vector = new Vector(0, 200);
            radar.NoticedObjects.Add(nw);
            radar.NoticedObjects.Add(nw2);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_ObjectInOpositeDirection_ObjectIsNoticed()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(400, 500);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400, 300, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 0;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, -200);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_ObjectInOpositeDirection_ObjectIsNoticed_carRotated90()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(100, 150);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(200, 150, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 90;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(-100, 0);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_ObjectInOpositeDirection_ObjectIsNoticed_carRotated180()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(100, 100);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(100, 300, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 180;
            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(0, 200);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_ObjectInOpositeDirection_ObjectIsNoticed_carRotated270()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(500, 300);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(400, 300, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 270;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(100, 0);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }

        [Fact]
        public void getWorldObjectItem_ObjectInOpositeDirection_ObjectIsNoticed_carRotated315()
        {
            Radar radar = new Radar();
            radar.CarPreviousPosition = new Point(500, 300);
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(300, 100, "", 0, 0, new List<List<Point>>());
            World.Instance.ControlledCar.Angle = 270;

            radar.NoticedObjects = new List<NoticedObject>();
            NoticedObject nw = new NoticedObject();
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            nw.Vector = new Vector(100, -100);
            radar.NoticedObjects.Add(nw);

            List<NoticedObject> wos = radar.getDangerousWorldObjects();

            Assert.Equal(1, wos.Count);
        }
    }

    public class RadarTest_UpdateBus {
        VirtualFunctionBus vfb;
        Radar radar;

        public RadarTest_UpdateBus()
        {
            World.Instance.ControlledCar = new AutomatedCar.Models.AutomatedCar(300, 100, "", 0, 0, new List<List<Point>>());
            vfb = new VirtualFunctionBus();
            radar = new Radar(vfb);
            radar.NoticedObjects = new List<NoticedObject>();
        }

        private void AddNoticedObject(NoticedObject nw, Vector vector)
        {
            nw.Vector = vector;
            nw.worldObject = new AutomatedCar.Models.AutomatedCar(0,0,"",0,0,new List<List<Point>>());
            radar.NoticedObjects.Add(nw);
        }

        [Fact]
        public void UpdateBus_RadarShouldProvidePacket()
        {
            Assert.Same(vfb.RadarSensorPacket, radar.RadarSensorPacket);
        }

        [Fact]
        public void UpdateBus_RadarShouldUpdatePacket()
        {
            AddNoticedObject(new NoticedObject(), new Vector(100, -100));

            radar.updateBus();

            Assert.Equal(vfb.RadarSensorPacket.DangerousObjects.Count, 1);
        }
    }
}