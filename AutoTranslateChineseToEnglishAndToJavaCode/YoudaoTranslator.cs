using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoTranslateChineseToEnglishAndToJavaCode
{
    public class YoudaoTranslator : ITranslator
    {
        public string Name
        {
            get
            {
                return "Youdao translate provider";
            }
        }

        public string Translate(string origin)
        {
            return DoTranslate(origin).Result;
        }

        private async Task<string> DoTranslate(string origin)
        {
            if(string.IsNullOrEmpty(origin))
            {
                return "origin_empty";
            }
            string translateUrl = $"http://fanyi.youdao.com/openapi.do?keyfrom=TextHandleUITool&key=683159649&type=data&doctype=json&version=1.1&q={origin}";

            var request = WebRequest.Create(translateUrl);
            request.Method = "GET";
            var response=request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result=await reader.ReadToEndAsync();
            var responseEntity=JsonConvert.DeserializeObject<ResponseEntity>(result);
            return responseEntity.GetTranslation();
        }
    }
    [JsonObject]
    public class ResponseEntity
    {
        [JsonProperty(PropertyName = "translation")]
        public List<string> Translation { get; set; }
        [JsonProperty(PropertyName = "errorCode")]
        public int ErrorCode { get; set; }
        public string GetTranslation()
        {
            if(ErrorCode==0&&Translation!=null&&Translation.Count>0)
            {
                string item = Translation[0];
                item = item.TrimStart(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                item = item.Trim();
                item = item.Substring(0, item.Length > 30 ? 30 : item.Length);
                item = item.Replace(',', '_');
                item = item.Replace(' ', '_');
                item = item.Replace('-', '_');
                return item;
            }
            else
            {
                return "translate_error";
            }
        }
    }
}
