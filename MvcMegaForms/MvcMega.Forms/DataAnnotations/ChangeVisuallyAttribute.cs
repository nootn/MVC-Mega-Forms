/*
Copyright (c) 2012 Andrew Newton (http://about.me/nootn)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcMega.Forms.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ChangeVisuallyAttribute : ValidationAttribute, IClientValidatable
    {
        public const string Separator = "~";

        public enum ChangeTo
        {
            Hidden,
            Disabled,
            Readonly,
        }

        public enum DisplayChangeIf
        {
            Equals,
            NotEquals,
            LessThan,
            LessThanOrEquals,
            GreaterThan,
            GreaterThanOrEquals,
            Contains,
            NotContains,
        }

        public ChangeVisuallyAttribute(ChangeTo to, string whenOtherPropertyName, DisplayChangeIf ifOperator, object value, bool conditionPassesIfNull, ComparisonValueType valueType = ComparisonValueType.String)
        {
            To = to;
            WhenOtherPropertyName = whenOtherPropertyName;
            If = ifOperator;
            Value = value;
            ConditionPassesIfNull = conditionPassesIfNull;
            ValueTypeToCompare = valueType;
        }

        public enum ComparisonValueType
        {
            String,
            Number,
            DateTime,
        }

        public ChangeTo To { get; set; }

        public string WhenOtherPropertyName { get; set; }

        public DisplayChangeIf If { get; set; }

        public object Value { get; set; }

        public bool ConditionPassesIfNull { get; set; }

        public ComparisonValueType ValueTypeToCompare { get; set; }

        /// <summary>
        ///This should be used if ValueTypeToCompare == ComparisonValueType.DateTime to let it know what the format of the date is.
        ///
        ///If specified, a format string that must be matched exactly by the stringDate using the available Format Strings, and any other text. If not specified, the method will automatically attempt to match any of the following formats:
        /// y-M-d    MMM d, y    MMM d,y    y-MMM-d    d-MMM-y    MMM d    MMM-d    d-MMM    M/d/y    M-d-y    M.d.y    M/d    M-d    d/M/y    d-M-y    d.M.y    d/M    d-M   
        /// (from  http://www.javascripttoolbox.com/lib/date/documentation.php)
        /// </summary>
        public string ValueFormat { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "changevisually",
            };

            var prop = metadata == null || metadata.ContainerType == null
               ? null
               : metadata.ContainerType.GetProperty(metadata.PropertyName);

            //string toValueForClient;
            //string otherPropertyNameForClient;
            //string ifOperatorForClient;
            //string valueForClient;
            //string conditionPassesIfNullForClient;
            //string valueTypeToCompareForClient;
            //string valueFormatForClient;
            //GetValuesForClient(prop, out toValueForClient, out otherPropertyNameForClient, out ifOperatorForClient, out valueForClient, out conditionPassesIfNullForClient, out valueTypeToCompareForClient, out valueFormatForClient);

            List<string> toValues;
            List<string> whenOtherPropertyNameValues;
            List<string> ifValues;
            List<string> valueValues;
            List<string> conditionPassesIfNullValues;
            List<string> valueTypeToCompareValues;
            List<string> valueFormatValues;
            GetValuesForClient(prop, out toValues, out whenOtherPropertyNameValues, out ifValues, out valueValues, out conditionPassesIfNullValues, out valueTypeToCompareValues, out valueFormatValues);

            var toValueForClient = string.Join(Separator, toValues).ToLower();
            var otherPropertyNameForClient = string.Join(Separator, whenOtherPropertyNameValues);
            var ifOperatorForClient = string.Join(Separator, ifValues).ToLower();
            var valueForClient = string.Join(Separator, valueValues).ToLower();
            var conditionPassesIfNullForClient = string.Join(Separator, conditionPassesIfNullValues).ToLower();
            var valueTypeToCompareForClient = string.Join(Separator, valueTypeToCompareValues).ToLower();
            var valueFormatForClient = string.Join(Separator, valueFormatValues);

            rule.ValidationParameters.Add("to", toValueForClient);
            rule.ValidationParameters.Add("otherpropertyname", otherPropertyNameForClient);
            rule.ValidationParameters.Add("ifoperator", ifOperatorForClient);
            rule.ValidationParameters.Add("value", valueForClient);
            rule.ValidationParameters.Add("conditionpassesifnull", conditionPassesIfNullForClient);
            rule.ValidationParameters.Add("valuetypetocompare", valueTypeToCompareForClient);
            rule.ValidationParameters.Add("valueformat", valueFormatForClient);

            yield return rule;
        }

        public static void GetValuesForClient(PropertyInfo prop, out List<string> toValues, out List<string> whenOtherPropertyNameValues, out List<string> ifValues,
                                               out List<string> valueValues, out List<string> conditionPassesIfNullValues,
                                               out List<string> valueTypeToCompareValues, out List<string> valueFormatValues)
        {
            toValues = new List<string>();
            whenOtherPropertyNameValues = new List<string>();
            ifValues = new List<string>();
            valueValues = new List<string>();
            conditionPassesIfNullValues = new List<string>();
            valueTypeToCompareValues = new List<string>();
            valueFormatValues = new List<string>();

            var allChangeVisuallyAttributesOnThisProperty = prop == null
                                                                ? null
                                                                : prop.GetCustomAttributes(typeof (ChangeVisuallyAttribute),
                                                                                           true);
            if (allChangeVisuallyAttributesOnThisProperty != null && allChangeVisuallyAttributesOnThisProperty.Any())
            {
                foreach (var currAttr in allChangeVisuallyAttributesOnThisProperty.Reverse())
                {
                    var attr = (ChangeVisuallyAttribute) currAttr;
                    toValues.Add(attr.To.ToString().ToLower());
                    whenOtherPropertyNameValues.Add(attr.WhenOtherPropertyName);
                    ifValues.Add(attr.If.ToString().ToLower());
                    var val = string.Empty;
                    if (attr.Value != null)
                    {
                        if (attr.Value.GetType().IsArray)
                        {
                            val = Json.Encode(attr.Value).ToLower();
                        }
                        else
                        {
                            val = attr.Value.ToString().ToLower();
                        }
                    }
                    valueValues.Add(val);
                    conditionPassesIfNullValues.Add(attr.ConditionPassesIfNull.ToString().ToLower());
                    valueTypeToCompareValues.Add(attr.ValueTypeToCompare.ToString().ToLower());
                    valueFormatValues.Add(attr.ValueFormat ?? string.Empty); //do not "toLower" the format!  It could be a date format that needs some captials
                }
            }
        }
    }
}
