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
        private ITournamentRepository _tournamentRepository;
        private IGameRepository _gameRepository;
        public UoW(TournamentAPIv2ApiContext context)
        {
            _context = context;
        }

        public ITournamentRepository TournamentRepository
        {
            get
            {
                if (_tournamentRepository == null)
                {
                    _tournamentRepository = new TournamentRepository(_context);
                }
                return _tournamentRepository;
            }
        }

        public IGameRepository GameRepository
        {
            get
            {
                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository(_context);
                }
                return _gameRepository;
            }
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
