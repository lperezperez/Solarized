namespace Solarized.ThemeGenerator
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using NDesk.Options;
    using Solarized.ThemeGenerator.Generators;
    using Solarized.ThemeGenerator.Properties;
    /// <summary>The startup class for the application.</summary>
    internal static class Program
    {
        #region Methods
        private static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider assembly) => assembly.GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().FirstOrDefault();
        /// <summary>Start point for the application.</summary>
        /// <param name="arguments">The application arguments.</param>
        [STAThread]
        private static void Main(params string[] arguments)
        {
            Console.WriteLine();
            var executingAssembly = typeof(Program).Assembly;
            Console.WriteLine(Resources.ApplicationInfo, executingAssembly.GetAttribute<AssemblyTitleAttribute>().Title, executingAssembly.GetAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion, VisualStudio.VisualStudioVersion);
            var createTemplate = false;
            var templateFilePath = VisualStudio.TemplateFileName;
            var darkThemeFilePath = VisualStudio.ThemeDarkFileName;
            var lightThemeFilePath = VisualStudio.ThemeLightFileName;
            var showHelp = false;
            var optionSet = new OptionSet { { "c|create-template", Resources.CreateTemplate, value => createTemplate = value != null }, { "t|template-path:", string.Format(Resources.TemplateFileName, VisualStudio.TemplateFileName), value => templateFilePath = value ?? VisualStudio.TemplateFileName }, { "d|dark-theme-path:", string.Format(Resources.ThemeDarkFileName, VisualStudio.ThemeDarkFileName), value => darkThemeFilePath = value ?? VisualStudio.ThemeDarkFileName }, { "l|light-theme-path:", string.Format(Resources.ThemeLightFileName, VisualStudio.ThemeLightFileName), value => lightThemeFilePath = value ?? VisualStudio.ThemeLightFileName }, { "h|?|help", Resources.ShowHelp, value => showHelp = value != null } };
            try { optionSet.Parse(arguments); }
            catch (OptionException optionException)
            {
                Console.WriteLine();
                Console.WriteLine(optionException.Message);
                showHelp = true;
            }
            if (showHelp)
            {
                ShowHelp(optionSet);
                return;
            }
            if (createTemplate)
            {
                if (File.Exists(darkThemeFilePath))
                    VisualStudio.WriteTemplate(templateFilePath, darkThemeFilePath, ColorScheme.Dark);
                else if (File.Exists(lightThemeFilePath))
                    VisualStudio.WriteTemplate(templateFilePath, lightThemeFilePath, ColorScheme.Light);
                else
                    throw new FileNotFoundException(Resources.ThemeFileNotFound);
                return;
            }
            if (!File.Exists(templateFilePath))
                throw new FileNotFoundException(Resources.TemplateFileNotFound);
            VisualStudio.WriteThemes(templateFilePath, darkThemeFilePath, lightThemeFilePath);
        }
        /// <summary>Shows the application help.</summary>
        /// <param name="optionSet">The command options.</param>
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
            Console.WriteLine($@"{AppDomain.CurrentDomain.FriendlyName} -c -t=C:\foo\bar.xml -d=C:\foo\bar.{VisualStudio.VsTheme}");
            Console.WriteLine(Resources.CreateTemplateDescription);
        }
        #endregion
    }
}