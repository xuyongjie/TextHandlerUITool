using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextHandleUITool.Load
{
    class TextFileLoader : ILoader
    {
        private string _filePath;
        public TextFileLoader(string filePath)
        {
            _filePath = filePath;
        }
        public string load()
        {
            using (var fileStream = new FileStream(_filePath, FileMode.Open))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
