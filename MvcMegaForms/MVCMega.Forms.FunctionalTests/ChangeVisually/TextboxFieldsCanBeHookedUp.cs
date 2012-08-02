using System;
using System.Linq;
using MVCMega.Forms.FunctionalTests.Pages;
using MVCMega.Forms.FunctionalTests.Pages.ChangeVisually;
using MvcMega.Forms.WebsiteForTesting.Models.ChangeVisuallyScreens;
using NUnit.Framework;
using OpenQA.Selenium;
using TestStack.BDDfy;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;
using By = TestStack.Seleno.PageObjects.Locators.By;

namespace MVCMega.Forms.FunctionalTests.ChangeVisually
{
    [TestFixture]
    [Story(AsA = "As a user",
        IWant = "I want to hide and show fields based on changing textbox values",
        SoThat = "So that my forms will be awesome")]
    public class TextboxFieldsCanBeHookedUp
    {
        private TextboxPage _page;

        private void GivenUserIsOnTheCheckboxPage()
        {
            _page = Application
                .HomePage
                .GoToTextboxPage();
        }

        private void WhenMakeNotEmptyToShowNextField1IsEmptyInitially()
        {
            var model = new TextboxModel();
            model.MakeNotEmptyToShowNextField1 = string.Empty;
            _page.FillWithModel(model);
        }

        private void WhenMakeNotEmptyToShowNextField1IsNotEmpty()
        {
            var model = new TextboxModel();
            model.MakeNotEmptyToShowNextField1 = "something";
            _page.FillWithModel(model);
        }

        private void ThenNextField1IsHidden()
        {
            //TODO: waiting on Seleno to get the element properly
            var elem = _page.AssertThatElements(By.Id("NextField1"));
            elem.Exist();
            elem.ConformTo(i => Assert.IsTrue(i.All(a => a.GetCssValue("display") == "none")));
        }

        private void ThenNextField1IsShown()
        {
            //TODO: waiting on Seleno to get the element properly
            var elem = _page.AssertThatElements(By.Id("NextField1"));
            elem.Exist();
            //elem.ConformTo(i => Assert.IsTrue(i.All(a => a.FindElement(new By.jQueryBy("").Parent(".control-group")).GetCssValue("display") == "block")));
        }

        [Test]
        public void SetTextboxToEmptyToHideNextValue()
        {
            this.Given(_ => _.GivenUserIsOnTheCheckboxPage())
                .When(_ => _.WhenMakeNotEmptyToShowNextField1IsEmptyInitially())
                .Then(_ => _.ThenNextField1IsHidden())
                .BDDfy();
        }

        [Test]
        public void SetTextboxToNotEmptyToShowNextValue()
        {
            this.Given(_ => _.GivenUserIsOnTheCheckboxPage())
                .When(_ => _.WhenMakeNotEmptyToShowNextField1IsNotEmpty())
                .Then(_ => _.ThenNextField1IsShown())
                .BDDfy();
        }
    }
}