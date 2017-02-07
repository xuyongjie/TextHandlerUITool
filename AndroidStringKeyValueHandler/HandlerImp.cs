using ITextHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidStringKeyValueHandler
{
    public class HandlerImp : IHandler
    {
        public const string NAME = "key value to Android xml string item";
        public string Name
        {
            get
            {
                return NAME;
            }
        }

        public string Handle(string resource)
        {
            if (string.IsNullOrEmpty(resource))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            var array = resource.Split(new char[] { '\n' });
            foreach (var item in array)
            {
                string temp = item.Trim();
                temp = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(temp, " ");
                int splitIndex = -1;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == ' ')
                    {
                        splitIndex = i;
                        break;
                    }
                }
                if (splitIndex != -1)
                {
                    builder.Append($"<string name=\"{temp.Substring(0, splitIndex).Trim()}\">{temp.Substring(splitIndex).Trim()}</string>\n");
                }
            }
            return builder.ToString();
        }
    }
}
