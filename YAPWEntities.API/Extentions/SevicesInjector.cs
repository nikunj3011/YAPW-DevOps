using Microsoft.EntityFrameworkCore;
using YAPW.Domain.Interfaces.External;
using YAPW.Domain.Services.Generic;
using YAPW.Domain.Services.Internal;
using YAPW.MainDb;

namespace YAPW.Extentions;

public static class SevicesInjector
{
    #region Service workers

    public static void AddGenericNamedEntityServices(this IServiceCollection services)
    {
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Brand, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Category, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Photo, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Pornstar, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Tag, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Type, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Video, DataContext>>();
        services.AddTransient<INameService, NameService>();
    }

    public static void AddGenericEntityServices(this IServiceCollection services)
    {
        //Operations
        //
        services.AddTransient<EntityServiceWorker<YAPW.MainDb.DbModels.Link, DataContext>>();
    }

    public static void AddServiceWorkers(this IServiceCollection services)
    {
        services.AddGenericNamedEntityServices();
        services.AddGenericEntityServices();
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
