using Localization;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace CAcertInstall
{
    public partial class App : Application
    {
        public App()
        {
            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                string LanguageOption = "-Language=";
                if (arg.Substring(0, LanguageOption.Length) == LanguageOption)
                {
                    string LanguageString = arg.Substring(LanguageOption.Length).ToUpper();

                    if (LanguageString == "0409")
                        LocalizedString.CurrentLanguage = LocalizedString.Language.ENU;

                    else if (LanguageString == "040C")
                        LocalizedString.CurrentLanguage = LocalizedString.Language.FRA;
                }
            }

            certificate = LoadCertificateFromResources();

            if (CertificateStore.IsCertificateInstalled(certificate))
                Process.GetCurrentProcess().Kill();
        }

        public X509Certificate2 LoadCertificateFromResources()
        {
            X509Certificate2 certificate = new X509Certificate2();

            using (Stream stream = ResourceAssembly.GetManifestResourceStream("CAcertInstall.Resources.root.crt"))
            {
                byte[] Data = new byte[stream.Length];
                stream.Read(Data, 0, Data.Length);

                certificate.Import(Data);
            }

            return certificate;
        }

        public static X509Certificate2 certificate;
    }
}
