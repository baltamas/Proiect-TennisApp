using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.Models;
using TennisApp.ViewModels;

namespace TennisApp.Validators
{
    public class ReviewsValidator : AbstractValidator<ReviewsViewModel>
    {
        public ReviewsValidator()
        {
            RuleFor(x => x.User).NotEmpty().WithMessage("User is mandatory!");
            RuleFor(x => x.Text).MinimumLength(20).WithMessage("The text must containt at least 20 chars!");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Introduce date! The date is mandatory!");
        }
    }
}
