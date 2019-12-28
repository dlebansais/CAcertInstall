namespace Localization
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// Represents a string with several values for different cultures.
    /// </summary>
    [DefaultBindingProperty("Current")]
    public class LocalizedString : DependencyObject, INotifyPropertyChanged
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedString"/> class.
        /// </summary>
        public LocalizedString()
        {
            StringList.Add(this);
        }

        /// <summary>
        /// Gets the instance of <see cref="Language"/> associated to the current culture.
        /// </summary>
        private static Language GetCurrentCultureLanguage()
        {
            switch (CultureInfo.CurrentCulture.LCID)
            {
                default:
                case 0x0409:
                    return Language.ENU;

                case 0x040C:
                    return Language.FRA;
            }
        }

        private static readonly List<LocalizedString> StringList = new List<LocalizedString>();
        #endregion

        #region Properties
        /// <summary>
        /// Identifies the <see cref="ENU"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ENUProperty = DependencyProperty.Register("ENU", typeof(string), typeof(LocalizedString), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <see cref="FRA"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FRAProperty = DependencyProperty.Register("FRA", typeof(string), typeof(LocalizedString), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the currently used culture.
        /// </summary>
        public static Language CurrentLanguage
        {
            get { return CurrentLanguageInternal; }
            set
            {
                CurrentLanguageInternal = value;
                RefreshAll();
                NotifyLanguageChanged(CurrentLanguageInternal);
            }
        }
        private static Language CurrentLanguageInternal = GetCurrentCultureLanguage();

        /// <summary>
        /// Gets or sets the string for the <see cref="Language.ENU"/> culture.
        /// </summary>
        public string ENU
        {
            get { return (string)GetValue(ENUProperty); }
            set { SetValue(ENUProperty, value); }
        }

        /// <summary>
        /// Gets or sets the string for the <see cref="Language.FRA"/> culture.
        /// </summary>
        public string FRA
        {
            get { return (string)GetValue(FRAProperty); }
            set { SetValue(FRAProperty, value); }
        }

        /// <summary>
        /// Gets the string for the current culture.
        /// </summary>
        public string Current
        {
            get
            {
                switch (CurrentLanguage)
                {
                    default:
                    case Language.ENU:
                        return (string)GetValue(ENUProperty);

                    case Language.FRA:
                        return (string)GetValue(FRAProperty);
                }
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Updates strings for all cultures.
        /// </summary>
        private static void RefreshAll()
        {
            foreach (LocalizedString s in StringList)
                s.Refresh();
        }

        /// <summary>
        /// Updates the string associated to the current culture.
        /// </summary>
        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Current));
        }
        #endregion

        #region Events
        /// <summary>
        /// Represents the method that will handle a change of the current language.
        /// </summary>
        /// <param name="newLanguage">The new language.</param>
        public delegate void LanguageChangedEventHandler(Language newLanguage);

        /// <summary>
        /// Occurs when the current language changes.
        /// </summary>
        public static event LanguageChangedEventHandler? LanguageChanged;

        /// <summary>
        /// Registers a handler for the <see cref="LanguageChanged"/> event.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        public static void RegisterLanguageChangedHandler(LanguageChangedEventHandler handler)
        {
            LanguageChanged += handler;
        }

        /// <summary>
        /// Invokes handlers of the <see cref="LanguageChanged"/> event.
        /// </summary>
        /// <param name="newLanguage">The new language.</param>
        private static void NotifyLanguageChanged(Language newLanguage)
        {
            LanguageChanged?.Invoke(newLanguage);
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
