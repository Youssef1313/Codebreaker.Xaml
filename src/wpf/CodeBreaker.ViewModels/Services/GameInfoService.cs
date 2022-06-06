using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBreaker.ViewModels.Services;

public class GameInfoService
{
    public string GameId { get; set; } = string.Empty;
    public int MoveNumber { get; set; }
}
