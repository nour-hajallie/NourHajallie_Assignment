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
    public class TextInputPage
    {

        protected IWebDriver driver;
        protected WebDriverWait driverWait;

        // Create a logger instance for the test class
        ILog log = LogManager.GetLogger(typeof(SampleAppPage));

        public TextInputPage(IWebDriver driver)
        {
            this.driver = driver;
            driverWait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 0, 30, 0));

            // Load the Log4Net configuration file
            log4net.Util.LogLog.InternalDebugging = true;
            XmlConfigurator.Configure(new FileInfo("../../../Config/log4net.config"));
        }

        //Locate the elements in the page
        public By buttonInputLocator => By.Id("newButtonName");
        public By buttonLocator => By.Id("updatingButton");


        public void setButtonNewName(String buttonNewName)
        {
            try
            {
                IWebElement buttonInputElement = driver.FindElement(buttonInputLocator);
                driverWait.Until(e => buttonInputElement);
                buttonInputElement.Clear();
                buttonInputElement.SendKeys(buttonNewName);
            }
            catch (Exception ex)
            {
                log.Error("Error while filling the button input ", ex);

                // Explicitly fail the test
                Assert.Fail("button input element was not found.");
            }
        }

        public void clickButton()
        {
            try
            {
                IWebElement buttonElement = driver.FindElement(buttonLocator);

                driverWait.Until(e => buttonElement);
                buttonElement.Click();

            }
            catch (Exception ex)
            {
                log.Error("Error while clicking on button ", ex);

                // Explicitly fail the test
                Assert.Fail("Button element was not found.");
            }
        }

        public string checkButtonNameChanged()
        {
            try
            {
                IWebElement buttonElement = driver.FindElement(buttonLocator);

                driverWait.Until(e => buttonElement);
                string buttonValue = buttonElement.Text;
                return buttonValue;

            }
            catch (Exception ex)
            {
                log.Error("Error while clicking on button ", ex);

                // Explicitly fail the test
                Assert.Fail("Button element was not found.");
                return null;
            }
        }
    }
}
