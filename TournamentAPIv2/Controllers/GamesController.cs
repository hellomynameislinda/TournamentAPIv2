using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentAPIv2.Data.Data;
using TournamentAPIv2.Core.Entities;
using TournamentAPIv2.Core.Repositories;

namespace TournamentAPIv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //private readonly TournamentAPIv2ApiContext _context;
        private readonly IGameRepository _gameRepository;

//        public GamesController(TournamentAPIv2ApiContext context)
        public GamesController(IGameRepository gameRepository)
        {
            //_context = context;
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            // return await _context.Game.ToListAsync();
            var allGames = await _gameRepository.GetAllAsync();
            return allGames.ToList();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            // var game = await _context.Game.FindAsync(id);
            var game = await _gameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            //  _context.Entry(game).State = EntityState.Modified;
            if (!await _gameRepository.AnyAsync(id))
            {
                return NotFound();
            }
            _gameRepository.Update(game);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!GameExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            //_context.Game.Add(game);
            //await _context.SaveChangesAsync();
            _gameRepository.Add(game);

            //return CreatedAtAction("GetGame", new { id = game.Id }, game);
            return NoContent();
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //var game = await _context.Game.FindAsync(id);
            var game = await _gameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            //_context.Game.Remove(game);
            //await _context.SaveChangesAsync();
            _gameRepository.Remove(game);

            return NoContent();
        }

        //private async bool GameExists(int id)
        //{
        //    //return _context.Game.Any(e => e.Id == id);
        //    return await _gameRepository.AnyAsync(id);
        //}
    }
}
