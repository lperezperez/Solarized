namespace Solarized.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Xml;
    /// <summary>The Visual Studio theme generator.</summary>
    public static class VisualStudio
    {
        #region Constants
        /// <summary>The default template file name.</summary>
        public const string TemplateFileName = VisualStudio.VsTheme + ".Template.2015.xml";
        /// <summary>The default dark theme file name.</summary>
        public const string ThemeDarkFileName = VisualStudio.ThemeDark + VisualStudio.ThemeExtension;
        /// <summary>The default light theme file name.</summary>
        public const string ThemeLightFileName = VisualStudio.ThemeLight + VisualStudio.ThemeExtension;
        private const string ThemeDark = VisualStudio.ThemePrefix + "(Dark)";
        private const string ThemeExtension = "." + VisualStudio.VsTheme;
        private const string ThemeLight = VisualStudio.ThemePrefix + "(Light)";
        private const string ThemePrefix = "Solarized 2015 ";
        private const string VsTheme = "VSTheme";
        #endregion
        #region Methods
        /// <summary>Writes the theme template file.</summary>
        /// <param name="templateFilePath">The theme template file path.</param>
        /// <param name="themeFilePath">The theme file path.</param>
        /// <param name="colorScheme">The color scheme.</param>
        public static void WriteTemplate(string templateFilePath, string themeFilePath, Dictionary<string, Color> colorScheme)
        {
            using (var xmlReader = XmlReader.Create(themeFilePath))
                using (var xmlWriter = XmlWriter.Create(templateFilePath))
                {
                    xmlWriter.WriteWhitespace("\n");
                    while (xmlReader.Read())
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                xmlWriter.WriteStartElement(xmlReader.LocalName);
                                if (xmlReader.LocalName == "Theme" && xmlReader.HasAttributes)
                                {
                                    while (xmlReader.MoveToNextAttribute())
                                        switch (xmlReader.Name)
                                        {
                                            case "Name":
                                                xmlWriter.WriteAttributeString(xmlReader.Name, "$ThemeName");
                                                break;
                                            case "GUID":
                                                xmlWriter.WriteAttributeString(xmlReader.Name, "$GUID");
                                                break;
                                            default:
                                                xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                                break;
                                        }
                                    xmlReader.MoveToElement();
                                }
                                else if ((xmlReader.LocalName == "Background" || xmlReader.LocalName == "Foreground") && xmlReader.HasAttributes)
                                {
                                    while (xmlReader.MoveToNextAttribute())
                                        xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Name == "Source" && int.TryParse(xmlReader.Value, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var colorParsed) ? xmlReader.Value.GetNearestColor(colorScheme) : xmlReader.Value);
                                    xmlReader.MoveToElement();
                                }
                                else
                                {
                                    xmlWriter.WriteAttributes(xmlReader, true);
                                }
                                if (xmlReader.IsEmptyElement)
                                    xmlWriter.WriteEndElement();
                                break;
                            case XmlNodeType.Whitespace:
                            case XmlNodeType.SignificantWhitespace:
                                xmlWriter.WriteWhitespace(xmlReader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                xmlWriter.WriteFullEndElement();
                                break;
                        }
                }
        }
        /// <summary>Writes both dark and light theme files.</summary>
        /// <param name="templateFilePath">The Visual Studio theme template file path.</param>
        /// <param name="darkThemeFilePath">The dark theme file path.</param>
        /// <param name="darkColorsScheme">The dark colors scheme.</param>
        /// <param name="lightThemeFilePath">The light theme file path.</param>
        /// <param name="lightColorsScheme">The light colors scheme.</param>
        public static void WriteThemes(string templateFilePath, string darkThemeFilePath, Dictionary<string, Color> darkColorsScheme, string lightThemeFilePath, Dictionary<string, Color> lightColorsScheme)
        {
            using (var xmlReader = XmlReader.Create(templateFilePath))
                using (var xmlWriterDark = XmlWriter.Create(darkThemeFilePath))
                    using (var xmlWriterLight = XmlWriter.Create(lightThemeFilePath))
                    {
                        xmlWriterDark.WriteWhitespace("\n");
                        xmlWriterLight.WriteWhitespace("\n");
                        while (xmlReader.Read())
                            switch (xmlReader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    xmlWriterDark.WriteStartElement(xmlReader.LocalName);
                                    xmlWriterLight.WriteStartElement(xmlReader.LocalName);
                                    if (xmlReader.LocalName == "Theme" && xmlReader.HasAttributes)
                                    {
                                        while (xmlReader.MoveToNextAttribute())
                                    switch (xmlReader.Name)
                                    {
                                        case "Name":
                                            xmlWriterDark.WriteAttributeString(xmlReader.Name, VisualStudio.ThemeDark);
                                            xmlWriterLight.WriteAttributeString(xmlReader.Name, VisualStudio.ThemeLight);
                                            break;
                                        case "GUID":
                                            xmlWriterDark.WriteAttributeString(xmlReader.Name, "{4f4527a7-e5d3-4382-8ba0-126c0f0d3fe9}");
                                            xmlWriterLight.WriteAttributeString(xmlReader.Name, "{4d3b11ea-fd11-48f8-a8df-6911f7a5d892}");
                                            break;
                                        default:
                                            xmlWriterDark.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                            xmlWriterLight.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                            break;
                                    }
                                {
                                    xmlWriterDark.WriteAttributeString(xmlReader.Name, xmlReader.Name == "Name" ? VisualStudio.ThemeDark : xmlReader.Value);
                                            xmlWriterLight.WriteAttributeString(xmlReader.Name, xmlReader.Name == "Name" ? VisualStudio.ThemeLight : xmlReader.Value);
                                        }
                                        xmlReader.MoveToElement();
                                    }
                                    else if ((xmlReader.LocalName == "Background" || xmlReader.LocalName == "Foreground") && xmlReader.HasAttributes)
                                    {
                                        while (xmlReader.MoveToNextAttribute())
                                        {
                                            if (xmlReader.Name == "Source")
                                            {
                                                var position = xmlReader.Value.IndexOf('$');
                                                if (position > 0)
                                                {
                                                    var key = xmlReader.Value.Substring(position);
                                                    if (darkColorsScheme.ContainsKey(key))
                                                    {
                                                        var darkColor = darkColorsScheme[key];
                                                        var lightColor = lightColorsScheme[key];
                                                        var alpha = xmlReader.Value.Substring(0, position);
                                                        xmlWriterDark.WriteAttributeString(xmlReader.Name, $"{alpha}{darkColor.R:X2}{darkColor.G:X2}{darkColor.B:X2}");
                                                        xmlWriterLight.WriteAttributeString(xmlReader.Name, $"{alpha}{lightColor.R:X2}{lightColor.G:X2}{lightColor.B:X2}");
                                                        continue;
                                                    }
                                                }
                                            }
                                            xmlWriterDark.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                            xmlWriterLight.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                        }
                                        xmlReader.MoveToElement();
                                    }
                                    else
                                    {
                                        xmlWriterDark.WriteAttributes(xmlReader, true);
                                        xmlWriterLight.WriteAttributes(xmlReader, true);
                                    }
                                    if (xmlReader.IsEmptyElement)
                                    {
                                        xmlWriterDark.WriteEndElement();
                                        xmlWriterLight.WriteEndElement();
                                    }
                                    break;
                                case XmlNodeType.Whitespace:
                                case XmlNodeType.SignificantWhitespace:
                                    xmlWriterDark.WriteWhitespace(xmlReader.Value);
                                    xmlWriterLight.WriteWhitespace(xmlReader.Value);
                                    break;
                                case XmlNodeType.EndElement:
                                    xmlWriterDark.WriteFullEndElement();
                                    xmlWriterLight.WriteFullEndElement();
                                    break;
                            }
                    }
        }
        /// <summary>Gets the nearest color of the specified <paramref name="inputColorHexArgb"/>.</summary>
        /// <param name="inputColorHexArgb">The hexadecimal ARGB string representation of the input color.</param>
        /// <param name="colorDictionary">The color dictionary to search for the nearest color.</param>
        /// <returns>The hexadecimal ARGB string representation of the nearest color of <paramref name="inputColorHexArgb"/>.</returns>
        private static string GetNearestColor(this string inputColorHexArgb, Dictionary<string, Color> colorDictionary)
        {
            var inputColor = Color.FromArgb(int.Parse(inputColorHexArgb, NumberStyles.HexNumber));
            var inputColorRed = Convert.ToDouble(inputColor.R);
            var inputColorGreen = Convert.ToDouble(inputColor.G);
            var inputColorBlue = Convert.ToDouble(inputColor.B);
            var minimumDistance = double.MaxValue;
            string nearestColor = null;
            foreach (var color in colorDictionary)
            {
                var colorRed = Math.Pow(Convert.ToDouble(color.Value.R) - inputColorRed, 2.0);
                var colorGreen = Math.Pow(Convert.ToDouble(color.Value.G) - inputColorGreen, 2.0);
                var colorBlue = Math.Pow(Convert.ToDouble(color.Value.B) - inputColorBlue, 2.0);
                var currentDistance = Math.Sqrt(colorBlue + colorGreen + colorRed);
                if (currentDistance > minimumDistance)
                    continue;
                if (currentDistance == 0.0)
                    return string.Concat(inputColor.A.ToString("X2"), color.Key);
                minimumDistance = currentDistance;
                nearestColor = color.Key;
            }
            return string.Concat(inputColor.A.ToString("X2"), nearestColor);
        }
        #endregion
    }
}