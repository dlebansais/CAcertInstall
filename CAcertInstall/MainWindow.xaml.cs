namespace CAcertInstall
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Represents the application main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            if (App.IsCommandLineValid)
            {
                if (App.IsInstallation)
                {
                    if (App.IsAlreadyPerformed)
                        CloseNow();
                }
                else
                {
                    if (App.IsAlreadyPerformed)
                    {
                        Certificate CertificateRoot = App.CertificateRoot;
                        Certificate CertificateClass3 = App.CertificateClass3;
                        bool Success = CertificateStore.UninstallCertificates(new Certificate[] { CertificateRoot, CertificateClass3 });

                        if (Success)
                            App.SetOperationSuccessful();
                    }

                    CloseNow();
                }
            }
            else
                CloseNow();
        }

        private void CloseNow()
        {
            Close();
        }
        #endregion

        #region Events
        /// <summary>
        /// Called when the user clicks the 'Yes' button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void OnYes(object sender, ExecutedRoutedEventArgs e)
        {
            StatusWindow Dlg = new StatusWindow();

            Certificate CertificateRoot = App.CertificateRoot;
            Certificate CertificateClass3 = App.CertificateClass3;
            bool Success = CertificateStore.InstallCertificates(new Certificate[] { CertificateRoot, CertificateClass3 });

            Dlg.Success = Success;
            Dlg.Owner = this;
            Dlg.ShowDialog();

            if (Success)
                App.SetOperationSuccessful();

            Close();
        }

        /// <summary>
        /// Called when the user clicks the 'No' button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void OnNo(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Called when the user clicks the 'Display license' link.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void OnLicenseClick(object sender, RoutedEventArgs e)
        {
            Process.Start("www.cacert.org/policy/RootDistributionLicense.php");
        }
        #endregion
    }
}
