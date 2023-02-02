using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Types;
public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command.");

        descriptor
            .Field(c => c.Platform)
            .ResolveWith<PlatformResolver>(r => r.GetPlatform(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the platform to which the command belongs.");
    }

    private class PlatformResolver
    {
        public Platform GetPlatform([Parent]Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.First(p => p.Id == command.PlatformId);
        }
    }
}