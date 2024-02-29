using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlSerializer
{
    internal class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }
        public IEnumerable<HtmlElement> Descendants()
        {
            // יצירת תור חדש
            Queue<HtmlElement> queue = new Queue<HtmlElement>();

            // הוספת האלמנט הנוכחי לתור
            queue.Enqueue(this);

            // לולאה כל עוד התור לא ריק
            while (queue.Count > 0)
            {
                // שליפת האלמנט הראשון מהתור
                HtmlElement element = queue.Dequeue();

                // החזרת האלמנט
                yield return element;

                // הוספת כל הבנים של האלמנט לתור
                foreach (HtmlElement child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var current = this;
            while (current.Parent != null)
            {
                yield return current.Parent;
                current = current.Parent;
            }
        }

    }
}