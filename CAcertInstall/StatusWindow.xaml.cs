namespace CAcertInstall
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Represents the application status window.
    /// </summary>
    public partial class StatusWindow : Window, INotifyPropertyChanged
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusWindow"/> class.
        /// </summary>
        public StatusWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful (true) or not (false).
        /// </summary>
        public bool Success { get; set; } = false;
        #endregion

        #region Events
        /// <summary>
        /// Called when the window is closed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void OnClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        /// Implements the <see cref="PropertyChanged"/> event.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> event. Must be called from within a property setter.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
