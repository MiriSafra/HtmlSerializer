using HtmlSerializer;
namespace HtmlSerializer
{
    public class Selector
    {
        public string Id { get; set; }
        public string TagName { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public List<Selector> Children { get; set; }




        //public void ConvertToSelector(string strQuery)
        //{
        //    // Initialize list of selectors
        //    var selectors = new List<Selector>();

        //    // Split query string by spaces
        //    var query = strQuery.Split(' ');

        //    Selector parentSelector = null;

        //    // Iterate through each part of the query
        //    foreach (var str in query)
        //    {
        //        var currentSelector = new Selector();

        //        // Check if the part contains an ID
        //        if (str.Contains("#"))
        //        {
        //            var startIndex = str.IndexOf("#");
        //            var endIndex = str.IndexOfAny(new char[] { '.', ' ' }, startIndex);
        //            currentSelector.Id = str.Substring(startIndex + 1, endIndex - startIndex - 1);
        //        }

        //        // Check if the part contains classes
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

        //        // Check if the part represents a valid HTML tag
        //        if (HtmlHelper.Instance.HtmlTags.Contains(str) || HtmlHelper.Instance.HtmlVoidTags.Contains(str))
        //            currentSelector.TagName = str;

        //        // Set parent-child relationship
        //        if (parentSelector != null)
        //        {
        //            currentSelector.Parent = parentSelector;
        //            if (currentSelector.Parent.Children == null)
        //                currentSelector.Parent.Children = new List<Selector>();
        //            currentSelector.Parent.Children.Add(currentSelector);
        //        }

        //        parentSelector = currentSelector; // Update parent selector for the next iteration
        //    }
        //}
        public void ConvertToSelector(string strQuery)
        {
            // Initialize list of selectors
            var query = strQuery.Split(' ');
            Selector parentSelector = null;

            foreach (var str in query)
            {
                var currentSelector = new Selector();

                // Check if the part contains an ID
                if (str.Contains("#"))
                {
                    var startIndex = str.IndexOf("#");
                    var endIndex = str.IndexOfAny(new char[] { '.', ' ' }, startIndex);
                    if (endIndex >= 0)
                    {
                        currentSelector.Id = str.Substring(startIndex + 1, endIndex - startIndex - 1);
                    }
                }

                // Check if the part contains classes
                if (str.Contains("."))
                {
                    currentSelector.Classes = new List<string>();
                    var startIndex = 0;
                    while (startIndex < str.Length && str.IndexOf(".", startIndex) != -1)
                    {
                        startIndex = str.IndexOf(".", startIndex);
                        var endIndex = str.IndexOfAny(new char[] { '.', ' ', '#' }, startIndex);
                        if (endIndex >= 0)
                        {
                            var classLength = endIndex - startIndex - 1;
                            if (classLength >= 0)
                            {
                                currentSelector.Classes.Add(str.Substring(startIndex + 1, classLength));
                            }
                        }
                        startIndex = endIndex;
                    }
                }

                // Check if the part represents a valid HTML tag
                if (HtmlHelper.Instance.HtmlTags.Contains(str) || HtmlHelper.Instance.HtmlVoidTags.Contains(str))
                {
                    currentSelector.TagName = str;
                }

                // Set parent-child relationship
                if (parentSelector != null)
                {
                    currentSelector.Parent = parentSelector;
                    if (currentSelector.Parent.Children == null)
                    {
                        currentSelector.Parent.Children = new List<Selector>();
                    }
                    currentSelector.Parent.Children.Add(currentSelector);
                }

                parentSelector = currentSelector; // Update parent selector for the next iteration
            }
        }
        public Selector GetRootSelector()
        {
            Selector current = this;
            while (current.Parent != null)
            {
                current = current.Parent;
            }
            return current;
        }
    }



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
}