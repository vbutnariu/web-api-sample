namespace Demo.Resources.Localization
{
	public interface ILocalizationService
	{
		string LocalizeString(string resourceKey);
		string LocalizeModelString(string resourceKey);
	}
}
