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
    public class AjaxData
    {
        protected IWebDriver driver;
        protected WebDriverWait driverWait;

        // Create a logger instance for the test class
        ILog log = LogManager.GetLogger(typeof(SampleAppPage));

        public AjaxData(IWebDriver driver)
        {
            this.driver = driver;
            driverWait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 0, 30, 0));

            // Load the Log4Net configuration file
            log4net.Util.LogLog.InternalDebugging = true;
            XmlConfigurator.Configure(new FileInfo("../../../Config/log4net.config"));
        }

        //Locate Element
        public By ajaxButton => By.Id("ajaxButton");
        public By ajaxData => By.Id("content");

       

    public void clickAjaxButton()
        {
            try
            {
                IWebElement ajaxButtonElement = driver.FindElement(ajaxButton);

                driverWait.Until(e => ajaxButtonElement);
                ajaxButtonElement.Click();

            }
            catch (Exception ex)
            {
                log.Error("Error while clicking on Ajax button ", ex);

                // Explicitly fail the test
                Assert.Fail("Ajax button element was not found.");
            }
        }

        public string ajaxDataIsDisplayed()
        {
            try
            { 
                // Use WebDriver to find the element
                IWebElement element = driver.FindElement(ajaxData);

                // Assert that the element has appeared
                Assert.IsNotNull(element, "The element did not appear within 15 seconds.");

                // Get the text value of the element
                string elementText = element.Text;

                return elementText;
            }
            catch (NoSuchElementException)
            {
                // Handle the case where the element is not found
                log.Error("Ajax data element was not found.");

                // Explicitly fail the test or return a specific value, depending on your requirements
                return null; // You can change this to return a sentinel value, throw an exception, or fail the test
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                log.Error("Error while waiting for Ajax Data", ex);

                // Explicitly fail the test or return a specific value, depending on your requirements
                return null; // You can change this to return a sentinel value, throw an exception, or fail the test
            }
        }

    }


}
