using FluentValidation;
using Sat.Recruitment.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The {PropertyName} is required");
            RuleFor(user => user.Email).NotEmpty().WithMessage("The {PropertyName} is required")
                .EmailAddress().WithMessage("The {PropertyName} is invalid");
            RuleFor(user => user.Address).NotEmpty().WithMessage("The {PropertyName} is required");
            RuleFor(user => user.Phone).NotEmpty().WithMessage("The {PropertyName} is required");
        }
    }
}
