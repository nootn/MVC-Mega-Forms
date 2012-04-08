using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcMega.Forms.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ChangeVisuallyAttribute : ValidationAttribute, IClientValidatable
    {
        public enum ChangeTo
        {
            Hidden,
            Disabled,
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

        public ChangeVisuallyAttribute(ChangeTo to, string whenOtherPropertyName, DisplayChangeIf ifOperator, object value, bool conditionPassesIfNull)
        {
            To = to;
            WhenOtherPropertyName = whenOtherPropertyName;
            If = ifOperator;
            Value = value;
            ConditionPassesIfNull = conditionPassesIfNull;
            JQueryParentContainerSelector = ".editor-field";
        }

        public ChangeTo To { get; set; }

        public string WhenOtherPropertyName { get; set; }

        public DisplayChangeIf If { get; set; }

        public object Value { get; set; }

        public bool ConditionPassesIfNull { get; set; }

        /// <summary>
        /// In order to hide the selected property, it's visual representation on the screen must be "contained" within some markup; A.K.A it's "Parent".
        /// A common example is that you have a label, the input field and a validation message.  If these are contained within a div with a class "container"
        /// then you should set this to ".container".
        /// </summary>
        public string JQueryParentContainerSelector { get; set; }

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

            rule.ValidationParameters.Add("to", To.ToString().ToLower());
            rule.ValidationParameters.Add("otherpropertyname", WhenOtherPropertyName);
            rule.ValidationParameters.Add("ifoperator", If.ToString().ToLower());
            rule.ValidationParameters.Add("value", Value == null ? null : Value.ToString().ToLower());
            rule.ValidationParameters.Add("conditionpassesifnull", ConditionPassesIfNull);
            rule.ValidationParameters.Add("jqueryparentcontainerselector", JQueryParentContainerSelector);

            yield return rule;
        }

    }
}
