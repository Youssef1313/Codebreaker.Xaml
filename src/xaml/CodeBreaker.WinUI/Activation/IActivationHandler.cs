using System.Threading.Tasks;

namespace CodeBreaker.WinUI.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
