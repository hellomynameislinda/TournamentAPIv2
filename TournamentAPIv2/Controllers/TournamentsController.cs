using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentAPIv2.Data.Data;
using TournamentAPIv2.Core.Entities;
using TournamentAPIv2.Data.Repositories;
using TournamentAPIv2.Core.Repositories;
using AutoMapper;
using TournamentAPIv2.Core.Dto;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Core.Types;

namespace TournamentAPIv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        //private readonly TournamentAPIv2ApiContext _context;
        //private readonly ITournamentRepository _tournamentRepository;
        private readonly IUoW _UoW;
        private readonly IMapper _mapper;

        //public TournamentsController(ITournamentRepository tournamentRepository)
        public TournamentsController(IUoW uoW, IMapper mapper)
        {
            //_tournamentRepository = tournamentRepository;
            _UoW = uoW;
            _mapper = mapper;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament()
        {
            //return await _context.Tournament.ToListAsync();
            //var allTournaments= await _tournamentRepository.GetAllAsync();
            var allTournaments= await _UoW.TournamentRepository.GetAllAsync();
            //return allTournaments.ToList();
            return Ok(_mapper.Map<IEnumerable<TournamentDTO>>(allTournaments.ToList()));
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            //var tournament = await _context.Tournament.FindAsync(id);
            //var tournament = await _tournamentRepository.GetAsync(id);
            var tournament = await _UoW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            //return tournament;
            return Ok(_mapper.Map<TournamentDTO>(tournament));
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            if (!await _UoW.TournamentRepository.AnyAsync(id))
            {
                return NotFound();
            }
            //_context.Entry(tournament).State = EntityState.Modified;
            _UoW.TournamentRepository.Update(tournament);
            await _UoW.CompleteAsync();

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TournamentExists(id))
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

        // PATCH: api/Tournaments/5
        [HttpPatch("{tournamentId}")]
        public async Task<ActionResult<TournamentDTO>> PatchTournament(int tournamentId, JsonPatchDocument<Tournament> patchDocument)
        {
            if (patchDocument is null)
            {
                return BadRequest(ModelState);
            }

            var tournament = await _UoW.TournamentRepository.GetAsync(tournamentId);

            if (tournament is null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(tournament, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _UoW.TournamentRepository.Update(tournament);
            await _UoW.CompleteAsync();
            
            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            //_context.Tournament.Add(tournament);
            //await _context.SaveChangesAsync();
            _UoW.TournamentRepository.Add(tournament);
            await _UoW.CompleteAsync();

            //return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
            return NoContent();
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            //var tournament = await _context.Tournament.FindAsync(id);
            var tournament = await _UoW.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }


            //_context.Tournament.Remove(tournament);
            //await _context.SaveChangesAsync();
            _UoW.TournamentRepository.Remove(tournament);
            await _UoW.CompleteAsync();

            return NoContent();
        }

        //private async bool TournamentExists(int id)
        //{
        //    //return _context.Tournament.Any(e => e.Id == id);
        //    return _tournamentRepository.AnyAsync(id);
        //}
    }
}
