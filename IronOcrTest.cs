using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronOcr;
using IronOcr.Languages;

namespace TesseractTest.OCR
{
    class IronOcrTest : IRecognize
    {
        public string TextResult { get; private set; }

        public TimeSpan Recognize(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new ArgumentException("путь к картинке не существует.");
            }

            var sw = new Stopwatch();
            sw.Start();

            var ocr = new AutoOcr()
            {
                Language = Russian.OcrLanguagePack,
                ReadBarCodes = true
            };
            var result = ocr.Read(imagePath);
            TextResult = result.Text;

            sw.Stop();
            return sw.Elapsed;
        }
    }
}
