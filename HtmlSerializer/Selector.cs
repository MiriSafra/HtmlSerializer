//using System;
//using System.Collections.Generic;

//namespace HtmlSerializer
//{
//    public class Selector
//    {
//        public string TagName { get; set; }
//        public string Id { get; set; }
//        public List<string> Classes { get; set; }
//        public Selector Parent { get; set; }
//        public List<Selector> Children { get; set; }

//        public Selector(string tagName = null, string id = null, List<string> classes = null, Selector parent = null)
//        {
//            TagName = tagName;
//            Id = id;
//            Classes = classes ?? new List<string>();
//            Parent = parent;
//            Children = new List<Selector>();
//        }

//        public static Selector FromQueryString(string queryString)
//        {
//            var selectors = queryString.Split(' ');
//            Selector rootSelector = null;
//            Selector currentSelector = null;

//            foreach (var selectorString in selectors)
//            {
//                var tagName = "";
//                var id = "";
//                var classes = new List<string>();

//                var parts = selectorString.Split('#');
//                tagName = parts[0];

//                if (parts.Length > 1)
//                {
//                    var idAndClasses = parts[1].Split('.');
//                    id = idAndClasses[0];
//                    if (idAndClasses.Length > 1)
//                        classes.AddRange(idAndClasses[1..]);
//                }
//                else if (selectorString.Contains('.'))
//                {
//                    parts = selectorString.Split('.');
//                    classes.AddRange(parts[1..]);
//                }

//                // Check if tagName contains invalid characters
//                if (tagName.Contains('.') || tagName.Contains('#'))
//                {
//                    Console.WriteLine($"Invalid TagName: {tagName}");
//                    tagName = "";
//                }

//                var newSelector = new Selector(tagName, id, classes);

//                if (rootSelector == null)
//                {
//                    rootSelector = newSelector;
//                    currentSelector = rootSelector;
//                }
//                else
//                {
//                    currentSelector.Children.Add(newSelector);
//                    newSelector.Parent = currentSelector;
//                    currentSelector = newSelector;
//                }
//            }

//            return rootSelector;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlSerializer
{
    public class Selector
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public List<Selector> Children { get; set; }

        public Selector(string name = null, string id = null, List<string> classes = null, Selector parent = null)
        {
            Name = name;
            Id = id;
            Classes = classes ?? new List<string>();
            Parent = parent;
            Children = new List<Selector>();
        }

        public static Selector FromQueryString(string queryString)
        {
            var parts = Regex.Split(queryString, @"(?=[#.])");

            var root = new Selector();
            Selector current = root;

            foreach (var part in parts)
            {
                var trimmedPart = part.Trim();
                if (trimmedPart == "")
                    continue;

                if (char.IsDigit(trimmedPart[0]))
                {
                    throw new ArgumentException("Invalid query string");
                }

                if (char.IsLetter(trimmedPart[0]))
                {
                    var selector = new Selector
                    {
                        Name = trimmedPart
                    };

                    current.Children.Add(selector);
                    selector.Parent = current;
                    current = selector;
                }
                else if (trimmedPart[0] == '#')
                {
                    current.Id = trimmedPart.Substring(1);
                }
                else if (trimmedPart[0] == '.')
                {
                    current.Classes.Add(trimmedPart.Substring(1));
                }
            }

            return root.Children.First();
        }
    }
}






//public string Id { get; set; }
//public string TagName { get; set; }
//public List<string> Classes { get; set; }
//public Selector Parent { get; set; }
//public List<Selector> Children { get; set; }



////public void ConvertToSelector(string strQuery)
////{
////    // Initialize list of selectors
////    var query = strQuery.Split(' ');
////    Selector parentSelector = null;

////    foreach (var str in query)
////    {
////        var currentSelector = new Selector();

////        // Check if the part contains an ID
////        if (str.Contains("#"))
////        {
////            var startIndex = str.IndexOf("#");
////            var endIndex = str.IndexOfAny(new char[] { '.', ' ' }, startIndex);
////            if (endIndex >= 0)
////            {
////                currentSelector.Id = str.Substring(startIndex + 1, endIndex - startIndex - 1);
////            }
////        }

