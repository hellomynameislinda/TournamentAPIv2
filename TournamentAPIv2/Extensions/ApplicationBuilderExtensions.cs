using Microsoft.EntityFrameworkCore;
using TournamentAPIv2.Data.Data;


namespace TournamentAPI.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TournamentAPIv2ApiContext>();

                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();

                try
                {
                    await SeedData.InitSeedDataAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }

        }

    
    }
}
