using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisApp.Data;
using TennisApp.Models;
using TennisApp.ViewModel;
using TennisApp.ViewModels;

namespace TennisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matches>>> GetMatches()
        {
            return await _context.Matches.ToListAsync();
        }
        [HttpGet("{id}/Reviews")]
        public ActionResult<IEnumerable<MatchesWithReviewsViewModel>> GetReviewsForMatch(int id)
            var query
        {
       /*     return _context.Reviews.Where(r => r.Matches.MatchId == id).ToList();
        }
        [HttpPost("{id}/Reviews")]
        public IActionResult PostReviewForMatch(int id, Reviews reviews)
        {
            reviews.Matches = _context.Matches.Find(id);
            if (reviews.Matches == null)
            {
                return NotFound();
            }
            _context.Reviews.Add(reviews);
            _context.SaveChanges();

            return Ok();
        }
*/
            // GET: api/Matches/5
            [HttpGet("{id}")]
        public async Task<ActionResult<Matches>> GetMatches(int id)
        {
            var matches = await _context.Matches.FindAsync(id);

            var matchesViewModel = new MatchesViewModel
            {
                MatchId = matches.MatchId,
                Stage = matches.Stage,
                Date = matches.Date,
                Player1 = matches.Player1,
                Player2 = matches.Player2,
                Winner = matches.Winner,
                Reviews = matches.Reviews
            };

            if (matches == null)
            {
                return NotFound();
            }

            return matches;
        }
      
        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatches(int id, Matches matches)
        {
            if (id != matches.MatchId)
            {
                return BadRequest();
            }

            _context.Entry(matches).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matches>> PostMatches(Matches matches)
        {
            _context.Matches.Add(matches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatches", new { id = matches.MatchId }, matches);
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatches(int id)
        {
            var matches = await _context.Matches.FindAsync(id);
            if (matches == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(matches);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchesExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
