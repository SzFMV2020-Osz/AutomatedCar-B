namespace AutomatedCar.SystemComponents
{
    // A 4 alap valtoallas, amit inputkent kapunk a HMI-tol
    public enum GearShifterPosition
    {
        D,
        N,
        R,
        P
    }

    // A 6 Drive-on beluli belso valtoallas
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

    // A 3 lehetseges allapotvaltozas Drive-on beluli allas allapotanak trackelesere
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

        // Trackeli, valtozott-e a DriveGear az aktualis ciklusban
        public ChangeState DriveGearChangeState { get; private set; }

        // A 6 Drive-on beluli belso valtoallas
        public DriveGear DriveGear { get; private set; }

        // A 4 alap valtoallas, amit inputkent kapunk a HMI-tol. Ha D-re valt, alaphelyzetbe rakja a DriveGear-t
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

        // Beallitja a Drive-on beluli valtoallast, a tarolt RPM, es az RPM valtozas alapjan. Allitja az ehhez tartozo DriveGearChangeState-et is
        public void SetDriveGear(int currentRPM, int deltaRPM)
        {
            if (this.Position != GearShifterPosition.D)
            {
                this.DriveGear = DriveGear.NotInDrive;
            }
            else
            {
                if (deltaRPM > 0 && currentRPM > UpShiftRPMThreshold) { this.NextDriveGear(); }
                else if (deltaRPM < 0 && currentRPM < DownShiftRPMThreshold) { this.PreviousDriveGear(); }
                else { this.DriveGearChangeState = ChangeState.None; }
            }
        }

        // Drive-on belul felfele valt 1et, ha lehet meg
        private void NextDriveGear()
        {
            switch (this.DriveGear)
            {
                case DriveGear.One:
                    this.DriveGear = DriveGear.Two;
                    this.DriveGearChangeState = ChangeState.Upshift;
                    break;
                case DriveGear.Two:
                    this.DriveGear = DriveGear.Three;
                    this.DriveGearChangeState = ChangeState.Upshift;
                    break;
                case DriveGear.Three:
                    this.DriveGear = DriveGear.Four;
                    this.DriveGearChangeState = ChangeState.Upshift;
                    break;
                case DriveGear.Four:
                    this.DriveGear = DriveGear.Five;
                    this.DriveGearChangeState = ChangeState.Upshift;
                    break;
                case DriveGear.Five:
                    this.DriveGear = DriveGear.Six;
                    this.DriveGearChangeState = ChangeState.Upshift;
                    break;
                default:
                    this.DriveGearChangeState = ChangeState.None;
                    break;
            }
        }

        // Drive-on belul lefele valt 1et, ha lehet meg
        private void PreviousDriveGear()
        {
            switch (this.DriveGear)
            {
                case DriveGear.Two:
                    this.DriveGear = DriveGear.One;
                    this.DriveGearChangeState = ChangeState.Downshift;
                    break;
                case DriveGear.Three:
                    this.DriveGear = DriveGear.Two;
                    this.DriveGearChangeState = ChangeState.Downshift;
                    break;
                case DriveGear.Four:
                    this.DriveGear = DriveGear.Three;
                    this.DriveGearChangeState = ChangeState.Downshift;
                    break;
                case DriveGear.Five:
                    this.DriveGear = DriveGear.Four;
                    this.DriveGearChangeState = ChangeState.Downshift;
                    break;
                case DriveGear.Six:
                    this.DriveGear = DriveGear.Five;
                    this.DriveGearChangeState = ChangeState.Downshift;
                    break;
                default:
                    this.DriveGearChangeState = ChangeState.None;
                    break;
            }
        }
    }
}
