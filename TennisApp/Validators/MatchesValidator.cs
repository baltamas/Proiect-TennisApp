using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.ViewModel;

namespace TennisApp.Validators
{
    public class MatchesValidator : AbstractValidator<MatchesViewModel>
    {
        public MatchesValidator()
        {
            RuleFor(x => x.Stage).NotEmpty().WithMessage("Stage is mandatory!");
            RuleFor(x => x.Date).NotEmpty().WithMessage("You need to specify the date!");
            RuleFor(x => x.Winner).NotEmpty().WithMessage("You need to choose who's the winner!");

        }

    }
}

