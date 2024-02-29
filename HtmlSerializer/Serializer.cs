using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlSerializer
{
      internal class Serializer
      {
            public async Task<string> Load(string url)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var html = await response.Content.ReadAsStringAsync();
                return html;
            }

            public HtmlElement BuildHtmlTree(string html)
            {
                // ניקוי רווחים מיותרים
                var cleanHtml = Regex.Replace(html, "\\n", "");

                // פיצול המחרוזת לפי תגיות
                var htmlLines = Regex.Split(cleanHtml, "<(.*?)>").Where(s => s.Length > 0);

                // יצירת אובייקט שורש
                var rootElement = new HtmlElement();
                var currentElement = rootElement;

                // עיבוד כל שורה
                foreach (var line in htmlLines)
                {
                    // חיתוך המילה הראשונה
                    var firstWord = line.Split(' ')[0];

                    // בדיקת תנאים
                    if (firstWord == "/html")
                    {
                        // הגענו לסוף ה-HTML
                        break;
                    }
                    else if (firstWord.StartsWith("/"))
                    {
                        // תגית סוגרת - מעבר לרמה קודמת
                        currentElement = currentElement.Parent;
                    }
                    else if (HtmlHelper.Instance.AllTags.Contains(firstWord))
                    {
                        // יצירת אובייקט חדש עבור תגית
                        var element = new HtmlElement();
                        element.Parent = currentElement;
                        currentElement.Children.Add(element);
                        currentElement = element;

                        // פירוק תכונות
                        var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line);
                        foreach (Match attribute in attributes)
                        {
                            currentElement.Attributes.Add(attribute.Groups[1].Value);
                            if (attribute.Groups[1].Value == "class")
                            {
                                // פירוק ערך ה-class לחלקים לפי רווח ועדכון של המאפיין Classes
                                string[] classParts = attribute.Groups[2].Value.Split(' ');
                                foreach (string part in classParts)
                                {
                                    currentElement.Classes.Add(part.Trim());
                                }
                            }
                            else if (attribute.Groups[1].Value == "id")
                                currentElement.Id = attribute.Groups[2].Value;
                            //Console.WriteLine("Attribute: " + attribute.Groups[1].Value);
                            //Console.WriteLine("Value: " + attribute.Groups[2].Value);
                        }

                        // עדכון שם
                        currentElement.Name = firstWord;

                        //בדיקת תגית סוגרת עצמית או שלא דורשת סגירה
                        if (HtmlHelper.Instance.SelfClosingTags.Contains(firstWord) || firstWord.EndsWith("/"))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        // טקסט פנימי
                        currentElement.InnerHtml += line;
                    }
                }

                return rootElement;
            }
      }
   
}
