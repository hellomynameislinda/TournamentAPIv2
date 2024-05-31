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
using AutoMapper;
using TournamentAPIv2.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace TournamentAPIv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //private readonly TournamentAPIv2ApiContext _context;
        //private readonly IGameRepository _gameRepository;
        private readonly IUoW _UoW;
        private readonly IMapper _mapper;

        //        public GamesController(TournamentAPIv2ApiContext context)
        //        public GamesController(IGameRepository gameRepository)
        public GamesController(IUoW uoW, IMapper mapper)
        {
            //_context = context;
            //_gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _UoW = uoW;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGame()
        {
            // return await _context.Game.ToListAsync();
            //var allGames = await _gameRepository.GetAllAsync();
            var allGames = await _UoW.GameRepository.GetAllAsync();
            //return allGames.ToList();
            return Ok(_mapper.Map<IEnumerable<GameDTO>>(allGames.ToList()));
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Game>> GetGame(int id)
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            // var game = await _context.Game.FindAsync(id);
            //var game = await _gameRepository.GetAsync(id);
            var game = await _UoW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            //return game;
            return Ok(_mapper.Map<GameDTO>(game));
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
            if (!await _UoW.GameRepository.AnyAsync(id))
            {
                return NotFound();
            }
            _UoW.GameRepository.Update(game);
            await _UoW.CompleteAsync();

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

        // PATCH: api/Games/5
        [HttpPatch("{gameId}")]
        public async Task<IActionResult> PatchGame(int gameId, [FromBody] JsonPatchDocument<Game> patchDocument)
        {
            if (patchDocument is null)
            {
                return BadRequest();
            }

            var game = await _UoW.GameRepository.GetAsync(gameId);

            if (game is null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(game, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _UoW.GameRepository.Update(game);
            await _UoW.CompleteAsync();

            return NoContent();
        }


        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            //_context.Game.Add(game);
            //await _context.SaveChangesAsync();
            _UoW.GameRepository.Add(game);
            await _UoW.CompleteAsync();

            //return CreatedAtAction("GetGame", new { id = game.Id }, game);
            return NoContent();
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //var game = await _context.Game.FindAsync(id);
            var game = await _UoW.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            //_context.Game.Remove(game);
            //await _context.SaveChangesAsync();
            _UoW.GameRepository.Remove(game);
            await _UoW.CompleteAsync();

            return NoContent();
        }

        //private async bool GameExists(int id)
        //{
        //    //return _context.Game.Any(e => e.Id == id);
        //    return await _gameRepository.AnyAsync(id);
        //}
    }
}
