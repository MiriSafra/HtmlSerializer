
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlSerializer
{
    internal class Selector
    {
        public string Id { get; set; }
        public string TagName { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        public Selector()
        {
            Classes = new List<string>();
        }
        // פונקציה סטטית שממירה מחרוזת של שאילתה לעץ של סלקטורים
        public static Selector Parse(string selectorString)
        {
            var parts = selectorString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Selector rootSelector = null;
            Selector currentSelector = null;

            foreach (var part in parts)
            {
                Selector selector = new Selector();
                selector.UpdateSelector(part);

                if (currentSelector != null)
                {
                    currentSelector.Child = selector;
                    selector.Parent = currentSelector;
                }
                else
                {
                    rootSelector = selector;
                }

                currentSelector = selector;
            }

            return rootSelector;
        }

        // פונקציה שמעדכנת את הסלקטור בהתאם לחלק של המחרוזת
        private void UpdateSelector(string part)
        {
            var subParts = Regex.Split(part, @"(?=[.#])");

            foreach (var subPart in subParts)
            {
                if (subPart.StartsWith('#'))
                {
                    this.Id = subPart.Substring(1);
                }
                else if (subPart.StartsWith('.'))
                {
                    this.Classes.Add(subPart.Substring(1));
                }
                else if (HtmlHelper.Instance.AllTags.Contains(subPart))
                {
                    this.TagName = subPart;
                }
            }
        }
    }
}