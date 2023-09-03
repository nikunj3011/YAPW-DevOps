namespace YAPW.Models.Models.Settings
{
    public class SettingsData
    {
        public string ConnectionString { get; set; }
        public string AzureKeyVaultUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ApiName { get; set; }
        public string NormalizedApiName { get; set; }
        public string IdentityProviderUrl { get; set; }
        public string SuperAdmin { get; set; }
        public string Password { get; set; }
    }


}
