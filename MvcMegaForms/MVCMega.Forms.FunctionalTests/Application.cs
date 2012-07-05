using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MVCMega.Forms.FunctionalTests.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Extensions;
using TestStack.Seleno.PageObjects;

namespace MVCMega.Forms.FunctionalTests
{
    public class Application : Page
    {
        public Application(RemoteWebDriver browser)
        {
            Browser = browser;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
        }

        void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            Browser.Close();
        }

        public HomePage Start()
        {
            return NavigateTo<HomePage>(IISExpressRunner.HomePage);
        }

        public static HomePage HomePage
        {
            get
            {
                var dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
                var solutionPath = dirInfo.Parent.Parent.Parent.FullName;
                var path = Path.Combine(solutionPath, "MvcMega.Forms.WebsiteForTesting");
                IISExpressRunner.Start(path, 12121);

                var browser = new InternetExplorerDriver(new InternetExplorerOptions { IntroduceInstabilityByIgnoringProtectedModeSettings = true });

                browser.SetImplicitTimeout(10);
                var homePage = new Application(browser).Start();
                return homePage;
            }
        }
    }
}
