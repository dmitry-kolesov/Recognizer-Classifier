using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Tesseract;


namespace TesseractTest.OCR
{
    public class TesseractWeldTest :IRecognize
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

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = Path.Combine(path, "tessdata");
            path = path.Replace("file:\\", "");
            using (var engine = new TesseractEngine(path, "rus", EngineMode.Default))
            {

                engine.SetVariable("tessedit_char_whitelist", "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеЁжзийклмнопрстуфхшщчцэюяъьы");
                engine.SetVariable("tessedit_unrej_any_wd", false);
                
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        TextResult = page.GetText();
                    }
                }
            }

            sw.Stop();
            return sw.Elapsed;
        }
    }
}
