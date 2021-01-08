using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    /// <summary>
    /// Read and write App.config
    /// </summary>
    class AppSettingReaderWriter
    {
        /// <summary>
        /// Read setting in entered section
        /// </summary>
        /// <param name="configSection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadSetting(ConfigSectionName configSection, KeyName key)
        {
            try
            {
                ConfigurationManager.RefreshSection(configSection.ToString());
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var section = ConfigurationManager.GetSection(configSection.ToString()) as NameValueCollection;
                return section[key.ToString()] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {              
                return string.Empty;
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
                config.Save(ConfigurationSaveMode.Full);
            }

            catch (ConfigurationErrorsException)
            {
               
            }
        }


        // TODO Wird wahrscheinlich noch gebraucht
        public List<string> ReadSection(ConfigSectionName configSection)
        {
            var list = new List<string>();
            try
            {
                ConfigurationManager.RefreshSection(configSection.ToString());
                var section = ConfigurationManager.GetSection(configSection.ToString()) as NameValueCollection;

                foreach (var key in section.AllKeys)
                {
                    list.Add(section[key.ToString()]);
                }               
                return list;
            }
            catch (ConfigurationErrorsException)
            {                
                return list;
            }
        }
        
    }


    /// <summary>
    /// SectionNameEnum
    /// </summary>
    public enum ConfigSectionName
    {
        hymnalInsertOptions
    }

    /// <summary>
    /// SettingsKeyEnum
    /// </summary>
    public enum KeyName
    {
        UseGreenscreen,
        ShowComponistAndAutor 
    }

}
