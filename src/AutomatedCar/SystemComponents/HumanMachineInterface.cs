namespace AutomatedCar.SystemComponents
{
    using System.Net.Http.Headers;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Input;

    public class HumanMachineInterface : SystemComponent, IHumanMachineInterface
    {
        private HMIPacket hmiPacket;
        private DebugPacket debugPacket;

        #region Variables
        private bool gaspedalDown;
        private bool breakpedalDown;
        private bool steeringRight;
        private bool steeringLeft;
        private bool gearUp;
        private bool gearDown;
        private bool acc;
        private bool accDistance;
        private bool accSpeedPlus;
        private bool accSpeedMinus;
        private bool laneKeeping;
        private bool parkingPilot;
        private bool turnSignalRight;
        private bool turnSignalLeft;
        private bool polygonDebug;
        private bool ultrasoundDebug;
        private bool radarDebug;
        private bool cameraDebug;
        #endregion

        public HumanMachineInterface(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
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

        public bool GearUp { get => this.gearUp; set => this.gearUp = value; }

        public bool GearDown { get => this.gearDown; set => this.gearDown = value; }

        public bool Acc { get => this.acc; set => this.acc = value; }

        public bool AccDistance { get => this.accDistance; set => this.accDistance = value; }

        public bool AccSpeedPlus { get => this.accSpeedPlus; set => this.accSpeedPlus = value; }

        public bool AccSpeedMinus { get => this.accSpeedMinus; set => this.accSpeedMinus = value; }

        public bool LaneKeeping { get => this.laneKeeping; set => this.laneKeeping = value; }

        public bool ParkingPilot { get => this.parkingPilot; set => this.parkingPilot = value; }

        public bool TurnSignalRight { get => this.turnSignalRight; set => this.turnSignalRight = value; }

        public bool TurnSignalLeft { get => this.turnSignalLeft; set => this.turnSignalLeft = value; }

        public bool PolygonDebug { get => this.polygonDebug; set => this.polygonDebug = value; }

        public bool UltrasoundDebug { get => this.ultrasoundDebug; set => this.ultrasoundDebug = value; }

        public bool RadarDebug { get => this.radarDebug; set => this.radarDebug = value; }

        public bool CameraDebug { get => this.cameraDebug; set => this.cameraDebug = value; }

        HMIPacket IHumanMachineInterface.hmiPacket { get => this.hmiPacket; }

        DebugPacket IHumanMachineInterface.debugPacket { get => this.debugPacket; }

        #endregion

        public override void Process()
        {
            this.hmiPacket.GearCalculate(this.gearUp, this.gearDown);
            this.hmiPacket.AccSet(this.acc);
            this.hmiPacket.AccDistanceSet(this.accDistance);
            this.hmiPacket.AccSpeedSet(this.accSpeedPlus, this.accSpeedMinus);
            this.hmiPacket.HandleGasPedal(this.gaspedalDown);
            this.hmiPacket.HandleBrakePedal(this.breakpedalDown);
            this.hmiPacket.HandleSteering(this.steeringRight, this.steeringLeft);
            this.hmiPacket.TurnSignalLeftSet(this.turnSignalLeft);
            this.hmiPacket.LaneKeepingSet(this.turnSignalRight);
            this.hmiPacket.ParkingPilotSet(this.parkingPilot);
            this.hmiPacket.TurnSignalRightSet(this.turnSignalRight);
            this.debugPacket.BoardCameraSet(this.cameraDebug);
            this.debugPacket.RadarSensorSet(this.radarDebug);
            this.debugPacket.UtrasoundSensorSet(this.ultrasoundDebug);
            this.debugPacket.PolygonSet(this.ultrasoundDebug);

            this.virtualFunctionBus.HMIPacket = this.hmiPacket;
            this.virtualFunctionBus.DebugPacket = this.debugPacket;

        }

        #region InputHandler
        public void InputHandler()
        {
            this.NormalInput(ref this.gaspedalDown, Key.Up);
            this.NormalInput(ref this.breakpedalDown, Key.Down);
            this.NormalInput(ref this.steeringLeft, Key.Left);
            this.NormalInput(ref this.steeringRight, Key.Right);
            this.PressInput(ref this.accSpeedPlus, Key.OemPlus);
            this.PressInput(ref this.accSpeedMinus, Key.OemMinus);
            this.PressInput(ref this.accDistance, Key.T);
            this.PressInput(ref this.gearUp, Key.W);
            this.PressInput(ref this.gearDown, Key.S);
            this.ToggleInput(ref this.turnSignalRight, Key.E);
            this.ToggleInput(ref this.turnSignalLeft, Key.Q);
            this.ToggleInput(ref this.acc, Key.A);
            this.ToggleInput(ref this.laneKeeping, Key.D);
            this.ToggleInput(ref this.parkingPilot, Key.R);
            this.ToggleInput(ref this.polygonDebug, Key.F);
            this.ToggleInput(ref this.ultrasoundDebug, Key.U);
            this.ToggleInput(ref this.cameraDebug, Key.Z);
            this.ToggleInput(ref this.radarDebug, Key.I);
        }

        public void NormalInput(ref bool variable, Key key)
        {
            if (Keyboard.IsKeyDown(key))
            {
                variable = true;
            }
            else
            {
                variable = false;
            }
        }

        public void PressInput(ref bool variable, Key key)
        {
            if (!variable)
            {
                if (Keyboard.IsPressableKeysDown(key) && Keyboard.IsKeyDown(key))
                {
                    variable = true;
                    Keyboard.PressableKeys.Remove(key);
                }
            }
            else
            {
                variable = false;
            }
        }

        public void ToggleInput(ref bool variable, Key key)
        {
            if (Keyboard.IsToggleableKeyDown(key))
            {
                variable = !variable;
                Keyboard.DeleteToggleableKey(key);
            }
        }

        #region replacesMethods
        //public void GaspedalKey()
        //{
        //    if (Keyboard.IsKeyDown(Key.Up))
        //    {
        //        this.gaspedalDown = true;
        //    }
        //    else
        //    {
        //        this.gaspedalDown = false;
        //    }
        //}
        //public void BreakpedalKey()
        //{
        //    if (Keyboard.IsKeyDown(Key.Down))
        //    {
        //        this.breakpedalDown = true;
        //    }
        //    else
        //    {
        //        this.breakpedalDown = false;
        //    }
        //}
        //public void SteerRightKey()
        //{
        //    if (Keyboard.IsKeyDown(Key.Right))
        //    {
        //        this.steeringRight = true;
        //    }
        //    else
        //    {
        //        this.steeringRight = false;
        //    }
        //}
        //public void SteerLeftKey()
        //{
        //    if (Keyboard.IsKeyDown(Key.Left))
        //    {
        //        this.steeringLeft = true;
        //    }
        //    else
        //    {
        //        this.steeringLeft = false;
        //    }
        //}
        //public void AccSpeedPlusKey()
        //{
        //    if (!this.accSpeedPlus)
        //    {
        //        if (Keyboard.IsPressableKeysDown(Key.OemPlus) && Keyboard.IsKeyDown(Key.OemPlus))
        //        {
        //            this.accSpeedPlus = true;
        //            Keyboard.PressableKeys.Remove(Key.OemPlus);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.OemPlus) && Keyboard.IsKeyDown(Key.OemPlus))
        //        {
        //            this.accSpeedPlus = false;
        //        }
        //    }
        //}
        //public void AccSpeedMinusKey()
        //{
        //    if (!this.accSpeedMinus)
        //    {
        //        if (Keyboard.IsPressableKeysDown(Key.OemMinus) && Keyboard.IsKeyDown(Key.OemMinus))
        //        {
        //            this.accSpeedMinus = true;
        //            Keyboard.PressableKeys.Remove(Key.OemMinus);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.OemMinus) && Keyboard.IsKeyDown(Key.OemMinus))
        //        {
        //            this.accSpeedMinus = false;
        //        }
        //    }
        //}
        //public void AccDistanceKey()
        //{
        //    if (!this.accDistance)
        //    {
        //        if (Keyboard.IsPressableKeysDown(Key.T) && Keyboard.IsKeyDown(Key.T))
        //        {
        //            this.accDistance = true;
        //            Keyboard.PressableKeys.Remove(Key.T);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.T) && Keyboard.IsKeyDown(Key.T))
        //        {
        //            this.accDistance = false;
        //        }
        //    }
        //}
        //public void TurnSignalRightKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.E))
        //    {
        //        this.turnSignalRight = !this.turnSignalRight;
        //        Keyboard.DeleteToggleableKey(Key.E);
        //    }
        //}
        //public void TurnSignalLeftKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.Q))
        //    {
        //        this.turnSignalLeft = !this.turnSignalLeft;
        //        Keyboard.DeleteToggleableKey(Key.Q);
        //    }
        //}
        //public void GearUpKey()
        //{
        //    if (!this.gearUp)
        //    {
        //        if (Keyboard.IsPressableKeysDown(Key.W) && Keyboard.IsKeyDown(Key.W))
        //        {
        //            this.gearUp = true;
        //            Keyboard.PressableKeys.Remove(Key.W);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.W) && Keyboard.IsKeyDown(Key.W))
        //        {
        //            this.gearUp = false;
        //        }
        //    }
        //}
        //public void GearDownKey()
        //{
        //    if (!this.gearDown)
        //    {
        //        if (Keyboard.IsPressableKeysDown(Key.S) && Keyboard.IsKeyDown(Key.S))
        //        {
        //            this.gearDown = true;
        //            Keyboard.PressableKeys.Remove(Key.W);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.S) && Keyboard.IsKeyDown(Key.S))
        //        {
        //            this.gearDown = false;
        //        }
        //    }
        //}
        //public void AccKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.A))
        //    {
        //        this.acc = !this.acc;
        //        Keyboard.DeleteToggleableKey(Key.A);
        //    }
        //}
        //public void LaneKeepingKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.D))
        //    {
        //        this.laneKeeping = !this.laneKeeping;
        //        Keyboard.DeleteToggleableKey(Key.D);
        //    }
        //}
        //public void ParkingPilotKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.R))
        //    {
        //        this.parkingPilot = !this.parkingPilot;
        //        Keyboard.DeleteToggleableKey(Key.R);
        //    }
        //}
        //public void UltrasoundDebugKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.U))
        //    {
        //        this.ultrasoundDebug = !this.ultrasoundDebug;
        //        Keyboard.DeleteToggleableKey(Key.U);
        //    }
        //}
        //public void BoardCameraDebugKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.Z))
        //    {
        //        this.cameraDebug = !this.cameraDebug;
        //        Keyboard.DeleteToggleableKey(Key.Z);
        //    }
        //}
        //public void RadarDebugKey()
        //{
        //    if (Keyboard.IsToggleableKeyDown(Key.I))
        //    {
        //        this.radarDebug = !this.radarDebug;
        //        Keyboard.DeleteToggleableKey(Key.I);
        //    }
        //} 
        #endregion
        #endregion
    }
}