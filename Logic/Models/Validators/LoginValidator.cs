using FluentValidation;
using Logic.Models.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class LoginValidator:AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(m => m.UserName).NotEmpty().NotNull();
            RuleFor(m=>m.Password).NotEmpty().NotNull();
        }
    }
}
