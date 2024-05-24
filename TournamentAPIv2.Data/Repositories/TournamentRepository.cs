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
    internal class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentAPIv2ApiContext _context;
        public TournamentRepository(TournamentAPIv2ApiContext context)
        {
            _context = context;
        }

        public void Add(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsynch(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tournament>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tournament> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
