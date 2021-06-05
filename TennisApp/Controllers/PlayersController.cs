using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisApp.Data;
using TennisApp.Models;
using TennisApp.ViewModels;
using TennisApp.ViewModels.PlayerViewModels;

namespace TennisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayersController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerViewModel>>> GetPlayer()
        {
            var players = await _context.Player.Select(p => _mapper.Map<PlayerViewModel>(p)).ToListAsync();
            return players;
        }

        // GET: api/Players/Ranking
        [HttpGet("Ranking")]
        public async Task<ActionResult<IEnumerable<PlayerRankingViewModel>>> GetRanking()
        {
            //var players = await _context.Player.OrderByDescending(p => p.PlayerScore).ToListAsync();

            //var customersByCountry = _context.Player.GroupBy(p => p.PlayerScore)
            //    .Select(g => new {
            //        PlayerScore = g.Key,
            //        Count = g.Count(), 
            //    });
            //var ranks = customersByCountry
            //    .Select(c => new
            //    {
            //        c.PlayerScore,
            //        c.Count,
            //        Rank = customersByCountry.Count(c2 => c2.Count > c.Count) + 1
            //    });

            //var a = ranks.ToQueryString();

            //var q = from s in _context.Player
            //        orderby s.PlayerScore descending
            //        select new PlayerRankingViewModel
            //        {
            //            FirstName = s.FirstName,
            //            LastName = s.LastName,
            //            Age = s.Age,
            //            Nationality = s.Nationality,
            //            Gender = s.Gender,
            //            PlayerRating = s.PlayerRating,
            //            PlayerScore = s.PlayerScore,
            //            Ranking = (from o in _context.Player
            //                    where o.PlayerScore > s.PlayerScore
            //                    select o).Count() + 1
            //        };
            //return await q.ToListAsync();

            var query = _context.Player.OrderByDescending(p => p.PlayerScore).Select(p => new PlayerRankingViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Nationality = p.Nationality,
                Gender = p.Gender,
                PlayerRating = p.PlayerRating,
                PlayerScore = p.PlayerScore,
                Ranking = (_context.Player.Where(p2 => p2.PlayerScore > p.PlayerScore).Count() + 1)
            });
            return await query.ToListAsync();

            //List<PlayerRankingViewModel> playerRankingViewModels = new List<PlayerRankingViewModel>();
            //int currentRanking = 0;
            //int currentScore = 0;
            //foreach(Player p in players)
            //{
            //    if(p.PlayerScore != currentScore)
            //    {
            //        currentRanking++;
            //        currentScore = p.PlayerScore;
            //    }
            //    PlayerRankingViewModel playerRankingViewModel = new PlayerRankingViewModel();
            //    playerRankingViewModel.Ranking = currentRanking;
            //    playerRankingViewModel.FirstName = p.FirstName;
            //    playerRankingViewModel.LastName = p.LastName;
            //    playerRankingViewModel.Age = p.Age;
            //    playerRankingViewModel.Nationality = p.Nationality;
            //    playerRankingViewModel.Gender = p.Gender;
            //    playerRankingViewModel.PlayerRating = p.PlayerRating;
            //    playerRankingViewModel.PlayerScore = p.PlayerScore;
            //    playerRankingViewModels.Add(playerRankingViewModel);
            //}

            //return playerRankingViewModels;
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerViewModel>> GetPlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            var playerViewModel = _mapper.Map<PlayerViewModel>(player);

            return playerViewModel;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, PlayerViewModel player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Player>(player)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerViewModel>> PostPlayer(PlayerViewModel playerRequest)
        {
            Player player = _mapper.Map<Player>(playerRequest);
            _context.Player.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.Id == id);
        }
    }
}
