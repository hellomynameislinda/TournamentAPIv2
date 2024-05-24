using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPIv2.Core.Entities;

namespace TournamentAPIv2.Data.Data
{
    public static class SeedData
    {
        private static Faker faker;

        public async static Task InitSeedDataAsync(TournamentAPIv2ApiContext context)
        {
            faker = new Faker("sv");
            var tournaments = SeedTournaments(60);
            await context.AddRangeAsync(tournaments);

            // The save has to occur twice, as there is no nav Tournament object on Games (if I added that, POST to the API would not work properly)
            await context.SaveChangesAsync();

            var savedTournaments = await context.Tournament.ToListAsync();

            var games = SeedGames(180, savedTournaments);
            await context.AddRangeAsync(games);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Tournament> SeedTournaments(int seedAmount)
        {
            var tournaments = new List<Tournament>();

            for (int i = 0; i < seedAmount; i++)
            {
                tournaments.Add(new Tournament
                {
                    Title = $"{faker.Address.City()} Open",
                    StartDate = faker.Date.Past(3)

                });
            }

            return tournaments;
        }
        private static IEnumerable<Game> SeedGames(int seedAmount, IEnumerable<Tournament> tournaments)
        {
            var games = new List<Game>();

            List<Tournament> tmpTournament = tournaments.ToList();

            Random rnd = new Random();

            for (int i = 0; i < seedAmount; i++)
            {
                int currentRandom = rnd.Next(0, tmpTournament.Count);
                Tournament randTournament = tmpTournament[currentRandom];
                //                int tId = tmpTournament[rnd.Next(0, tmpTournament.Count)].Id;
                DateTime startDate = randTournament.StartDate;
                games.Add(new Game
                {
                    Title = faker.Address.City(),
                    Time = faker.Date.Between(startDate, startDate.AddMonths(2)),
                    TournamentId = randTournament.Id
                });
            }
            return games;
        }
    }
}
