using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoomMate.Persistence.Repositories;

namespace RoomMate.Persistence
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddSqliteDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection"); ;
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            services.AddDbContext<RoomMateDbContext>(opt => opt.UseSqlite(connection));

            return services;
        }
    }
}
