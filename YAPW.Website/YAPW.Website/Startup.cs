using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ditech.Portal.NET
{
    public class Startup
    {
        private string apiUrl;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureKeyVault(
	            new Uri($"https://yapw-keyvault.vault.azure.net/"),
	            new DefaultAzureCredential());
			var client = new SecretClient(new Uri($"https://yapw-keyvault.vault.azure.net/"), new DefaultAzureCredential());
			var secret = client.GetSecret("APIUrl");
			apiUrl = secret.Value.Value;
			Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie();

            services.AddHttpClient("api", c =>
            {
                c.BaseAddress = new Uri(apiUrl);
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) =>
            {
                return true;
            }
            });

            // Add folders to search when looking for views
            //
            services.Configure<RazorViewEngineOptions>(options =>
            {
                // {2} is area, {1} is controller, {0} is the action
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/DashBoard/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Admin/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Admin/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Shared/AdminLTE/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Partials/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Error/{0}" + RazorViewEngine.ViewExtension);
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Account/Signin", "");
            });
            services.AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
                    .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			app.UseSerilogRequestLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseDeveloperExceptionPage();
            app.UseHsts(option => option.MaxAge(days: 365).IncludeSubdomains());
            app.UseXContentTypeOptions();
            app.UseXXssProtection(option => option.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthorization();
            app.UseAuthentication();
            //app.UseMiddleware<SerilogRequestLogger>();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                   name: "mainsArea",
                   areaName: "Main",
                   template: "{area:exists}/{controller=stats}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
