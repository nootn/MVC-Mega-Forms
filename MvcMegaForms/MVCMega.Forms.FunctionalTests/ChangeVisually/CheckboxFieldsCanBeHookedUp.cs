using System;
using MVCMega.Forms.FunctionalTests.Pages;
using MVCMega.Forms.FunctionalTests.Pages.ChangeVisually;
using MvcMega.Forms.WebsiteForTesting.Models.ChangeVisuallyScreens;
using NUnit.Framework;
using TestStack.BDDfy;
using TestStack.BDDfy.Core;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;
using TestStack.Seleno.PageObjects.Locators;

namespace MVCMega.Forms.FunctionalTests.ChangeVisually
{
    [TestFixture]
    [Story(AsA = "As a user",
        IWant = "I want to hide and show fields based on ticking and un-ticking check boxes",
        SoThat = "So that my forms will be awesome")]
    public class CheckboxFieldsCanBeHookedUp
    {
        private CheckboxPage _checkboxPage;

        private void GivenUserIsOnTheCheckboxPage()
        {
            _checkboxPage = Application
                .HomePage
                .GoToCheckboxPage();
        }

        private void WhenShowNextFieldIsChecked()
        {
            var model = new CheckboxModel();
            model.TickCheckboxToShowNextField = true;
            _checkboxPage.FillWithModel(model);
        }

        private void ThenNextFieldIsShown()
        {
            var elem = _checkboxPage.AssertThatElements(By.Id("NextField"));

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