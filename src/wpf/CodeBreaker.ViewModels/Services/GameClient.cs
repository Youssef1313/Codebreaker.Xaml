using CodeBreaker.Shared.APIModels;

using System.Net.Http.Json;

namespace CodeBreaker.ViewModels.Services;

public class GameClient
{
    private readonly HttpClient _httpClient;
    private int _moveNumber = 0;
    private string? _gameId;
    public GameClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task StartGameAsync(string name)
    {
        CreateGameRequest request = new(name);
        var responseMessage = await _httpClient.PostAsJsonAsync("start", request);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<CreateGameResponse>();
        _moveNumber = 1;
        _gameId = response.Id;
    }

    public async Task<string[]> SetMove(string[] colorNames)
    {
        if (_gameId is null) throw new InvalidOperationException($"call {nameof(StartGameAsync)} before");

        MoveRequest moveRequest = new(_gameId, _moveNumber++, colorNames);

        var responseMessage = await _httpClient.PostAsJsonAsync("move", moveRequest);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<MoveResponse>();
        return response.KeyPegs?.ToArray() ?? new string[0];
    }
}
