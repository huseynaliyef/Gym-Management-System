using FluentValidation;
using Logic.Models.DTOs.Personel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class PersonelAddFunctionValidator:AbstractValidator<PersonelAddFunctionDTO>
    {
        public PersonelAddFunctionValidator()
        {
            RuleFor(m => m.FunctionName).NotEmpty().NotNull().MaximumLength(30);
        }
    }
}
