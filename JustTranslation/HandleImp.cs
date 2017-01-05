using ITextHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustTranslation
{
    public class HandleImp : IHandler
    {
        public const string NAME = "Auto Translate by YOUDAO";
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
                return "input null";
            }
            ITranslator translator = new YoudaoTranslator();
            var array = resource.Split(new char[] { '\n' });
            StringBuilder builder = new StringBuilder();
            foreach(var item in array)
            {
                string translation = translator.Translate(item);
                builder.Append(translation).Append("\n");
            }
            return builder.ToString();
        }
    }
}
