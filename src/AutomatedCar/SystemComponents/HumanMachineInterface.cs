namespace AutomatedCar.SystemComponents
{
    using System.Net.Http.Headers;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Input;
    using MsgBox;
    using Views;

    public class HumanMachineInterface : SystemComponent, IHumanMachineInterface
    {
        private HMIPacket hmiPacket;

        public HumanMachineInterface(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.hmiPacket = new HMIPacket();
            virtualFunctionBus.HMIPacket = this.hmiPacket;
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
        #endregion

        public override void Process()
        {
            this.InputHandler();
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
            this.PolygonSet(this.PolygonDebug);
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
            this.TurnSignalRight = this.ToggleInput(this.TurnSignalRight, Key.E);
            this.TurnSignalLeft = this.ToggleInput(this.TurnSignalLeft, Key.Q);
            this.Acc = this.ToggleInput(this.Acc, Key.A);
            this.LaneKeeping = this.ToggleInput(this.LaneKeeping, Key.D);
            this.ParkingPilot = this.ToggleInput(this.ParkingPilot, Key.R);
            this.UltrasoundDebug = this.ToggleInput(this.UltrasoundDebug, Key.U);
            this.CameraDebug = this.ToggleInput(this.CameraDebug, Key.Z);
            this.RadarDebug = this.ToggleInput(this.RadarDebug, Key.I);
            this.PolygonDebug = this.ToggleInput(this.PolygonDebug, Key.F);
            this.PressInputMessageBox(Key.F1);
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

        private void PressInputMessageBox(Key key)
        {
            if (Keyboard.IsPressableKeysDown(key) && (key == Key.F1))
            {
                Keyboard.DeletePressableKeys(key);
                MessageBox.Show(null, MessageBox.MessageBoxButtons.Ok);
            }
        }

        private bool ToggleInput(bool previousValue, Key key)
        {
            if (Keyboard.IsToggleableKeyDown(key) && Keyboard.DeleteToggleableKey(key))
            {
                previousValue = !previousValue;
            }

            return previousValue;
        }
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

            if (this.BreakpedalDown)
            {
                this.hmiPacket.Gaspedal = 0;
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
            World.Instance.ControlledCar.PolygonVisible = newValue;
        }

        public void UtrasoundSensorSet(bool newValue)
        {
            World.Instance.ControlledCar.UltraSoundVisible = newValue;
        }

        public void RadarSensorSet(bool newValue)
        {
            World.Instance.ControlledCar.RadarVisible = newValue;
        }

        public void BoardCameraSet(bool newValue)
        {
            World.Instance.ControlledCar.CameraVisible = newValue;
        }
        #endregion
    }
}