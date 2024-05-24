using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPIv2.Core.Entities;

namespace TournamentAPIv2.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id);
        Task<bool> AnyAsynch(int id);
        void Add(Tournament tournament);
        void Update(Tournament tournament);
        void Remove(Tournament tournament);
    }
}
