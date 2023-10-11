using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class MembershipUpdateValidator:AbstractValidator<MembershipUpdateDTO>
    {
        public MembershipUpdateValidator()
        {
            RuleFor(m=>m.Id).NotNull().NotEmpty();
        }
    }
}
