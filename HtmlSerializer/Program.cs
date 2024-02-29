using HtmlSerializer;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
Serializer htmlSerializer = new Serializer();

var html =await htmlSerializer.Load("https://forum.netfree.link/category/1/%D7%94%D7%9B%D7%A8%D7%96%D7%95%D7%AA?");
HtmlElement htmlElement= htmlSerializer.BuildHtmlTree(html);
Selector selector = Selector.Parse("ul.nav.navbar-nav");
var result=HtmlElementExtensions.FindElementsBySelector(htmlElement, selector);
foreach (var item in result)
{
    Console.Write(item.Name + " " + item.Id + " ");
	foreach (var c in item.Classes)
	{
		Console.Write(c+" ");
	}
	Console.WriteLine();
}