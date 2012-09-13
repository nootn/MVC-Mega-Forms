using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;

namespace MVCMega.Forms.FunctionalTests.Pages
{
    public class SingleFormPageBase<TViewModel> : Page<TViewModel> where TViewModel : class, new()
    {
        /// <summary>
        /// Works if there is only one form on the page - fills in the form with the supplied model
        /// </summary>
        /// <param name="model">The model to fill the form with</param>
        public void FillSingleForm(TViewModel model)
        {
            Input().Model(model);
        }


        /// <summary>
        /// Works if there is only one form on the page - submits the form
        /// </summary>
        /// <returns>The page that is navigated to after submitting the form</returns>
        public Page SubmitSingleForm()
        {
            return Navigate().To<Page>(By.CssSelector("input[type=submit]"));
        }
    }
}
