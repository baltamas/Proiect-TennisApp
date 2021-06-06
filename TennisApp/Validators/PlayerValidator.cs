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
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Lirst name is mandatory!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is mandatory!");
            RuleFor(x => x.Nationality).MinimumLength(3);
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Please specify the gender: Female or Male!");
            RuleFor(x => x.PlayerRating).InclusiveBetween(1, 7);
        }

    }
}



