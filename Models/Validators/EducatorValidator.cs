using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Validators
{
    public class EducatorValidator : AbstractValidator<Educator>
    {
        public EducatorValidator()
        {
            RuleFor(x => x.Specialization)
                .NotNull()
                .NotEmpty()
                .Length(1, 16)
                .WithName("Specializacja")
                .Must(x => x == "Physics" || x == "Math");
        }
    }
}
