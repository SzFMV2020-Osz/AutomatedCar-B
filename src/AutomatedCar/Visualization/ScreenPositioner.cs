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


            


            foreach(WorldObject wo in world.WorldObjects) {

                Point p = pco.getPositionFromScreen(wo, world.VisibleWidth, world.VisibleHeight);
                wo.VisibleX = Convert.ToInt32(p.X);
                wo.VisibleY = Convert.ToInt32(p.Y);
            }

            world.ControlledCar.VisibleX = (world.VisibleWidth/2)-world.ControlledCar._rotationCenterPointX - world.ControlledCar.Width/2;
            world.ControlledCar.VisibleY = (world.VisibleHeight/2)-world.ControlledCar._rotationCenterPointY - world.ControlledCar.Height/2;
        }


        private bool closeToLeftEdge(World world, AutomatedCar car){
            return  car.X < (world.VisibleWidth/2);
        }

        private bool closeToTopEdge(World world, AutomatedCar car){
            return  car.Y < (world.VisibleHeight/2);
        }

         private bool closeToRightEdge(World world, AutomatedCar car){
            return  car.X > (world.Width - (world.VisibleWidth/2));
        }

        private bool closeToBottomEdge(World world, AutomatedCar car){
            return  car.Y > (world.Height - (world.VisibleHeight/2));
        }

    }
}