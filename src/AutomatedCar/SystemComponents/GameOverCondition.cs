namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AvaloniaAppTemplate.Namespace;

    public class GameOverCondition : SystemComponent
    {
        public bool IsGameOver { get; set; }

        public GameOverCondition(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.IsGameOver = false;
        }

        public override void Process()
        {
            if (World.Instance.ControlledCar.HealthPoints == 0 && !IsGameOver)
            {
                this.IsGameOver = true;
                var result = GameOverMessage.Show(null);

                result.ContinueWith(x => {
                    this.IsGameOver = false;
                    World.Instance.ControlledCar.Reset();
                });
            }
        }
    }
}