using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TennisApp.ViewModels;

namespace TennisApp.Validators
{
    public class PlayerValidator : AbstractValidator<PlayerViewModel>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.FirstName).MinimumLength(3);
            RuleFor(x => x.LastName).MinimumLength(3);
            RuleFor(x => x.PlayerRating).InclusiveBetween(1, 7);
        }

    }
}



