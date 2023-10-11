using FluentValidation;
using Logic.Models.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class MemberRegisterValidator:AbstractValidator<MemberRegisterDTO>
    {
        public MemberRegisterValidator()
        {
            RuleFor(m=>m.UserName).NotNull().NotEmpty();
            RuleFor(m => m.Age).NotNull().NotEmpty();
            RuleFor(m => m.Gender).NotNull().NotEmpty();
            RuleFor(m => m.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(m => m.PhoneNumber).NotNull().NotEmpty();
            RuleFor(m => m.Password).NotNull().NotEmpty();
        }
    }
}
