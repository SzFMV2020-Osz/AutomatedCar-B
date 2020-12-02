namespace AutomatedCar.SystemComponents
{
    using System.Net.Http.Headers;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia.Input;
    using MsgBox;
    using Views;

    public class GameOverCondition : SystemComponent
    {

        public GameOverCondition(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            
        }

        #region Properties

        public bool IsGameOver { get; set; }

        #endregion

        public override void Process()
        {
            
        }
    }
}