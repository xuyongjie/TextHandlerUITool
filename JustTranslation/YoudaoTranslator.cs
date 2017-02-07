﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JustTranslation
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
                StringBuilder builder = new StringBuilder();
                int index = 0;
                foreach(var trans in Translation)
                {
                    if(index>0)
                    {
                        builder.Append(" ");
                    }
                    builder.Append(trans);
                    index++;
                }
                return builder.ToString();
            }
            else
            {
                return "translate_error";
            }
        }
    }
}
