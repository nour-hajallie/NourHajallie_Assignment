using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Configuration;
using NUnit.Framework;
using NourHajallie_AutomationProject.Config;
using log4net;
using NUnit.Framework.Internal;
using log4net.Config;
using NourHajallie_AutomationProject.PageObjects;
using Assert = NUnit.Framework.Assert;
using NourHajallie_AutomationProject.DataEntities;
using NourHajallie_AutomationProject.BackendAPI;

namespace NourHajallie_AutomationProject
{
    [TestClass]
    public class UnitTest
    {
        IWebDriver driver;
        EnvironmentConfig environmentConfig;

        // Create a logger instance for the test class
        ILog log = LogManager.GetLogger(typeof(Test));

        string username;
        string password;
        string getPetId;
        string deletePetId;
        string api_key;

        [SetUp]
        public void Setup()
        {
            environmentConfig = new EnvironmentConfig();

            //Values reading from App.config file
            var configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = "../../../App.config" };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);


            string driverPath = config.AppSettings.Settings["driverPath"].Value;
            username = config.AppSettings.Settings["username"].Value;
            password = config.AppSettings.Settings["password"].Value;
            getPetId = config.AppSettings.Settings["getPetId"].Value;
            deletePetId = config.AppSettings.Settings["deletePetId"].Value;
            api_key = config.AppSettings.Settings["api_key"].Value;

            //Create reference for the browser
            // Test with Browser (Comment if you want to test without browser)
            // driver = new ChromeDriver(driverPath);

            //Test without Browser (Comment them if you want to test with browser)
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            driver = new ChromeDriver(chromeOptions);


