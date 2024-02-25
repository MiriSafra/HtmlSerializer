using HtmlSerializer;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

var url = "https://forum.netfree.link/category/1/%D7%94%D7%9B%D7%A8%D7%96%D7%95%D7%AA?";
var serializer = new Serializer();
var htmlTree = await serializer.HtmlSerializer(url);


//// יצירת Selector לחיפוש רקע ב-HTML
//var selector = new HtmlSerializer.Selector("div", "background");

//// מציאת אלמנטים העונים על ה-Selector בעץ DOM
//var backgroundElements = htmlTree.FindElements(selector);

//// הדפסת כל אלמנט שנמצא על ידי ה-Selector
//foreach (var element in backgroundElements)
//{
//    Console.WriteLine($"Found element with ID: {element.Id} and class: {string.Join(",", element.Classes)}");
//}
string queryString = "ul.nav.navbar-nav";
Selector selector = Selector.FromQueryString(queryString);
Console.WriteLine($"TagName: {selector.Name}");
Console.WriteLine($"id: {selector.Id}");
for (int i = 0; i < selector.Classes.Count; i++)
    Console.WriteLine($"classes:{selector.Classes[i]}");
///


////Selector selector = new Selector();
////selector.ConvertToSelector("ul.nav.navbar-nav");
//string queryString = "ul.nav.navbar-nav";

var result = htmlTree.FindElements(selector);
result.ToList().ForEach(e => Console.WriteLine(e.ToString()));
////string selectorString = "#main .important";
//if (htmlTree != null)
//{
//    PrintHtmlElements(htmlTree);
//}
//else
//{
//    Console.WriteLine("Failed to serialize the HTML content from the specified URL.");
//}

//void PrintHtmlElements(HtmlElement element)
//{
//    Console.WriteLine($"Element tag: {element.Name}");

//    if (element.Attributes != null)
//    {
//        Console.WriteLine($"Element attributes: {string.Join(", ", element.Attributes.Select(a => $"{a.Key}='{a.Value}'"))}");
//    }
//    else
//    {
//        Console.WriteLine("Element has no attributes.");
//    }

//    if (element.Children != null)
//    {
//        foreach (var child in element.Children)
//        {
//            PrintHtmlElements(child);
//        }
//    }
//}


//var selectorString = "div#mydiv"; // דוגמה למחרוזת המייצצגת סלקטור
//var selector = Selector.FromQueryString(selectorString); // המרת המחרוזת לאובייקט מסוג Selector
//var matchingElements = htmlTree.FindElements(selector); // חיפוש אלמנטים בעץ ה-HTML שעונים לסלקטור

//foreach (var element in matchingElements)
//{
//    Console.WriteLine($"Element tag: {element.Name}");
//    Console.WriteLine($"Element attributes: {string.Join(", ", element.Attributes)}");
//    Console.WriteLine($"Element classes: {string.Join(", ", element.Classes)}");
//    Console.WriteLine($"Element inner HTML: {element.InnerHtml}");
//    Console.WriteLine();
//}


//Console.WriteLine("-----------------" + selector.GetRootSelector().TagName);
//var rootSelector = selector.GetRootSelector();
//if (rootSelector != null)
//{
//    Console.WriteLine("-----------------" + rootSelector.TagName);
//}
//else
//{
//    Console.WriteLine("Root selector not found.");
//}

//string queryString = "div#main .container .item.active";
//Selector rootSelector = Selector.FromQueryString(queryString);
//Selector currentSelector = rootSelector;

//while (currentSelector != null)
//{

//    Console.WriteLine($"TagName: {currentSelector.TagName}, Id: {currentSelector.Id}, Classes: {string.Join(", ", currentSelector.Classes)}");
//    currentSelector = currentSelector.Children.Count > 0 ? currentSelector.Children[0] : null;
//}