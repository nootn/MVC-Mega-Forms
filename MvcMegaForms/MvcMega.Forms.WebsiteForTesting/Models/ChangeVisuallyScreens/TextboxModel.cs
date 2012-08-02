// /*
// Copyright (c) 2012 Andrew Newton (http://about.me/nootn)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using MvcMega.Forms.DataAnnotations;

namespace MvcMega.Forms.WebsiteForTesting.Models.ChangeVisuallyScreens
{
    public class TextboxModel
    {
        public string MakeNotEmptyToShowNextField1 { get; set; }

        //Note that empty string are considered 'null' when comparing, so the 'ConditionPassesIfNull' must be 'true' for this to work
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "MakeNotEmptyToShowNextField1",
            ChangeVisuallyAttribute.DisplayChangeIf.Equals, "", true)]
        public string NextField1 { get; set; }


        public string MakeYesToShowNextField2 { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "MakeYesToShowNextField2",
            ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, "yes", true)]
        public string NextField2 { get; set; }


        public string MakeNumberLessThan3ToShowNextField3 { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "MakeNumberLessThan3ToShowNextField3",
            ChangeVisuallyAttribute.DisplayChangeIf.GreaterThanOrEquals, "3", true)]
        public string NextField3 { get; set; }
    }
}