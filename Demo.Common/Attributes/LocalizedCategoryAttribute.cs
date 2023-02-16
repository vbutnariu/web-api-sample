using Demo.Resources.Localization;
using System.ComponentModel;

namespace Demo.Common.Attributes
{
    public class LocalizedCategoryAttribute : CategoryAttribute
    {


        protected override string GetLocalizedString(string value)
        {

            string displayName = new LocalizationService().LocalizeModelString(value);

            return string.IsNullOrEmpty(displayName)
                ? value
                : displayName;
        }

        private LocalizationService localizationService;
        private ILocalizationService LocalizationService
        {
            get
            {
                if (localizationService == null)
                    localizationService = new LocalizationService();
                return localizationService;
            }
        }

        public LocalizedCategoryAttribute(string key) : base(key) { }
    }
}