namespace AutomatedCar
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.Visualization;
    using Avalonia.Input;
    using Avalonia;

    public class ScreenPositioner 
    {

        public static void AlignItemsToScreen(World world) {
            
            PositionComputeObject pco = new PositionComputeObject(world.ControlledCar);

           

            foreach(WorldObject wo in world.WorldObjects){
                wo.VisibleX = world.ControlledCar.X*(-1);
                wo.VisibleY = world.ControlledCar.Y*(-1);
            }

            world.ControlledCar.VisibleX = (world.Width/2)-world.ControlledCar._rotationCenterPointX - world.ControlledCar.Width/2;
            world.ControlledCar.VisibleY = (world.Height/2)-world.ControlledCar._rotationCenterPointY - world.ControlledCar.Height/2;
        }

    }
}