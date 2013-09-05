/*
Copyright (c) 2012 Andrew Newton (http://about.me/nootn)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcMega.Forms.DataAnnotations;
using MvcMega.Forms.MVC;

namespace MvcMega.Forms.WebsiteForTesting.Models
{
    public class ComplicatedForm
    {
        public ComplicatedForm()
        {
            PopulateAllTestDropDownItems();
            //TestSelectedDropDownItemId = 1; //this is for the detect form changes..

            TimeOfDayForMaskTesting = new TimeSpan(14, 15, 0);
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
            AllParentItems.Add(new SelectListItem { Text = "Parent 2", Value = "2", Selected=true });
            AllParentItems.Add(new SelectListItem { Text = "Parent 3", Value = "3" });

            AllChildItems = new CascadingSelectList();
            AllChildItems.ParentSelectListPropertyName = "ParentItemId";
            AllChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "1", Value = "0" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 1 of Parent 1", ParentValue = "1", Value = "1" });
            AllChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "2", Value = "0" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 1 of Parent 2", ParentValue = "2", Value = "2" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 2 of Parent 2", ParentValue = "2", Value = "3", Selected=true });
            AllChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "3", Value = "0" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 1 of Parent 3", ParentValue = "3", Value = "4" });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 2 of Parent 3", ParentValue = "3", Value = "5", Selected=true });
            AllChildItems.Add(new ChildSelectListItem { Text = "Child 3 of Parent 3", ParentValue = "3", Value = "6" });

            AllChildOfChildItems = new CascadingSelectList();
            AllChildOfChildItems.ParentSelectListPropertyName = "ChildItemId";
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "1", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 1", ParentValue = "1", Value = "1" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "2", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 2", ParentValue = "2", Value = "2" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 2 of Child 2", ParentValue = "2", Value = "3", Selected=true });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "3", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 3", ParentValue = "3", Value = "4" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 2 of Child 3", ParentValue = "3", Value = "5", Selected=true });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 3 of Child 3", ParentValue = "3", Value = "6" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "[Please Select]", ParentValue = "4", Value = "0" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 1 of Child 4", ParentValue = "4", Value = "7" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 2 of Child 4", ParentValue = "4", Value = "8", Selected=true });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 3 of Child 4", ParentValue = "4", Value = "9" });
            AllChildOfChildItems.Add(new ChildSelectListItem { Text = "ChildOfChild 4 of Child 4", ParentValue = "4", Value = "10" });

        }

        //This will show as a checkbox on the screen - checking it will show "TheNextHidingField"
        public bool ShowTheNextHidingField { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must supply TheNextHidingField if ShowTheNextHidingField is 'Yes'")]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ShowTheNextHidingField", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, false)]
        public string TheNextHidingField { get; set; }

        //This will show as a checkbox on the screen - checking it will enable "TheNextDisabledField"
        public bool EnableTheNextDisabledOrHiddenField { get; set; }

        public string TypeHideToHideOrDisableOrReadonly { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "EnableTheNextDisabledOrHiddenField", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, false)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "TypeHideToHideOrDisableOrReadonly", ChangeVisuallyAttribute.DisplayChangeIf.Equals, "Disable", false)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TypeHideToHideOrDisableOrReadonly", ChangeVisuallyAttribute.DisplayChangeIf.Equals, "Hide", false)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Readonly, "TypeHideToHideOrDisableOrReadonly", ChangeVisuallyAttribute.DisplayChangeIf.Equals, "Readonly", false)]
        public string TheNextDisabledOrHiddenField { get; set; }

        [HiddenInput]
        public int ComplicatedFormId { get; set; }

        [Required]
        public string EqualToInitial { get; set; }

        //[EqualTo("EqualToInitial", ErrorMessage = "EqualToSecond must be equal to EqualToInitial.")]
        public string EqualToSecond { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "EqualToSecond", ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, "", false)]
        public string EqualToSecondDisableIfSet { get; set; }

        public bool ShowNextField { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ShowNextField", ChangeVisuallyAttribute.DisplayChangeIf.Equals, false, false)]
        public string NextField { get; set; }

        public List<SelectListItem> AllTestDropDownItems { get; set; }

        public int TestSelectedDropDownItemId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DropDownOther is required if 'Other' is selected in TestSelectedDropDownItemId")]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TestSelectedDropDownItemId", ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, "3", true)]
        public string DropDownOther { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TestSelectedDropDownItemId", ChangeVisuallyAttribute.DisplayChangeIf.NotEquals, new[] { 1, 2 }, true)]
        public string DropDownShowIfOneOrTwo { get; set; }

        public int[] TestSelectedMultiSelectItemIds { get; set; }

        //[RequiredIfContains("TestSelectedMultiSelectItemIds", 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "MultiSelectOther is required if 'Other' is selected in TestSelectedMultiSelectItemIds")]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "TestSelectedMultiSelectItemIds", ChangeVisuallyAttribute.DisplayChangeIf.NotContains, "3", true)]
        public string MultiSelectOther { get; set; }

        [RequiredIfNotContains("TestSelectedMultiSelectItemIds", 2)]
        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Disabled, "TestSelectedMultiSelectItemIds", ChangeVisuallyAttribute.DisplayChangeIf.Contains, "2", true)]
        public string OnlySupplyIfMultiSelectDoesNotContainTwo { get; set; }

        [RequiredIfContainsOneOf("TestSelectedMultiSelectItemIds", new object[] { 1, 2 })]
        public string RequiredIfOneOrTwo { get; set; }


        public List<SelectListItem> AllParentItems { get; set; }

        public int ParentItemId { get; set; }

        public CascadingSelectList AllChildItems { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ParentItemId", ChangeVisuallyAttribute.DisplayChangeIf.Equals, 0, true)]
        public int ChildItemId { get; set; }

        public CascadingSelectList AllChildOfChildItems { get; set; }

        [ChangeVisually(ChangeVisuallyAttribute.ChangeTo.Hidden, "ChildItemId", ChangeVisuallyAttribute.DisplayChangeIf.Equals, 0, true)]
        public int[] ChildOfChildItemIds { get; set; }

        public SubDetails FirstSubDetails { get; set; }

        public SubDetails SecondSubDetails { get; set; }

        public TimeSpan TimeOfDayForMaskTesting { get; set; }

        public string PlainJaneFieldForStyleTesting { get; set; }
    }
}