# Selenium C# demo

*This is a home assignment: To build a basic structure for a web-application automation testing framework.*

--> Reference: https://www.toolsqa.com/selenium-c-sharp/

## Applied:
- PageObject design pattern.
- BrowserFactory.
- Data Driven using JSON.
- Page Generator.
- Extent Report.

--> Use this framework to do this **Scripting challenge #1**.

## Scripting challenge #1:

Create Data Driven & Keywork Driven in Selenium WebDriver using NUnit to verify product title with scenario as below:
1.	Open Chrome Browser
2.	Maximize the browser window
3.	Navigate to www.amazon.com webpage.
4.	Select "Books" from search category dropdown
5.	Enter search key: "Selenium"
6.	Click "Go" button
7.	Click the first search result item title
8.	Verify that product title is correct ("Learn Selenium: Build data-driven test frameworks for mobile and web applications with Selenium Web Driver 3")

## Requirements:

1. Create common function Search()
2. Wrap up functions into PageObject class:
	- GoToUrl()
	- InputText()
	- ClickAndWait()
3. Create keywork driven for:
	- WaitForElementPresent()
	- WaitForElementDisplayed()
	- WaitForElementClickable()
	- SelectBy() to use for step 4
	- Apply keyworks for this test script.
4. Apply data driven(JSON) for this test to search with:
	- Data 1: C# 7.0 in a Nutshell => title = "C# 7.0 in a Nutshell: The Definitive Reference"
	- Data 2: Test Automation using Selenium Webdriver 3.0 with C# => title = "Test Automation using Selenium Webdriver 3.0 with C#")
	- Data 3: Learn Selenium: => title = "Learn Selenium: Build data-driven test frameworks for mobile and web applications with Selenium Web Driver 3"
	
--> Test case should run against all test data.

## Demo videos:
- Scripting challenge #1: https://www.youtube.com/watch?v=x_M4louXJXA
