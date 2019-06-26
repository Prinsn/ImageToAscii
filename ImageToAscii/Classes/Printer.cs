using ImageToAscii.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToAscii
{
    public class Printer : IBrightChar
    {
        private const string ASCII_MAP = "`^\",:;Il!i~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
        private static readonly double RELATIVE_BRIGHTNESS = ASCII_MAP.Length / 255.0;

        private Func<Color, int> _brightnessAlgorithm { get; set; }
        private bool brightBackground { get; set; }

        public Printer(Func<Color, int> brightnessAlg, bool printOnBright = false)
        {
            _brightnessAlgorithm = brightnessAlg;
            brightBackground = printOnBright;
        }

        public char BrightToAscii(int brightness)
        {
            var relBright = brightBackground ? 1 - RELATIVE_BRIGHTNESS : RELATIVE_BRIGHTNESS; 
            var newBright = brightness * relBright;            
            var index = Math.Max(0, (int)newBright - 1);
            return ASCII_MAP[index];
        }

        public string ImageToString(Bitmap image)
        {
            var convertedImage = image.ImageToAscii(_brightnessAlgorithm, BrightToAscii);
            var sb = new StringBuilder();
            CharMapIterator(convertedImage, sb.Append);

            return sb.ToString();
        }

        /// <summary>
        /// This just looks more interesting to watch render
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public void SlowConsolePrint(Bitmap image)
        {
            var convertedImage = image.ImageToAscii(_brightnessAlgorithm, BrightToAscii);
            CharMapIterator(convertedImage, s => { Console.Write(s); return null; });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ascii"></param>
        /// <param name="writer"></param>
        /// <param name="rotate45Deg">Changes printing from Row by Row to Column by Row (ie 45 degree rotation)</param>
        /// <returns></returns>
        private string CharMapIterator(char[,] ascii, Func<string, object> writer, bool rotate45Deg = false)
        {
            var outter = rotate45Deg ? ascii.GetLength(0) : ascii.GetLength(1);
            var inner = rotate45Deg ? ascii.GetLength(1) : ascii.GetLength(0);

            for (var y = 0; y < outter; y++)
            {
                for (var x = 0; x < inner; x++)
                {                    
                    writer(ascii[x, y] + "");                    
                }
                writer(Environment.NewLine);
            }

            return "";
        }
    }
}
