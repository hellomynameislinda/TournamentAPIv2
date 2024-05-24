﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPIv2.Core.Repositories
{
    public interface IUoW
    {
        ITournamentRepository TournamentRepository { get; }
        IGameRepository GameRepository { get; }
        Task CompleteAsync();
    }
}
