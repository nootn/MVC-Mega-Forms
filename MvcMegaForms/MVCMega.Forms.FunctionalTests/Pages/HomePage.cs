using MVCMega.Forms.FunctionalTests.Pages.ChangeVisually;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;

namespace MVCMega.Forms.FunctionalTests.Pages
{
    public class HomePage : Page
    {
        public CheckboxPage GoToCheckboxPage()
        {
            return NavigateTo<CheckboxPage>(By.LinkText("Checkbox"));
        }


        //public MainNavigationMenuComponent MainMenu
        //{
        //    get { return GetComponent<MainNavigationMenuComponent>(); }
        //}
    }
}
