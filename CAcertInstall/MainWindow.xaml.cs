﻿namespace CAcertInstall
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Init
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            if (App.IsInstallation)
            {
                if (App.IsAlreadyInstalled)
                    Close();
            }
            else
            {
                if (App.IsAlreadyInstalled)
                {
                    Certificate CertificateRoot = App.CertificateRoot;
                    Certificate CertificateClass3 = App.CertificateClass3;
                    bool Success = CertificateStore.UninstallCertificates(new Certificate[] { CertificateRoot, CertificateClass3 });

                    if (Success)
                        App.SetOperationSuccessful();
                }

                Close();
            }
        }
        #endregion

        #region Events
        private void OnYes(object sender, ExecutedRoutedEventArgs e)
        {
            StatusWindow Dlg = new StatusWindow();

            Certificate CertificateRoot = App.CertificateRoot;
            Certificate CertificateClass3 = App.CertificateClass3;
            bool Success = CertificateStore.InstallCertificates(new Certificate[] { CertificateRoot, CertificateClass3 });

            Dlg.Success = Success;
            Dlg.ShowDialog();

            if (Success)
                App.SetOperationSuccessful();

            Close();
        }

        private void OnNo(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void OnLicenseClick(object sender, RoutedEventArgs e)
        {
            Process.Start("www.cacert.org/policy/RootDistributionLicense.php");
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        public void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
