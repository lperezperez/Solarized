namespace Solarized.ThemeGenerator
{
    using System.Collections.Generic;
    using System.Drawing;
    /// <summary>Represents a Solarized color scheme.</summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{String, Color}"/>
    public sealed class ColorScheme : Dictionary<string, Color>
    {
        #region Constants
        internal const string BackgroundDefault = "BackgroundDefault";
        internal const string BackgroundHighlight = "BackgroundHighlight";
        internal const string EmphasizedContent = "EmphasizedContent";
        internal const string Highlight1 = "Highlight1";
        internal const string Highlight2 = "Highlight2";
        internal const string MiddleGray = "MiddleGray";
        internal const string PrimaryContent = "PrimaryContent";
        internal const string SecondaryContent = "SecondaryContent";
        #endregion
        #region Fields
        /// <summary>The dark <see cref="ColorScheme"/>.</summary>
        public static readonly ColorScheme Dark = new ColorScheme { { BackgroundDefault, Palette.Base03 }, { BackgroundHighlight, Palette.Base02 }, { SecondaryContent, Palette.Base01 }, { MiddleGray, Palette.Base00 }, { PrimaryContent, Palette.Base0 }, { EmphasizedContent, Palette.Base1 }, { Highlight1, Palette.Base2 }, { Highlight2, Palette.Base3 } };
        /// <summary>The light <see cref="ColorScheme"/>.</summary>
        public static readonly ColorScheme Light = new ColorScheme { { BackgroundDefault, Palette.Base3 }, { BackgroundHighlight, Palette.Base2 }, { SecondaryContent, Palette.Base1 }, { MiddleGray, Palette.Base0 }, { PrimaryContent, Palette.Base00 }, { EmphasizedContent, Palette.Base01 }, { Highlight1, Palette.Base02 }, { Highlight2, Palette.Base03 } };
        #endregion
        #region Constructors
        public ColorScheme()
        {
            foreach (var color in new Dictionary<string, Color> { { nameof(Palette.Yellow), Palette.Yellow }, { nameof(Palette.Orange), Palette.Orange }, { nameof(Palette.Red), Palette.Red }, { nameof(Palette.Magenta), Palette.Magenta }, { nameof(Palette.Violet), Palette.Violet }, { nameof(Palette.Blue), Palette.Blue }, { nameof(Palette.Cyan), Palette.Cyan }, { nameof(Palette.Green), Palette.Green } })
                this.Add(color.Key, color.Value);
        }
        #endregion
        #region Methods
        /// <summary>Gets the nearest color of the specified <paramref name="inputColor"/>.</summary>
        /// <param name="inputColor">The hexadecimal ARGB string representation of the input color.</param>
        /// <returns>The hexadecimal ARGB string representation of the nearest color of <paramref name="inputColorHexArgb"/>.</returns>
        public KeyValuePair<string, Color> NearestColor(Color inputColor)
        {
            using (var enumerator = this.GetEnumerator())
            {
                var nearestColor = enumerator.Current;
                var minDistance = double.MaxValue;
                while (enumerator.MoveNext())
                {
                    var currentColor = enumerator.Current;
                    var currentDistance = inputColor.GetDistance(currentColor.Value);
                    if (currentDistance > minDistance)
                        continue;
                    if (currentDistance == 0)
                        return currentColor;
                    minDistance = currentDistance;
                    nearestColor = currentColor;
                }
                return nearestColor;
            }
        }
        #endregion
    }
}