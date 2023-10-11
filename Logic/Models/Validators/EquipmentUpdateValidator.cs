using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class EquipmentUpdateValidator:AbstractValidator<EquipmentUpdateDTO>
    {
        public EquipmentUpdateValidator()
        {
            RuleFor(m => m.EquipmentId).NotNull().NotEmpty();
        }
    }
}
