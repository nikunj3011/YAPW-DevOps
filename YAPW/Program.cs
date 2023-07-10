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
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("GlobalConfig"));

//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=YAPWDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", c => c.MigrationsAssembly("YAPW.MainDb")));
//builder.Services.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=YAPWDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", c => c.MigrationsAssembly("YAPW.MainDb"));
//builder.Services.adda(connectionString);
//void ConfigureServices(IServiceCollection services)
//{
////}
///
builder.Services.AddServiceWorkers();
builder.Services.AddDatabases(connectionString);

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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
