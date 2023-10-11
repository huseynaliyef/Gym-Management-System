using FluentValidation;
using Logic.Models.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class RemoveUserValidator:AbstractValidator<RemoveUserDTO>
    {
        public RemoveUserValidator()
        {
            RuleFor(m => m.UserName).NotEmpty().NotNull();
        }
    }
}
