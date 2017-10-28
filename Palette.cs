namespace Solarized.ThemeGenerator
{
    using System.Drawing;
    /// <summary>Solarized colors palette.</summary>
    public static class Palette
    {
        #region Fields
        /// <summary>Content "light disabled" / "dark primary" color.</summary>
        public static readonly Color Base0 = ColorTranslator.FromHtml("#839496");
        /// <summary>Content "dark disabled" / " light primary" color.</summary>
        public static readonly Color Base00 = ColorTranslator.FromHtml("#657B83");
        /// <summary>Content "dark secondary" / "light emphasized" color.</summary>
        public static readonly Color Base01 = ColorTranslator.FromHtml("#586E75");
        /// <summary>Dark background highlight color.</summary>
        public static readonly Color Base02 = ColorTranslator.FromHtml("#073642");
        /// <summary>Dark background color.</summary>
        public static readonly Color Base03 = ColorTranslator.FromHtml("#002B36");
        /// <summary>Content "light secondary" / "dark emphasized" color.</summary>
        public static readonly Color Base1 = ColorTranslator.FromHtml("#93A1A1");
        /// <summary>Light background highlight color.</summary>
        public static readonly Color Base2 = ColorTranslator.FromHtml("#EEE8D5");
        /// <summary>Light background color.</summary>
        public static readonly Color Base3 = ColorTranslator.FromHtml("#FDF6E3");
        /// <summary>The blue accent color.</summary>
        /// <remarks>Monotone.</remarks>
        public static readonly Color Blue = ColorTranslator.FromHtml("#268BD2");
        /// <summary>The cyan (Analogous) accent color.</summary>
        /// <remarks>Triad.</remarks>
        public static readonly Color Cyan = ColorTranslator.FromHtml("#2AA198");
        /// <summary>The green accent color.</summary>
        /// <remarks>Tetrad.</remarks>
        public static readonly Color Green = ColorTranslator.FromHtml("#859900");
        /// <summary>The magenta accent color.</summary>
        /// <remarks>Tetrad.</remarks>
        public static readonly Color Magenta = ColorTranslator.FromHtml("#D33682");
        /// <summary>The orange accent color.</summary>
        /// <remarks>Complement.</remarks>
        public static readonly Color Orange = ColorTranslator.FromHtml("#CB4B16");
        /// <summary>The red accent color.</summary>
        /// <remarks>Triad.</remarks>
        public static readonly Color Red = ColorTranslator.FromHtml("#DC322F");
        /// <summary>The violet accent color.</summary>
        /// <remarks>Analogous.</remarks>
        public static readonly Color Violet = ColorTranslator.FromHtml("#6C71C4");
        /// <summary>The yellow accent color.</summary>
        /// <remarks>Split-complementary.</remarks>
        public static readonly Color Yellow = ColorTranslator.FromHtml("#B58900");
        #endregion
    }
}