////        // Check if the part contains classes
////        if (str.Contains("."))
////        {
////            currentSelector.Classes = new List<string>();
////            var startIndex = 0;
////            while (startIndex < str.Length && str.IndexOf(".", startIndex) != -1)
////            {
////                startIndex = str.IndexOf(".", startIndex);
////                var endIndex = str.IndexOfAny(new char[] { '.', ' ', '#' }, startIndex);
////                if (endIndex >= 0)
////                {
////                    var classLength = endIndex - startIndex - 1;
////                    if (classLength >= 0)
////                    {
////                        currentSelector.Classes.Add(str.Substring(startIndex + 1, classLength));
////                    }
////                }
////                startIndex = endIndex;
////            }
////        }

////        // Check if the part represents a valid HTML tag
////        if (HtmlHelper.Instance.HtmlTags.Contains(str) || HtmlHelper.Instance.HtmlVoidTags.Contains(str))
////        {
////            currentSelector.TagName = str;
////        }

////        // Set parent-child relationship
////        if (parentSelector != null)
////        {
////            currentSelector.Parent = parentSelector;
////            if (currentSelector.Parent.Children == null)
////            {
////                currentSelector.Parent.Children = new List<Selector>();
////            }
////            currentSelector.Parent.Children.Add(currentSelector);
////        }

////        parentSelector = currentSelector; // Update parent selector for the next iteration
////    }
////}

//public void ConvertToSelector(string strQuery)
//{
//    // Initialize list of selectors
//    var query = strQuery.Split(' ');
//    Selector parentSelector = null;

//    for(int i=0; i<query.Length; i++)
//    {
//        Console.WriteLine($"Processing string: {query[i]}");
//        var currentSelector = new Selector();
//        if ( query[i][0]!='#' && query[i][0] !='.')
//        {
//            if (query[i].Contains('.')|| query[i].Contains('#'))
//            {
//                TagName = query[i].Substring(0, query[i].IndexOfAny(new char[] { '.', '#' }));

//                Console.WriteLine("tag:: " + TagName.ToString());
//            }
//            else
//            {
//                TagName = query[i];
//                Console.WriteLine("tag:: " + TagName.ToString());
//            }
//        }
//        query[i] = query[i].Replace(TagName, "");
//        Console.WriteLine("tag:: "+TagName);
//        // Check if the part contains an ID
//        if (query[i].Contains("#"))
//        {
//            var endIndex=-1;
//            Console.WriteLine($"Processing string: {query[i]}");
//            var startIndex = query[i].IndexOf("#");
//            if (query[i].Contains("."))
//            {
//                 endIndex = query[i].IndexOf('.', startIndex);
//            }
//            else
//            {
//                 endIndex = query[i].Length;
//            }

//            Console.WriteLine("endindex!!" + endIndex);
//            if (endIndex > 0)
//            {
//                Console.WriteLine("currentIn   :" + currentSelector.Id);
//                currentSelector.Id = query[i].Substring(startIndex + 1, endIndex - startIndex - 1);
//                Console.WriteLine("current  : " + currentSelector.Id);
//            }
//        }
//        Console.WriteLine("current" + currentSelector.Id);
//        // Check if the part contains classes
//        if (query[i][0]=='#')
//        {
//            Console.WriteLine($"Processing string: {query[i]}");
//            currentSelector.Classes = new List<string>();
//            var startIndex = 1;
//            var endIndex = 1;
//            while (startIndex < query[i].Length)
//            {
//                if (startIndex == -1) break; // Exit loop if no more '.' found
//                if (query[i].Substring(2).Contains("#"))
//                {
//                    endIndex = query[i].IndexOfAny(new char[] { '#' }, startIndex);
//                }
//                else
//                {
//                    endIndex= query[i].Length;
//                }
//                if (endIndex > 0)
//                {
//                    //Console.WriteLine("@@@@@@@@@@@@@@");
//                    var classLength = endIndex - startIndex - 1;
//                    if (classLength >= 0)
//                    {
//                        currentSelector.Classes.Add(query[i].Substring(startIndex + 1, classLength));
//                    }
//                    startIndex = endIndex; // Move startIndex after processing endIndex
//                    //Console.WriteLine("!!!!!!!!!!!!!!!!");
//                }
//                else
//                {
//                    // No valid endIndex found, exit loop
//                    break;
//                }
//                if (query[i] != null)
//                    startIndex = 0;
//                else
//                    startIndex = -1;
//            }
//        }

