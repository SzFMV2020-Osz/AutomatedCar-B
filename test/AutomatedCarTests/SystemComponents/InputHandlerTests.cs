using AutomatedCar;
using AutomatedCar.SystemComponents;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.SystemComponents
{
    public class InputHandlerTests : IDisposable
    {
        static HumanMachineInterface hmi;

        public InputHandlerTests()
        {
            hmi = new HumanMachineInterface(new VirtualFunctionBus());
        }

        [Fact(Skip = "This is just a method to fill up the keboard class's HashSet")]
        public void KeyboardFill()
        {
            for (int i = 0; i < 173; i++)
            {
                Keyboard.Keys.Add((Key)i);
            }
            for (int i = 10001; i < 10005; i++)
            {
                Keyboard.Keys.Add((Key)i);
            }
            Keyboard.ToggleableKeys.Add(Key.E);
            Keyboard.ToggleableKeys.Add(Key.Q);
            Keyboard.ToggleableKeys.Add(Key.A);
            Keyboard.ToggleableKeys.Add(Key.D);
            Keyboard.ToggleableKeys.Add(Key.R);
            Keyboard.ToggleableKeys.Add(Key.U);
            Keyboard.ToggleableKeys.Add(Key.Z);
            Keyboard.ToggleableKeys.Add(Key.I);
        }

        [Fact]
        public void WhitoutKeyPressedAllVarialbeRemainsFalse()
        {           
            hmi.InputHandler();
            Assert.False(hmi.Acc);
            Assert.False(hmi.AccDistance);
            Assert.False(hmi.AccSpeedMinus);
            Assert.False(hmi.AccSpeedPlus);
            Assert.False(hmi.BreakpedalDown);
            Assert.False(hmi.CameraDebug);
            Assert.False(hmi.GaspedalDown);
            Assert.False(hmi.GearDown);
            Assert.False(hmi.GearUp);
            Assert.False(hmi.LaneKeeping);
            Assert.False(hmi.ParkingPilot);
            Assert.False(hmi.RadarDebug);
            Assert.False(hmi.SteeringLeft);
            Assert.False(hmi.SteeringRight);
            Assert.False(hmi.TurnSignalLeft);
            Assert.False(hmi.TurnSignalRight);
            Assert.False(hmi.UltrasoundDebug);
        }

        [Fact]
        public void WithBadInputsNormalInputsVariableStaysFalse()
        {
            KeyboardFill();
            Keyboard.Keys.Remove(Key.Up);
            Keyboard.Keys.Remove(Key.Down);
            Keyboard.Keys.Remove(Key.Left);
            Keyboard.Keys.Remove(Key.Right);

            hmi.InputHandler();

            Assert.False(hmi.GaspedalDown);
            Assert.False(hmi.BreakpedalDown);
            Assert.False(hmi.SteeringLeft);
            Assert.False(hmi.SteeringRight);
        }

        [Fact]
        public void WithBadInputsPressInputsVariableStaysFalse()
        {
            KeyboardFill();
            Keyboard.Keys.Remove(Key.Add);
            Keyboard.Keys.Remove(Key.Subtract);
            Keyboard.Keys.Remove(Key.T);
            Keyboard.Keys.Remove(Key.W);
            Keyboard.Keys.Remove(Key.S);

            hmi.InputHandler();

            Assert.False(hmi.AccSpeedMinus);
            Assert.False(hmi.AccSpeedPlus);
            Assert.False(hmi.AccDistance);
            Assert.False(hmi.GearDown);
            Assert.False(hmi.GearUp);
        }

        [Fact]
        public void WithBadInputsToggleInputsVariableStaysFalse()
        {
            KeyboardFill();
            Keyboard.ToggleableKeys.Remove(Key.E);
            Keyboard.ToggleableKeys.Remove(Key.Q);
            Keyboard.ToggleableKeys.Remove(Key.A);
            Keyboard.ToggleableKeys.Remove(Key.D);
            Keyboard.ToggleableKeys.Remove(Key.R);
            Keyboard.ToggleableKeys.Remove(Key.U);
            Keyboard.ToggleableKeys.Remove(Key.Z);
            Keyboard.ToggleableKeys.Remove(Key.I);

            hmi.InputHandler();

            Assert.False(hmi.TurnSignalRight);
            Assert.False(hmi.TurnSignalLeft);
            Assert.False(hmi.Acc);
            Assert.False(hmi.LaneKeeping);
            Assert.False(hmi.ParkingPilot);
            Assert.False(hmi.UltrasoundDebug);
            Assert.False(hmi.RadarDebug);
            Assert.False(hmi.CameraDebug);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueGaspedal()
        {
            Keyboard.Keys.Add(Key.Up);
            hmi.InputHandler();

            Assert.True(hmi.GaspedalDown);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueBreakpedal()
        {
            Keyboard.Keys.Add(Key.Down);
            hmi.InputHandler();

            Assert.True(hmi.BreakpedalDown);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueSteeringRight()
        {
            Keyboard.Keys.Add(Key.Right);
            hmi.InputHandler();

            Assert.True(hmi.SteeringRight);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueSteeringLeft()
        {
            Keyboard.Keys.Add(Key.Left);
            hmi.InputHandler();

            Assert.True(hmi.SteeringLeft);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueAcc()
        {
            Keyboard.ToggleableKeys.Add(Key.A);
            hmi.InputHandler();

            Assert.True(hmi.Acc);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueAccDistance()
        {
            Keyboard.Keys.Add(Key.T);
            Keyboard.PressableKeys.Add(Key.T);

            hmi.InputHandler();

            Assert.True(hmi.AccDistance);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueAccSpeedPlus()
        {
            Keyboard.Keys.Add(Key.Add);
            Keyboard.PressableKeys.Add(Key.Add);

            hmi.InputHandler();

            Assert.True(hmi.AccSpeedPlus);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueAccSpeedMinus()
        {
            Keyboard.Keys.Add(Key.Subtract);
            Keyboard.PressableKeys.Add(Key.Subtract);

            hmi.InputHandler();

            Assert.True(hmi.AccSpeedMinus);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueTurnSignalRight()
        {
            Keyboard.ToggleableKeys.Add(Key.E);

            hmi.InputHandler();

            Assert.True(hmi.TurnSignalRight);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueTurnSignalLeft()
        {
            Keyboard.ToggleableKeys.Add(Key.Q);

            hmi.InputHandler();

            Assert.True(hmi.TurnSignalLeft);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueGeerUp()
        {
            Keyboard.Keys.Add(Key.W);
            Keyboard.PressableKeys.Add(Key.W);

            hmi.InputHandler();

            Assert.True(hmi.GearUp);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueGeerDown()
        {
            Keyboard.Keys.Add(Key.S);
            Keyboard.PressableKeys.Add(Key.S);

            hmi.InputHandler();

            Assert.True(hmi.GearDown);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueLaneKeeping()
        {
            Keyboard.ToggleableKeys.Add(Key.D);

            hmi.InputHandler();

            Assert.True(hmi.LaneKeeping);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueParkingPilot()
        {
            Keyboard.ToggleableKeys.Add(Key.R);

            hmi.InputHandler();

            Assert.True(hmi.ParkingPilot);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueUtrasoundDebug()
        {
            Keyboard.ToggleableKeys.Add(Key.U);

            hmi.InputHandler();

            Assert.True(hmi.UltrasoundDebug);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueRadarDebug()
        {
            Keyboard.ToggleableKeys.Add(Key.I);

            hmi.InputHandler();

            Assert.True(hmi.RadarDebug);
        }

        [Fact]
        public void WithTheRightKeyPressedTrueCameraDebug()
        {
            Keyboard.ToggleableKeys.Add(Key.Z);

            hmi.InputHandler();

            Assert.True(hmi.CameraDebug);
        }

        public void Dispose()
        {
            Keyboard.Keys.Clear();
            Keyboard.ToggleableKeys.Clear();
            Keyboard.PressableKeys.Clear();
        }
    }
}
