using HtmlSerializer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    public class Serializer
    {
        private readonly HttpClient _client;

        public Serializer()
        {
            _client = new HttpClient();
        }

        public async Task<HtmlElement> HtmlSerializer(string url)
        {

            var html = await Load(url);

            var cleanHtml = new Regex("\\s").Replace(html, "");

            var htmlLines = new Regex("<( .*? )>").Split(cleanHtml).Where(s => s.Length > 0);

            var htmlElement = "<div id=\"my-id\" class=\"my-class-1 my-class-2\" width=\"108%\">text</div>";
            var attributes = new Regex("([*\\s] *? )=\"( .*? )\"").Matches(htmlElement);
            var tags = Regex.Matches(cleanHtml, @"<(?<tag>[^\s>/]+)([^>]*)>");
            return BuildHtmlTree(tags);
        }

        public async Task<string> Load(string url)
        {
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        private HtmlElement BuildHtmlTree(IEnumerable<Match> tags, HtmlElement parent = null)
        {
            HtmlElement root = null;
            HtmlElement currentElement = null;

            foreach (Match tag in tags)
            {
                var htmlElement = tag.Value;

                // Check if the tag is void or not
                bool isSelfClosingTag = htmlElement.EndsWith("/>");

                // Extract tag name
                var tagNameStartIndex = htmlElement.IndexOf('<') + 1;
                var tagNameEndIndex = htmlElement.IndexOfAny(new char[] { ' ', '/' }, tagNameStartIndex);
                if (tagNameEndIndex > tagNameStartIndex)
                {
                    var tagName = htmlElement.Substring(tagNameStartIndex, tagNameEndIndex - tagNameStartIndex).ToLower();

                    // Create new element
                    var newElement = new HtmlElement
                    {
                        Name = tagName,
                        Parent = parent
                    };

                    // Check if it's a void or self-closing tag
                    if (!isSelfClosingTag && !HtmlHelper.Instance.HtmlVoidTags.Contains(tagName))
                    {
                        // If not a self-closing tag, set as the current element
                        currentElement = newElement;

                        // Update root if it's not set
                        if (root == null)
                            root = newElement;
                    }

                    // Add the new element to its parent if it exists
                    if (parent != null)
                        parent.Children.Add(newElement);

                    // Check for attributes
                    var attributes = Regex.Matches(htmlElement, "([\\w-]+)\\s*=\\s*['\"]([^'\"]*?)['\"]");
                    foreach (Match attribute in attributes)
                    {
                        var attributeName = attribute.Groups[1].Value.Trim();
                        var attributeValue = attribute.Groups[2].Value.Trim();

                        // Check if the attribute is 'class'
                        if (attributeName.Equals("class"))
                        {
                            // Split the attribute value by spaces and update the Classes property accordingly
                            var classes = attributeValue.Split(' ');
                            foreach (var cls in classes)
                            {
                                // Add each class to the Classes list
                                newElement.Classes.Add(cls);
                            }
                        }
                    }

                    // Check for inner text if it's not a self-closing tag
                    if (!isSelfClosingTag)
                    {
                        var innerTextStartIndex = htmlElement.IndexOf('>') + 1;
                        var innerTextEndIndex = htmlElement.LastIndexOf('<');
                        if (innerTextEndIndex > innerTextStartIndex)
                        {
                            var innerText = htmlElement.Substring(innerTextStartIndex, innerTextEndIndex - innerTextStartIndex);
                            if (!string.IsNullOrWhiteSpace(innerText))
                            {
                                // Update InnerHtml property
                                newElement.InnerHtml = innerText;
                            }
                        }
                    }
                }
            }

            return root;
        }

    }


}


