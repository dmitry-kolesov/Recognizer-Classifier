using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.OCR;
using TesseractTest.OCR;

namespace TesseractPatagamesTest.OCR
{
    class EmguOcr : IRecognize
    {
        private Tesseract engine;

        public string TextResult { get; private set; }


        public EmguOcr()
        {
            engine = new Emgu.CV.OCR.Tesseract("tessdata", "rus", OcrEngineMode.Default);
        }

        public TimeSpan Recognize(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                throw new ArgumentException("путь к картинке не существует.");
            }

            var sw = new Stopwatch();
            sw.Start();
            
            var img = new Image<Emgu.CV.Structure.Gray, byte>(imagePath);

            engine.SetImage(img);
            engine.Recognize();
            var result = engine.GetCharacters().ToList();

            var sb = new StringBuilder();

            result.ForEach(c => sb.Append(c.Text));

            TextResult = sb.ToString();

            sw.Stop();
            return sw.Elapsed;
        }
    }
}
