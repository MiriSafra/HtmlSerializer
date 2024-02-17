using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace HtmlSerializer
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance=new HtmlHelper();
        public static HtmlHelper Instance=> _instance;
        public string[] HtmlVoidTags { get; set; }
        public string[] HtmlTags { get; set; }
        private HtmlHelper()
        {
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Json/HtmlVoidTags.json"));
            HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Json/HtmlTags.json"));
        }
    }
}
