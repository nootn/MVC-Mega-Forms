using System;
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
                                                                            CascadingSelectList selectList)
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

            return System.Web.Mvc.Html.SelectExtensions.DropDownListFor(
                htmlHelper, expression, selectList, null,
                new
                    {
                        combos = finalItems.ToString(),
                        parentListId = selectList.ParentSelectListPropertyName
                    });
        }

        private static void EnsureNoSpecialCharacters(string value)
        {
            if (value.Any(currChar => currChar.Equals("[") || currChar.Equals("~") || currChar.Equals(";") || currChar.Equals("]")))
            {
                throw new InvalidOperationException(string.Format("You are not able to use special characters '[', '~', ';' or ']' in the Value: '{0}'", value));
            }
        }
    }
}