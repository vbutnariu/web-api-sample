using Demo.Resources.Localization;
using System.ComponentModel;

namespace Demo.Common.Attributes
{

    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public override string DisplayName
        {
            get
            {
                string displayName = LocalizationService.LocalizeModelString(ResourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? ResourceKey
                    : displayName;
            }
        }

        public string ResourceKey { get; }
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

        public LocalizedDisplayNameAttribute(string resourceKey)
        {
            ResourceKey = resourceKey;
        }
    }
}