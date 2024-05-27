using Azure.Identity;
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
    public class GameRepository : IGameRepository
    {
        private readonly TournamentAPIv2ApiContext _context;
        public GameRepository(TournamentAPIv2ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            // return await _context.Game.Where(g => g.Id == id).FirstOrDefaultAsync();
            return await _context.Game.FindAsync(id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Game.AnyAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Game.Add(game);
            _context.SaveChanges();
        }

        public void Update(Game game)
        {
            _context.Game.Update(game);
            _context.SaveChanges();
        }

        public void Remove(Game game)
        {
            _context.Game.Remove(game);
            _context.SaveChanges();
        }
    }
}
