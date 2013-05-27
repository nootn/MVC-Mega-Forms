// /*
// Copyright (c) 2013 Andrew Newton (http://www.nootn.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcMega.Forms.ActionFilters
{
    /// <summary>
    /// Add this attribute to Action methods that you want to prevent being processed more than once within a specific time period on the server.
    /// This may store a fair amount of data per user so is ideal for Intranet scenarios but might not be a good idea in high volume web applications.
    /// This relies on Session Lock (one Request per user session at a time) which is the default behaviour of ASP.NET.
    /// </summary>
    public class PreventDoubleAction : ActionFilterAttribute
    {
        public enum PreventionLevel
        {
            /// <summary>
            ///     Prevents based on action alone, no matter what data was passed
            /// </summary>
            ActionOnly = 1,

            /// <summary>
            ///     Prevents based on the action and what data was passed in, where the action parameters are shallow values
            /// </summary>
            ActionPlusDataShallow = 2,

            /// <summary>
            ///     Prevents based on the action and what data was passed in, where the action parameters are deep (E.g. structured items)
            /// </summary>
            ActionPlusDataDeep = 3,
        }

        private const string ViewDataKeyPrefix = "!PDA!_";

        private readonly int _preventionExpiryInSeconds;
        private readonly PreventionLevel _preventionLevel;

        private PreventDoubleActionData _currentActionData;
        private string _key;

        public PreventDoubleAction(int preventionExpiryInSeconds, PreventionLevel preventionLevel)
        {
            _preventionExpiryInSeconds = preventionExpiryInSeconds;
            _preventionLevel = preventionLevel;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _key = GetViewDataKey(filterContext);

            var allKeys = filterContext.Controller.TempData.Keys.ToArray();

            //Access all tempdata to figure out which ones can be cleaned out (otherwise they just keep building up, one per action)
            foreach (var currKey in allKeys.Where(_ => _ != _key && _.StartsWith(ViewDataKeyPrefix)))
            {
                //pull it out of temp data
                var item = filterContext.Controller.TempData[currKey] as PreventDoubleActionData;
                if (item != null && !IsExpired(item))
                {
                    //put it back in, we might need it!
                    filterContext.Controller.TempData[currKey] = item;
                }
                else
                {
                    filterContext.Controller.TempData.Remove(currKey);
                }
            }

            _currentActionData = filterContext.Controller.TempData[_key] as PreventDoubleActionData;

            if (_currentActionData != null && !IsExpired(_currentActionData))
            {
                //not expired, reuse it
                filterContext.Result = _currentActionData.Result;

                //put it back in, we might need it again!
                filterContext.Controller.TempData[_key] = _currentActionData;
            }
            else
            {
                _currentActionData = new PreventDoubleActionData
                    {
                        DateAdded = DateTimeOffset.Now,
                        PreventionExpiryInSeconds = _preventionExpiryInSeconds,
                        Result = null
                    };
                filterContext.Controller.TempData[_key] = _currentActionData;
            }
        }

        private static bool IsExpired(PreventDoubleActionData item)
        {
            return item == null ||
                   DateTimeOffset.Now.Subtract(new TimeSpan(0, 0, 0, item.PreventionExpiryInSeconds)) >= item.DateAdded;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            //update the result
            _currentActionData.Result = filterContext.Result;
        }

        private string GetViewDataKey(ActionExecutingContext filterContext)
        {
            var key = string.Concat(ViewDataKeyPrefix, filterContext.RequestContext.HttpContext.Request.HttpMethod,
                                    "_",
                                    filterContext.ActionDescriptor.ActionName, "_",
                                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerType);
            if (_preventionLevel == PreventionLevel.ActionPlusDataShallow)
            {
                key = string.Concat(key, "_",
                                    string.Join("|",
                                                filterContext.ActionParameters.Select(
                                                    _ => string.Concat(_.Key, "=", _.Value))));
            }
            else if (_preventionLevel == PreventionLevel.ActionPlusDataDeep)
            {
                var ser = new JavaScriptSerializer();
                key = string.Concat(key, "_",
                                    string.Join("|",
                                                filterContext.ActionParameters.Select(
                                                    _ =>
                                                    string.Concat(_.Key, "=",
                                                                  _.Value == null ? "" : ser.Serialize(_.Value)))));
            }
            return key;
        }


        private class PreventDoubleActionData
        {
            public DateTimeOffset DateAdded { get; set; }

            public ActionResult Result { get; set; }

            public int PreventionExpiryInSeconds { get; set; }
        }
    }
}