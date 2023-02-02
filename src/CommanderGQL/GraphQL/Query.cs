using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL;
public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    [UsePaging]
    public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context) => context.Platforms;

    [UseDbContext(typeof(AppDbContext))]
    [UsePaging]
    public IQueryable<Command> GetCommand([ScopedService] AppDbContext context) => context.Commands;
}