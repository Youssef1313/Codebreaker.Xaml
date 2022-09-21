using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.ViewModels.Services;

using Microsoft.Extensions.Options;

using Moq;

namespace CodeBreaker.ViewModels.Tests;

public class GamePageViewModelTests
{
    private readonly GamePageViewModel _viewModel;

    public GamePageViewModelTests()
    {
        Mock<IGameClient> gameClient = new();
        gameClient.Setup(
            client => client.StartGameAsync("Test", "6x4Game"))
            .ReturnsAsync(new CreateGameResponse()
            {
                Game = new ()
                {
                    GameId = Guid.NewGuid(),
                    Start = DateTime.Now,
                    Type = new (
                        "6x4Game",
                        new List<string>() { "Black", "White", "Red", "Green", "Blue", "Yellow" }.AsReadOnly(),
                        4,
                        12),
                    Username = "Test",
                    Code = new List<string>() { "Black", "White", "Black", "Red" }.AsReadOnly()
                }
            });

        Mock<IOptions<GamePageViewModelOptions>> options = new();
        options.Setup(o => o.Value).Returns(new GamePageViewModelOptions() { EnableDialogs = false });

        Mock<IDialogService> dialogService = new();

        Mock<IAuthService> authService = new();

        _viewModel = new GamePageViewModel(gameClient.Object, options.Object, dialogService.Object, authService.Object);
    }
    
    [Fact]
    public async Task TestGameModeStartedAfterStart()
    {
        _viewModel.Name = "Test";
        await _viewModel.StartGameCommand.ExecuteAsync(null);

        Assert.Equal(GameMode.Started, _viewModel.GameStatus);
    }

    [Fact]
    public async Task TestInProgressNotificationAfterStart()
    {
        List<bool> expected = new() { true, false };
        _viewModel.Name = "Test";
        List<bool> inProgressValues = new();

        _viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName is "InProgress")
            {
                inProgressValues.Add(_viewModel.InProgress);
            }
        };
        
        await _viewModel.StartGameCommand.ExecuteAsync(null);

        Assert.Equal(expected, inProgressValues);
    }
}
