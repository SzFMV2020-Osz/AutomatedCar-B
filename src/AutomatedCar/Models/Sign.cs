// <copyright file="Sign.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Required class so it can store the speed limit or the text on the street signs.
    /// </summary>
    public class Sign : WorldObject
    {
        /// <summary>
        /// Gets stores the text or numbers of the sign in a string. Can be set only when loaded.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sign"/> class.
        /// Public constructor for signs.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="filename">Filename for visualization.</param>
        /// <param name="text">Text written on the table. duh.</param>
        public Sign(int x, int y, string filename, string text)
            : base(x, y, filename)
        {
            this.Text = text;
        }
    }
}
