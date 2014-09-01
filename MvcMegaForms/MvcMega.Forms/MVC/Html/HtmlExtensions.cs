// // /*
// // Copyright (c) 2014 Andrew Newton (http://www.nootn.com)
// // 
// // Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// // 
// // The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// // 
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// // */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace MvcMega.Forms.MVC.Html
{
    public static class HtmlExtensions
    {
        //These default css class constants are handy for use with Twitter Bootstrap v3 (and above hopefully) with form class "form-horizontal".
        //If you are not using "form-horizontal, see the comment above the Func<string> methods below for how to override these defaults either globally or in particular forms
        public const string DefaultControlGroupClass = "form-group";
        public const string DefaultControlsClass = "controls col-sm-9";
        public const string DefaultHelpInlineClass = "help-inline";
        public const string DefaultLabelClass = "control-label col-sm-3";

        //You can override the defaults globally by changing these, or within each form using the method "BeginControlClassOverrideGroup"
        //Since these are able to be set globally, they are a bit dangerous.
        //If I were not trying to maintain backwards compatibility I would have designed this differently.
        public static Func<string> GetDefaultControlGroupClass = () => DefaultControlGroupClass;
        public static Func<string> GetDefaultControlsClass = () => DefaultControlsClass;
        public static Func<string> GetDefaultHelpInlineClass = () => DefaultHelpInlineClass;
        public static Func<string> GetDefaultLabelClass = () => DefaultLabelClass;

        public static ControlClassOverrideGroup BeginControlClassOverrideGroup(this HtmlHelper htmlHelper, string defaultControlGroupClass = null, string defaultControlsClass = null,
            string defaultHelpInlineClass = null, string defaultLabelClass = null)
        {
            var container = new ControlClassOverrideGroup(defaultControlGroupClass, defaultControlsClass, defaultHelpInlineClass, defaultLabelClass);
            return container;
        }

        public static ControlGroup BeginControlGroup(this HtmlHelper htmlHelper,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null)
        {
            var container = new ControlGroup(htmlHelper.ViewContext);
            WriteStartTagWithClass(htmlHelper, container.Tag, controlGroupClass, controlGroupHtmlAttributes);
            return container;
        }

        public static Controls BeginControls(this HtmlHelper htmlHelper, string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null)
        {
            var container = new Controls(htmlHelper.ViewContext);

            //manipulate class, overriding default if necessary
            if (string.Equals(controlsClass, DefaultControlsClass, StringComparison.CurrentCultureIgnoreCase)
                && !string.Equals(controlsClass, GetDefaultControlsClass(), StringComparison.CurrentCultureIgnoreCase))
            {
                //Set it to the overriden default
                controlsClass = GetDefaultControlsClass();
            }

            WriteStartTagWithClass(htmlHelper, container.Tag, controlsClass, controlsHtmlAttributes);
            return container;
        }

        public static HelpInline BeginHelpInline(this HtmlHelper htmlHelper,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null)
        {
            var container = new HelpInline(htmlHelper.ViewContext);

            //manipulate class, overriding default if necessary
            if (string.Equals(helpInlineClass, DefaultHelpInlineClass, StringComparison.CurrentCultureIgnoreCase)
                && !string.Equals(helpInlineClass, GetDefaultHelpInlineClass(), StringComparison.CurrentCultureIgnoreCase))
            {
                //Set it to the overriden default
                helpInlineClass = GetDefaultHelpInlineClass();
            }

            WriteStartTagWithClass(htmlHelper, container.Tag, helpInlineClass, helpInlineHtmlAttributes);
            return container;
        }

        public static MvcHtmlString ControlLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes =
                null)
        {
            if (labelHtmlAttributes == null)
            {
                labelHtmlAttributes = new Dictionary<string, object>();
            }

            //manipulate class, overriding default if necessary
            if (string.Equals(labelClass, DefaultLabelClass, StringComparison.CurrentCultureIgnoreCase)
                && !string.Equals(labelClass, GetDefaultLabelClass(), StringComparison.CurrentCultureIgnoreCase))
            {
                //Set it to the overriden default
                labelClass = GetDefaultLabelClass();
            }

            if (labelHtmlAttributes.ContainsKey("class"))
            {
                var item = labelHtmlAttributes["class"];
                labelHtmlAttributes["class"] = item == null
                    ? labelClass
                    : string.Concat(item.ToString(), " ", labelClass);
            }
            else
            {
                labelHtmlAttributes.Add("class", labelClass);
            }

            return htmlHelper.LabelFor(expression, labelHtmlAttributes);
        }

        public static MvcHtmlString ControlBundleFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null,
            string templateName = "", bool readOnly = false, MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.EditorFor(expression, templateName),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleTextBoxFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.TextBoxFor(expression), controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleTextAreaFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null,
            int rows = 4, int columns = 20, bool readOnly = false, MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.TextAreaFor(expression, rows, columns, null),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleCheckBoxFor<TModel>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.CheckBoxFor(expression), controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundlePasswordFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.PasswordFor(expression), controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleRadioButtonFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            object value,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.RadioButtonFor(expression, value),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleDropDownListFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.DropDownListFor(expression, selectList),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleDropDownChildListFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            CascadingSelectList selectList,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.DropDownChildListFor(expression, selectList),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleListBoxFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectList,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.ListBoxFor(expression, selectList),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleListBoxChildFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            CascadingSelectList selectList,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null, bool readOnly = false,
            MvcHtmlString controlForReadOnly = null)
        {
            return ControlBundleFor(htmlHelper, expression, htmlHelper.ListBoxChildFor(expression, selectList),
                controlGroupClass,
                controlGroupHtmlAttributes, labelClass, labelHtmlAttributes, controlsClass,
                controlsHtmlAttributes, helpInlineClass, helpInlineHtmlAttributes, readOnly, controlForReadOnly);
        }

        public static MvcHtmlString ControlBundleFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            MvcHtmlString control,
            string controlGroupClass = DefaultControlGroupClass,
            IDictionary<string, object> controlGroupHtmlAttributes = null,
            string labelClass = DefaultLabelClass,
            IDictionary<string, object> labelHtmlAttributes = null,
            string controlsClass = DefaultControlsClass,
            IDictionary<string, object> controlsHtmlAttributes = null,
            string helpInlineClass = DefaultHelpInlineClass,
            IDictionary<string, object> helpInlineHtmlAttributes = null,
            bool readOnly = false, MvcHtmlString controlForReadOnly = null)
        {
            using (BeginControlGroup(htmlHelper, controlGroupClass, controlGroupHtmlAttributes))
            {
                htmlHelper.ViewContext.Writer.Write(ControlLabelFor(htmlHelper, expression, labelClass,
                    labelHtmlAttributes));
                using (BeginControls(htmlHelper, controlsClass, controlsHtmlAttributes))
                {
                    if (readOnly)
                    {
                        htmlHelper.ViewContext.Writer.Write(controlForReadOnly ?? htmlHelper.DisplayFor(expression));
                    }
                    else
                    {
                        htmlHelper.ViewContext.Writer.Write(control);
                    }
                    using (BeginHelpInline(htmlHelper, helpInlineClass, helpInlineHtmlAttributes))
                    {
                        htmlHelper.ViewContext.Writer.Write(htmlHelper.ValidationMessageFor(expression));
                    }
                }
            }

            return new MvcHtmlString("");
        }


        private static void WriteStartTagWithClass(HtmlHelper htmlHelper, HtmlTextWriterTag tag, string containerClass,
            IDictionary<string, object> otherHtmlAttributes)
        {
            var tagBuilder = new TagBuilder(tag.ToString().ToLower());
            if (otherHtmlAttributes != null)
            {
                tagBuilder.MergeAttributes(otherHtmlAttributes);
            }
            tagBuilder.MergeAttribute("class", containerClass);
            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
        }
    }
}