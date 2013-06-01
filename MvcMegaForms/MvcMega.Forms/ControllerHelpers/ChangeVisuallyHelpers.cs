// /*
// Copyright (c) 2013 Andrew Newton (http://www.nootn.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;
using Jurassic;
using MvcMega.Forms.DataAnnotations;

namespace MvcMega.Forms.ControllerHelpers
{
    public static class ChangeVisuallyHelpers
    {
        public static bool IsValidEditableFieldsOnly<T>(ModelStateDictionary modelState, T model)
        {
            var isValid = modelState.IsValid;
            if (isValid)
            {
                return true;
            }

            //If it is not valid, check if there are any errors
            var props = typeof(T).GetProperties();
            foreach (var propertyInfo in props)
            {
                if (modelState.ContainsKey(propertyInfo.Name) && modelState[propertyInfo.Name].Errors.Any() &&
                    ConditionMetForChangeVisually(propertyInfo, model, props))
                {
                    //There were errors for this property, but it was not editable so remove them
                    modelState[propertyInfo.Name].Errors.Clear();
                }
            }

            //re-evaluate validity after potentially having removed some errors
            return modelState.IsValid;
        }


        private static bool ConditionMetForChangeVisually<T>(PropertyInfo prop, T model, PropertyInfo[] modelProps)
        {
            var engine = new ScriptEngine();
            var src = GetJavaScript();
            engine.Evaluate(src);

            List<string> toValues;
            List<string> whenOtherPropertyNameValues;
            List<string> ifValues;
            List<string> valueValues;
            List<string> conditionPassesIfNullValues;
            List<string> valueTypeToCompareValues;
            List<string> valueFormatValues;
            ChangeVisuallyAttribute.GetValuesForClient(prop, out toValues, out whenOtherPropertyNameValues, out ifValues, out valueValues, out conditionPassesIfNullValues, out valueTypeToCompareValues, out valueFormatValues);

            //check if we have any conditions.. if we do loop through them and return true as soon as one is found to pass
            if (toValues.Any())
            {
                for (var i = 0; i < toValues.Count; i++)
                {
                     //get the "other property" value
                    string otherPropValue = null;
                    var otherProp =
                        modelProps.FirstOrDefault(_ => string.Equals(_.Name, whenOtherPropertyNameValues[i]));
                    if (otherProp != null)
                    {
                        var val = otherProp.GetValue(model, null);
                        if (val != null)
                        {
                            otherPropValue = Json.Encode(val);
                        }
                    }

                    var ifOperator = ifValues[i];
                    var expectedValue = valueValues[i];
                    var actualValue = otherPropValue;
                    var conditionPassesIfNull = conditionPassesIfNullValues[i];
                    var valueTypeToCompare = valueTypeToCompareValues[i];
                    var valueFormat = valueFormatValues[i];

                    //Call the JavaScript function to evaluate
                    var res = engine.CallGlobalFunction<bool>("ConditionMetForChangeVisually", ifOperator, expectedValue,
                                                              actualValue, conditionPassesIfNull, valueTypeToCompare,
                                                              valueFormat);
                    if (res)
                    {
                        //we found one where the condition was met
                        return true;
                    }
                }
            }

            return false;
        }

        private static string GetJavaScript()
        {
            using (var stream = typeof(ChangeVisuallyHelpers).Assembly
                                                              .GetManifestResourceStream(
                                                                  "MvcMega.Forms.Content.Scripts.MvcMegaFormsSubsetForJurassic.js")
                )
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}