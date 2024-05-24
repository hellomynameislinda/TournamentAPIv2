using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPIv2.Core.Entities;

namespace TournamentAPIv2.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(int id);
        Task<bool> AnyAsynch(int id);
        void Add(Game game);
        void Update(Game game);
        void Remove(Game game);
    }
}
