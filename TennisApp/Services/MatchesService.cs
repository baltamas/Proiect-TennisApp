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
    public class MatchesService
    {
        public ApplicationDbContext _context;
        
        public MatchesService(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public async Task<ServiceResponse<List<Matches>, IEnumerable<EntityManagementError>>> GetMatches()
        {
            var matches = await _context.Matches.ToListAsync();
            var serviceResponse = new ServiceResponse<List<Matches>, IEnumerable<EntityManagementError>>();
            serviceResponse.ResponseOk = matches;
            return serviceResponse;
        }
        public async Task<ServiceResponse<Matches, IEnumerable<EntityManagementError>>> GetMatchesById(int id)
        {
            var matches = await _context.Matches.FindAsync(id);

            var serviceResponse = new ServiceResponse<Matches, IEnumerable<EntityManagementError>>();
            serviceResponse.ResponseOk = matches;
            return serviceResponse;
        }
        public async Task<ServiceResponse<Matches, IEnumerable<EntityManagementError>>> PostMatches(Matches matches)
        {
            _context.Matches.Add(matches);
            var serviceResponse = new ServiceResponse<Matches, IEnumerable<EntityManagementError>>();

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.ResponseOk = matches;
            }
            catch (Exception e)
            {
                var errors = new List<EntityManagementError>();
                errors.Add(new EntityManagementError { Code = e.GetType().ToString(), Description = e.Message });
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<bool, IEnumerable<EntityManagementError>>> DeleteMatches(int matchId)
        {
            var serviceResponse = new ServiceResponse<bool, IEnumerable<EntityManagementError>>();

            try
            {
                var matches = await _context.Matches.FindAsync(matchId);
                _context.Matches.Remove(matches);
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
