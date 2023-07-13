using System.Collections.Generic;

namespace YAPW.Service.Broker
{
    public class AppSettings
    {
        public List<Environment> Environments { get; set; }
    }

    public class Environment
    {
        public string Name { get; set; }
        public SettingsData SettingsData { get; set; }
    }
    public class SettingsData
    {
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiURL { get; set; }
    }
}