//        // Check if the part represents a valid HTML tag
//        if (HtmlHelper.Instance.HtmlTags.Contains(query[i]) || HtmlHelper.Instance.HtmlVoidTags.Contains(query[i]))
//        {
//            Console.WriteLine("contain");
//            Console.WriteLine($"Processing string: {query[i]}");
//            currentSelector.TagName = query[i];
//        }

//        // Set parent-child relationship
//        if (parentSelector != null)
//        {
//            Console.WriteLine("!!@#$%^&");
//            currentSelector.Parent = parentSelector;
//            if (currentSelector.Parent.Children == null)
//            {
//                Console.WriteLine("miri safra");
//                currentSelector.Parent.Children = new List<Selector>();
//            }
//            Console.WriteLine("miri!!");
//            currentSelector.Parent.Children.Add(currentSelector);
//        }
//        Console.WriteLine("new!!");
//        parentSelector = currentSelector; // Update parent selector for the next iteration
//    }
//}





//public void ConvertToSelector(string strQuery)
//{
//    // Initialize list of selectors
//    var query = strQuery.Split(' ');
//    Selector parentSelector = null;
//    int i = 0;
//    foreach (var str in query)
//    {
//        var currentSelector = new Selector();
//        Console.WriteLine($"Processing string: {str}");
//        // Check if the part contains an ID
//        if (str.Contains("#"))
//        {
//            var startIndex = str.IndexOf("#");
//            var endIndex = str.IndexOfAny(new char[] { '.', ' ' }, startIndex);
//            if (endIndex >= 0)
//            {
//                currentSelector.Id = str.Substring(startIndex + 1, endIndex - startIndex - 1);
//            }
//            Console.WriteLine($"Processing string: {str}");
//        }

//        // Check if the part contains classes
//        if (str.Contains("."))
//        {
//            Console.WriteLine($"Processing string: {str}");
//            currentSelector.Classes = new List<string>();
//            var startIndex = 0;

//            while (startIndex < str.Length && str.IndexOf(".", startIndex) != -1)
//            {
//                Console.WriteLine("while"+i++);
//                startIndex = str.IndexOf(".", startIndex);
//                var endIndex = str.IndexOfAny(new char[] { '.', ' ', '#' }, startIndex);
//                if (endIndex >= 0)
//                {
//                    var classLength = endIndex - startIndex - 1;
//                    if (classLength >= 0)
//                    {
//                        currentSelector.Classes.Add(str.Substring(startIndex + 1, classLength));
//                    }
//                    startIndex = endIndex; // Move startIndex after processing endIndex
//                }
//                else
//                {
//                    // No valid endIndex found, exit loop
//                    break;
//                }
//            }
//        }

//        // Check if the part represents a valid HTML tag
//        if (HtmlHelper.Instance.HtmlTags.Contains(str) || HtmlHelper.Instance.HtmlVoidTags.Contains(str))
//        {
//            currentSelector.TagName = str;
//            Console.WriteLine($"Processing string: {str}");
//        }

//        // Set parent-child relationship
//        if (parentSelector != null)
//        {
//            Console.WriteLine($"Processing string: {str}");
//            currentSelector.Parent = parentSelector;
//            if (currentSelector.Parent.Children == null)
//            {
//                currentSelector.Parent.Children = new List<Selector>();
//                Console.WriteLine($"Processing string: {str}");
//            }
//            currentSelector.Parent.Children.Add(currentSelector);
//        }
//        Console.WriteLine("new!!");
//        parentSelector = currentSelector; // Update parent selector for the next iteration
//    }
//}


//public Selector GetRootSelector()
//    {
//        Selector current = this;
//        while (current.Parent != null)
//        {
//            current = current.Parent;
//        }
//        return current;
//    }
//}



//public void ConvertToSelector(string strQuery)
//{
//    // הגדרת רשימת סלקטורים

//    // פיצול מחרוזת השאילתא לפי רווחים
//    var query = strQuery.Split(' ');

