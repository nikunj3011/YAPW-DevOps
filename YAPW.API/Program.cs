using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using System;
using System.Diagnostics;
using System.Net;
using YAPW.Extentions;
using YAPW.Models.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{

//    serverOptions.Listen(IPAddress.Any, 7024, options =>
//    {
//        options.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;

//    });
//    serverOptions.Listen(IPAddress.Any, 5206, options =>
//    {
//        options.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
//    });
//});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureHttpsDefaults(listenOptions =>
    {
        // ...
    });
});
Activity.DefaultIdFormat = ActivityIdFormat.W3C;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("javaApi", c =>
{
    c.BaseAddress = new Uri("http://localhost:8083/");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

//var metricServer = new MetricServer(port: 1234);
//metricServer.Start();

//var meters = new OtelMetrics();




//builder.Services.AddOpenTelemetry().WithMetrics(opts => opts
//     //.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("BookStore.WebApi"))
//    .AddConsoleExporter()
//    .AddAspNetCoreInstrumentation()
//    .AddHttpClientInstrumentation()
//    .AddPrometheusExporter(
//    opt =>
//    {
//        //opt.StartHttpListener = true;
//        //opt.s = new string[] { $"http://localhost:9184/" };
//        opt.ScrapeEndpointPath = $"https://localhost:5001/";
//    }
//    )
//    .AddMeter("Microsoft.AspNetCore.Hosting",
//                         "Microsoft.AspNetCore.Server.Kestrel")
//    .AddRuntimeInstrumentation()
//    );

//builder.Services.AddOpenTelemetry()
//    .WithTracing(builder => builder
//        .AddAspNetCoreInstrumentation()
//        .AddSqlClientInstrumentation()
//        .AddHttpClientInstrumentation()
//        .AddConsoleExporter()
//        .AddOtlpExporter()
//        //opt =>
//        //{
//        //   opt.Endpoint = new Uri("http://localhost:16686/");
//        //   opt.Protocol = OtlpExportProtocol.HttpProtobuf;
//        //})
//        .AddZipkinExporter()
//        .AddSource("YAPW.API.NET")
//        .SetResourceBuilder(
//            ResourceBuilder.CreateDefault()
//                .AddService(serviceName: "YAPW.API.NET")));

//builder.Services.AddLogging(logging => logging.AddOpenTelemetry(openTelemetryLoggerOptions =>
//{
//    openTelemetryLoggerOptions.SetResourceBuilder(
//        ResourceBuilder.CreateEmpty()
//            // Replace "GettingStarted" with the name of your application
//            .AddService("GettingStarted")
//            .AddAttributes(new Dictionary<string, object>
//            {
//                // Add any desired resource attributes here
//                ["deployment.environment"] = "development"
//            }));

//    // Some important options to improve data quality
//    openTelemetryLoggerOptions.IncludeScopes = true;
//    openTelemetryLoggerOptions.IncludeFormattedMessage = true;

//    openTelemetryLoggerOptions.AddOtlpExporter(exporter =>
//    {
//        // The full endpoint path is required here, when using
//        // the `HttpProtobuf` protocol option.
//        exporter.Endpoint = new Uri("http://localhost:5341/ingest/otlp/v1/logs");
//        exporter.Protocol = OtlpExportProtocol.HttpProtobuf;
//        // Optional `X-Seq-ApiKey` header for authentication, if required
//        exporter.Headers = "X-Seq-ApiKey=abcde12345";
//    });
//}));




var appSettings = builder.Configuration.GetSection("GlobalConfig").Get<AppSetting>();
var currentEnvironmentSettings = appSettings.Environments.SingleOrDefault(e => e.Name.ToLower() == builder.Environment.EnvironmentName.ToLower());
var connectionString = currentEnvironmentSettings.SettingsData.ConnectionString;
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("GlobalConfig"));
builder.Services.AddServiceWorkers();

//var azureKeyVaultUrl = currentEnvironmentSettings.SettingsData.AzureKeyVaultUrl;
//builder.Configuration.AddAzureKeyVault(
//       new Uri($"https://{azureKeyVaultUrl}.vault.azure.net/"),
//       new DefaultAzureCredential());
//var client = new SecretClient(new Uri($"https://{azureKeyVaultUrl}.vault.azure.net/"), new DefaultAzureCredential());
//var secret = await client.GetSecretAsync("ConnectionStrings");
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





//app.UseOpenTelemetryPrometheusScrapingEndpoint("metrics");





//app.MapPrometheusScrapingEndpoint("metrics");
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}

//app.UseKestrel(options => { options.Listen(IPAddress.Any, 1234); });

app.UseHttpsRedirection();
app.UseOutputCache();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
