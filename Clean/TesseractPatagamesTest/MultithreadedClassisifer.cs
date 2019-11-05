using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace TesseractPatagamesTest
{
    class MultithreadedClassisifer
    {
        readonly List<Thread> threadPool = new List<Thread>();
        private const string FileMask = "*.txt";

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

                    var filesCount = dir.GetFiles(FileMask).Length;

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
            var dir = new DirectoryInfo(param.Path);
            if (!dir.Exists)
            {
                Console.WriteLine($"ERROR :: {dir.FullName} is not exists");
            }

            var files = dir.GetFiles(FileMask);
            if (param.EndIndex == 0)
            {
                param.EndIndex = files.Length;
            }

            var ouputPath = Path.Combine(dir.Parent.FullName, $"otput_{dir.Name}_{param.StartIndex}-{param.EndIndex}.txt");
            using (var sw = new StreamWriter(ouputPath))
            {
                for (int i = param.StartIndex; i < param.EndIndex; i++)
                {
                    var finalString = "";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var inputPath = files[i].FullName;
                    try
                    {
                        if (!File.Exists(inputPath))
                        {
                            Console.WriteLine($"{inputPath} doesnt exists");
                            continue;
                        }

                        string inputText = string.Empty;
                        using (var sr = new StreamReader(inputPath))
                        {
                            inputText = sr.ReadToEnd();
                            sr.Close();
                        }

                        var splitted = inputText.Split(' ');

                        double finalCoefs = 0.0;
                        sw.Write($"{files[i].Name}");
                        finalString += $"{files[i].Name}";
                        foreach (var documentTypeGroup in MultithreadedPathsMap.ConditionsMap)
                        {
                            var intermedaiteCoefs = new List<double>();
                            foreach (var pathThreadseCondition in documentTypeGroup.Conditions)
                            {
                                intermedaiteCoefs.Add(splitted.Max(s => pathThreadseCondition.Max(x =>
                                    TanimotoStringComparer.Tanimoto(x, s, 1.3))));
                            }

                            var conditionStrinResult = $";\t{documentTypeGroup.Path} == {finalCoefs.ToString("F4")}";
                            finalCoefs = intermedaiteCoefs.Average();
                            sw.Write(conditionStrinResult);
                            finalString += conditionStrinResult;
                        }

                        sw.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR :: {e}");
                    }

                    stopwatch.Stop();

                    Console.WriteLine(
                        $"{dir.Name} :: {i} of {param.EndIndex - param.StartIndex} :: {stopwatch.ElapsedMilliseconds} msec");
                    Console.WriteLine(finalString);
                    Thread.Sleep(20);
                }

                sw.Close();
            }
        }
    }
}
