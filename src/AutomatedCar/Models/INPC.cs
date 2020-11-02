namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INPC : IMoveable
    {
        public int Rotation { get; set; }

        public int Speed { get; set; }
    }
}
