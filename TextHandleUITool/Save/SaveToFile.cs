using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextHandleUITool.Save
{
    class SaveToFile : ISave
    {
        private string _filePath;
        public SaveToFile(string filePath)
        {
            _filePath = filePath;
        }
        public void Save(string result)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result)))
            {
                using (FileStream filestream = new FileStream(_filePath, FileMode.OpenOrCreate))
                {
                    stream.CopyTo(filestream);
                    filestream.Flush();
                }
            }
        }
    }
}
