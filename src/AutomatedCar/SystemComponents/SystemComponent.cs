namespace AutomatedCar.SystemComponents
{
    public abstract class SystemComponent
    {
        protected VirtualFunctionBus virtualFunctionBus;

        protected SystemComponent(VirtualFunctionBus virtualFunctionBus = null)
        {
            if (virtualFunctionBus != null)
            {
                this.virtualFunctionBus = virtualFunctionBus;
                virtualFunctionBus.RegisterComponent(this);
            }
        }

        public abstract void Process();
    }
}