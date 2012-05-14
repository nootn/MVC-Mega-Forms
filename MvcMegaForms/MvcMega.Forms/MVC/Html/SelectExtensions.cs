/*
Copyright (c) 2012 Andrew Newton (http://about.me/nootn)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace MvcMega.Forms.MVC.Html
{
    public static class SelectExtensions
    {
        
        public static MvcHtmlString DropDownChildListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                            Expression<Func<TModel, TProperty>>
                                                                                expression,
                                                                            CascadingSelectList selectList, string optionLabel = null, Dictionary<string, object> htmlAttributes = null)
        {
            var combosValue = GetCombosAttributeValue(selectList);

            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            htmlAttributes.Add("combos", combosValue);
            htmlAttributes.Add("parentListId", selectList.ParentSelectListPropertyName);

            return System.Web.Mvc.Html.SelectExtensions.DropDownListFor(
                htmlHelper, expression, selectList, optionLabel,
                htmlAttributes);
        }


        public static MvcHtmlString DropDownChildList(this HtmlHelper htmlHelper, string name, CascadingSelectList selectList, string optionLabel = null, Dictionary<string, object> htmlAttributes = null)
        {
            var combosValue = GetCombosAttributeValue(selectList);

            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }

            htmlAttributes.Add("combos", combosValue);
            htmlAttributes.Add("parentListId", selectList.ParentSelectListPropertyName);

            return System.Web.Mvc.Html.SelectExtensions.DropDownList(
                htmlHelper, name, selectList, optionLabel,
                htmlAttributes);
        }


        public static StringBuilder GetCombosAttributeValue(CascadingSelectList selectList)
        {
            var list = selectList.ToList();
            var finalItems = new StringBuilder();
            if (list.Any())
            {
                var parentIds = list.Select(i => i.ParentValue).Distinct().ToList();
                if (parentIds.Any())
                {
                    foreach (var currParent in parentIds)
                    {
                        EnsureNoSpecialCharacters(currParent);
                        finalItems.Append(currParent);
                        finalItems.Append("{");
                        var childIds = list.Where(i => i.ParentValue == currParent);
                        if (childIds.Any())
                        {
                            foreach (var currChild in childIds)
                            {
                                EnsureNoSpecialCharacters(currChild.Value);
                                EnsureNoSpecialCharacters(currChild.Text);
                                finalItems.AppendFormat("{0}~{1};", currChild.Value, currChild.Text);
                            }
                            finalItems.Remove(finalItems.Length - 1, 1);
                        }
                        finalItems.Append("}");
                    }
                }
            }
            return finalItems;
        }

        private static void EnsureNoSpecialCharacters(string value)
        {
            if (value.Any(currChar => currChar.Equals("{") || currChar.Equals("~") || currChar.Equals(";") || currChar.Equals("}")))
            {
                throw new InvalidOperationException(string.Format("You are not able to use special characters '{1}', '~', ';' or '{2}' in the Value: '{0}'", value, "{", "}"));
            }
        }
    }
}