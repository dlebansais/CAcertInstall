#nullable enable

using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;

namespace CAcertInstall
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnYes(object sender, ExecutedRoutedEventArgs e)
        {
            StatusWindow Dlg = new StatusWindow();

            Certificate CertificateRoot = App.CertificateRoot;
            Certificate CertificateClass3 = App.CertificateClass3;

            Dlg.Success = CertificateStore.InstallCertificates(new Certificate[] { CertificateRoot, CertificateClass3 });
            Dlg.ShowDialog();

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

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
