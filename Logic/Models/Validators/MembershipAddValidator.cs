using Data.Entities;
using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class MembershipAddValidator:AbstractValidator<MembershipAddDTO>
    {
        public MembershipAddValidator()
        {
            RuleFor(m=>m.MembershipName).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(m => m.PackageId).NotNull().NotEmpty();
        }
    }
}
