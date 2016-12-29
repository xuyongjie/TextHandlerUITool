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
            if(string.IsNullOrEmpty(resource))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            var array = resource.Split(new char[] { '\n' });
            foreach(var item in array)
            {
                var subArr = item.Split(new char[] { '\t','\r' });
                if (subArr != null && subArr.Length > 1)
                {
                    builder.Append($"<string name=\"{subArr[0]}\">{subArr[1]}</string>\n");
                }
            }
            return builder.ToString();
        }
    }
}
