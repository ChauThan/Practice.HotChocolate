using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Types;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents any software or service that has a command line interface.");

        descriptor
            .Field(p => p.LicenseKey)
            .Ignore();

        descriptor
            .Field(p => p.Commands)
            .ResolveWith<CommandResolver>(r => r.GetCommands(default!, default!))
            .UseDbContext<AppDbContext>();
    }

    private class CommandResolver
    {
        public IQueryable<Command> GetCommands([Parent]Platform platform, [ScopedService] AppDbContext appDbContext)
        {
            return appDbContext.Commands.Where(p => p.PlatformId == platform.Id);
        }
    }
}