using Azure;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Metadata;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.Extentions;

public static class SevicesInjector
{
    #region Service workers

    public static void AddGenericNamedEntityServices(this IServiceCollection services)
    {
        services.AddTransient<NamedEntityServiceWorker<MainDb.DbModels.Type, DataContext>>();
        //services.AddTransient<EntityRepositoryServiceWorker<EntityRepository<EntityBase, DitechDalDbContext>, EntityBase, DitechDalDbContext>>();
        //services.AddTransient<EntityRepositoryServiceWorker<NamedEntityRepository<NamedEntity, DitechDalDbContext>, NamedEntity, DitechDalDbContext>>();
    }

    public static void AddGenericEntityServices(this IServiceCollection services)
    {
        //Operations
        //
        //services.AddTransient<EntityServiceWorker<Tank, DitechDalDbContext>>();
    }

    public static void AddServiceWorkers(this IServiceCollection services)
    {
        services.AddScoped<ServiceWorker<DataContext>>();
        services.AddGenericEntityServices();
        services.AddGenericNamedEntityServices();
        //services.AddTransient<IIdentityServiceWorker<ConfigurationDbContext>, IdentityServiceWorker<ConfigurationDbContext>>();
        //services.AddSamsaraServiceWorkers();
    }

    #endregion Service workers

    #region Databases Services

    public static void AddDatabases(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or whitespace.", nameof(connectionString));
        }

        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });
    }
    #endregion Databases Services
}
