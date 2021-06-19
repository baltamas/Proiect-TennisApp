using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using TennisApp.Data;
using TennisApp.Services;
using AutoMapper;
using System.Threading.Tasks;

namespace TestingMatches
{
    public class TestMatchesService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            Console.WriteLine("In setup.");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;
            _context = new ApplicationDbContext(options, new OperationalStoreOptionsForTests());

            _context.Matches.Add(new TennisApp.Models.Matches { Stage = "optimi", Player1Id = 1, Player2Id = 2 });
            _context.Matches.Add(new TennisApp.Models.Matches { Stage = "semifinale", Player1Id = 1, Player2Id = 2 });
            _context.SaveChanges();
        }
        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var matches in _context.Matches)
            {
                _context.Remove(matches);
            }
            _context.SaveChanges();
        }

        [Test]
        public async Task TestGetMatches()
        {
            var service = new TennisApp.Services.MatchesService(_context);
            var serviceResponse = await service.GetMatches();
            var matchesCount = serviceResponse.ResponseOk.Count;
            Assert.AreEqual(2, matchesCount);

        }

        [Test]
        public async Task TestGetMatchById()
        {
            var service = new TennisApp.Services.MatchesService(_context);
            var serviceResponse = await service.GetMatchesById(1);
            var matches = serviceResponse.ResponseOk;
            Assert.AreEqual(1,matches.MatchId);
        }

        [Test]
        public async Task TestPostMatches()
        {
            var service = new TennisApp.Services.MatchesService(_context);
            var serviceResponse = await service.PostMatches(new TennisApp.Models.Matches {MatchId = 3, Stage = "semifinale", Player1Id = 1, Player2Id = 2 });
            var matches = serviceResponse.ResponseOk;
            Assert.AreEqual(matches.MatchId, actual: 3);
        }

        [Test]
        public async Task TestDeleteMatches()
        {
            var service = new TennisApp.Services.MatchesService(_context);
            var matches = await _context.Matches.FindAsync(1);
            var serviceResponse = await service.DeleteMatches(1);
            var matchDeleted = serviceResponse.ResponseOk;
            Assert.True(matchDeleted);
        }


    }
}