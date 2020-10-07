// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using AutomatedCar.Models;
// using AutomatedCar.Visualization;
// using Avalonia.Input;
// using Avalonia;
// using AutomatedCar;

// namespace VisualisationTests
// {
//     [TestClass]
//     public class AlignItemsToScreenTest
//     {
//         World w;

//         [TestInitialize]
//         public void init() {
//             w = World.Instance;
//             w.Width = 500;
//             w.Height = 400;
//             w.VisibleWidth = 300;
//             w.VisibleHeight = 200;
//         }

//         [TestMethod]
//         public void CarAt150x100_ObjectAtTheWorldOrigo()
//         {
//             // Arrange
//             WorldObject wo = new WorldObject(0,0, "");
//             WorldObject wo2 = new WorldObject(100,100, "");
//             AutomatedCar.Models.AutomatedCar ac = new AutomatedCar.Models.AutomatedCar(w.VisibleWidth/2,w.VisibleHeight/2, "");
//             this.w.ControlledCar = ac;
//             this.w.AddObject(wo);
//             this.w.AddObject(wo2);

//             // Act
//             ScreenPositioner.Instance.AlignItemsToScreen(w);

//             // Assert
//             Assert.AreEqual(wo.VisibleX, 0);
//             Assert.AreEqual(wo.VisibleY, 0);
//             Assert.AreEqual(wo2.VisibleX, 100);
//             Assert.AreEqual(wo2.VisibleY, 100);
//         }

//         [TestMethod]
//         public void CarisCloseToLeft()
//         {
//             // Arrange
//             AutomatedCar.Models.AutomatedCar ac = new AutomatedCar.Models.AutomatedCar(100,200, "");
//             ac.Width = 20;
//             ac.Height = 30;
//             ac.RotationCenterPointX = 0;
//             ac.RotationCenterPointY = 0;
//             this.w.ControlledCar = ac;

//             // Act
//             ScreenPositioner.Instance.AlignItemsToScreen(w);

//             // Assert
//             Assert.AreEqual(90, ac.VisibleX);
//         }

//         [TestMethod]
//         public void CarisCloseToLeftHandleOtherWorldObjects()
//         {
//             // Arrange
//             AutomatedCar.Models.AutomatedCar ac = new AutomatedCar.Models.AutomatedCar(100,200, "");
//             ac.Width = 20;
//             ac.Height = 30;
//             ac.RotationCenterPointX = 0;
//             ac.RotationCenterPointY = 0;
//             this.w.ControlledCar = ac;

//             WorldObject wo = new WorldObject(0,0, "");
//             this.w.ControlledCar = ac;
//             this.w.AddObject(wo);

//             // Act
//             ScreenPositioner.Instance.AlignItemsToScreen(w);

//             // Assert
//             Assert.AreEqual(0, wo.VisibleX);
//         }

//         [TestMethod]
//         public void CarAt250x200_ObjectAtTheWorldOrigo()
//         {
//             // Arrange
//             WorldObject wo = new WorldObject(100,100, "");
//             AutomatedCar.Models.AutomatedCar ac = new AutomatedCar.Models.AutomatedCar(250,200, "");
//             this.w.ControlledCar = ac;
//             this.w.AddObject(wo);
            
//             // Act
//             ScreenPositioner.Instance.AlignItemsToScreen(w);
            
//             // Assert
//             Assert.AreEqual(wo.VisibleX, 0);
//             Assert.AreEqual(wo.VisibleY, 0);
//         }
//     }
// }