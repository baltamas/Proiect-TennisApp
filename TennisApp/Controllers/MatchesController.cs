using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MatchesController> _logger;
        private readonly IMapper _mapper;

        public MatchesController(ApplicationDbContext context, ILogger<MatchesController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchesViewModel>>> GetMatches()
        {
            var matches = await _context.Matches.Select (m => _mapper.Map<MatchesViewModel>(m)).ToListAsync();

            return matches;
        }

            // GET: api/Matches/5
            [HttpGet("{id}")]
        public async Task<ActionResult<MatchesViewModel>> GetMatches(int id)
        {
            var matches = await _context.Matches.FindAsync(id);

            if (matches == null)
            {
                return NotFound();
            }

            var matchesViewModel = _mapper.Map<MatchesViewModel>(matches);
            return matchesViewModel;
        }

        [HttpGet("{id}/Reviews")]
        public ActionResult<IEnumerable<MatchesWithReviewsViewModel>> GetReviewsForMatch(int id)

        {
            var query = _context.Matches.Where(m => m.MatchId == id)
                  .Include(m => m.Reviews)
                  .Select(m => _mapper.Map<MatchesWithReviewsViewModel>(m));

            return query.ToList();
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatches(int id, MatchesViewModel matches)
        {
            if (id != matches.MatchId)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Matches>(matches)).State = EntityState.Modified;

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
        public async Task<ActionResult<Matches>> PostMatches(MatchesViewModel matches)
        {
            _context.Matches.Add(_mapper.Map<Matches>(matches));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatches", new { id = matches.MatchId }, matches);
        }

        [HttpPost("{id}/Reviews")]
        public IActionResult PostReviewsForMatch(int id, ReviewsViewModel reviews)
        {
            var matches = _context.Matches
                .Where(m => m.MatchId == id)
                .Include(m => m.Reviews).FirstOrDefault();

            if (matches == null)
            {
                return NotFound();
            }

            matches.Reviews.Add(_mapper.Map<Reviews>(reviews));
            _context.Entry(matches).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
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
        // DELETE: api/Matches/1/Reviews/5
        [HttpDelete("{id}/Reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool MatchesExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
