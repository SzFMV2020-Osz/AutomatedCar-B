// <copyright file="IWorld.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.Models
{
    using Avalonia;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface for IWorld objects.
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Returns an instance of a World object.
        /// </summary>
        /// <returns>Returns an instance of the World.</returns>
        public static World GetInstance()
        {
            return new World();
        }

        /// <summary>
        /// For sensors: Returns a list of worldobject that can be found in an area defined by the given list of points.
        /// </summary>
        /// <param name="searchArea">Area to be searched, defined by a list of points.</param>
        /// <returns>Returns a list of worldobjects in a given area.</returns>
        public List<WorldObject> SearchInRange(List<Point> searchArea);

        /// <summary>
        /// For getting the controlledCars position, geometry etc.
        /// </summary>
        /// <returns>Returns the instance of the controlled car as an AutomatedCar.</returns>
        public AutomatedCar GetControlledCar();

        /// <summary>
        /// For getting the NPC-s position, geometry etc.
        /// </summary>
        /// <returns>Returns a list of WorldObjects containing all the NPCs</returns>
        public List<WorldObject> GetNPCs();
    }
}
