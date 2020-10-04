namespace AutomatedCar.SystemComponents
{
    public enum GearShifterPosition
    {
        D,
        N,
        R,
        P
    }

    public enum DriveGear
    {
        NotInDrive = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6
    }

    public enum ChangeState
    {
        Upshift,
        Downshift,
        None
    }

    public class GearShifter
    {
        private const int UpShiftRPMThreshold = 3000;
        private const int DownShiftRPMThreshold = 1000;

        public ChangeState State { get; private set; }

        public DriveGear DriveGear { get; private set; }

        public GearShifterPosition Position
        {
            get
            {
                return this.Position;
            }

            set
            {
                if (this.Position != GearShifterPosition.D && value == GearShifterPosition.D)
                {
                    this.DriveGear = DriveGear.One;
                }

                this.Position = value;
            }
        }

        public void SetDriveGear(int currentRPM, int deltaRPM)
        {
            if (this.Position != GearShifterPosition.D)
            {
                this.DriveGear = DriveGear.NotInDrive;
            }
            else
            {
                if (deltaRPM > 0 && currentRPM > UpShiftRPMThreshold)
                {
                    this.NextDriveGear();
                }
                else if (deltaRPM < 0 && currentRPM < DownShiftRPMThreshold) { this.PreviousDriveGear(); }
            }
        }

        private void NextDriveGear()
        {
            switch (this.DriveGear)
            {
                case DriveGear.One:
                    this.DriveGear = DriveGear.Two;
                    this.State = ChangeState.Upshift;
                    break;
                case DriveGear.Two:
                    this.DriveGear = DriveGear.Three;
                    this.State = ChangeState.Upshift;
                    break;
                case DriveGear.Three:
                    this.DriveGear = DriveGear.Four;
                    this.State = ChangeState.Upshift;
                    break;
                case DriveGear.Four:
                    this.DriveGear = DriveGear.Five;
                    this.State = ChangeState.Upshift;
                    break;
                case DriveGear.Five:
                    this.DriveGear = DriveGear.Six;
                    this.State = ChangeState.Upshift;
                    break;
                default:
                    this.State = ChangeState.None;
                    break;
            }
        }

        private void PreviousDriveGear()
        {
            switch (this.DriveGear)
            {
                case DriveGear.Two:
                    this.DriveGear = DriveGear.One;
                    this.State = ChangeState.Downshift;
                    break;
                case DriveGear.Three:
                    this.DriveGear = DriveGear.Two;
                    this.State = ChangeState.Downshift;
                    break;
                case DriveGear.Four:
                    this.DriveGear = DriveGear.Three;
                    this.State = ChangeState.Downshift;
                    break;
                case DriveGear.Five:
                    this.DriveGear = DriveGear.Four;
                    this.State = ChangeState.Downshift;
                    break;
                case DriveGear.Six:
                    this.DriveGear = DriveGear.Five;
                    this.State = ChangeState.Downshift;
                    break;
                default:
                    this.State = ChangeState.None;
                    break;
            }
        }
    }
}
