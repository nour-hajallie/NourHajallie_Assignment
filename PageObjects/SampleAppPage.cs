using log4net.Config;
using log4net;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NourHajallie_AutomationProject.PageObjects
{
    public class SampleAppPage
    {
        protected IWebDriver driver;
        protected WebDriverWait driverWait;

        // Create a logger instance for the test class
        ILog log = LogManager.GetLogger(typeof(SampleAppPage));

        public SampleAppPage(IWebDriver driver)
        {
            this.driver = driver;
            driverWait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 0, 30, 0));

            // Load the Log4Net configuration file
            log4net.Util.LogLog.InternalDebugging = true;
            XmlConfigurator.Configure(new FileInfo("../../../Config/log4net.config"));
        }

        //Locate the elements in the page
        public By usernameInput => By.Name("UserName");
        public By passwordInput => By.Name("Password");
        public By loginButton => By.Id("login");
        public By successMessage => By.Id("loginstatus");
        public void setUsernameField(String username)
        {
            try
            {
                IWebElement usernameInputElement = driver.FindElement(usernameInput);
                driverWait.Until(e => usernameInputElement);
                usernameInputElement.Clear();
                usernameInputElement.SendKeys(username);
            }
            catch (Exception ex)
            {
                log.Error("Error while filling the username ", ex);

                // Explicitly fail the test
                Assert.Fail("Username input element was not found.");
            }
        }

        public void setPasswordField(String password)
        {
            try
            {
                IWebElement passwordInputElement = driver.FindElement(passwordInput);
                driverWait.Until(e => passwordInputElement);
                passwordInputElement.Clear();
                passwordInputElement.SendKeys(password);
            }
            catch (Exception ex)
            {
                log.Error("Error while filling the password ", ex);

                // Explicitly fail the test
                Assert.Fail("Password input element was not found.");
            }
        }

        public void clickLoginButton()
        {
            try
            {
                IWebElement loginButtonElement = driver.FindElement(loginButton);

                driverWait.Until(e => loginButtonElement);
                loginButtonElement.Click();

            }
            catch (Exception ex)
            {
                log.Error("Error while clicking on login button ", ex);

                // Explicitly fail the test
                Assert.Fail("Login button element was not found.");
            }
        }

        public string getSuccessMessageText()
        {
            try
            {
                IWebElement successMessageElement = driver.FindElement(successMessage);

                if (successMessageElement.Displayed)
                {
                    string successMessageText = successMessageElement.Text.Trim();
                    log.Info($"Success message is displayed with text: '{successMessageText}'");
                    return successMessageText;
                }
                else
                {
                    string errorMessage = "Success message element is not displayed.";
                    log.Error(errorMessage);
                    Assert.Fail(errorMessage);
                    return null; // The element is not displayed
                }
            }
            catch (NoSuchElementException)
            {
                string errorMessage = "Success message element was not found.";
                log.Error(errorMessage);
                Assert.Fail(errorMessage);
                return null; // The element was not found
            }
        }


    }
}
