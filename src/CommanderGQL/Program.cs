using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Types;
using CommanderGQL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddPooledDbContextFactory<AppDbContext>(
    opt => opt.UseSqlServer(configuration.GetConnectionString("CommandConStr")));
services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.UseGraphQLVoyager();

app.UseDbInitializer(context => {
    context.Database.Migrate();

    if(context.Platforms.Any()){
        return;
    }

    var stuffsToLearn = new string[] {"GraphQL", "C#", "SQL Server", "Docker", "Kubernetes", "Blazor", "Postgre", "Azure"};

    foreach(var stuff in stuffsToLearn)
    {
        context.Platforms.Add(new Platform
        {
            Name = stuff, 
            LicenseKey = $"{stuff} LicenseKey",
            Commands = new Command[]{
                new Command {CommandLine = $"Learn {stuff}", HowTo = "I don't know!!!"}
            }
        });
    }
    context.SaveChanges();
});

app.MapGet("/", () => "Hello World!");

app.Run();
