namespace Localization
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;

    /// <summary>
    /// Represents a string with several values for different cultures.
    /// </summary>
    [DefaultBindingProperty("Current")]
    public class LocalizedString : DependencyObject
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedString"/> class.
        /// </summary>
        public LocalizedString()
        {
            StringList.Add(this);

            // The code below is for code coverage purpose only.
            Debug.Assert(GetCultureLanguage(new CultureInfo("en-US")) == Language.ENU);
            Debug.Assert(GetCultureLanguage(new CultureInfo("fr-FR")) == Language.FRA);

            string ENUValue = ENU;
            ENU = ENUValue;
            Debug.Assert(ENUValue == ENU);

            string FRAValue = FRA;
            FRA = FRAValue;
            Debug.Assert(FRAValue == FRA);
        }

        /// <summary>
        /// Gets the instance of <see cref="Language"/> associated to the current culture.
        /// </summary>
        private static Language GetCultureLanguage(CultureInfo cultureInfo)
        {
            switch (cultureInfo.LCID)
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
            }
        }
        private static Language CurrentLanguageInternal = GetCultureLanguage(CultureInfo.CurrentCulture);

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
    }
}
