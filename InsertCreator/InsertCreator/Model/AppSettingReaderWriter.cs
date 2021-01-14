using System.Configuration;

namespace Liedeinblendung.Model
{
    /// <summary>
    /// SettingsKeyEnum
    /// </summary>
    public enum KeyName
    {
        UseGreenscreen,
        ShowComponistAndAutor,
        UseLogo
    }

    /// <summary>
    /// Read and write App.config
    /// </summary>
    internal class AppSettingReaderWriter
    {
        #region Public Methods

        /// <summary>
        /// Read setting in entered section
        /// </summary>
        /// <param name="configSection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadSetting(KeyName key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key.ToString()] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                return "";
            }
        }

        /// <summary>
        /// Write Setting in AppSettings
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteAppSetting(KeyName key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove(key.ToString());
                config.AppSettings.Settings.Add(key.ToString(), value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
            }
        }

        #endregion Public Methods
    }
}