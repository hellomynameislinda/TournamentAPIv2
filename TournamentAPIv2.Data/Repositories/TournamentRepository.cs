using Microsoft.EntityFrameworkCore;
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
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentAPIv2ApiContext _context;
        public TournamentRepository(TournamentAPIv2ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Tournament.ToListAsync();
        }

        public async Task<Tournament> GetAsync(int id)
        {
            return await _context.Tournament.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournament.AnyAsync(t => t.Id == id);
        }

        public void Add(Tournament tournament)
        {
            _context.Tournament.Add(tournament);
            //_context.SaveChanges();
        }

        public void Update(Tournament tournament)
        {
            _context.Tournament.Update(tournament);
            //_context.SaveChanges();
        }
        public void Remove(Tournament tournament)
        {
            _context.Tournament.Remove(tournament);
            //_context.SaveChanges();
        }
    }
}
