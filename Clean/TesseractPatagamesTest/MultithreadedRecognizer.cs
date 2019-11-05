using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TesseractPatagamesTest.OCR;

namespace TesseractPatagamesTest
{
    class MultithreadedRecognizer
    {
        readonly List<Thread> threadPool = new List<Thread>();

        public void StartThreads()
        {
            foreach (var pathThreadse in MultithreadedPathsMap.Paths)
            {
                if (pathThreadse.MaxThreadCnt == 1)
                {
                    var thread = new Thread(new ParameterizedThreadStart(DirWalker));
                    thread.Start(new ThreadStartParameter()
                    {
                        Path = pathThreadse.Path,
                        StartIndex = 0
                    });

                    threadPool.Add(thread);
                }
                else
                {
                    var dir = new DirectoryInfo(pathThreadse.Path);
                    if (!dir.Exists)
                    {
                        throw new ArgumentException($"{dir.FullName} is not exists");
                    }

                    var filesCount = dir.GetFiles("*.tif*").Length;

                    for (int i = 0; i < pathThreadse.MaxThreadCnt; i++)
                    {
                        var thread = new Thread(new ParameterizedThreadStart(DirWalker));
                        thread.Start(new ThreadStartParameter()
                        {
                            Path = pathThreadse.Path,
                            StartIndex = filesCount / pathThreadse.MaxThreadCnt * i,
                            EndIndex = Math.Min(filesCount / pathThreadse.MaxThreadCnt * (i + 1), filesCount)
                        });

                        threadPool.Add(thread);
                    }
                }
            }
        }

        public void ThreadsWatcher()
        {

        }

        private void DirWalker(object startParameter)
        {
            var param = (ThreadStartParameter)startParameter;
            var recognizer = new EmguOcr();
            var dir = new DirectoryInfo(param.Path);
            if (!dir.Exists)
            {
                Console.WriteLine($"ERROR :: {dir.FullName} is not exists");
            }

            var files = dir.GetFiles("*.tif*");
            if (param.EndIndex == 0)
            {
                param.EndIndex = files.Length;
            }

            for (int i = param.StartIndex; i < param.EndIndex; i++)
            {
                try
                {
                    var extension = ".tiff";
                    var tifExtension = ".tif";
                    if (!files[i].FullName.Contains(extension)
                        && files[i].FullName.Contains(tifExtension))
                    {
                        extension = tifExtension;
                    }

                    var txtFilePath = files[i].FullName.Replace(extension, ".txt");

                    if (File.Exists(txtFilePath))
                    {
                        //already recognized
                        Console.WriteLine($"{txtFilePath} already exists");
                        continue;
                    }

                    var result = recognizer.Recognize(files[i].FullName);
                    using (var sw = new StreamWriter(txtFilePath))
                    {
                        sw.WriteLine(recognizer.TextResult);
                        sw.Close();
                    }

                    Console.WriteLine($"{dir.Name} :: {i} of {param.EndIndex - param.StartIndex} :: {result.Seconds} sec");
                    Thread.Sleep(50);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR :: {e}");
                }
            }
        }

        private void RecognizeFile(string filePath)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
