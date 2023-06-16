using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var grpcClient = scope.ServiceProvider.GetService<IPlatformDataClient>();
        var platforms = grpcClient.ReturnAllPlatforms();

        var repo = scope.ServiceProvider.GetService<ICommandRepo>();
        SeedData(repo, platforms);
    }

    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
        foreach (var platform in platforms)
        {
            if (!repo.ExternalPlatformExists(platform.ExternalId))
            {
                repo.CreatePlatform(platform);
            }
        }

        repo.SaveChanges();
    }
}