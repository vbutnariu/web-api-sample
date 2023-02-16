using System;
using System.Configuration;
using System.Globalization;
using System.Resources;

namespace Demo.Resources.Localization
{

	public class LocalizationService : ILocalizationService
	{
		private readonly static CultureInfo culture;
		private const string dictionary = "ServiceResources";
		private const string modelDictionary = "ModelResources";
		private const string resourcesAssembly = "Pm.Resources";
		private static ResourceManager modelResourcesManager;
		private static ResourceManager stringResourceManager;

		private static ResourceManager serviceResourceManager;

		static LocalizationService()
		{
			modelResourcesManager = new ResourceManager("NC.Resources.Localisation.ModelResources", typeof(ResourceAsemblyLocator).Assembly);
			serviceResourceManager = new ResourceManager("NC.Resources.Localisation.ServiceResources", typeof(ResourceAsemblyLocator).Assembly);
			stringResourceManager = new ResourceManager("NC.Resources.Localisation.Strings", typeof(LocalizationService).Assembly);
			culture = new CultureInfo(ConfigurationManager.AppSettings["CultureInfo"] ?? "de");
		}
		public string LocalizeString(string resourceKey)
		{
			return serviceResourceManager.GetString(resourceKey, culture) ?? resourceKey;
		}

		public static object LocalizeWizardString(string v)
		{
			throw new NotImplementedException();
		}

		public static string Localize(string resourceKey)
		{
			return serviceResourceManager.GetString(resourceKey, culture) ?? resourceKey;
		}

		public string LocalizeModelString(string resourceKey)
		{
			return modelResourcesManager.GetString(resourceKey, culture) ?? resourceKey;
		}

		public static string StaticLocalizeModelString(string resourceKey)
		{
			return modelResourcesManager.GetString(resourceKey, culture) ?? resourceKey;
		}

		public static string LocalizeUIString(string resourceKey)
		{
			return stringResourceManager.GetString(resourceKey, culture) ?? resourceKey;
		}
	}
}
