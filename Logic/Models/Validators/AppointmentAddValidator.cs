using FluentValidation;
using Logic.Models.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class AppointmentAddValidator:AbstractValidator<AppointmentAddDTO>
    {
        public AppointmentAddValidator()
        {
            RuleFor(m=>m.PersonelId).NotEmpty().NotNull();
            RuleFor(m=>m.Duration).NotEmpty().NotNull();
        }
    }
}
