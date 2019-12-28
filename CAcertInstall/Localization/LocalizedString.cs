namespace Localization
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [DefaultBindingProperty("Current")]
    public class LocalizedString : DependencyObject, INotifyPropertyChanged
    {
        #region Init
        public LocalizedString()
        {
            StringList.Add(this);
        }

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

        private static List<LocalizedString> StringList = new List<LocalizedString>();
        #endregion

        #region Properties
        public enum Language
        {
            ENU,
            FRA,
        }

        public static readonly DependencyProperty ENUProperty = DependencyProperty.Register("ENU", typeof(string), typeof(LocalizedString), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty FRAProperty = DependencyProperty.Register("FRA", typeof(string), typeof(LocalizedString), new PropertyMetadata(string.Empty));

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

        public string ENU
        {
            get { return (string)GetValue(ENUProperty); }
            set { SetValue(ENUProperty, value); }
        }

        public string FRA
        {
            get { return (string)GetValue(FRAProperty); }
            set { SetValue(FRAProperty, value); }
        }

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
        private static void RefreshAll()
        {
            foreach (LocalizedString s in StringList)
                s.Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Current));
        }
        #endregion

        #region Events
        public delegate void LanguageChangedEventHandler(Language newLanguage);
        public static event LanguageChangedEventHandler? LanguageChanged;

        public static void RegisterLanguageChangedHandler(LanguageChangedEventHandler handler)
        {
            LanguageChanged += handler;
        }

        private static void NotifyLanguageChanged(Language newLanguage)
        {
            LanguageChanged?.Invoke(newLanguage);
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
