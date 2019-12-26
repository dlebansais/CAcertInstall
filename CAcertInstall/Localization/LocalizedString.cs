#nullable enable

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Globalization;

namespace Localization
{
    [DefaultBindingProperty("Current")]
    public class LocalizedString: DependencyObject, INotifyPropertyChanged
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
        public enum Language { ENU, FRA }

        public static readonly DependencyProperty ENUProperty = DependencyProperty.Register("ENU", typeof(string), typeof(LocalizedString), new PropertyMetadata(""));
        public static readonly DependencyProperty FRAProperty = DependencyProperty.Register("FRA", typeof(string), typeof(LocalizedString), new PropertyMetadata(""));

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

        public static Language CurrentLanguage
        {
            get { return _CurrentLanguage; }
            set
            {
                _CurrentLanguage = value;
                RefreshAll();
                NotifyLanguageChanged(_CurrentLanguage);
            }
        }
        private static Language _CurrentLanguage = GetCurrentCultureLanguage();

        public string Current
        {
            get 
            {
                switch (_CurrentLanguage)
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
        static private void RefreshAll()
        {
            foreach (LocalizedString s in StringList)
                s.Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged("Current");
        }
        #endregion

        #region Events
        public delegate void LanguageChangedEventHandler(Language NewLanguage);
        public static event LanguageChangedEventHandler? LanguageChanged;

        public static void RegisterLanguageChangedHandler(LanguageChangedEventHandler Handler)
        {
            LanguageChanged += Handler;
        }

        private static void NotifyLanguageChanged(Language NewLanguage)
        {
            LanguageChanged?.Invoke(NewLanguage);
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
