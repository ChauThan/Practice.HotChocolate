using CommanderGQL.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Builder;

public static class DataBuilderExtensions
{
    public static void UseDbInitializer(this IApplicationBuilder app, Action<AppDbContext> action)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var dbFactory = serviceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var db = dbFactory.CreateDbContext();

        action(db);
    }
}