#nullable enable

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CAcertInstall
{
    public partial class StatusWindow : Window, INotifyPropertyChanged
    {
        #region Init
        public StatusWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Properties
        public bool Success { get; set; } = false;
        #endregion

        #region Events
        private void OnClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
