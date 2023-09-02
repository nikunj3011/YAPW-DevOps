using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using YAPW.Extentions;
using YAPW.Models.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("javaApi", c =>
{
    c.BaseAddress = new Uri("http://localhost:8083/");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

var appSettings = builder.Configuration.GetSection("GlobalConfig").Get<AppSetting>();
var currentEnvironmentSettings = appSettings.Environments.SingleOrDefault(e => e.Name.ToLower() == builder.Environment.EnvironmentName.ToLower());
var connectionString = currentEnvironmentSettings.SettingsData.ConnectionString;
//var azureKeyVaultUrl = currentEnvironmentSettings.SettingsData.AzureKeyVaultUrl;
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("GlobalConfig"));
//builder.Configuration.AddAzureKeyVault(
//       new Uri($"https://{azureKeyVaultUrl}.vault.azure.net/"),
//       new DefaultAzureCredential());
//var client = new SecretClient(new Uri($"https://{azureKeyVaultUrl}.vault.azure.net/"), new DefaultAzureCredential());
//var secret = await client.GetSecretAsync("ConnectionString");

builder.Services.AddServiceWorkers();
//builder.Services.AddDatabases(secret.Value.Value, false);
builder.Services.AddDatabases(connectionString, false);
builder.Services.AddMemoryCache();
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy
        .Expire(TimeSpan.FromDays(1)));
    options.AddPolicy("CategoyMinimalPolicy", policy => policy
        .Expire(TimeSpan.FromDays(1))
        .Tag("CategoyMinimalPolicy_Tag"));
});

//builder.Services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
//    .AddOAuthValidation();
//SevicesInjector.AddGenericNamedEntityServices(builder);
//builder.Services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Type, DataContext>>();
//builder.Services.AddTransient<INameService, NameService>();
//builder.Services.Add(SevicesInjector);
builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<ITypeService, TypeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOutputCache();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
