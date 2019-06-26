using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToAscii
{
    public static class Converter
    {
        /// <summary>
        /// Converts image to array of colors: "pixels"
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Color[,] ToPixels(this Bitmap image)
        {
            return image.PixelMapper(pixel => pixel);
        }

        /// <summary>
        /// Converts image to array of "brightnesses" based on provided function
        /// </summary>
        /// <param name="image"></param>
        /// <param name="brightAlg">Brightness algorithm for converting color to int</param>
        /// <returns></returns>
        public static int[,] ImageToBrightness(this Bitmap image, Func<Color, int> brightAlg)
        {
            return PixelMapper(image, brightAlg);
        }

        /// <summary>
        /// Converts image to array of characters, based on proivded functions
        /// </summary>
        /// <param name="image"></param>
        /// <param name="brightAlg">Brightness algorithm for converting color to int</param>
        /// <param name="charBrightMap">Algorithm for converting a brightness to an ascii character</param>
        /// <returns></returns>
        public static char[,] ImageToAscii(this Bitmap image, Func<Color, int> brightAlg, Func<int, char> charBrightMap)
        {
            return PixelMapper(image, pixel =>
            {
                var bright = brightAlg(pixel);
                return charBrightMap(bright);
            });
        }

        /// <summary>
        /// Converts image to array of {brightness, based on proivded functions
        /// </summary>
        /// <param name="image"></param>
        /// <param name="brightAlg">Brightness algorithm for converting color to int</param>
        /// <param name="charBrightMap">Algorithm for converting a brightness to an ascii character</param>
        /// <returns></returns>
        public static ColorBrightChar[,] ImageToColorAscii(this Bitmap image, Func<Color, int> brightAlg, Func<int, char> charBrightMap)
        {
            return PixelMapper(image, pixel =>
            {
                var bright = brightAlg(pixel);
                var c = charBrightMap(bright);
                return new ColorBrightChar()
                {
                    Brightness = bright,
                    Color = pixel,
                    Char = c
                };
            });
        }     
        
        private static TOut[,] PixelMapper<TOut>(this Bitmap image, Func<Color, TOut> predicate)
        {
            var height = image.Height;
            var width = image.Width;
            var ascii = new TOut[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    ascii[x, y] = predicate(image.GetPixel(x, y));
                }
            }

            return ascii;
        }
    }
}
