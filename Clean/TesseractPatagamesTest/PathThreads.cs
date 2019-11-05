using System.Collections.Generic;

namespace TesseractPatagamesTest
{
    class PathThreads
    {
        public string Path { get; set; }
        public int MaxThreadCnt { get; set; }

        public List<List<string>> Conditions { get; set; }

        public DocumentType DocumentType  { get; set; }
    }
}