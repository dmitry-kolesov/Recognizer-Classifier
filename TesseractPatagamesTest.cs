using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patagames.Ocr;
using Patagames.Ocr.Enums;

namespace TesseractTest.OCR
{
    public class TesseractPatagamesTest : IRecognize
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
            var api = OcrApi.Create();
            api.Init(new Languages[] { Languages.Russian, Languages.English });

            //var renderer = OcrTextRenderer.Create("output");
            //api.ProcessPages(imagePath, null, 0, renderer);
            //api.Clear();
            //api.SetImage(OcrPix.FromFile( imagePath));
            //api.Recognize();
            TextResult = api.GetTextFromImage(imagePath);//.GetUtf8Text();
            //TextResult = Encoding.Unicode.GetString( Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Encoding.UTF8.GetBytes(TextResult)));
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
