namespace Solarized.ThemeGenerator.Generators
{
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using JetBrains.Annotations;
    /// <summary>The Visual Studio theme generator.</summary>
    public static class VisualStudio
    {
        #region Constants
        /// <summary>Default template file name.</summary>
        public const string TemplateFileName = VsTheme + ".Template." + VisualStudioVersion + ".xml";
        /// <summary>Default dark theme file name.</summary>
        public const string ThemeDarkFileName = ThemeDark + ThemeExtension;
        /// <summary>Default light theme file name.</summary>
        public const string ThemeLightFileName = ThemeLight + ThemeExtension;
        /// <summary>Visual Studio version.</summary>
        public const string VisualStudioVersion = "2017";
        /// <summary>Visual Studio theme extension.</summary>
        public const string VsTheme = "VSTheme";
        /// <summary>Global unique identifier.</summary>
        private const string Guid = @"GUID";
        /// <summary>Dark theme name.</summary>
        private const string ThemeDark = ThemePrefix + "(Dark)";
        /// <summary>Theme suffix.</summary>
        private const string ThemeExtension = "." + VsTheme;
        /// <summary>The light theme name.</summary>
        private const string ThemeLight = ThemePrefix + "(Light)";
        /// <summary>Visual Studio theme prefix.</summary>
        private const string ThemePrefix = "Solarized " + VisualStudioVersion + " ";
        #endregion
        #region Methods
        /// <summary>Writes the themes template file.</summary>
        /// <param name="templateFilePath">The template file path.</param>
        /// <param name="sourceThemeFilePath">The source theme file path.</param>
        /// <param name="colorScheme">The <see cref="ColorScheme"/>.</param>
        /// <exception cref="FileNotFoundException"><paramref name="sourceThemeFilePath"/> file was not found.</exception>
        public static void WriteTemplate(string templateFilePath, [NotNull] string sourceThemeFilePath, ColorScheme colorScheme)
        {
            using (var xmlReader = XmlReader.Create(sourceThemeFilePath))
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
                                            case Guid:
                                                xmlWriter.WriteAttributeString(xmlReader.Name, @"$GUID");
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
                                        if (xmlReader.Name == "Source" && int.TryParse(xmlReader.Value, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var _))
                                        {
                                            var nearestColor = colorScheme.NearestColor(Color.FromArgb(int.Parse(xmlReader.Value, NumberStyles.HexNumber)));
                                            xmlWriter.WriteAttributeString(xmlReader.Name, $"{nearestColor.Value.A:X}${nearestColor.Key}");
                                        }
                                        else
                                            xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                    xmlReader.MoveToElement();
                                }
                                else
                                    xmlWriter.WriteAttributes(xmlReader, true);
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
        /// <param name="lightThemeFilePath">The light theme file path.</param>
        /// <exception cref="FileNotFoundException"><paramref name="templateFilePath"/> file was not found.</exception>
        public static void WriteThemes([NotNull] string templateFilePath, string darkThemeFilePath, string lightThemeFilePath)
        {
            using (var xmlReader = XmlReader.Create(templateFilePath))
                using (var xmlWriterDark = XmlWriter.Create(darkThemeFilePath))
                    using (var xmlWriterLight = XmlWriter.Create(lightThemeFilePath))
                    {
                        var darkColorScheme = ColorScheme.Dark;
                        var lightColorScheme = ColorScheme.Light;
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
                                                    xmlWriterDark.WriteAttributeString(xmlReader.Name, ThemeDark);
                                                    xmlWriterLight.WriteAttributeString(xmlReader.Name, ThemeLight);
                                                    break;
                                                case Guid:
                                                    xmlWriterDark.WriteAttributeString(xmlReader.Name, "{4f4527a7-e5d3-4382-8ba0-126c0f0d3fe9}");
                                                    xmlWriterLight.WriteAttributeString(xmlReader.Name, "{4d3b11ea-fd11-48f8-a8df-6911f7a5d892}");
                                                    break;
                                                default:
                                                    xmlWriterDark.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                                    xmlWriterLight.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                                                    break;
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
                                                    var key = xmlReader.Value.Substring(position + 1);
                                                    if (darkColorScheme.ContainsKey(key))
                                                    {
                                                        var darkColor = darkColorScheme[key];
                                                        var lightColor = lightColorScheme[key];
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
        #endregion
    }
}