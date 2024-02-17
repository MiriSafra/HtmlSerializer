using HtmlSerializer;
using System.Text.RegularExpressions;

//var url = "https://forum.netfree.link/category/1/%D7%94%D7%9B%D7%A8%D7%96%D7%95%D7%AA?";
//var serializer = new Serializer();
//var htmlTree = await serializer.HtmlSerializer(url);
//Selector selector = new Selector();
//selector.ConvertToSelector("ul.nav.navbar-nav");
//var result = htmlTree.FindElements(selector);
//result.ToList().ForEach(e => Console.WriteLine(e.ToString()));
string selectorString = "#main .important";
Selector selector = new Selector();
selector.ConvertToSelector(selectorString);
Console.WriteLine("-----------------" + string.Join(",", selector.Id));

