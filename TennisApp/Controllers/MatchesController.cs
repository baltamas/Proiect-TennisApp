using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TennisApp.Data;
using TennisApp.Models;
using TennisApp.ViewModel;
using TennisApp.ViewModels;
using TennisApp.ViewModels.MatchesViewModels;
using TennisApp.ViewModels.PlayerViewModels;

namespace TennisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MatchesController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MatchesController(ApplicationDbContext context, ILogger<MatchesController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
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
        public ActionResult<MatchesWithReviewsViewModel> GetReviewsForMatch(int id)

        {
            var query = _context.Matches.Where(m => m.MatchId == id)
                  .Include(m => m.Reviews)
                  .Select(m => _mapper.Map<MatchesWithReviewsViewModel>(m));

            return query.ToList()[0];
        }

        [HttpGet("Players")]
        public ActionResult<IEnumerable<MatchesWithPlayerNamesViewModel>> GetMatchesWithPlayers()
        {
            var query = _context.Matches.Include(m => m.Player1).Include(m => m.Player2)
                .Select(m => new MatchesWithPlayerNamesViewModel
                {
                    MatchId = m.MatchId,
                    Stage = m.Stage,
                    Date = m.Date,
                    Player1Name = m.Player1.FirstName + " " + m.Player1.LastName,
                    Player2Name = m.Player2.FirstName + " " + m.Player2.LastName,
                    Winner = m.Winner,
                    Player1Id = m.Player1Id.Value,
                    Player2Id = m.Player2Id.Value
                });

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

            // updatare winner
            if(matches.Winner != null)
            {
                var winnerId = matches.Winner.Value ? matches.Player1Id : matches.Player2Id;

                var player = await _context.Player.FindAsync(winnerId.Value);
                player.PlayerScore++;
                _context.Entry(player).State = EntityState.Modified;

                var dep1 = _context.Matches.Where(m => m.Dep1Id == id).ToList();
                if(dep1.Count > 0)
                {
                    dep1[0].Player1Id = winnerId;
                    _context.Entry(dep1[0]).State = EntityState.Modified;
                }
                var dep2 = _context.Matches.Where(m => m.Dep2Id == id).ToList();
                if (dep2.Count > 0)
                {
                    dep2[0].Player2Id = winnerId;
                    _context.Entry(dep2[0]).State = EntityState.Modified;
                }                
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
        public async Task<ActionResult<Matches>> PostMatches(MatchViewModel match)
        {
            _context.Matches.Add(_mapper.Map<Matches>(match));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatches", new { id = match.Id }, match);
        }

        // POST: api/Matches/5/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/Players")]
        public async Task<ActionResult<Matches>> AddPlayersToMatch(int id, List<PlayerViewModel> players)
        {
            var match = _context.Matches.Where(m => m.MatchId == id).FirstOrDefault();
            if (match == null)
            {
                return NotFound();
            }
            if (players.Count != 2)
            {
                return BadRequest();
            }

            var player1 = _context.Player.Where(p => p.Id == players[0].Id).FirstOrDefault();
            var player2 = _context.Player.Where(p => p.Id == players[1].Id).FirstOrDefault();

            match.Player1 = player1;
            match.Player2 = player2;

            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Matches/5/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/Player")]
        public async Task<ActionResult<Matches>> AddPlayer1ToMatch(int id, PlayerWithPositionViewModel playerViewModel)
        {
            var match = _context.Matches.Where(m => m.MatchId == id).FirstOrDefault();
            if (match == null)
            {
                return NotFound();
            }
            if(playerViewModel.Pos !=1 && playerViewModel.Pos !=2)
            {
                return BadRequest();
            }
            
            var player = _context.Player.Where(p => p.Id == playerViewModel.Id).FirstOrDefault();

            if (playerViewModel.Pos == 1)
                match.Player1 = player;
            else
                match.Player2 = player;

            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        //[HttpPost("{id}/Reviews")]
        //public async Task<IActionResult> PostReviewsForMatch(int id, ReviewsViewModel reviews)
        //{
        //    var matches = _context.Matches
        //        .Where(m => m.MatchId == id)
        //        .Include(m => m.Reviews).FirstOrDefault();

        //    if (matches == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    reviews.User = user;

        //    matches.Reviews.Add(_mapper.Map<Reviews>(reviews));
        //    _context.Entry(matches).State = EntityState.Modified;
        //    _context.SaveChanges();

        //    return Ok();
        //}

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
