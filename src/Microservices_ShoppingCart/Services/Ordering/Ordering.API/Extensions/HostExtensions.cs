using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,
                                                        Action<TContext, IServiceProvider> seeder,
                                                        int? retry = 0) where TContext : DbContext
        {
            int retryAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext));

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while migrating the database used on context {DbContextName}", typeof(TContext));
                    if (retryAvailability < 50)
                    {
                        retryAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryAvailability);
                    }                
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext,
                                    IServiceProvider> seeder,
                                    TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
