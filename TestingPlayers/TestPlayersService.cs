using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using TennisApp.Data;
using TennisApp.Services;
using AutoMapper;
using System.Threading.Tasks;

namespace TestingPlayers
{
    public class TestPlayersService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            Console.WriteLine("In setup.");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestTennisDB")
                .Options;
            _context = new ApplicationDbContext(options, new OperationalStoreOptionsForTests());
            _context.Player.Add(new TennisApp.Models.Player { Id = 1 , FirstName = "Andrei", LastName = "Pop", Age = 28, Nationality = "RO", Gender = "Male", PlayerRating = 6, PlayerScore = 22 });  ;
            _context.Player.Add(new TennisApp.Models.Player { Id = 2, FirstName = "Marius", LastName = "Avram", Age = 32, Nationality = "RO", Gender = "Male", PlayerRating = 7, PlayerScore = 24 });
            _context.SaveChanges();
        }
        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var players in _context.Player)
            {
                _context.Remove(players);
            }
            _context.SaveChanges();
        }

        [Test]
        public async Task TestGetPlayers()
        {
            var service = new PlayersService(_context);
            var serviceResponse = await service.GetPlayers();
            var playersCount = serviceResponse.ResponseOk.Count;
            Assert.AreEqual(2, playersCount);

        }

        [Test]
        public async Task TestGetPlayerById()
        {
            var service = new PlayersService(_context);
            var serviceResponse = await service.GetPlayersById(1);
            var players = serviceResponse.ResponseOk;
            Assert.AreEqual(1, players.Id);
        }

        [Test]
        public async Task TestPostPlayers()
        {
            var service = new PlayersService(_context);
            var serviceResponse = await service.PostPlayer(new TennisApp.Models.Player { Id = 3, FirstName = "Tim", LastName = "Anderson", Age = 32, Nationality = "USA" , Gender = "Male", PlayerRating = 6, PlayerScore = 20 });
            var players = serviceResponse.ResponseOk;
            Assert.AreEqual(players.Id, actual: 3);
        }

        [Test]
        public async Task TestDeletePlayers()
        {
            var service = new PlayersService(_context);
            var players = await _context.Player.FindAsync(1);
            var serviceResponse = await service.DeletePlayer(1);
            var playerDeleted = serviceResponse.ResponseOk;
            Assert.True(playerDeleted);
        }

    }
}