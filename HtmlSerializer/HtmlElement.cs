



using System;
using System.Collections.Generic;

namespace HtmlSerializer
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Attributes = new Dictionary<string, string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                HtmlElement current = queue.Dequeue();
                yield return current;

                foreach (HtmlElement child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public Queue<HtmlElement> Ancestors()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            HtmlElement current = this;
            while (current.Parent != null)
            {
                current = current.Parent;
                queue.Enqueue(current);
            }
            return queue;
        }
        public IEnumerable<HtmlElement> FindElements(Selector selector)
        {
            HashSet<HtmlElement> elementsSet = new HashSet<HtmlElement>();
            FindElementsRecursively(this, selector, elementsSet);
            return elementsSet;
        }

        private void FindElementsRecursively(HtmlElement element, Selector selector, HashSet<HtmlElement> elementsSet)
        {
            foreach (HtmlElement descendant in element.Descendants())
            {
                if (MatchesSelector(descendant, selector))
                {
                    elementsSet.Add(descendant);
                    Console.WriteLine( "llsslsll");
                }
            }
        }

        private bool MatchesSelector(HtmlElement element, Selector selector)
        {
            Console.WriteLine("love");
            if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(selector.Name) && element.Name != selector.Name)
            {
                return false;
            }

            if (selector.Classes != null && selector.Classes.Count > 0)
            {
                foreach (string className in selector.Classes)
                {
                    if (!element.Classes.Contains(className))
                    {
                        return false;
                    }
                }
            }

            if (selector.Parent != null)
            {
                if (!MatchesSelector(element.Parent, selector.Parent))
                {
                    return false;
                }
            }

            if (selector.Children != null && selector.Children.Count > 0)
            {
                foreach (Selector childSelector in selector.Children)
                {
                    bool found = false;
                    foreach (HtmlElement child in element.Children)
                    {
                        if (MatchesSelector(child, childSelector))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

}

//public IEnumerable<HtmlElement> FindElements(Selector selector)
//        {
//            HashSet<HtmlElement> elementsSet = new HashSet<HtmlElement>();
//            FindElementsRecursively(this, selector, elementsSet);
//            return elementsSet;
//        }

//        private void FindElementsRecursively(HtmlElement element, Selector selector, HashSet<HtmlElement> elementsSet)
//        {
//            if (MatchesSelector(element, selector))
//            {
//                elementsSet.Add(element);
//            }

//            foreach (HtmlElement child in element.Children)
//            {
//                FindElementsRecursively(child, selector, elementsSet);
//            }
//        }

//        private bool MatchesSelector(HtmlElement element, Selector selector)
//        {
//            if (!string.IsNullOrEmpty(selector.Id) && element.Id != selector.Id)
//            {
//                return false;
//            }

//            if (!string.IsNullOrEmpty(selector.Name) && element.Name != selector.Name)
//            {
//                return false;
//            }

//            if (selector.Classes != null && selector.Classes.Count > 0)
//            {
//                foreach (string className in selector.Classes)
//                {
//                    if (!element.Classes.Contains(className))
//                    {
//                        return false;
//                    }
//                }
//            }

//            if (selector.Parent != null)
//            {
//                if (!MatchesSelector(element.Parent, selector.Parent))
//                {
//                    return false;
//                }
//            }

//            if (selector.Children != null && selector.Children.Count > 0)
//            {
//                foreach (Selector childSelector in selector.Children)
//                {
//                    bool found = false;
//                    foreach (HtmlElement child in element.Children)
//                    {
//                        if (MatchesSelector(child, childSelector))
//                        {
//                            found = true;
//                            break;
//                        }
//                    }
//                    if (!found)
//                    {
//                        return false;
//                    }
//                }
//            }

//            return true;
//        }
//    }