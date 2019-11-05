using System;
using System.Collections.Generic;
using System.IO;
using TesseractPatagamesTest.OCR;
using TesseractTest.OCR;

namespace TesseractPatagamesTest
{
    class Program
    {
        static List<IRecognize> recognizers =
            new List<IRecognize>()
            {
                new EmguOcr(),
                new TesseractTest.OCR.TesseractPatagamesTest(),
            };

        static void Main(string[] args)
        {
            var starter = new MultithreadedClassisifer();
            starter.StartThreads();
        
            Console.ReadLine();
        }

        static void PatagamesMassTest()
        {
            var dir = new DirectoryInfo("C:\\Users\\d.kolesov\\source\\repos\\Tests\\TesseractTest\\test_data");
            var items = dir.GetFiles("*.tiff");

            var recognizer = new TesseractTest.OCR.TesseractPatagamesTest();
            foreach (var fileInfo in items)
            {
                var result = recognizer.Recognize(fileInfo.FullName);
                Console.WriteLine("Recognizer:: " + recognizer.GetType().FullName);
                Console.WriteLine("Seconds:: " + result.TotalSeconds);
                Console.WriteLine("Resulted text::" + recognizer.TextResult);
                Console.WriteLine();
            }
        }

        static void PatagamesSingleTest()
        {
            var recognizer = new TesseractTest.OCR.TesseractPatagamesTest();
            var result = recognizer.Recognize("C:\\Users\\d.kolesov\\Downloads\\договор перевода.tiff");
            Console.WriteLine("Recognizer:: " + recognizer.GetType().FullName);
            Console.WriteLine("Seconds:: " + result.TotalSeconds);
            Console.WriteLine("Resulted text::" + recognizer.TextResult);
            Console.WriteLine();
        }

        static void MassTest()
        {
            var dir = new DirectoryInfo("C:\\Users\\d.kolesov\\source\\repos\\Tests\\TesseractTest\\test_data");
            var items = dir.GetFiles("*.tiff");

            var resultString = new List<string>();
            using (var sw = new StreamWriter("massTest_output.txt"))
            {
                foreach (var recognizer in recognizers)
                {
                    foreach (var fileInfo in items)
                    {
                        var result = recognizer.Recognize(fileInfo.FullName);
                        var textResult = "Recognizer:: " + recognizer.GetType().FullName + Environment.NewLine
                                         + "Seconds:: " + result.TotalSeconds + Environment.NewLine
                                         + "Item:: " + fileInfo.Name + Environment.NewLine
                                         + "Resulted text::" + recognizer.TextResult + Environment.NewLine
                            + "=============================================================" + Environment.NewLine;
                        Console.WriteLine(textResult);
                        sw.WriteLine(textResult);
                        resultString.Add("Item:: " + fileInfo.Name + "|| Seconds:: " + result.TotalSeconds + "|| Recognizer:: " + recognizer.GetType().FullName);
                    }
                }

                resultString.ForEach(x => sw.WriteLine(x));
                sw.Close();
            }
        }
    }
}

