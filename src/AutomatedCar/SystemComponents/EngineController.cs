namespace AutomatedCar.SystemComponents
{
    using SystemComponents.Packets;

    public class EngineController
    {
        private const double GearRatioReverse = 2.9; // Az egyik linkelt pelda atteteibol nezve, konzisztens a tobbi attettel
        private const int RPMDecayPerTick = -500; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg
        private const double MinimumBrakeForce = 500; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg
        private const int ForceToPixelVelocityConversionConstant = 5; // Nagyjabol 10 autohossz/sec-re akartam maximalizalni a sebesseget (~162 km/h 4,5m hosszu autonal), ez a szam azt kozeliti (230px az auto, MaxRPM miatt max ResultantForce 11500)
        private const int MaxRPM = 6000; // Nagyjabol realisztikus maximum, de ez is valtozhat, ha szukseges
        private const int NeutralRPMIncrease = 500; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg
        private const int BrakePedalScaling = 100; // Ugy valasztottam, hogy a max fekezesi ero kozelitse a max eloremeneti erot (jelenleg eloremenetMax: 11500, fekMax: 10500). Valtozhat meg
        private const int GasPedalScaling = 1; // Egyelore tetszolegesen eldontott ertek - meg valtozik valoszinuleg

        public int RPM { get; private set; }

        public GearShifter GearShifter { get; private set; }

        public double VelocityPixelsPerSecond { get; private set; }

        private IPowerTrainPacket PowerTrainPacket { get; set; }

        public EngineController()
        {
            this.GearShifter = new GearShifter();
            this.RPM = 0;
            this.VelocityPixelsPerSecond = 0;
        }

        public void UpdateEngineProperties(IPowerTrainPacket packet)
        {
            this.PowerTrainPacket = packet;
            this.GearShifter.Position = this.PowerTrainPacket.GearShifterPosition;
            this.GearShifter.SetDriveGear(this.RPM, this.CalculateRPMChange());
            this.SetRPM();
            this.SetVelocityInPixels();
        }

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

            this.RPM = ((int)tempRPM < MaxRPM) ? (int)tempRPM : MaxRPM;
        }

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

        private int CalculateRPMChange() =>
            this.PowerTrainPacket.GasPedal != 0 ? this.PowerTrainPacket.GasPedal * GasPedalScaling : RPMDecayPerTick;

        private void SetVelocityInPixels() =>
            this.VelocityPixelsPerSecond = this.CalculateResultantForce() / ForceToPixelVelocityConversionConstant;

        private double CalculateResultantForce() =>
            (this.CalculateDriveForce() - this.CalculateBrakeForce() > 0) ? this.CalculateDriveForce() - this.CalculateBrakeForce() : 0;

        private double CalculateBrakeForce() =>
            MinimumBrakeForce + (this.PowerTrainPacket.BrakePedal * BrakePedalScaling);

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
