using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TennisApp.Models;
using TennisApp.ViewModel;
using TennisApp.ViewModels;

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
        }
        }
    }

