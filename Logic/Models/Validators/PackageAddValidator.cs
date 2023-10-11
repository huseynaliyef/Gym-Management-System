using FluentValidation;
using Logic.Models.DTOs.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class PackageAddValidator:AbstractValidator<PackageAddDTO>
    {
        public PackageAddValidator()
        {
            RuleFor(m=>m.PackageName).NotEmpty().NotNull();
            RuleFor(m=>m.Duration).NotEmpty().NotNull();
            RuleFor(m=>m.Price).NotEmpty().NotNull();
        }
    }
}
