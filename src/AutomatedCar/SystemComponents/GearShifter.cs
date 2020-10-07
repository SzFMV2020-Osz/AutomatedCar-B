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
        NotInDrive,
        DriveOne,
        DriveTwo,
        DriveThree,
        DriveFour,
        DriveFive,
        DriveSix
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
        private DriveGear[] driveGearLabels = { DriveGear.NotInDrive, DriveGear.DriveOne, DriveGear.DriveTwo, DriveGear.DriveThree, DriveGear.DriveFour, DriveGear.DriveFive, DriveGear.DriveSix };
        private double[] driveGearRatios = { 0, 2.66, 1.78, 1.3, 1, 0.74, 0.5 };
        private GearShifterPosition position;

        // Trackeli, valtozott-e a DriveGear az aktualis ciklusban
        public ChangeState DriveGearChangeState { get; private set; }

        // Az aktualis Drive-on beluli allas
        public Gear CurrentDriveGear { get; private set; }

        // A 7 (NotInDrive+6) lehetseges valtoallas Drive-on belul
        public Gear[] DriveGears { get; private set; }

        // Az aktualis valtoallas
        public GearShifterPosition Position
        {
            get
            {
                return this.position;
            }

            set
            {
                if (this.position != GearShifterPosition.D && value == GearShifterPosition.D)
                {
                    this.CurrentDriveGear = this.DriveGears[1];
                }

                this.position = value;
            }
        }

        // Konstruktor, a driveGearLabels es driveGearRatios alapjan feltolti a DriveGears tombot - ezek lesznek a Drive belso fokozatai
        public GearShifter()
        {
            this.Position = GearShifterPosition.N;
            this.DriveGears = new Gear[7];
            for (int i = 0; i < this.DriveGears.Length; i++)
            {
                this.DriveGears[i] = new Gear(this.driveGearRatios[i], this.driveGearLabels[i], i);
            }

            this.CurrentDriveGear = this.DriveGears[0];
            this.DriveGearChangeState = ChangeState.None;
        }

        // Beallitja a Drive-on beluli valtoallast, a tarolt RPM, es az RPM valtozas alapjan. Allitja az ehhez tartozo DriveGearChangeState-et is
        public void SetDriveGear(int currentRPM, int deltaRPM)
        {
            if (this.Position != GearShifterPosition.D)
            {
                this.CurrentDriveGear = this.DriveGears[0];
            }
            else
            {
                if (deltaRPM > 0 && (currentRPM + deltaRPM) > UpShiftRPMThreshold) { this.NextDriveGear(); }
                else if (deltaRPM < 0 && (currentRPM + deltaRPM) < DownShiftRPMThreshold) { this.PreviousDriveGear(); }
                else { this.DriveGearChangeState = ChangeState.None; }
            }
        }

        // Drive-on belul felfele valt egyet, ha lehet meg
        private void NextDriveGear()
        {
            if (this.CurrentDriveGear.SequenceNumber < 6)
            {
                this.CurrentDriveGear = this.DriveGears[this.CurrentDriveGear.SequenceNumber + 1];
                this.DriveGearChangeState = ChangeState.Upshift;
            }
            else
            {
                this.DriveGearChangeState = ChangeState.None;
            }
        }

        // Drive-on belul lefele valt egyet, ha lehet meg
        private void PreviousDriveGear()
        {
            if (this.CurrentDriveGear.SequenceNumber > 1)
            {
                this.CurrentDriveGear = this.DriveGears[this.CurrentDriveGear.SequenceNumber - 1];
                this.DriveGearChangeState = ChangeState.Downshift;
            }
            else
            {
                this.DriveGearChangeState = ChangeState.None;
            }
        }
    }
}
