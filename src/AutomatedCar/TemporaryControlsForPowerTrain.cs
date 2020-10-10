namespace AutomatedCar
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia.Input;

    class TemporaryControlsForPowerTrain
    {
        private int gasPedal = 0;
        private int brakePedal = 0;
        private int steeringWheel = 0;
        private GearShifterPosition gearShifterPosition = GearShifterPosition.D;

        public void ExecuteControls()
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                if (this.gasPedal < 100) { this.gasPedal++; }
            }
            else if (Keyboard.IsKeyDown(Key.Space))
            {
                if (this.brakePedal < 100) { this.brakePedal++; }
            }
            else if (Keyboard.IsKeyDown(Key.Left))
            {
                if (this.steeringWheel < 100) { this.steeringWheel++; }
            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                if (this.steeringWheel > -100) { this.steeringWheel--; }
            }
            else if (Keyboard.IsKeyDown(Key.R))
            {
                this.gearShifterPosition = GearShifterPosition.R;
            }
            else if (Keyboard.IsKeyDown(Key.D))
            {
                this.gearShifterPosition = GearShifterPosition.D;
            }
            else if (Keyboard.IsKeyDown(Key.P))
            {
                this.gearShifterPosition = GearShifterPosition.P;
            }
            else if (Keyboard.IsKeyDown(Key.N))
            {
                this.gearShifterPosition = GearShifterPosition.N;
            }
            else
            {
                this.brakePedal = 0;
                this.gasPedal = 0;
                this.steeringWheel = 0;
            }

            World.Instance.ControlledCar.VirtualFunctionBus.PowerTrainPacket.UpdatePowerTrainPacket(this.gasPedal, this.brakePedal, this.steeringWheel, this.gearShifterPosition);
        }
    }
}
