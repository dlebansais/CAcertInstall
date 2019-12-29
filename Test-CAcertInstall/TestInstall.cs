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
        public void TestInstall_Default()
        {
            // Launch Notepad
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Projects\CAcertInstall\CAcertInstall\bin\x64\Debug\CAcertInstall.exe");
            appiumOptions.AddAdditionalCapability("appArguments", "-language:040C");

            try
            {
                WindowsDriver<WindowsElement> NotepadSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);

                NotepadSession.Close();
            }
            catch
            {
            }

            /*
            // Use the session to control the app
            WindowsElement EditElement = NotepadSession.FindElementByClassName("Edit");
            EditElement.SendKeys("This is some text");

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("NotepadSession.CloseApp");
            NotepadSession.CloseApp();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("NotepadSession.FindElementByName");
            WindowsElement CloseDialogElement = NotepadSession.FindElementByName("Notepad");

            Console.WriteLine("CloseDialogElement.FindElementByName");
            AppiumWebElement NoSaveButtonElement = CloseDialogElement.FindElementByName("Don't Save");

            Console.WriteLine("NoSaveButtonElement.Click");
            NoSaveButtonElement.Click();

            Console.WriteLine("Done");
            */
        }
    }
}
