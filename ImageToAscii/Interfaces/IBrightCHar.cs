using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToAscii.Interfaces
{
    /// <summary>
    /// Interfaces for classes that need to supply some way for converting a brightness integer to a character value
    /// </summary>
    public interface IBrightChar
    {
        char BrightToAscii(int brightness);
    }
}
