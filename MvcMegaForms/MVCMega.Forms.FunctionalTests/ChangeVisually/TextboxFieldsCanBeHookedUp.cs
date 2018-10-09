// /*
// Copyright (c) 2012 Andrew Newton (http://about.me/nootn)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using System.Linq;
using MVCMega.Forms.FunctionalTests.Pages;
using MVCMega.Forms.FunctionalTests.Pages.ChangeVisually;
using MvcMega.Forms.WebsiteForTesting.Models.ChangeVisuallyScreens;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using TestStack.BDDfy;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace MVCMega.Forms.FunctionalTests.ChangeVisually
{
    [TestFixture]
    [Story(AsA = "As a user",
        IWant = "I want to hide and show fields based on changing textbox values",
        SoThat = "So that my forms will be awesome")]
    public class TextboxFieldsCanBeHookedUp
    {
        private TextboxPage _page;

        private void GivenUserIsOnTheTextboxPage()
        {
            _page = AssemblyFixture.Host.Instance.NavigateToInitialPage<HomePage>()
                .GoToTextboxPage();
        }

        private void WhenMakeNotEmptyToShowNextField1IsEmptyInitially()
        {
            _page.MakeNotEmptyToShowNextField1.SendKeys("SomeValue\t");

            _page.ClearMakeNotEmptyToShowNextField1();
        }

        private void WhenMakeNotEmptyToShowNextField1IsNotEmpty()
        {
            _page.ClearMakeNotEmptyToShowNextField1();
            _page.MakeNotEmptyToShowNextField1.SendKeys("SomeValue\t");            
        }

        private void ThenNextField1IsHidden()
        {
            //TODO: waiting on Seleno to get the element properly            
            Assert.False(_page.NextField1.Displayed);            
        }

        private void ThenNextField1IsShown()
        {
            //TODO: waiting on Seleno to get the element properly            
            Assert.True(_page.NextField1.Displayed);            
        }

        [Test]
        public void SetTextboxToEmptyToHideNextValue()
        {
            this.Given(_ => _.GivenUserIsOnTheTextboxPage())
                .When(_ => _.WhenMakeNotEmptyToShowNextField1IsEmptyInitially())
                .Then(_ => _.ThenNextField1IsHidden())
                .BDDfy();
        }

        [Test]
        public void SetTextboxToNotEmptyToShowNextValue()
        {
            this.Given(_ => _.GivenUserIsOnTheTextboxPage())
                .When(_ => _.WhenMakeNotEmptyToShowNextField1IsNotEmpty())
                .Then(_ => _.ThenNextField1IsShown())
                .BDDfy();
        }
    }
}