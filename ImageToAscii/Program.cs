using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToAscii
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string inputval;
            if (args.Length > 0)
            {
                inputval = args[0];
            }
            else
            {
                Console.WriteLine("Brightness options: [1] Average RGB, [2] Lightness, [3] Luminosity\n");
                inputval = Console.ReadLine();
            }

            Func<Color, int> alg = GetAlg(inputval);

            try
            {
                var fileDia = new OpenFileDialog();
                //fileDia.InitialDirectory = "%userprofile%/Pictures";
                if (fileDia.ShowDialog() == DialogResult.OK)
                {
                    var image = (Bitmap)Bitmap.FromFile(fileDia.FileName);
                    HandleForImage(image);
                    Console.Write(new Printer(alg).ImageToString(image));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something broke: " + e.Message);
            }

            Console.WriteLine("\nFin.");
            Console.ReadLine();
        }

        private static Func<Color, int> GetAlg(string v)
        {
            switch (v)
            {
                case "1":
                    return BrightnessAlgorithms.Average;
                case "2":
                    return BrightnessAlgorithms.Lightness;
                case "3":
                    return BrightnessAlgorithms.Luminosity;                                     
                default:
                    Console.WriteLine($"Did not understand \"{v}\", using Average RGB");
                    goto case "1";
            }            
        }

        private static void HandleForImage(Image image)
        {
            if(image.Width > Console.BufferWidth)
            {
                //Offset of 1 for scrollbars
                var newWidth = Math.Min(image.Width + 1, Console.LargestWindowWidth);
                if (newWidth == Console.LargestWindowWidth)
                {
                    Task.Run(() => MessageBox.Show($"Image width {image.Width} is possibly too large for this window, try decreasing the font size"));
                }

                Console.WindowWidth = newWidth;
            }

            Console.WindowHeight = Console.LargestWindowHeight;
            Console.BufferHeight = 4000;
        }
    }
}
