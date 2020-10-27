using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Models
{
    public interface INPC : IMoveable
    {
        public int Rotation { get; set; }
        public int Speed { get; set; }
        public void Move();
    }
}
