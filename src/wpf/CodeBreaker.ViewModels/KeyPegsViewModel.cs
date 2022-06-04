using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
