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
using MvcMega.Forms.MVC;

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
            AllTestDropDownItems.Add(new SelectListItem { Text = "One", Value = "1" });
            AllTestDropDownItems.Add(new SelectListItem { Text = "Two", Value = "2" });
            AllTestDropDownItems.Add(new SelectListItem { Text = "Other", Value = "3" });

            AllParentItems = new List<SelectListItem>();
            AllParentItems.Add(new SelectListItem { Text = "[Please Select]", Value = "0" });
            AllParentItems.Add(new SelectListItem { Text = "Parent 1", Value = "1" });
            AllParentItems.Add(new SelectListItem { Text = "Parent 2", Value = "2" });
            AllParentItems.Add(new SelectListItem { Text = "Parent 3", Value = "3" });

            AllChildItems = new CascadingSelectList();
            AllChildItems.ParentSelectListPropertyName = "ParentItemId";
            AllChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "1", Value = "0" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 1 of Parent 1", ParentValue = "1", Value = "1" });
            AllChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "2", Value = "0" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 1 of Parent 2", ParentValue = "2", Value = "2" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 2 of Parent 2", ParentValue = "2", Value = "3" });
            AllChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "3", Value = "0" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 1 of Parent 3", ParentValue = "3", Value = "4" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 2 of Parent 3", ParentValue = "3", Value = "5" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 3 of Parent 3", ParentValue = "3", Value = "6" });

            AllChildOfChildItems = new CascadingSelectList();
            AllChildOfChildItems.ParentSelectListPropertyName = "ChildItemId";
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "1", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 1", ParentValue = "1", Value = "1" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "2", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 2", ParentValue = "2", Value = "2" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 2 of Child 2", ParentValue = "2", Value = "3" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "3", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 3", ParentValue = "3", Value = "4" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 2 of Child 3", ParentValue = "3", Value = "5" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 3 of Child 3", ParentValue = "3", Value = "6" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "4", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 4", ParentValue = "4", Value = "7" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 2 of Child 4", ParentValue = "4", Value = "8" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 3 of Child 4", ParentValue = "4", Value = "9" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 4 of Child 4", ParentValue = "4", Value = "10" });

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


        public List<SelectListItem> AllParentItems { get; set; }

        public int ParentItemId { get; set; }

        public CascadingSelectList AllChildItems { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ParentItemId", ChangeVisuallyAttribute.DisplayChangeIf.Equals, 0, true)]
        public int ChildItemId { get; set; }

        public CascadingSelectList AllChildOfChildItems { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ChildItemId", ChangeVisuallyAttribute.DisplayChangeIf.Equals, 0, true)]
        public int ChildOfChildItemId { get; set; }
    }
}