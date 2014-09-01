// /*
// Copyright (c) 2012 Andrew Newton (http://about.me/nootn)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using System;
using MvcMega.Forms.MVC.Html;

namespace MvcMega.Forms.MVC
{
    public class ControlClassOverrideGroup : IDisposable
    {
        private readonly string _defaultControlGroupClassOriginalValue;
        private readonly string _defaultControlsClassOriginalValue;
        private readonly string _defaultHelpInlineClassOriginalValue;
        private readonly string _defaultLabelClassOriginalValue;

        private bool _disposed;

        public ControlClassOverrideGroup(string defaultControlGroupClass = null, string defaultControlsClass = null,
            string defaultHelpInlineClass = null, string defaultLabelClass = null)
        {
            if (defaultControlGroupClass != null)
            {
                _defaultControlGroupClassOriginalValue = HtmlExtensions.GetDefaultControlGroupClass();
                HtmlExtensions.GetDefaultControlGroupClass = () => defaultControlGroupClass;
            }

            if (defaultControlsClass != null)
            {
                _defaultControlsClassOriginalValue = HtmlExtensions.GetDefaultControlsClass();
                HtmlExtensions.GetDefaultControlsClass = () => defaultControlsClass;
            }

            if (defaultControlsClass != null)
            {
                _defaultHelpInlineClassOriginalValue = HtmlExtensions.GetDefaultHelpInlineClass();
                HtmlExtensions.GetDefaultHelpInlineClass = () => defaultHelpInlineClass;
            }

            if (defaultLabelClass != null)
            {
                _defaultLabelClassOriginalValue = HtmlExtensions.GetDefaultLabelClass();
                HtmlExtensions.GetDefaultLabelClass = () => defaultLabelClass;
            }
        }

        /// <summary>
        ///     Releases all resources that are used by the current instance of the class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and, optionally, managed resources used by the current instance of the class.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;

            //Reset all the defaults at the end
            if (_defaultControlGroupClassOriginalValue != null)
            {
                HtmlExtensions.GetDefaultControlGroupClass = () => _defaultControlGroupClassOriginalValue;
            }

            if (_defaultControlsClassOriginalValue != null)
            {
                HtmlExtensions.GetDefaultControlsClass = () => _defaultControlsClassOriginalValue;
            }

            if (_defaultHelpInlineClassOriginalValue != null)
            {
                HtmlExtensions.GetDefaultHelpInlineClass = () => _defaultHelpInlineClassOriginalValue;
            }

            if (_defaultLabelClassOriginalValue != null)
            {
                HtmlExtensions.GetDefaultLabelClass = () => _defaultLabelClassOriginalValue;
            }
        }
    }
}