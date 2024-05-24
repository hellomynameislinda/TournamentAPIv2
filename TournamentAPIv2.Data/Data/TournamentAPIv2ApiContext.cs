using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentAPIv2.Core.Entities;

namespace TournamentAPIv2.Data.Data
{
    public class TournamentAPIv2ApiContext : DbContext
    {
        public TournamentAPIv2ApiContext (DbContextOptions<TournamentAPIv2ApiContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentAPIv2.Core.Entities.Tournament> Tournament { get; set; } = default!;
        public DbSet<TournamentAPIv2.Core.Entities.Game> Game { get; set; } = default!;
    }
}
