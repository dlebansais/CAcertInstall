namespace CAcertInstall
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Represents the application status window.
    /// </summary>
    public partial class StatusWindow : Window
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
        public bool Success { get; set; }
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
    }
}
