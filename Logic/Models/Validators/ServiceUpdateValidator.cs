using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class ServiceUpdateValidator:AbstractValidator<ServiceUpdateDTO>
    {
        public ServiceUpdateValidator()
        {
            RuleFor(m=>m.ServiceId).NotNull().NotEmpty();
            RuleFor(m=>m.ServiceName).NotNull().NotEmpty();
        }
    }
}
