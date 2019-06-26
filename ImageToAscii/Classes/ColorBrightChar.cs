using System.Drawing;

namespace ImageToAscii
{
    /// <summary>
    /// Wrapper for "color ascii" pixel
    /// </summary>
    public class ColorBrightChar
    {
        /// <summary>
        /// Brightness percentage, probably analagous to alpha with no transparency
        /// </summary>
        public int Brightness { get; set; }
        public Color Color { get; set; }
        public char Char { get; set; }
    }
}
