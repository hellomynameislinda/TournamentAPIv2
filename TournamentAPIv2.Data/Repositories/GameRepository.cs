using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPIv2.Core.Entities;
using TournamentAPIv2.Core.Repositories;
using TournamentAPIv2.Data.Data;

namespace TournamentAPIv2.Data.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private readonly TournamentAPIv2ApiContext _context;
        public GameRepository(TournamentAPIv2ApiContext context)
        {
            _context = context;
        }

        public void Add(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsynch(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Game game)
        {
            throw new NotImplementedException();
        }

        public void Update(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
