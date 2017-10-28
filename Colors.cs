namespace Solarized.ThemeGenerator
{
    using System;
    using System.Drawing;
    /// <summary>Contains extension methods for <see cref="Color"/>.</summary>
    internal static class Colors
    {
        #region Methods
        /// <summary>Gets the distance between <paramref name="color1"/> and <paramref name="color2"/>.</summary>
        /// <param name="color1">A <see cref="Color"/> value.</param>
        /// <param name="color2">A <see cref="Color"/> value to compare.</param>
        /// <returns>The distance between <paramref name="color1"/> and <paramref name="color2"/>.</returns>
        public static double GetDistance(this Color color1, Color color2) => Math.Sqrt(color1.R.GetDistance(color2.R) + color1.G.GetDistance(color2.G) + color1.B.GetDistance(color2.B));
        /// <summary>Gets the distance between <paramref name="value1"/> and <paramref name="value2"/>.</summary>
        /// <param name="value1">A RGB component value.</param>
        /// <param name="value2">A RGB component value to compare.</param>
        /// <returns>The distance between <paramref name="value1"/> and <paramref name="value2"/>.</returns>
        private static double GetDistance(this byte value1, byte value2) => Math.Pow(value1 - value2, 2.0);
        #endregion
    }
}