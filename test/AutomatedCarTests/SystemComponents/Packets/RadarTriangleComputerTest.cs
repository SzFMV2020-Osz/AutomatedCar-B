using AutomatedCar.SystemComponents;
using AutomatedCar.SystemComponents.Packets;
using Avalonia.Layout;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System.ComponentModel;
using Xunit;
using AutomatedCar.Models.RadarUtil;
using System.Collections.Generic;
using Avalonia.Media;

namespace Tests.SystemComponents.Packets
{
    public class RadarTriangleComputerTest_fromCarCoordinates_to_WorldCoordinates
    {
        RadarTriangleComputer radar = new RadarTriangleComputer();

        [Fact]
        public void Radar_CarInOrigo_CarRotated0_radarViewDistance0_radarOffsetFromCarCenter0()
        {
            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(0,points[0].X);
            Assert.Equal(0,points[0].Y);
            Assert.Equal(0,points[1].X);
            Assert.Equal(0,points[1].Y);
            Assert.Equal(0,points[2].X);
            Assert.Equal(0,points[2].Y);
        }

        [Fact]
        public void Radar_CarInX20Y0_CarRotated0_radarViewDistance0_radarOffsetFromCarCenter0()
        {
            this.radar.carX = 20;
            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(20,points[0].X);
            Assert.Equal(0,points[0].Y);
            Assert.Equal(20,points[1].X);
            Assert.Equal(0,points[1].Y);
            Assert.Equal(20,points[2].X);
            Assert.Equal(0,points[2].Y);
        }

        [Fact]
        public void Radar_CarInX0Y20_CarRotated0_radarViewDistance0_radarOffsetFromCarCenter0()
        {
            this.radar.carY = 20;
            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(0,points[0].X);
            Assert.Equal(20,points[0].Y);
            Assert.Equal(0,points[1].X);
            Assert.Equal(20,points[1].Y);
            Assert.Equal(0,points[2].X);
            Assert.Equal(20,points[2].Y);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated90_radarViewDistance0_radarOffsetFromCarCenter0()
        {
            radar.rotate = 90;

            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(0,points[0].X);
            Assert.Equal(0,points[0].Y);
            Assert.Equal(0,points[1].X);
            Assert.Equal(0,points[1].Y);
            Assert.Equal(0,points[2].X);
            Assert.Equal(0,points[2].Y);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated0_radarViewDistance0_radarOffsetFromCarCenter120()
        {
            this.radar.offset = 120;

            var points = this.radar.computeTriangleInWorld(); 

            Assert.Equal(120, points[0].X);
            Assert.Equal(0, points[0].Y);
            Assert.Equal(120, points[1].X);
            Assert.Equal(0, points[1].Y);
            Assert.Equal(120, points[2].X);
            Assert.Equal(0, points[2].Y);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated90_radarViewDistance0_radarOffsetFromCarCenter120()
        {
            this.radar.offset = 120;
            this.radar.rotate = 90;

            var points = this.radar.computeTriangleInWorld(); 

            Assert.Equal(0, points[0].X);
            Assert.Equal(120, points[0].Y);
            Assert.Equal(0, points[1].X);
            Assert.Equal(120, points[1].Y);
            Assert.Equal(0, points[2].X);
            Assert.Equal(120, points[2].Y);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated180_radarViewDistance0_radarOffsetFromCarCenter120()
        {
            this.radar.offset = 120;
            this.radar.rotate = 180;
            
            var points = this.radar.computeTriangleInWorld(); 

            Assert.Equal(-120, points[0].X);
            Assert.Equal(0, points[0].Y);
            Assert.Equal(-120, points[1].X);
            Assert.Equal(0, points[1].Y);
            Assert.Equal(-120, points[2].X);
            Assert.Equal(0, points[2].Y);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated270_radarViewDistance0_radarOffsetFromCarCenter120()
        {
            this.radar.offset = 120;
            this.radar.rotate = 270;

            var points = this.radar.computeTriangleInWorld(); 

            Assert.Equal(0, points[0].X);
            Assert.Equal(-120, points[0].Y);
            Assert.Equal(0, points[1].X);
            Assert.Equal(-120, points[1].Y);
            Assert.Equal(0, points[2].X);
            Assert.Equal(-120, points[2].Y);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated0_radarViewDistance100_radarOffsetFromCarCenter0()
        {
            this.radar.distance = 100;

            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(0,points[0].Y);
            Assert.Equal(0,points[0].X);
            Assert.Equal(58,points[1].Y);
            Assert.Equal(100,points[1].X);
            Assert.Equal(-58,points[2].Y);
            Assert.Equal(100,points[2].X);
        }

        [Fact]
        public void Radar_CarInOrigo_CarRotated0_radarViewDistance100_radarOffsetFromCarCenter10()
        {
            this.radar.distance = 100;
            this.radar.offset = 10;

            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(0,points[0].Y);
            Assert.Equal(10,points[0].X);
            Assert.Equal(58,points[1].Y);
            Assert.Equal(110,points[1].X);
            Assert.Equal(-58,points[2].Y);
            Assert.Equal(110,points[2].X);
        }

        [Fact]
        public void Radar_other_values()
        {
            radar.carX = 200;
            radar.carY = 150;

            this.radar.distance = 200;
            this.radar.offset = 100;
            this.radar.rotate = 0;

            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(300,points[0].X);
            Assert.Equal(150,points[0].Y);
            Assert.Equal(500,points[1].X);
            Assert.Equal(265,points[1].Y);
            Assert.Equal(500,points[2].X);
            Assert.Equal(35,points[2].Y);
        }

        [Fact]
        public void Radar_other_values_90()
        {
            radar.carX = 200;
            radar.carY = 200;

            this.radar.distance = 200;
            this.radar.offset = 100;
            this.radar.rotate = 90;

            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(200,points[0].X);
            Assert.Equal(300,points[0].Y);
            Assert.Equal(200-115,points[1].X);
            Assert.Equal(500,points[1].Y);
            Assert.Equal(200+115,points[2].X);
            Assert.Equal(500,points[2].Y);
        }

        [Fact]
        public void Radar_other_values_180()
        {
            radar.carX = 400;
            radar.carY = 300;

            this.radar.distance = 100;
            this.radar.offset = 100;
            this.radar.rotate = 180;
            this.radar.angle = 20;

            var points = this.radar.computeTriangleInWorld();

            Assert.Equal(300,points[0].X);
            Assert.Equal(300,points[0].Y);
            Assert.Equal(200,points[1].X);
            Assert.Equal(300-36,points[1].Y);
            Assert.Equal(200,points[2].X);
            Assert.Equal(336,points[2].Y);
        }
    }
}