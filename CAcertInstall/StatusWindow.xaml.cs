#nullable enable

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CAcertInstall
{
    public partial class StatusWindow : Window, INotifyPropertyChanged
    {
        public StatusWindow()
        {
            InitializeComponent();
            DataContext = this;

            _Success = false;
        }

        private void OnClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }
        private bool _Success;

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
