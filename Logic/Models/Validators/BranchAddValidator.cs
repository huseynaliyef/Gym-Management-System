using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class BranchAddValidator:AbstractValidator<BranchAddDTO>
    {
        public BranchAddValidator()
        {
            RuleFor(m=>m.Name).NotEmpty().NotNull();
            RuleFor(m=>m.Address).NotEmpty().NotNull();
            RuleFor(m=>m.Capacity).NotEmpty().NotNull();
            RuleFor(m=>m.WorkDateFrom).NotEmpty().NotNull();
            RuleFor(m=>m.WorkDateTo).NotEmpty().NotNull();
        }
    }
}
