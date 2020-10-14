namespace AutomatedCar.SystemComponents
{
    using System.Net.Http.Headers;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Input;

    public class HumanMachineInterface : SystemComponent, IHumanMachineInterface
    {
        private HMIPacket hmiPacket;
        private DebugPacket debugPacket;

        public HumanMachineInterface(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.hmiPacket = new HMIPacket();
            this.debugPacket = new DebugPacket();
            virtualFunctionBus.HMIPacket = this.hmiPacket;
            virtualFunctionBus.DebugPacket = this.debugPacket;
        }

        #region Properties
        public bool GaspedalDown { get; private set; }

        public bool BreakpedalDown { get; private set; }

        public bool SteeringRight { get; private set; }

        public bool SteeringLeft { get; private set; }

        public bool GearUp { get; private set; }

        public bool GearDown { get; private set; }

        public bool Acc { get; private set; }

        public bool AccDistance { get; private set; }

        public bool AccSpeedPlus { get; private set; }

        public bool AccSpeedMinus { get; private set; }

        public bool LaneKeeping { get; private set; }

        public bool ParkingPilot { get; private set; }

        public bool TurnSignalRight { get; private set; }

        public bool TurnSignalLeft { get; private set; }

        public bool UltrasoundDebug { get; private set; }

        public bool RadarDebug { get; private set; }

        public bool CameraDebug { get; private set; }

        public bool PolygonDebug { get; private set; }

        public HMIPacket HmiPacket { get => this.hmiPacket; }

        public DebugPacket DebugPacket { get => this.debugPacket; }
        #endregion

        public override void Process()
        {
            this.GearCalculate(this.GearUp, this.GearDown);
            this.AccSet(this.Acc);
            this.AccDistanceSet(this.AccDistance);
            this.AccSpeedSet(this.AccSpeedPlus, this.AccSpeedMinus);
            this.HandleGasPedal(this.GaspedalDown);
            this.HandleBrakePedal(this.BreakpedalDown);
            this.HandleSteering(this.SteeringRight, this.SteeringLeft);
            this.TurnSignalLeftSet(this.TurnSignalLeft);
            this.LaneKeepingSet(this.LaneKeeping);
            this.ParkingPilotSet(this.ParkingPilot);
            this.TurnSignalRightSet(this.TurnSignalRight);
            this.BoardCameraSet(this.CameraDebug);
            this.RadarSensorSet(this.RadarDebug);
            this.UtrasoundSensorSet(this.UltrasoundDebug);
            this.PolygonDebugSet(this.PolygonDebug);

            this.virtualFunctionBus.HMIPacket = this.hmiPacket;
            this.virtualFunctionBus.DebugPacket = this.debugPacket;
        }

        #region InputHandler
        public void InputHandler()
        {
            this.GaspedalDown = this.NormalInput(Key.Up);
            this.BreakpedalDown = this.NormalInput(Key.Down);
            this.SteeringLeft = this.NormalInput(Key.Left);
            this.SteeringRight = this.NormalInput(Key.Right);
            this.AccSpeedPlus = this.PressInput(this.AccSpeedPlus, Key.Add);
            this.AccSpeedMinus = this.PressInput(this.AccSpeedMinus, Key.Subtract);
            this.AccDistance = this.PressInput(this.AccDistance, Key.T);
            this.GearUp = this.PressInput(this.GearUp, Key.W);
            this.GearDown = this.PressInput(this.GearDown, Key.S);
            if (this.ToggleInput(Key.E))
            {
                this.TurnSignalRight = !this.TurnSignalRight;
            }

            if (this.ToggleInput(Key.Q))
            {
                this.TurnSignalLeft = !this.TurnSignalLeft;
            }

            if (this.ToggleInput(Key.A))
            {
                this.Acc = !this.Acc;
            }

            if (this.ToggleInput(Key.D))
            {
                this.LaneKeeping = !this.LaneKeeping;
            }

            if (this.ToggleInput(Key.R))
            {
                this.ParkingPilot = !this.ParkingPilot;
            }

            if (this.ToggleInput(Key.U))
            {
                this.UltrasoundDebug = !this.UltrasoundDebug;
            }

            if (this.ToggleInput(Key.Z))
            {
                this.CameraDebug = !this.CameraDebug;
            }

            if (this.ToggleInput(Key.I))
            {
                this.RadarDebug = !this.RadarDebug;
            }

            if (this.ToggleInput(Key.F))
            {
                this.PolygonDebug = !this.PolygonDebug;
            }
        }

        private bool NormalInput(Key key) => Keyboard.IsKeyDown(key);

        private bool PressInput(bool pressInput, Key key)
        {
            if (!pressInput)
            {
                if (Keyboard.IsPressableKeysDown(key))
                {
                    Keyboard.DeletePressableKeys(key);
                    return true;
                }
            }

            return false;
        }

        private bool ToggleInput(Key key) => Keyboard.IsToggleableKeyDown(key) && Keyboard.DeleteToggleableKey(key);

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
        //        if (Keyboard.IsPressableKeysDown(Key.Add) && Keyboard.IsKeyDown(Key.Add))
        //        {
        //            this.accSpeedPlus = true;
        //            Keyboard.PressableKeys.Remove(Key.Add);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.Add) && Keyboard.IsKeyDown(Key.Add))
        //        {
        //            this.accSpeedPlus = false;
        //        }
        //    }
        //}
        //public void AccSpeedMinusKey()
        //{
        //    if (!this.accSpeedMinus)
        //    {
        //        if (Keyboard.IsPressableKeysDown(Key.Subtract) && Keyboard.IsKeyDown(Key.Subtract))
        //        {
        //            this.accSpeedMinus = true;
        //            Keyboard.PressableKeys.Remove(Key.Subtract);
        //        }
        //    }
        //    else
        //    {
        //        if (!Keyboard.IsPressableKeysDown(Key.Subtract) && Keyboard.IsKeyDown(Key.Subtract))
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

        #region InputProcess
        public void GearCalculate(bool up, bool down)
        {
            if (!up & down)
            {
                this.DownShift();
            }
            else if (up & !down)
            {
                this.UpShift();
            }
        }

        void UpShift()
        {
            switch (this.hmiPacket.Gear)
            {
                case Gears.P:
                    this.hmiPacket.Gear = Gears.R;
                    break;
                case Gears.N:
                    this.hmiPacket.Gear = Gears.D;
                    break;
                case Gears.R:
                    this.hmiPacket.Gear = Gears.N;
                    break;
                default:
                    break;
            }
        }

        void DownShift()
        {
            switch (this.hmiPacket.Gear)
            {
                case Gears.D:
                    this.hmiPacket.Gear = Gears.N;
                    break;
                case Gears.N:
                    this.hmiPacket.Gear = Gears.R;
                    break;
                case Gears.R:
                    this.hmiPacket.Gear = Gears.P;
                    break;
                default:
                    break;
            }
        }

        public void AccSpeedSet(bool plus, bool minus)
        {
            if (plus & !minus)
            {
                this.AccSpeedIncrease();
            }
            else if (!plus & minus)
            {
                this.AccSpeedDecrease();
            }
        }

        void AccSpeedDecrease()
        {
            if (this.hmiPacket.AccSpeed != 30)
            {
                this.hmiPacket.AccSpeed -= 10;
            }
        }

        void AccSpeedIncrease()
        {
            if (this.hmiPacket.AccSpeed != 160)
            {
                this.hmiPacket.AccSpeed += 10;
            }
        }

        public void AccDistanceSet(bool change)
        {
            if (change)
            {
                if (this.hmiPacket.AccDistance == 1.4)
                {
                    this.AccDistanceReset();
                }
                else
                {
                    this.AccDistanceIncrease();
                }
            }
        }

        void AccDistanceReset()
        {
            this.hmiPacket.AccDistance = 0.8;
        }

        void AccDistanceIncrease()
        {
            this.hmiPacket.AccDistance += 0.2;
        }

        public void AccSet(bool newValue)
        {
            this.hmiPacket.Acc = newValue;
        }

        public void HandleGasPedal(bool isGasPedalDown)
        {
            if (isGasPedalDown)
            {
                this.hmiPacket.Gaspedal = this.Increase(this.hmiPacket.Gaspedal, 1000);
            }
            else
            {
                this.hmiPacket.Gaspedal = this.Decrease(this.hmiPacket.Gaspedal, 1000);
            }
        }

        public void HandleBrakePedal(bool isBrakePedalDown)
        {
            if (isBrakePedalDown)
            {
                this.hmiPacket.Breakpedal = this.Increase(this.hmiPacket.Breakpedal, 500);
            }
            else
            {
                this.hmiPacket.Breakpedal = this.Decrease(this.hmiPacket.Breakpedal, 500);
            }
        }

        public void HandleSteering(bool right, bool left)
        {
            if (right && !left)
            {
                this.hmiPacket.Steering = this.SteeringIncrease(this.hmiPacket.Steering, 1000);
            }
            else if (!right && left)
            {
                this.hmiPacket.Steering = this.SteeringDecrease(this.hmiPacket.Steering, 1000);
            }
            else
            {
                if (this.hmiPacket.Steering > 0)
                {
                    this.hmiPacket.Steering = this.SteeringDecrease(this.hmiPacket.Steering, 1000);
                    if (this.hmiPacket.Steering < 0)
                    {
                        this.hmiPacket.Steering = 0;
                    }
                }
                else if (this.hmiPacket.Steering < 0)
                {
                    this.hmiPacket.Steering = this.SteeringIncrease(this.hmiPacket.Steering, 1000);
                    if (this.hmiPacket.Steering > 0)
                    {
                        this.hmiPacket.Steering = 0;
                    }
                }
            }
        }

        double Increase(double pedal, double millisecondsToReachMaxValue)
        {
            if (pedal < 100)
            {
                pedal += 10 / (millisecondsToReachMaxValue / 1000 * 6);
            }

            if (pedal > 100)
            {
                pedal = 100;
            }

            return pedal;
        }

        double Decrease(double pedal, double millisecondsToReachMaxValue)
        {
            if (pedal > 0)
            {
                pedal -= 10 / (millisecondsToReachMaxValue / 1000 * 6);
            }

            if (pedal < 0)
            {
                pedal = 0;
            }

            return pedal;
        }

        double SteeringIncrease(double steer, double millisecondsToReachMaxValue)
        {
            if (steer < 100)
            {
                steer += 10 / (millisecondsToReachMaxValue / 1000 * 6);
            }

            if (steer > 100)
            {
                steer = 100;
            }

            return steer;
        }

        double SteeringDecrease(double steer, double millisecondsToReachMaxValue)
        {
            if (steer > -100)
            {
                steer -= 10 / (millisecondsToReachMaxValue / 1000 * 6);
            }

            if (steer < -100)
            {
                steer = -100;
            }

            return steer;
        }

        public void TurnSignalRightSet(bool newValue)
        {
            this.hmiPacket.TurnSignalRight = newValue;
        }

        public void TurnSignalLeftSet(bool newValue)
        {
            this.hmiPacket.TurnSignalLeft = newValue;
        }

        public void LaneKeepingSet(bool newValue)
        {
            this.hmiPacket.LaneKeeping = newValue;
        }

        public void ParkingPilotSet(bool newValue)
        {
            this.hmiPacket.ParkingPilot = newValue;
        }

        public void PolygonSet(bool newValue)
        {
            this.debugPacket.Polygon = newValue;
        }

        public void UtrasoundSensorSet(bool newValue)
        {
            this.debugPacket.UtrasoundSensor = newValue;
        }

        public void RadarSensorSet(bool newValue)
        {
            this.debugPacket.RadarSensor = newValue;
        }

        public void BoardCameraSet(bool newValue)
        {
            this.debugPacket.BoardCamera = newValue;
        }

        public void PolygonDebugSet(bool newValue)
        {
            this.debugPacket.Polygon = newValue;
        }
        #endregion
    }
}