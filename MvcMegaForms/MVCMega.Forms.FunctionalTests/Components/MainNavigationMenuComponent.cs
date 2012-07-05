
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;

namespace MVCMega.Forms.FunctionalTests.Components
{
    public class MainNavigationMenuComponent : UiComponent
    {
        public HomePage GoToHomePage()
        {
            return NavigateTo<HomePage>(By.LinkText("Admin"));
        }
    }
}