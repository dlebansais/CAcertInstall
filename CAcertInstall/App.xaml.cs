using Localization;
using System;
using System.Diagnostics;
using System.Globalization;
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
                    string LanguageString = arg.Substring(LanguageOption.Length).ToUpper(CultureInfo.CurrentCulture);

                    if (LanguageString == "0409")
                        LocalizedString.CurrentLanguage = LocalizedString.Language.ENU;

                    else if (LanguageString == "040C")
                        LocalizedString.CurrentLanguage = LocalizedString.Language.FRA;
                }
            }

            X509Certificate2 x509certificateRoot = LoadCertificateFromResources("root.crt");
            CertificateRoot = new Certificate(x509certificateRoot, StoreName.Root);
            X509Certificate2 x509certificateClass3 = LoadCertificateFromResources("class3.crt");
            CertificateClass3 = new Certificate(x509certificateClass3, StoreName.CertificateAuthority);

            if (CertificateStore.IsCertificateInstalled(CertificateRoot) && CertificateStore.IsCertificateInstalled(CertificateClass3))
                Process.GetCurrentProcess().Kill();
        }

        public static X509Certificate2 LoadCertificateFromResources(string Name)
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

        public static Certificate CertificateRoot { get; private set; }
        public static Certificate CertificateClass3 { get; private set; }
    }
}
