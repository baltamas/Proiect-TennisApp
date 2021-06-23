using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.Data;
using TennisApp.Models;
using TennisApp.ViewModel;
namespace TennisApp.Services
{
    public class PlayersService
    {
        public ApplicationDbContext _context;

        public PlayersService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponse<List<Player>, IEnumerable<EntityManagementError>>> GetPlayers()
        {
            var players = await _context.Player.ToListAsync();
            var serviceResponse = new ServiceResponse<List<Player>, IEnumerable<EntityManagementError>>();
            serviceResponse.ResponseOk = players;
            return serviceResponse;
        }
        public async Task<ServiceResponse<Player, IEnumerable<EntityManagementError>>> GetPlayersById(int id)
        {
            var players = await _context.Player.FindAsync(id);

            var serviceResponse = new ServiceResponse<Player, IEnumerable<EntityManagementError>>();
            serviceResponse.ResponseOk = players;
            return serviceResponse;
        }
        public async Task<ServiceResponse<Player, IEnumerable<EntityManagementError>>> PostPlayer(Player player)
        {
            _context.Player.Add(player);
            var serviceResponse = new ServiceResponse<Player, IEnumerable<EntityManagementError>>();

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = player;
            }
            catch (Exception e)
            {
                var errors = new List<EntityManagementError>();
                errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<bool, IEnumerable<EntityManagementError>>> DeletePlayer(int playerId)
        {
            var serviceResponse = new ServiceResponse<bool, IEnumerable<EntityManagementError>>();

            try
            {
                var player = await _context.Player.FindAsync(playerId);
                _context.Player.Remove(player);
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = true;
            }
            catch (Exception e)
            {
                var errors = new List<EntityManagementError>();
                errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }

    }
}
