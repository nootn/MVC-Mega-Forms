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
using System.Web.Mvc;
using System.Web.UI;

namespace MvcMega.Forms.MVC
{
    public abstract class MvcHtmlContainerBase : IDisposable
    {
        protected readonly ViewContext ViewContext;
        private bool _disposed;

        /// <summary>
        ///   Initializes a new instance of the class using the specified view context.
        /// </summary>
        /// <param name="viewContext"> An object that encapsulates the information that is required in order to render a view. </param>
        /// <exception cref="T:System.ArgumentNullException">The
        ///   <paramref name="viewContext" />
        ///   parameter is null.</exception>
        protected MvcHtmlContainerBase(ViewContext viewContext)
        {
            if (viewContext == null)
                throw new ArgumentNullException("viewContext");
            ViewContext = viewContext;
        }

        public abstract HtmlTextWriterTag Tag { get; }

        /// <summary>
        ///   Releases all resources that are used by the current instance of the class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged and, optionally, managed resources used by the current instance of the class.
        /// </summary>
        /// <param name="disposing"> true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;

            ViewContext.Writer.Write(string.Concat("</", Tag.ToString().ToLower(), ">"));
        }

        /// <summary>
        ///   Ends the container and disposes of all resources.
        /// </summary>
        public void EndControlGroup()
        {
            Dispose(true);
        }
    }
}