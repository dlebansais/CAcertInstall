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
        private WindowsDriver<WindowsElement> LaunchApp()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));

            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @".\CAcertInstall\bin\x64\Debug\CAcertInstall.exe");
            appiumOptions.AddAdditionalCapability("appArguments", "bad");

            return new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        [TestMethod]
        public void TestInstall1()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("Non");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Done");
        }

        [TestMethod]
        public void TestInstall2()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("No");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Done");
        }

        [TestMethod]
        public void TestInstall3()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("Oui");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            /*var Locator = Session.SwitchTo();
            var ActiveElement = Locator.ActiveElement();
            ActiveElement.Click();*/

            WindowsElement ButtonCloseElement = Session.FindElementByName("Fermer");
            ButtonCloseElement.Click();
 
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Session.CloseApp();

            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine("Done");
        }
    }
}
