using FluentValidation;
using Logic.Models.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(m=>m.Token).NotNull().NotEmpty();
            RuleFor(m=>m.newPassword).NotNull().NotEmpty();
            RuleFor(m=>m.confirmPassword).NotNull().NotEmpty();

        }
    }
}
