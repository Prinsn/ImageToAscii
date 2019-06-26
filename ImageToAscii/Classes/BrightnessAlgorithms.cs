using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToAscii
{
    public static class BrightnessAlgorithms
    {
        public static int Average(Color color)
        {
            return (color.R + color.G + color.B) / 3;
        }

        public static int Lightness(Color color)
        {
            var vals = new[] { color.R, color.G, color.B };
            return (vals.Max() + vals.Min()) / 2;
        }

        public static int Luminosity(Color color)
        {
            return (int) Math.Round(0.21 * color.R + 0.72 * color.G + 0.07 * color.B);
        }        
    }
}
