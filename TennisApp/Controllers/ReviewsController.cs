using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(ApplicationDbContext context, ILogger<ReviewsController> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("{id}/Reviews")]
        public async Task<IActionResult> PostReviewsForMatch(int id, ReviewsViewModel reviews)
        {
            var matches = _context.Matches
                .Where(m => m.MatchId == id)
                .Include(m => m.Reviews).FirstOrDefault();

            if (matches == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            reviews.User = user;

            matches.Reviews.Add(_mapper.Map<Reviews>(reviews));
            _context.Entry(matches).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }
    }
}
