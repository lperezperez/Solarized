namespace Solarized.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using NDesk.Options;
    using Solarized.Generator.Properties;
    /// <summary>The program class.</summary>
    internal static class Program
    {
        #region Properties
        private static Dictionary<string, Color> AccentColorsScheme => new Dictionary<string, Color> { { "$Yellow", Palette.Yellow }, { "$Orange", Palette.Orange }, { "$Red", Palette.Red }, { "$Magenta", Palette.Magenta }, { "$Violet", Palette.Violet }, { "$Blue", Palette.Blue }, { "$Cyan", Palette.Cyan }, { "$Green", Palette.Green } };
        #endregion
        #region Methods
        private static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider assembly) => assembly.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().FirstOrDefault();
        private static Dictionary<string, Color> GetDarkColorScheme(this Dictionary<string, Color> colors) => colors.MergeColorSchmes(new Dictionary<string, Color> { { "$BackgroundDefault", Palette.Base03 }, { "$BackgroundHighlight", Palette.Base02 }, { "$SecondaryContent", Palette.Base01 }, { "$MiddleGray", Palette.Base00 }, { "$PrimaryContent", Palette.Base0 }, { "$EmphasizedContent", Palette.Base1 }, { "$Highlight1", Palette.Base2 }, { "$Highlight2", Palette.Base3 } });
        private static Dictionary<string, Color> GetLightColorScheme(this Dictionary<string, Color> colors) => colors.MergeColorSchmes(new Dictionary<string, Color> { { "$BackgroundDefault", Palette.Base3 }, { "$BackgroundHighlight", Palette.Base2 }, { "$SecondaryContent", Palette.Base1 }, { "$MiddleGray", Palette.Base0 }, { "$PrimaryContent", Palette.Base00 }, { "$EmphasizedContent", Palette.Base01 }, { "$Highlight1", Palette.Base02 }, { "$Highlight2", Palette.Base03 } });
        /// <summary>Start point for the application.</summary>
        /// <param name="arguments">The <see cref="Program"/> arguments.</param>
        [STAThread]
        private static void Main(params string[] arguments)
        {
            Console.WriteLine();
            var executingAssembly = typeof(Program).Assembly;
            Console.WriteLine(Resources.ApplicationInfo, executingAssembly.GetAttribute<AssemblyTitleAttribute>().Title, executingAssembly.GetAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            var createTemplate = false;
            var templateFilePath = VisualStudio.TemplateFileName;
            var darkThemeFilePath = VisualStudio.ThemeDarkFileName;
            var themeLightFileName = VisualStudio.ThemeLightFileName;
            var showHelp = false;
            var optionSet = new OptionSet();
            optionSet.Add("c|create-template", Resources.CreateTemplate, value => createTemplate = value != null);
            optionSet.Add("t|template-path:", string.Format(Resources.TemplateFileName, VisualStudio.TemplateFileName), value => templateFilePath = value ?? VisualStudio.TemplateFileName);
            optionSet.Add("d|dark-theme-path:", string.Format(Resources.ThemeDarkFileName, VisualStudio.ThemeDarkFileName), value => darkThemeFilePath = value ?? VisualStudio.ThemeDarkFileName);
            optionSet.Add("l|light-theme-path:", string.Format(Resources.ThemeLightFileName, VisualStudio.ThemeLightFileName), value => themeLightFileName = value ?? VisualStudio.ThemeLightFileName);
            optionSet.Add("h|?|help", Resources.ShowHelp, value => showHelp = value != null);
            try
            {
                optionSet.Parse(arguments);
                if (showHelp)
                {
                    Program.ShowHelp(optionSet);
                    return;
                }
                if (createTemplate)
                {
                    var themeDarkFileNameExists = File.Exists(darkThemeFilePath);
                    var themeLightFileNameExists = File.Exists(themeLightFileName);
                    if (!(themeDarkFileNameExists || themeLightFileNameExists))
                        throw new FileNotFoundException(Resources.ThemeFileNotFound);
                    if (themeDarkFileNameExists)
                        VisualStudio.WriteTemplate(templateFilePath, darkThemeFilePath, Program.AccentColorsScheme.GetDarkColorScheme());
                    else
                        VisualStudio.WriteTemplate(templateFilePath, themeLightFileName, Program.AccentColorsScheme.GetLightColorScheme());
                    return;
                }
                if (!File.Exists(templateFilePath))
                    throw new FileNotFoundException(Resources.TemplateFileNotFound);
                var accentColorsScheme = Program.AccentColorsScheme;
                VisualStudio.WriteThemes(templateFilePath, darkThemeFilePath, accentColorsScheme.GetDarkColorScheme(), themeLightFileName, accentColorsScheme.GetLightColorScheme());
            }
            catch (OptionException optionException)
            {
                Console.WriteLine();
                Console.WriteLine(optionException.Message);
                Program.ShowHelp(optionSet);
            }
        }
        private static Dictionary<string, Color> MergeColorSchmes(this Dictionary<string, Color> colors1, Dictionary<string, Color> colors2) => colors1.Concat(colors2).ToDictionary(pair => pair.Key, x => x.Value);
        private static void ShowHelp(OptionSet optionSet)
        {
            Console.WriteLine();
            Console.WriteLine(Resources.Usage);
            Console.WriteLine();
            Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} [{Resources.Options}]");
            Console.WriteLine();
            Console.WriteLine($"{Resources.Options}:");
            Console.WriteLine();
            optionSet.WriteOptionDescriptions(Console.Out);
            Console.WriteLine();
            Console.WriteLine(Resources.Examples);
            Console.WriteLine();
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine(Resources.CreateThemesDescription);
            Console.WriteLine();
            Console.WriteLine($@"{AppDomain.CurrentDomain.FriendlyName} -c -t=C:\foo\bar.xml -d=C:\foo\bar.vstheme");
            Console.WriteLine(Resources.CreateTemplateDescription);
        }
        #endregion
    }
}