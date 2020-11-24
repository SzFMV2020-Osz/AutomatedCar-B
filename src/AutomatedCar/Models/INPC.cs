namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INPC : IMoveable
    {
        public void LoadNpcRoute(string filePath);
        public void SetStartPosition();
    }
}
