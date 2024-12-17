using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Threading;

namespace ClockAppAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up desired capabilities for Appium and Android
            var appiumOptions = new OpenQA.Selenium.Appium.AppiumOptions();
            appiumOptions.AddAdditionalCapability("platformName", "Android");
            appiumOptions.AddAdditionalCapability("deviceName", "emulator-5554");  // Replace with your device/emulator name
            appiumOptions.AddAdditionalCapability("appPackage", "com.android.deskclock");
            appiumOptions.AddAdditionalCapability("appActivity", "com.android.deskclock.DeskClock");
            appiumOptions.AddAdditionalCapability("noReset", true);  // Avoid app reset between tests

            // Initialize the Appium driver
            var driver = new AndroidDriver<IWebElement>(new Uri("http://localhost:4723/wd/hub"), appiumOptions);
            
            try
            {
                // Step 1: Open the Clock app (Appium will open it automatically using appPackage and appActivity)

                // Step 2: Navigate to the Alarm tab
                IWebElement alarmTab = driver.FindElementByAccessibilityId("Alarm");
                alarmTab.Click();

                // Step 3: Create a New Alarm
                IWebElement addAlarmButton = driver.FindElementById("com.android.deskclock:id/fab");
                addAlarmButton.Click();

                // Set the alarm time (assume time picker is visible)
                IWebElement timePicker = driver.FindElementById("com.android.deskclock:id/timepicker");
                // Here you would need to set the time using the time picker (depending on app's UI, this can vary)
                // Example: Set the hour and minute, handle the time picker interaction

                // Save the alarm
                IWebElement saveButton = driver.FindElementById("com.android.deskclock:id/save");
                saveButton.Click();

                // Step 4: Enable the Alarm (if not enabled by default)
                IWebElement enableToggle = driver.FindElementById("com.android.deskclock:id/enable_alarm");
                if (enableToggle.Selected == false)
                {
                    enableToggle.Click(); // Ensure it's turned on
                }

                // Step 5: Verify the Alarm is saved with expected details
                IWebElement alarmList = driver.FindElementById("com.android.deskclock:id/alarm_list");
                bool alarmExists = alarmList.Displayed;  // Check if alarms are listed
                if (alarmExists)
                {
                    Console.WriteLine("Alarm is saved and visible in the alarm list.");
                }
                else
                {
                    Console.WriteLine("Alarm is not saved correctly.");
                }

                // Step 6: Delete the Alarm
                IWebElement deleteButton = driver.FindElementById("com.android.deskclock:id/delete");
                deleteButton.Click();

                // Step 7: Validate the Alarm has been removed
                try
                {
                    alarmList = driver.FindElementById("com.android.deskclock:id/alarm_list");
                    bool alarmRemoved = !alarmList.Displayed;  // Check if no alarms are visible
                    if (alarmRemoved)
                    {
                        Console.WriteLine("Alarm has been successfully removed.");
                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Alarm has been successfully removed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during automation: " + ex.Message);
            }
            finally
            {
                // Close the Appium driver and stop the session
                driver.Quit();
            }
        }
    }
}
