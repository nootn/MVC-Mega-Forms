using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace MvcMega.Forms.DataAnnotations
{
    public class RequiredIfNotContains : ValidationAttribute, IClientValidatable
    {
        public RequiredIfNotContains(string dependentProperty, object dependentValue)
        {
            if (dependentProperty == null) throw new ArgumentNullException("dependentProperty");
            if (dependentValue == null) throw new ArgumentNullException("dependentValue");

            DependentProperty = dependentProperty;
            DependentValue = dependentValue;
        }

        public string DependentProperty { get; set; }

        public object DependentValue { get; set; }

        public bool AllowEmptyStrings { get; set; }

        #region IClientValidatable Members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                           {
                               ValidationType = "requiredifnotcontains",
                           };

            rule.ValidationParameters.Add("dependentproperty", DependentProperty);
            rule.ValidationParameters.Add("dependentvalue", DependentValue);

            yield return rule;
        }

        #endregion

        private bool IsFieldSupplied(object value)
        {
            if (value == null)
                return false;
            var str = value as string;
            if (str != null && !AllowEmptyStrings)
                return str.Trim().Length != 0;
            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsFieldSupplied(value))
            {
                if (DependentValue == null) throw new InvalidOperationException("DependentValue must not be null");

                var otherPropertyInfo = validationContext.ObjectType.GetProperty(DependentProperty);
                if (otherPropertyInfo == null)
                {
                    return
                        new ValidationResult(String.Format(CultureInfo.CurrentCulture,
                                                           "Could not find a property named {0}.", DependentProperty));
                }

                var val = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (val == null)
                {
                    //If the dependent value is null, it can't contain this value
                    if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                    {
                        ErrorMessage = string.Format(CultureInfo.CurrentCulture,
                                                     "This field must be supplied because {0} did not have a value and so it did not contain value {1}.",
                                                     DependentProperty,
                                                     DependentValue);
                    }
                    return new ValidationResult(ErrorMessage);
                }

                if (val is IEnumerable)
                {
                    var conditionMet = false;
                    var en = ((IEnumerable) val).GetEnumerator();

                    while (en.MoveNext() && !conditionMet)
                    {
                        if (en.Current.Equals(DependentValue))
                        {
                            conditionMet = true;
                        }
                    }

                    if (!conditionMet)
                    {
                        if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                        {
                            ErrorMessage = string.Format(CultureInfo.CurrentCulture,
                                                         "This field must be supplied because {0} did not contain the value '{1}'.",
                                                         DependentProperty,
                                                         DependentValue);
                        }
                        return new ValidationResult(ErrorMessage);
                    }
                }
                else
                {
                    throw new ApplicationException(
                        string.Format(
                            "The type of value being compared must be IEnumerable when using RequiredIfNotContains attribute.  Type '{0}' is not IEnumerable",
                            val.GetType().FullName));
                }
            }
            return null;
        }
    }
}