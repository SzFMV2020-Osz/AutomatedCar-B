namespace AutomatedCar.SystemComponents
{
    using System.Net.Http.Headers;
    using AutomatedCar.SystemComponents.Packets;

    public class HumanMachineInterface : SystemComponent, IHumanMachineInterface
    {
        private HMIPacket hmiPacket;
        private DebugPacket debugPacket;

        #region Variables
        private bool gaspedalDown;
        private bool breakpedalDown;
        private bool steeringRight;
        private bool steeringLeft;
        private bool geerUp;
        private bool geerDown;
        private bool aCC;
        private bool accDistance;
        private bool accSpeedPus;
        private bool accSpeedMinus;
        private bool laneKeeping;
        private bool parkingPilot;
        private bool turnSignalRight;
        private bool turnSignalLeft;
        private bool utrasoundDebug;
        private bool radarDebug;
        private bool cameraDebug;
        #endregion

        public HumanMachineInterface(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            virtualFunctionBus.RegisterComponent(this);

            this.hmiPacket = new HMIPacket();
            this.debugPacket = new DebugPacket();
            virtualFunctionBus.HMIPacket = this.hmiPacket;
            virtualFunctionBus.DebugPacket = this.debugPacket;
        }

        #region Properties
        public bool GaspedalDown { get => this.gaspedalDown; set => this.gaspedalDown = value; }

        public bool BreakpedalDown { get => this.breakpedalDown; set => this.breakpedalDown = value; }

        public bool SteeringRight { get => this.steeringRight; set => this.steeringRight = value; }

        public bool SteeringLeft { get => this.steeringLeft; set => this.steeringLeft = value; }

        public bool GeerUp { get => this.geerUp; set => this.geerUp = value; }

        public bool GeerDown { get => this.geerDown; set => this.geerDown = value; }

        public bool ACC { get => this.aCC; set => this.aCC = value; }

        public bool ACCDistance { get => this.accDistance; set => this.accDistance = value; }

        public bool ACCSpeedPus { get => this.accSpeedPus; set => this.accSpeedPus = value; }

        public bool ACCSpeedMinus { get => this.accSpeedMinus; set => this.accSpeedMinus = value; }

        public bool LaneKeeping { get => this.laneKeeping; set => this.laneKeeping = value; }

        public bool ParkingPilot { get => this.parkingPilot; set => this.parkingPilot = value; }

        public bool TurnSignalRight { get => this.turnSignalRight; set => this.turnSignalRight = value; }

        public bool TurnSignalLeft { get => this.turnSignalLeft; set => this.turnSignalLeft = value; }

        public bool UtrasoundDebug { get => this.utrasoundDebug; set => this.utrasoundDebug = value; }

        public bool RadarDebug { get => this.radarDebug; set => this.radarDebug = value; }

        public bool CameraDebug { get => this.cameraDebug; set => this.cameraDebug = value; }

        HMIPacket IHumanMachineInterface.hmiPacket { get => this.hmiPacket; }

        DebugPacket IHumanMachineInterface.debugPacket { get => this.debugPacket; }
        #endregion

        public override void Process()
        {
        }
    }
}