//    Selector parentSelector = null;

//    // עבור כל חלק בשאילתא
//    foreach (var str in query)
//    {
//        var currentSelector = new Selector();

//        // בדיקה האם החלק מכיל ID
//        if (str.Contains("#"))
//        {
//            var startIndex = str.IndexOf("#");
//            var endIndex = str.IndexOfAny(new char[] { '.', ' ' }, startIndex);
//            currentSelector.Id = str.Substring(startIndex + 1, endIndex - startIndex - 1);
//        }

//        // בדיקה האם החלק מכיל classes
//        if (str.Contains("."))
//        {
//            currentSelector.Classes = new List<string>();
//            var startIndex = 0;
//            while (startIndex < str.Length && str.IndexOf(".", startIndex) != -1)
//            {
//                startIndex = str.IndexOf(".", startIndex);
//                var endIndex = str.IndexOfAny(new char[] { '.', ' ', '#' }, startIndex);
//                if (endIndex != -1)
//                {
//                    var classLength = endIndex - startIndex - 1;
//                    if (classLength >= 0)
//                    {
//                        currentSelector.Classes.Add(str.Substring(startIndex + 1, classLength));
//                    }
//                }
//                startIndex = endIndex;
//            }
//        }

//        // בדיקה האם החלק מייצג tag HTML תקין
//        if (HtmlHelper.Instance.HtmlTags.Contains(str) || HtmlHelper.Instance.HtmlVoidTags.Contains(str))
//            currentSelector.TagName = str;

//        // קישור הורה-ילד
//        if (parentSelector != null)
//        {
//            currentSelector.Parent = parentSelector;
//            if (currentSelector.Parent.Children == null)
//                currentSelector.Parent.Children = new List<Selector>();
//            currentSelector.Parent.Children.Add(currentSelector);
//        }

//        parentSelector = currentSelector;
//    }
//}



//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace HtmlSerializer
//{
//    public class Selector
//    {
//        public string Id { get; set; }
//        public string TagName { get; set; }
//        public List<string> Classes { get; set; }
//        public Selector Parent { get; set; }
//        public List<Selector> Children { get; set; }

//        public Selector(string strQuery = "")
//        {
//            ConvertToSelector(strQuery);
//        }

//        public void ConvertToSelector(string strQuery = "")
//        {
//            // Initialize list of selectors
//            List<Selector> selectors = new List<Selector>();

//            // Split query string by spaces
//            string[] query = strQuery.Split(' ');

//            // Iterate through each part of the query
//            foreach (string str in query)
//            {
//                Selector currentSelector = new Selector();

//                // Check if the part contains an ID
//                if (str.Contains("#"))
//                {
//                    int startIndex = str.IndexOf("#");
//                    int endIndex = str.IndexOfAny(new char[] { '.', ' ' }, startIndex);
//                    currentSelector.Id = str.Substring(startIndex + 1, endIndex - startIndex - 1);
//                }

//                // Check if the part contains classes
//                if (str.Contains("."))
//                {
//                    currentSelector.Classes = new List<string>();
//                    int startIndex = 0;
//                    while (startIndex < str.Length && str.IndexOf(".", startIndex) != -1)
//                    {
//                        startIndex = str.IndexOf(".", startIndex);
//                        int endIndex = str.IndexOfAny(new char[] { '.', ' ', '#' }, startIndex);
//                        currentSelector.Classes.Add(str.Substring(startIndex + 1, endIndex - startIndex - 1));
//                        startIndex = endIndex;
//                    }
//                }

//                // Check if the part represents a valid HTML tag
//                if (HtmlHelper.Instance.HtmlTags.Contains(str) || HtmlHelper.Instance.HtmlVoidTags.Contains(str))
//                    currentSelector.TagName = str;

//                // Add the current selector to the list
//                selectors.Add(currentSelector);

//                // Set parent-child relationship
//                if (selectors.Count > 1)
//                {
//                    currentSelector.Parent = selectors[selectors.Count - 2];
//                    if (currentSelector.Parent.Children == null)
//                        currentSelector.Parent.Children = new List<Selector>();
//                    currentSelector.Parent.Children.Add(currentSelector);
//                }
//            }
//        }
//    }
//}
//}