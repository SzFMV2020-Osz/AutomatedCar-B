using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedCar.SystemComponents
{
    public class Gear
    {
        public double GearRatio { get; private set; }
        public DriveGear Label { get; private set; }
        public int SequenceNumber { get; private set; }

        public Gear(double ratio, DriveGear label, int sequenceNumber)
        {
            this.GearRatio = ratio;
            this.Label = label;
            this.SequenceNumber = sequenceNumber;
        }
    }
}
