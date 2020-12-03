namespace AutomatedCar.SystemComponents
{
    using System.Net.Http.Headers;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Input;
    using AvaloniaAppTemplate.Namespace;
    using MsgBox;
    using Views;

    public class GameOverCondition : SystemComponent
    {
        public bool IsGameOver { get; set; }

        public GameOverCondition(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.IsGameOver = false;
        }

        #region Properties


        #endregion

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