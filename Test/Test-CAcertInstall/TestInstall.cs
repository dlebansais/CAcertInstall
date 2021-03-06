﻿namespace TestCAcertInstall
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Appium;
    using OpenQA.Selenium.Appium.Windows;

    [TestClass]
    public class TestInstall
    {
        /*
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Context = context;
        }

        private static TestContext Context;
        */

        [TestMethod]
        public void TestInstall1()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("Non");
            ButtonNoElement.Click();

            StopApp(Session);
        }

        [TestMethod]
        public void TestInstall2()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("No");
            ButtonNoElement.Click();

            StopApp(Session);
        }

        [TestMethod]
        public void TestInstall3()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement LicenseLinkElement = Session.FindElementByName("Distribution License");
            LicenseLinkElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Session.CloseApp();

            StopApp(Session);
        }

        [TestMethod]
        public void TestInstall4()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("Yes");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            WindowsElement ButtonCloseElement = Session.FindElementByName("Close") as WindowsElement;
            ButtonCloseElement.SendKeys("C");

            StopApp(Session);
        }

        [TestMethod]
        public void TestInstall5()
        {
            WindowsDriver<WindowsElement> Session = LaunchApp();

            WindowsElement ButtonNoElement = Session.FindElementByName("Yes");
            ButtonNoElement.Click();

            Thread.Sleep(TimeSpan.FromSeconds(2));

            WindowsElement ButtonCloseElement = Session.FindElementByName("Close") as WindowsElement;
            ButtonCloseElement.SendKeys("C");

            StopApp(Session);
        }

        private WindowsDriver<WindowsElement> LaunchApp()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));

            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @".\Test\Test-CAcertInstall\bin\x64\Debug\net48\CAcertInstall.exe");
            appiumOptions.AddAdditionalCapability("appArguments", "bad");

            return new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        private void StopApp(WindowsDriver<WindowsElement> session)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            using WindowsDriver<WindowsElement> DeletedSession = session;
        }
    }
}
