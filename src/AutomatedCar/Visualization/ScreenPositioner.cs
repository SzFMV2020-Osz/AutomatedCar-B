namespace AutomatedCar
{
    using System;
    using AutomatedCar.Models;
    using AutomatedCar.Visualization;
    using Avalonia.Input;
    using Avalonia;

    public class ScreenPositioner 
    {
        public static ScreenPositioner Instance { get; } = new ScreenPositioner();

        public void AlignItemsToScreen(World world) { 


            // PositionComputeObject pco = new PositionComputeObject(world.ControlledCar);

            // foreach(WorldObject wo in world.WorldObjects) {
            //      Point p = pco.getPositionFromScreen(wo, world.VisibleWidth, world.VisibleHeight);
            //     wo.VisibleX = Convert.ToInt32(p.X);
            //     wo.VisibleY = Convert.ToInt32(p.Y);
            // }

            // world.ControlledCar.VisibleX = 600;//ComputeCarPositionHorisontal(world, world.VisibleWidth/2);
            // world.ControlledCar.VisibleY = 600;//computeCarPositionVertical(world, world.VisibleHeight/2);
        }
 
        // private int ComputeCarPositionHorisontal(World world, int currentX) {
        //     return currentX-world.ControlledCar.RotationCenterPointX - (world.ControlledCar.Width/2);
        // }
        // private int ComputeCarPositionHorisontalRight(World world) {
        //     return world.VisibleWidth - (world.Width - world.ControlledCar.X)-world.ControlledCar.RotationCenterPointX - (world.ControlledCar.Width/2);
        // }

        // private int computeCarPositionVertical(World world, int currentY) {
        //     return currentY-world.ControlledCar.RotationCenterPointY-world.ControlledCar.RotationCenterPointY;
        // }

        // private int computeCarPositionVerticalBottom(World world) {
        //     return world.VisibleHeight - (world.Height - world.ControlledCar.Y)-world.ControlledCar.RotationCenterPointY;
        // }

        // private bool closeToLeftEdge(World world, AutomatedCar car){
        //     return  car.X < (world.VisibleWidth/2);
        // }

        // private bool closeToTopEdge(World world, AutomatedCar car){
        //     return  car.Y < (world.VisibleHeight/2);
        // }

        //  private bool closeToRightEdge(World world, AutomatedCar car){
        //     return  car.X > (world.Width - (world.VisibleWidth/2));
        // }

        // private bool closeToBottomEdge(World world, AutomatedCar car){
        //     return  car.Y > (world.Height - (world.VisibleHeight/2));
        // }
    }
}