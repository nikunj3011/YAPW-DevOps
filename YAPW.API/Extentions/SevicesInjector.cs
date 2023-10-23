using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YAPW.Domain.Interfaces.External;
using YAPW.Domain.Interfaces.Shared.Microsoft365;
using YAPW.Domain.Repositories.Shared.Microsoft365;
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
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Actor, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Tag, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Type, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.Video, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.VideoTitle, DataContext>>();
        services.AddTransient<NamedEntityServiceWorker<YAPW.MainDb.DbModels.View, DataContext>>();
        services.AddTransient<INameService, NameService>();
    }

    public static void AddGenericEntityServices(this IServiceCollection services)
    {
        //Operations
        //
        services.AddTransient<EntityServiceWorker<YAPW.MainDb.DbModels.Link, DataContext>>();
        services.AddTransient<EntityServiceWorker<YAPW.MainDb.DbModels.VideoInfoVideoTitle, DataContext>>();
    }

    public static void AddServiceWorkers(this IServiceCollection services)
    {
        services.AddScoped<ServiceWorker<DataContext>>();
        services.AddGenericNamedEntityServices();
        services.AddGenericEntityServices();
        services.AddMicrosoft365ServiceWorkers();
    }

	#endregion Service workers

	#region Microsoft365 Services

	public static void AddMicrosoft365ServiceWorkers(this IServiceCollection services)
	{
		services.AddTransient<IEmailService, EmailRepository>();
	}

	#endregion Microsoft365 Services

	#region Databases Services

	public static void AddDatabases(this IServiceCollection services, string connectionString, bool isMySql = false)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or whitespace.", nameof(connectionString));
        }

        if(isMySql)
        {
		    var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
		    // Replace 'YourDbContext' with the name of your own DbContext derived class.
		    services.AddDbContext<DataContext>(
			    dbContextOptions => dbContextOptions
				    .UseMySql(connectionString, serverVersion)
				    // The following three options help with debugging, but should
				    // be changed or removed for production.
				    .LogTo(Console.WriteLine, LogLevel.Information)
				    .EnableSensitiveDataLogging()
				    .EnableDetailedErrors()
		    );
	    }
        else
        {
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(connectionString);
				options.EnableSensitiveDataLogging();
				options.EnableDetailedErrors();
			});
		}
    }
    #endregion Databases Services
}
