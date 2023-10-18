# Automation Project

The aim of this project is to implement automation testing for UI and API with the objective of validating Chosen key scenarios.

The repository include functions on the uitestingplayground for UI and petstore.swagger.io for API to verify that the expected results are achieved.


# Technologies and Languages

Sample Test Automation project written in C#

Project uses RestSharp and NUnit for the API functions and Selenium for the UI functions.

Visual Studio 2022 was used to run locally the project/tests and Jenkins was used to create the pipeline.

All dependencies installed from NuGet for easy management.

# Directories and Files

The project contains 

	- BackendAPI directory: this directory contains the class that returns the responses of the APIs.

	- Config directory: this directory contains the environment config class and the log4net config file.

	- DataEntities directory:  this directory contains the classes to map the response of the api used in the backend functions.
	
	- PageObjects directory: in this directory you will find the classes containing the selectors and functions related to each page used 
	
	in the front end functions.

	- App.config file storing the sensitive information.

	- UnitTest.cs: class where the UI and API Test functions are created.

	- Jenkinsfile: this file includes the Pipeline that defines a series of steps that are executed in a specific order to automate a software development process. In our case, 3 steps will be executed: CheckOut, Test and Retrieve logs.

## Log4Net Configuration

This application uses log4net to log important information and errors. 

The 'log4net.config' file contains the logging configuration settings. 

By default, the file is configured to log to a file named 'LogReport_yyyyMMdd.txt' in the directory 'bin\Debug\reportResult\Logs\LogReport.logLogs'.

To modify the logging configuration, you can modify the 'log4net.config' file directly. 

For example, you can change the logging level, the output location, or the formatting of the log messages.


# Pre-Requisite

Below are the requirements or conditions that need to be filled in App.config before the testing can be carried out:

1- Specify the location of the chromedriver in your device.

2- Fill username value (use any non-empty user name).

3- Fill password value to be equal to 'pwd'.

4- Fill a getPetId value (Value must be an integer and valid).

5- Fill a deletedPetId value (Value must be an integer and valid).

6- Fill the api_key value to be equal to "special_key" (Specified for this sample).

App.config should be filled in this way: 

```
	<appSettings>
		<add key="driverPath" value="CHROMEDRIVER_PATH" />
		<add key="username" value="USERNAME" />
		<add key="password" value="PASSWORD"/>
		<add key="getPetId" value="INTEGER_VALUE"/>
		<add key="deletePetId" value="INTEGER_VALUE"/>
		<add key="api_key" value="special_key"/>
	</appSettings>
```

# Installation
1. Clone the repository: 

```
git clone https://github.com/nour-hajallie/NourHajallie_Assignment.git
```

2. Open the solution file NourHajallie_Assignment.sln in Visual Studio.

3. Install the NuGet packages by selecting from the "Manage NuGet Packages".

4. Build the solution by selecting "Build Solution" from the "Build" menu or pressing Ctrl+Shift+B.

## Running the project Locally

The project was designed to run in a headless mode (headless browser), so the below steps can be executed to run the project locally and in a normal mode ( You will be able to see the browser executing the test):

1. In UnitTest1.cs make sure to **remove the comment** from line 52 

```
// driver = new ChromeDriver(driverPath);
```

2. In UnitTest1.cs make sure to **comment** lines 55-56-57 

```
var chromeOptions = new ChromeOptions();
chromeOptions.AddArgument("--headless");
driver = new ChromeDriver(chromeOptions);
```

3. Open the Test Explorer by selecting "Test Explorer" from the "Test" menu.

4. Run the tests by selecting "Run All" in the Test Explorer or by pressing Ctrl+R, A.

## Running the Jenkins Pipeline

To run the Jenkins pipeline, follow these steps:

1. Install Jenkins on your local machine or any server on the cloud.

	1.1. Make sure to install the needed plugins for this project in Jenkins

		- Git

		- ChromeDriver

		- DotNet SDK Support

2. Create a new Jenkins job and select "Pipeline" as the job type.

	2.1. Configure the project/pipeline created by clicking on "Configure".

	2.2. Under Build Triggers section, select "GitHub hook trigger for GITScm polling".

	2.3. Under Pipeline section, fill the below:

		- Definition:pipeline script from scm 
		
		- SCM: Git 
		
		- repository url: you_repo_url 

		- Branch Specifier: main 

		- Script Path: Jenkinfile (that the name of the file we have in our repo)

	2.4. Apply and Save the configuration.

3. Establish a connection between Github and Jenkins (using [Webhook](https://www.cprime.com/resources/blog/how-to-integrate-jenkins-github/)).
	
		- The Jenkins pipeline will be reading the Jenkins file from this repo to run the stages or in case any push was done to the repo, the pipeline will be running automatically.

4. Save the job configuration and click on "Build Now" to run the pipeline.

Picture showing Pipeline running without errors : [PipelineRun](pipelineResult.png)

Picture showing Pipeline Logs : [PipelineLogs](PipelineLogs.png)

Picture showing Pipeline running with errors : [PipelineErrors](PipelineError.png)

# Scenarios Chosen for testing

The scenarios chosen to be automated are the below:

## UI Test Functions:

For the UI Test functions, i chose the feature that contains fields and buttons interaction.

1. Sample App: TestUiSampleApp function was designed because this kind of function is always available in an automation project for login and authentication. 

In this function we are Filling in the username and password, click on Login button to assert that the welcome message will be displayed

2. Ajax Data: TestUIAjaxData function was designed because its important for a test to be able to wait for an element to show up while waiting a specific response.

In this function we are clicking on the button and waiting for 15 seconds to make sure the test waits for label text to appear.

3. Text Input: TestUITextInput was designed to ensure the seamless synchronization of user inputs and application behavior. 

Entering text using DOM events can sometimes lead to unexpected issues, and this automation guarantees that the name input and the subsequent button value align correctly, 

addressing any potential discrepancies that manual testing might overlook.

In this function, we are performing a name input into a field and, upon clicking the button, the name entered in the input field will be set as the new value for the button.


## API Test Functions:

For the API Test function, i chose the api's in order to cover the following methods POST/GET/DELETE

1. Create List Of Users: TestApiPostListOfUsers function was designed to create a list of users with given input list.

2. Get Pet Data By Id: TestApiGetPetById function was designed to get the data of a pet based on the pet id.

3. Delete Pet by Id: TestApiDeletePetById function was designed to delete a pet based on the pet id. test
