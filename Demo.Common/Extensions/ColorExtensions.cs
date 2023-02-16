using System.Collections.Generic;
using System.Drawing;

namespace Demo.Common.Extensions
{
    public static class ColorExtensions
    {
        public static string ColorToHexString(this Color color)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb()));
        }

        public static Color[] GetStandardColorArrayMatlab(this Color value)
        {
            List<Color> colors = new List<Color>();
            colors.Add(Color.FromArgb(127, 0, 0));
            colors.Add(Color.FromArgb(255, 2, 0));
            colors.Add(Color.FromArgb(255, 131, 0));
            colors.Add(Color.FromArgb(249, 255, 6));
            colors.Add(Color.FromArgb(119, 255, 135));
            colors.Add(Color.FromArgb(0, 244, 255));
            colors.Add(Color.FromArgb(0, 115, 255));
            colors.Add(Color.FromArgb(0, 0, 240));
            colors.Add(Color.FromArgb(0, 0, 127));
            return colors.ToArray();
        }

        public static Color[] GetStandardColorArray(this Color value)
        {
            List<Color> colors = new List<Color>();
            colors.Add(Color.White);
            colors.Add(Color.FromArgb(236, 246, 248));
            colors.Add(Color.Green);
            colors.Add(Color.Yellow);
            colors.Add(Color.Red);
            colors.Add(Color.Violet);
            colors.Add(Color.Blue);
            colors.Add(Color.Magenta);
            colors.Add(Color.Orange);
            colors.Add(Color.Gray);
            return colors.ToArray();
        }

    }
}
