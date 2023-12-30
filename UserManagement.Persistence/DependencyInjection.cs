using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

using UserManagement.Application.Abstractions.Data;

namespace UserManagement.Persistence;

public static class DependencyInjection
{
    private static MongoClient? _mongoClient;

    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("MONGODB");
        _mongoClient = new MongoClient(connectionString);
        IMongoDatabase db = _mongoClient.GetDatabase("UserManagement");

        services.AddDbContext<UserMangementDbContext>(options => options.UseMongoDB(_mongoClient, db.DatabaseNamespace.DatabaseName));
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<UserMangementDbContext>());
        //services.AddSingleton<IDbConnection>(_ => db);

        return services;
    }
}
