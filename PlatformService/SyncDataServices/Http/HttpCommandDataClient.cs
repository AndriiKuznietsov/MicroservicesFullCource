using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json");

        var responce = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

        if (responce.IsSuccessStatusCode)
        {
            Console.Write("--> Sync POST to CommandService was OK!");
        }
        else
        {
            Console.Write("--> Sync POST to CommandService was NOT OK!");
        }
    }
}