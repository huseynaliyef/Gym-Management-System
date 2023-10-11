using FluentValidation;
using Logic.Models.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class RecoveryEmailValidator:AbstractValidator<RecoveryEmailDTO>
    {
        public RecoveryEmailValidator()
        {
            RuleFor(m=>m.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
