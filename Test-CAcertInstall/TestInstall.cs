namespace TestCAcertInstall
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Appium;
    using OpenQA.Selenium.Appium.Windows;

    [TestClass]
    public class TestInstall
    {
        [TestMethod]
        public void TestInstall1()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));

            // Launch
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Projects\CAcertInstall\CAcertInstall\bin\x64\Debug\CAcertInstall.exe");
            appiumOptions.AddAdditionalCapability("appArguments", "bad");

            WindowsDriver<WindowsElement> Session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);

            // Use the session to control the app

            WindowsElement ButtonNoElement = Session.FindElementByName("Non");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("Done");
        }

        [TestMethod]
        public void TestInstall2()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));

            // Launch
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Projects\CAcertInstall\CAcertInstall\bin\x64\Debug\CAcertInstall.exe");
            appiumOptions.AddAdditionalCapability("appArguments", "bad");

            WindowsDriver<WindowsElement> Session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);

            // Use the session to control the app

            WindowsElement ButtonNoElement = Session.FindElementByName("Non");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("Done");
        }

        [TestMethod]
        public void TestInstall3()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));

            // Launch
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Projects\CAcertInstall\CAcertInstall\bin\x64\Debug\CAcertInstall.exe");
            appiumOptions.AddAdditionalCapability("appArguments", "bad");

            WindowsDriver<WindowsElement> Session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);

            // Use the session to control the app

            WindowsElement ButtonNoElement = Session.FindElementByName("No");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            /*
            WindowsElement EditElement = Session.FindElementByClassName("Button");
            EditElement.SendKeys("This is some text");

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("Session.CloseApp");
            Session.CloseApp();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("CloseDialogElement.FindElementByName");
            AppiumWebElement NoSaveButtonElement = CloseDialogElement.FindElementByName("Don't Save");
            */

            Console.WriteLine("Done");
        }
    }
}
