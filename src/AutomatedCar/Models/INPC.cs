using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.Models
{
    public interface INPC
    {
        public int Rotation { get; set; }
        public int Speed { get; set; }
        public void Move();
    }
}
