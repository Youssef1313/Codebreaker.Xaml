using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeBreaker.ViewModels;

[ObservableRecipient]
[ObservableObject]
public partial class KeyPegsViewModel
{
    public KeyPegsViewModel()
    {

    }

    public string[] KeyPegs { get; set; }
}
