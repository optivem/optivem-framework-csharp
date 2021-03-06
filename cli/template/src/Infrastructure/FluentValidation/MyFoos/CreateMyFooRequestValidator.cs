﻿using FluentValidation;
using Atomiv.Infrastructure.FluentValidation;
using Cli.Core.Application.MyFoos.Requests;

namespace Cli.Infrastructure.FluentValidation.MyFoos
{
    public class CreateMyFooRequestValidator : BaseValidator<CreateMyFooRequest>
    {
        public CreateMyFooRequestValidator()
        {
            RuleFor(e => e.FirstName).NotNull();
            RuleFor(e => e.LastName).NotNull();
        }
    }
}
