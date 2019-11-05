using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesseractTest.OCR
{
    interface IRecognize
    {
        string TextResult { get; }

        TimeSpan Recognize(string imagePath);
    }
}
