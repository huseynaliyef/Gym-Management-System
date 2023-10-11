using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class EquipmentAddValidator:AbstractValidator<EquipmentAddDTO>
    {
        public EquipmentAddValidator()
        {
            RuleFor(m=>m.BranchId).NotNull().NotEmpty();
            RuleFor(m=>m.EquipmentName).NotNull().NotEmpty();
            RuleFor(m=>m.Description).NotNull().NotEmpty();
            RuleFor(m=>m.count).NotNull().NotEmpty();
            RuleFor(m=>m.Status).NotNull().NotEmpty();
        }
    }
}
