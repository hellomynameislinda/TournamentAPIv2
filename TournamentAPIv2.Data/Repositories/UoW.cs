using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPIv2.Core.Repositories;
using TournamentAPIv2.Data.Data;

namespace TournamentAPIv2.Data.Repositories
{
    public class UoW : IUoW
    {
        private readonly TournamentAPIv2ApiContext _context;
        public UoW(TournamentAPIv2ApiContext context)
        {
            _context = context;
        }

        public ITournamentRepository TournamentRepository => throw new NotImplementedException();

        public IGameRepository GameRepository => throw new NotImplementedException();

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
