namespace Solarized.ThemeGenerator.Generators
{
    using Microsoft.Win32;
    public class Cmd
    {
        #region Methods
        public void SetTheme()
        {
            using (var console = Registry.CurrentUser.OpenSubKey("Console", true))
            {
                if (console == null)
                    return;
                var index = 0;
                console.SetValue(GetName(index++), ColorScheme.BackgroundDefault);
                console.SetValue(GetName(index++), ColorScheme.PrimaryContent);
                console.SetValue(GetName(index++), ColorScheme.SecondaryContent);
                console.SetValue(GetName(index++), ColorScheme.EmphasizedContent);
                console.SetValue(GetName(index++), nameof(Palette.Orange));
                console.SetValue(GetName(index++), nameof(Palette.Violet));
                console.SetValue(GetName(index++), ColorScheme.MiddleGray);
                console.SetValue(GetName(index++), ColorScheme.Highlight1);
                console.SetValue(GetName(index++), ColorScheme.BackgroundHighlight);
                console.SetValue(GetName(index++), nameof(Palette.Blue));
                console.SetValue(GetName(index++), nameof(Palette.Green));
                console.SetValue(GetName(index++), nameof(Palette.Cyan));
                console.SetValue(GetName(index++), nameof(Palette.Red));
                console.SetValue(GetName(index++), nameof(Palette.Magenta));
                console.SetValue(GetName(index++), nameof(Palette.Yellow));
                console.SetValue(GetName(index), ColorScheme.Highlight2);
            }
        }
        private static string GetName(int index) => $"ColorTable{index:00}";
        #endregion
    }
}