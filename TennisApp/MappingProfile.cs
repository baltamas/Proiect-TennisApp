using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TennisApp.Models;
using TennisApp.ViewModel;
using TennisApp.ViewModels;
using TennisApp.ViewModels.MatchesViewModels;
using TennisApp.ViewModels.PlayerViewModels;

namespace TennisApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Matches, MatchesViewModel>().ReverseMap();
            CreateMap<Reviews, ReviewsViewModel>().ReverseMap();
            CreateMap<Matches, MatchesWithReviewsViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>().ReverseMap();
            CreateMap<Matches, MatchViewModel>().ReverseMap();
            CreateMap<Player, PlayerRankingViewModel>().ReverseMap();
            CreateMap<Matches, MatchesWithPlayersViewModel>().ReverseMap();
            CreateMap<Matches, MatchesWithPlayerNamesViewModel>().ReverseMap();

        }
    }
}

