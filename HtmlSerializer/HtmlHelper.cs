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
        public string[] SelfClosingTags { get; set; }
        public string[] AllTags { get; set; }
        private HtmlHelper()
        {
            SelfClosingTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Json/SelfClosingTags.json"));
            AllTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("Json/AllTags.json"));
        }
    }
}
