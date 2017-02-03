namespace Solarized.Generator
{
    using System.Drawing;
    using System.Globalization;
    /// <summary>Solarized colors palette.</summary>
    public struct Palette
    {
        #region Fields
        /// <summary>Dark background.</summary>
        public static Color Base03 = Palette.GetFromArgbHexadecimalColor("FF002B36");
        /// <summary>Dark background highlight.</summary>
        public static Color Base02 = Palette.GetFromArgbHexadecimalColor("FF073642");
        /// <summary>Content "dark secondary" / "light emphasized".</summary>
        public static Color Base01 = Palette.GetFromArgbHexadecimalColor("FF586E75");
        /// <summary>Content "dark disabled" / " light primary".</summary>
        public static Color Base00 = Palette.GetFromArgbHexadecimalColor("FF657B83");
        /// <summary>Content "light disabled" / "dark primary".</summary>
        public static Color Base0 = Palette.GetFromArgbHexadecimalColor("FF839496");
        /// <summary>Content "light secondary" / "dark emphasized".</summary>
        public static Color Base1 = Palette.GetFromArgbHexadecimalColor("FF93A1A1");
        /// <summary>Light background highlight.</summary>
        public static Color Base2 = Palette.GetFromArgbHexadecimalColor("FFEEE8D5");
        /// <summary>Light background.</summary>
        public static Color Base3 = Palette.GetFromArgbHexadecimalColor("FFFDF6E3");
        /// <summary>The <see cref="Palette.Red"/> accent color.</summary>
        public static Color Red = Palette.GetFromArgbHexadecimalColor("FFDC322F");
        /// <summary>The <see cref="Palette.Orange"/> accent color.</summary>
        public static Color Orange = Palette.GetFromArgbHexadecimalColor("FFCB4B16");
        /// <summary>The <see cref="Palette.Yellow"/> accent color.</summary>
        public static Color Yellow = Palette.GetFromArgbHexadecimalColor("FFB58900");
        /// <summary>The <see cref="Palette.Green"/> accent color.</summary>
        public static Color Green = Palette.GetFromArgbHexadecimalColor("FF859900");
        /// <summary>The <see cref="Palette.Blue"/> accent color.</summary>
        public static Color Blue = Palette.GetFromArgbHexadecimalColor("FF268BD2");
        /// <summary>The <see cref="Palette.Cyan"/> accent color.</summary>
        public static Color Cyan = Palette.GetFromArgbHexadecimalColor("FF2AA198");
        /// <summary>The <see cref="Palette.Magenta"/> accent color.</summary>
        public static Color Magenta = Palette.GetFromArgbHexadecimalColor("FFD33682");
        /// <summary>The <see cref="Palette.Violet"/> accent color.</summary>
        public static Color Violet = Palette.GetFromArgbHexadecimalColor("FF6C71C4");
        #endregion
        #region Methods
        /// <summary>Gets the ARGB <see cref="System.Drawing.Color"/> in hexadecimal format.</summary>
        /// <param name="color">The <see cref="System.String"/> that contains the ARGB data.</param>
        /// <returns>The ARGB <see cref="System.Drawing.Color"/> .</returns>
        private static Color GetFromArgbHexadecimalColor(string color) => Color.FromArgb(int.Parse(color, NumberStyles.HexNumber));
        #endregion
    }
}