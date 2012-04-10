/*
Copyright (c) 2012 Andrew Newton (http://about.me/nootn)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foolproof;
using MvcMega.Forms.DataAnnotations;

namespace MvcMegaFormsDemo.Models
{
    public class ComplicatedForm
    {
        public ComplicatedForm()
        {
            PopulateAllTestDropDownItems();
        }

        public void PopulateAllTestDropDownItems()
        {
            AllTestDropDownItems = new List<SelectListItem>();
            AllTestDropDownItems.Add(new SelectListItem {Text = "One", Value = "1"});
            AllTestDropDownItems.Add(new SelectListItem {Text = "Two", Value = "2"});
            AllTestDropDownItems.Add(new SelectListItem {Text = "Other", Value = "3"});
        }

        [HiddenInput]
        public int ComplicatedFormId { get; set; }

        [Required]
        public string EqualToInitial { get; set; }

        [EqualTo("EqualToInitial", ErrorMessage = "EqualToSecond must be equal to EqualToInitial.")]
        public string EqualToSecond { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "EqualToSecond", ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, "", false)]
        public string EqualToSecondDisableIfSet { get; set; }

        public bool ShowNextField { get; set; }

        [RequiredIf("ShowNextField", Operator.EqualTo, true)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ShowNextField", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, false)]
        public string NextField { get; set; }

        public List<SelectListItem> AllTestDropDownItems { get; set; }

        public int TestSelectedDropDownItemId { get; set; }

        [RequiredIf("TestSelectedDropDownItemId", Operator.EqualTo, 3)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TestSelectedDropDownItemId", ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, "3", false)]
        public string DropDownOther { get; set; }

        public int[] TestSelectedMultiSelectItemIds { get; set; }
        
        [RequiredIfContains("TestSelectedMultiSelectItemIds", 3)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TestSelectedMultiSelectItemIds", ChangeVisuallyAttribute.DisplayChangeIf.NotContains, "3", false)]
        public string MultiSelectOther { get; set; }

        [RequiredIfNotContains("TestSelectedMultiSelectItemIds", 2)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "TestSelectedMultiSelectItemIds", ChangeVisuallyAttribute.DisplayChangeIf.Contains, "2", false)]
        public string OnlySupplyIfMultiSelectDoesNotContainTwo { get; set; }

    }
}