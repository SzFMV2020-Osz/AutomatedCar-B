using AutomatedCar.SystemComponents;
using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;

using System.Text;

namespace AutomatedCar.Models
{
    public class PlayerCar : AutomatedCar
    {

     

        public PlayerCar(int x, int y, string filename)
            : base(x, y, filename)
        {


        }

        
        public SolidColorBrush RadarBrush { get; set; }
        public Geometry RadarGeometry { get; set; }
    }
}
