namespace CAcertInstall
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows;
    using Localization;

    /// <summary>
    /// Represents the application main class.
    /// </summary>
    public partial class App : Application
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            string[] args = Environment.GetCommandLineArgs();

            for (int Index = 1; Index < args.Length; Index++)
            {
                string arg = args[Index];
                ParseArgument(arg);
            }

            IsAlreadyPerformed = CheckIfAlreadyPerformed();

            Exit += OnExit;
        }

        private void ParseArgument(string arg)
        {
            const string LanguageOption = "--language=";
            const string UninstallOption = "--uninstall";

            if (arg.Length >= LanguageOption.Length && arg.Substring(0, LanguageOption.Length) == LanguageOption)
            {
                string LanguageString = arg.Substring(LanguageOption.Length).ToUpperInvariant();

                if (LanguageString == "0409")
                    LocalizedString.CurrentLanguage = Language.ENU;
                else if (LanguageString == "040C")
                    LocalizedString.CurrentLanguage = Language.FRA;
                else
                    IsCommandLineValid = false;
            }
            else if (arg == UninstallOption)
                IsInstallation = false;
            else
                IsCommandLineValid = false;
        }

        private bool CheckIfAlreadyPerformed()
        {
            bool Result = true;
            Result &= CertificateStore.IsCertificateInstalled(CertificateRoot);
            Result &= CertificateStore.IsCertificateInstalled(CertificateClass3);

            return Result;
        }
        #endregion

        #region Exit
        /// <summary>
        /// Executes the last piece of code before the application exits.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void OnExit(object sender, ExitEventArgs e)
        {
            bool NoAction = false;
            if (IsInstallation && IsAlreadyPerformed)
                NoAction = true;
            if (!IsInstallation && !IsAlreadyPerformed)
                NoAction = true;

            if (!IsCommandLineValid)
                e.ApplicationExitCode = -2;
            else if (NoAction)
                e.ApplicationExitCode = 1;
            else if (IsOperationSuccessful)
                e.ApplicationExitCode = 0;
            else
                e.ApplicationExitCode = -1;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the root certificate.
        /// </summary>
        public static Certificate CertificateRoot { get; } = CertificateFromResourceName("root.crt", StoreName.Root);

        /// <summary>
        /// Gets the class 3 certificate.
        /// </summary>
        public static Certificate CertificateClass3 { get; } = CertificateFromResourceName("class3.crt", StoreName.CertificateAuthority);

        /// <summary>
        /// Gets a value indicating whether the application command line was valid (true), or invalid (false).
        /// </summary>
        public static bool IsCommandLineValid { get; private set; } = true;

        /// <summary>
        /// Gets a value indicating whether the application is run to install (true), or to uninstall (false).
        /// </summary>
        public static bool IsInstallation { get; private set; } = true;

        /// <summary>
        /// Gets a value indicating whether the requested operation is necessary (false) or not (true).
        /// </summary>
        public static bool IsAlreadyPerformed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the requested operation has been executed successfully.
        /// </summary>
        public static bool IsOperationSuccessful { get; private set; }

        /// <summary>
        /// Gets a certificate from the application resources.
        /// </summary>
        /// <param name="resourceName">The name of the resource containing the certificate.</param>
        /// <param name="storeName">Name of the store for which this certificate is intended.</param>
        private static Certificate CertificateFromResourceName(string resourceName, StoreName storeName)
        {
            X509Certificate2 x509certificate = LoadCertificateFromResources(resourceName);
            Certificate Result = new Certificate(x509certificate, storeName);

            return Result;
        }

        /// <summary>
        /// Gets a certificate from the application resources.
        /// </summary>
        /// <param name="resourceName">The name of the resource containing the certificate.</param>
        private static X509Certificate2 LoadCertificateFromResources(string resourceName)
        {
            X509Certificate2 Certificate = new X509Certificate2();

            using (Stream stream = ResourceAssembly.GetManifestResourceStream($"CAcertInstall.Resources.{resourceName}"))
            {
                byte[] Data = new byte[stream.Length];
                stream.Read(Data, 0, Data.Length);

                Certificate.Import(Data);
            }

            return Certificate;
        }
        #endregion

        #region Client Interface
        /// <summary>
        /// Sets the <see cref="IsOperationSuccessful"/> flag to true.
        /// </summary>
        public static void SetOperationSuccessful()
        {
            IsOperationSuccessful = true;
        }
        #endregion
    }
}
