namespace AutomatedCar.SystemComponents
{
    using SystemComponents.Packets;

    class EngineController
    {
        private const double GearRatioReverse = 2.9; // Az egyik linkelt pelda atteteibol nezve, konzisztens a tobbi attettel
        private const int RPMDecayPerTick = -500; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg
        private const double MinimumBrakeForce = 500; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg
        private const int ForceToPixelVelocityConversionConstant = 5; // Nagyjabol 10 autohossz/sec-re akartam maximalizalni a sebesseget (~162 km/h 4,5m hosszu autonal), ez a szam azt kozeliti (230px az auto, MaxRPM miatt max ResultantForce 11500)
        private const int MaxRPM = 6000; // Nagyjabol realisztikus maximum, de ez is valtozhat, ha szukseges
        private const int NeutralRPMIncrease = 500; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg
        private const int BrakePedalScaling = 100; // Ugy valasztottam, hogy a max fekezesi ero kozelitse a max eloremeneti erot (jelenleg eloremenetMax: 11500, fekMax: 10500). Valtozhat meg
        private const int GasPedalScaling = 1; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg

        // RPM erteke
        public int RPM { get; private set; }

        // Automata sebvaltot megvalosito osztaly
        public GearShifter GearShifter { get; private set; }

        // Gyorsulas erteke (px/tick)
        public double VelocityInPixels { get; private set; }

        // Packet, tartalmazza a HMI-tol erkezo inputot
        private IPowerTrainPacket PowerTrainPacket { get; set; }

        // Konstruktor, jovoben a packet-et majd az ot tartalmazo PowerTrain osztalytol kapja meg.
        public EngineController(IPowerTrainPacket packet)
        {
            this.PowerTrainPacket = packet;
            this.GearShifter = new GearShifter();
        }

        public EngineController()
        {
            this.GearShifter = new GearShifter();
        }

        // Ez a metodus frissiti az osztaly belso ertekeit: Valtoallas, azon belul DriveGear, RPM, es sebesseg
        public void UpdateEngineProperties(IPowerTrainPacket packet)
        {
            this.PowerTrainPacket = packet;
            this.GearShifter.Position = this.PowerTrainPacket.GearShifterPosition;
            this.GearShifter.SetDriveGear(this.RPM, this.CalculateRPMChange());
            this.SetRPM();
            this.SetVelocityInPixels();
        }

        // Beallitja az RPM erteket, valto- es gazpedal allasnak megfeleloen.:
        // Drive: korrigalja az RPM-et, ha DriveGear valtas tortent
        // Neutral: "porgeti" a motort, egy relative nagy konstans ertekkel
        // Reverse: Semmi - feltetelezzuk, hogy nem lehet eloremenet kozben Reverse-be valtani, igy nincs szukseg ujraszamolni attet alapjan
        // Park: Nullazza az RPM-et - doku alapjan ugy vesszuk, hogy a Park egyenlo a kezifek behuzasaval is
        private void SetRPM()
        {
            double tempRPM = this.RPM + this.CalculateRPMChange();
            switch (this.GearShifter.Position)
            {
                case GearShifterPosition.D:
                    tempRPM *= this.AdjustRPMOnGearChange();
                    break;
                case GearShifterPosition.N:
                    tempRPM += NeutralRPMIncrease;
                    break;
                case GearShifterPosition.R:
                    break;
                case GearShifterPosition.P:
                    tempRPM = 0;
                    break;
            }

            this.RPM = ((int)tempRPM > MaxRPM) ? (int)tempRPM : MaxRPM;
        }

        // Egy szorzot ad vissza, ami DriveGear-ek kozotti valtasnal korrigalja az RPM-et az uj attetnek megfeleloen, hogy az eloremeneti ero ne valtozzon
        private double AdjustRPMOnGearChange()
        {
            switch (this.GearShifter.DriveGearChangeState)
            {
                case ChangeState.Upshift:
                    return this.GearShifter.CurrentDriveGear.GearRatio / this.GearShifter.DriveGears[this.GearShifter.CurrentDriveGear.SequenceNumber - 1].GearRatio;
                case ChangeState.Downshift:
                    return this.GearShifter.CurrentDriveGear.GearRatio / this.GearShifter.DriveGears[this.GearShifter.CurrentDriveGear.SequenceNumber + 1].GearRatio;
                default:
                    return 1;
            }
        }

        // RPM a pedallallassal aranyosan (0-100) valtozik tickenkent, felengedett pedal eseten pedig RPMDecayPerTick ertekkel csokken
        private int CalculateRPMChange() =>
            this.PowerTrainPacket.GasPedal != 0 ? this.PowerTrainPacket.GasPedal * GasPedalScaling : RPMDecayPerTick;

        // A kiszamolt eredo erot pixel gyorsulas ertekre forditja
        private void SetVelocityInPixels() =>
            this.VelocityInPixels = this.CalculateResultantForce() / ForceToPixelVelocityConversionConstant;

        // Eredo ero kiszamitasa, ami nem mehet 0 ala 
        private double CalculateResultantForce() =>
            (this.CalculateDriveForce() - this.CalculateBrakeForce() > 0) ? this.CalculateDriveForce() - this.CalculateBrakeForce() : 0;

        // Fekezesi ero kiszamitasa, plusz egy konstans minimalis ertek
        private double CalculateBrakeForce() =>
            MinimumBrakeForce + (this.PowerTrainPacket.BrakePedal * BrakePedalScaling);

        // Eloremeneti ero kiszamitasa, valtoallastol fuggoen. Irannyal itt nem foglalkozunk, csak nagysaggal
        private double CalculateDriveForce()
        {
            switch (this.GearShifter.Position)
            {
                case GearShifterPosition.D:
                    return this.RPM / this.GearShifter.CurrentDriveGear.GearRatio;
                case GearShifterPosition.R:
                    return this.RPM / GearRatioReverse;
                default:
                    return 0;
            }
        }
    }
}
