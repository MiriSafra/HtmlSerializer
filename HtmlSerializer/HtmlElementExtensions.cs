using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{

        internal static class HtmlElementExtensions
        {
            // פונקציה המרחיבה את הפונקציונליות של מחלקת HtmlElement
            public static IEnumerable<HtmlElement> FindElementsBySelector(this HtmlElement root, Selector selector)
            {
                // אוסף של אלמנטים המתאימים לסלקטור
                HashSet<HtmlElement> matchedElements = new HashSet<HtmlElement>();

                // קריאה רקורסיבית לפונקציה הפנימית עם האוסף של האלמנט והסלקטור
                FindElementsRecursive(root, selector, matchedElements);

                // החזרת האוסף של האלמנטים המתאימים
                return matchedElements;
            }

            // פונקציה רקורסיבית למציאת אלמנטים על פי סלקטור
            private static void FindElementsRecursive(HtmlElement elements, Selector selector, HashSet<HtmlElement> matchedElements)
            {
                // בדיקה אם הגענו לסלקטור האחרון
                if (selector == null)
                {
                    return;
                }
                // בדיקה האם האלמנט הנוכחי עונה על הסלקטור הנתון
                if (MatchElement(elements, selector))
                {
                    // הוספת האלמנט לתוך המבנה הנתונים HashSet
                    matchedElements.Add(elements);
                    selector = selector.Child;
                }

                // קריאה רקורסיבית לפונקציה עם כל הצאצאים של האלמנט הנוכחי
                foreach (var child in elements.Children)
                {
                    FindElementsRecursive(child, selector, matchedElements);
                }
            }

            // פונקציה שבודקת האם אלמנט עונה לסלקטור נתון
            private static bool MatchElement(HtmlElement element, Selector selector)
            {
                // בדיקה אם יש סלקטור של id ואם האלמנט מתאים לו
                if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
                    return false;

                // בדיקה אם יש סלקטור של תג ואם האלמנט מתאים לו
                if (!string.IsNullOrEmpty(selector.TagName) && element.Name != selector.TagName)
                    return false;

                // בדיקה אם יש סלקטורים של קלאסים ואם האלמנט מתאים לאחד מהם
                if (selector.Classes.Any() && !element.Classes.Any(cls => selector.Classes.Contains(cls)))
                    return false;

                return true;
            }

        }
   
}