            // Load the Log4Net configuration file
            log4net.Util.LogLog.InternalDebugging = true;
            XmlConfigurator.Configure(new FileInfo("../../../Config/log4net.config"));
        }

        [Test, Order(1)]
        public void TestUISampleApp()
        {
            //Scenario:
            //User fill username
            //User fill password
            //User click on Login button
            //Assert that the welcome message will be displayed

            log.Info("Testing UI Sample App Page Started");

            // Navigate to a Sample App Url
            String sampleAppUrl = environmentConfig.getSampleAppUrl();
            driver.Navigate().GoToUrl(sampleAppUrl);

            SampleAppPage sampleAppPage = new SampleAppPage(driver);

            log.Info("Fill the username");
            sampleAppPage.setUsernameField(username);

            log.Info("Fill the password");
            sampleAppPage.setPasswordField(password);

            log.Info("Click on Login");
            sampleAppPage.clickLoginButton();

            log.Info("Check if the success message is displayed");
            string successMessage = sampleAppPage.getSuccessMessageText();

            //Assertion
            if (successMessage != null)
            {
                log.Info($"Success message: {successMessage}");

                if (successMessage.Equals($"Welcome, {username}!"))
                {
                    log.Info($"Success message is as expected: 'Welcome, {username}!'");
                }
                else
                {
                    log.Error($"Success message is unexpected: '{successMessage}'");
                    Assert.Fail("Success message is not as expected.");
                }
            }
            else
            {
                log.Error("Success message was not found.");
                Assert.Fail("Success message element not found.");
            }

            // Close the browser
            driver.Quit();
        }

        [Test, Order(2)]
        public void TestUIAjaxData()
        {
            //Scenario:
            //User click on Button
            //Wait for data to appear (16 seconds)
            //Assert that it waits for label text to appear

            log.Info("Testing UI Sample App Page Started");

            // Navigate to a Ajax Data Url
            String ajaxUrl = environmentConfig.getAjaxUrl();
            driver.Navigate().GoToUrl(ajaxUrl);

            AjaxData ajaxData = new AjaxData(driver);

            log.Info("Click on Ajax Button");
            ajaxData.clickAjaxButton();

            // Create a WebDriverWait with the specified duration
            log.Info("Wait for 15 sec");
            Thread.Sleep(16000); // 16 seconds = 16,000 milliseconds

            String ajaxDataValue = ajaxData.ajaxDataIsDisplayed();
            log.Info("Ajax Data is displayed");

            // Assert that the value is equal to the expected text
            Assert.AreEqual("Data loaded with AJAX get request.", ajaxDataValue);
            log.Info("Ajax data is as expected: " + ajaxDataValue);

        }

        [Test, Order(3)]
        public void TestUITextInput()
        {
            //Scenario:
            //User record setting text into the input field
            //User click the button.
            //Assert that the button name is changing.

            log.Info("Testing UI Text Input Page Started");

            // Navigate to a Text Input Url
            String textInputUrl = environmentConfig.getTextInputUrl();
            driver.Navigate().GoToUrl(textInputUrl);

            TextInputPage textInput = new TextInputPage(driver);

            //Fill the button Input
            String buttonNewValue = "NewButton";
            log.Info("Fill the button Input with this value: " + buttonNewValue);
            textInput.setButtonNewName(buttonNewValue);

            //Click on the button
            log.Info("Click on the button");
            textInput.clickButton();

            //Check the new button value
            log.Info("Check the new button value");
            string newButtonAfterClick = textInput.checkButtonNameChanged();

            //Assert that the new value is the same that was filled in the input
            log.Info("Assert that the new value is the same that was filled in the input");
            Assert.AreEqual(buttonNewValue, newButtonAfterClick);
            log.Info("New button value is " + newButtonAfterClick);
        }

        [Test, Order(4)]
        public void TestApiPostListOfUsers()
        {
            //API FOR TEST: POST v2/user/createWithList
            //Creat list of user
            //Call the API POST v2/user/createWithList
            //Assert that the response code is 200 -- list of users created successfully

            log.Info("Testing API Create list of users with given input");

            // Define users to create
            var usersToCreate = new List<UserResponse>
        {
            new UserResponse
            {
                id = 1,
                username = "userxxx",
                firstName = "User 1",
                lastName = "LastName 1",
                email = "user1@hotmail.com",
                password = "xxxxxxx",
                phone = "00xx2222222",
                userStatus = 0
            },
            new UserResponse
            {
                id = 2,
                username = "useryyyy",
                firstName = "User 2",
                lastName = "LastName 2",
                email = "user2@hotmail.com",
                password = "xxxxyyyy",
                phone = "00xx2222222",
                userStatus = 0
            }
        };

            ApiFunctions api = new ApiFunctions();

            //Call the endpoint
            log.Info("Call the function of API create list users and send the users defined to create");
            ApiResponse createdUsersResponse = api.PostListOfUsers(usersToCreate);

            // Assert that the code is 200
            log.Info("Assert that the code sent from API is 200");
            Assert.AreEqual(200, createdUsersResponse.code);
            log.Info("List of users created successfully");

        }

        [Test, Order(5)]
        public void TestApiGetPetById()
        {
            //API FOR TEST: GET /v2/pet/{petId}
            //Get the info of a pet by Id from app.config file
            //Call the API GET /v2/pet/{petId}
            //Assert that the code is 200 and display the data of this pet

            log.Info("Testing API Get the Pet info by pet id");

            ApiFunctions api = new ApiFunctions();

            //Call the endpoint
            log.Info("Call the function of API to get the data of a pet");
            PetResponse petResponse = api.GetPetById(getPetId);

            //Assert that the data of the pet are sent
            log.Info("Assert that the data of the Pet are sent");
            Assert.IsNotNull(petResponse.id);
            log.Info("List of Pet sent successfully");
            log.Info("Dog Id: " + petResponse.id);
            log.Info("Dog Name: " + petResponse.name);
            log.Info("Dog Category Id: " + petResponse.category.id);
            log.Info("Dog Category Name: " + petResponse.category.name);
            log.Info("Dog PhotoUrl: " + petResponse.photoUrls[0]);
            log.Info("Dog Tags Id: " + petResponse.tags[0].id);
            log.Info("Dog Tags Name: " + petResponse.tags[0].name);
            log.Info("Dog Status: " + petResponse.status);


        }

        [Test, Order(6)]
        public void TestApiDeletePetById()
        {
            //API FOR TEST: DELETE /v2/pet/{petId}
            //Get the info of a pet to delete by Id from app.config file
            //Call the API DELETE /v2/pet/{petId}
            //Assert that the code is 200 and the pet is deleted

            log.Info("Testing API Delete a pet");

            ApiFunctions api = new ApiFunctions();

            //Call the endpoint
            log.Info("Call the function of API to delete a pet by petId");
            ApiResponse deletedPetResponse = api.DeletePetByPetId(deletePetId, api_key);

            // Assert that the code is 200
            Assert.AreEqual(200, deletedPetResponse.code, "Pet with id " + deletedPetResponse.message + " is already deleted or not available");
            log.Info("Pet with Pet Id " + deletedPetResponse.message + " is deleted");

        }
    }
}