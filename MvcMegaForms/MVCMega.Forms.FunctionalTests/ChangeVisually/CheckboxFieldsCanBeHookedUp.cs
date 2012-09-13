// /*
// Copyright (c) 2012 Andrew Newton (http://about.me/nootn)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using MVCMega.Forms.FunctionalTests.Pages;
using MVCMega.Forms.FunctionalTests.Pages.ChangeVisually;
using MvcMega.Forms.WebsiteForTesting.Models.ChangeVisuallyScreens;
using NUnit.Framework;
using OpenQA.Selenium;
using TestStack.BDDfy;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace MVCMega.Forms.FunctionalTests.ChangeVisually
{
    [TestFixture]
    [Story(AsA = "As a user",
        IWant = "I want to hide and show fields based on ticking and un-ticking check boxes",
        SoThat = "So that my forms will be awesome")]
    public class CheckboxFieldsCanBeHookedUp
    {
        private CheckboxPage _page;

        private void GivenUserIsOnTheCheckboxPage()
        {
            _page = new HomePage()
                .GoToCheckboxPage();
        }

        private void WhenShowNextFieldIsChecked()
        {
            var model = new CheckboxModel();
            model.TickCheckboxToShowNextField = true;
            _page.FillSingleForm(model);
        }

        private void ThenNextFieldIsShown()
        {
            var elem = _page.AssertThatElements(By.Id("NextField"));

            //TODO: assert that elem exists and is not hidden (I.e. check html attributes..)

            //Mehdi, could there be a nicer way to get the ID?  We know the model type.. so something like:
            //var elem = _homePage.GetElementByModelProperty(m => m.NextField).
        }

        [Test]
        public void SetCheckboxFromUntickedToTicked()
        {
            this.Given(_ => _.GivenUserIsOnTheCheckboxPage())
                .When(_ => _.WhenShowNextFieldIsChecked())
                .Then(_ => _.ThenNextFieldIsShown())
                .BDDfy();
        }
    }
}