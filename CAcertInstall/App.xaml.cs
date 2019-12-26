#nullable enable

using Localization;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace CAcertInstall
{
    public partial class App : Application
    {
        #region Init
        public App()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                string LanguageOption = "-Language=";
                string UninstallOption = "-Uninstall";

                if (arg.Substring(0, LanguageOption.Length) == LanguageOption)
                {
                    string LanguageString = arg.Substring(LanguageOption.Length).ToUpper(CultureInfo.CurrentCulture);

                    if (LanguageString == "0409")
                        LocalizedString.CurrentLanguage = LocalizedString.Language.ENU;

                    else if (LanguageString == "040C")
                        LocalizedString.CurrentLanguage = LocalizedString.Language.FRA;
                }
                else if (arg.Substring(0, UninstallOption.Length) == UninstallOption)
                    IsInstallation = false;
            }

            IsAlreadyInstalled = CertificateStore.IsCertificateInstalled(CertificateRoot) && CertificateStore.IsCertificateInstalled(CertificateClass3);
            Exit += OnExit;
        }
        #endregion

        #region Exit
        private void OnExit(object sender, ExitEventArgs e)
        {
            if ((IsInstallation && IsAlreadyInstalled) || (!IsInstallation && !IsAlreadyInstalled))
                e.ApplicationExitCode = 1;
            else if (IsOperationSuccessful)
                e.ApplicationExitCode = 0;
            else
                e.ApplicationExitCode = -1;
        }
        #endregion

        #region Properties
        public static Certificate CertificateRoot { get; } = CertificateFromResourceName("root.crt", StoreName.Root);
        public static Certificate CertificateClass3 { get; } = CertificateFromResourceName("class3.crt", StoreName.CertificateAuthority);
        public static bool IsInstallation { get; private set; } = true;
        public static bool IsAlreadyInstalled { get; private set; }
        public static bool IsOperationSuccessful { get; private set; }

        private static Certificate CertificateFromResourceName(string resourceName, StoreName storeName)
        {
            X509Certificate2 x509certificate = LoadCertificateFromResources(resourceName);
            Certificate Result = new Certificate(x509certificate, storeName);

            return Result;
        }

        private static X509Certificate2 LoadCertificateFromResources(string Name)
        {
            X509Certificate2 Certificate = new X509Certificate2();

            using (Stream stream = ResourceAssembly.GetManifestResourceStream("CAcertInstall.Resources." + Name))
            {
                byte[] Data = new byte[stream.Length];
                stream.Read(Data, 0, Data.Length);

                Certificate.Import(Data);
            }

            return Certificate;
        }
        #endregion

        #region Client Interface
        public static void SetOperationSuccessful()
        {
            IsOperationSuccessful = true;
        }
        #endregion
    }
}
