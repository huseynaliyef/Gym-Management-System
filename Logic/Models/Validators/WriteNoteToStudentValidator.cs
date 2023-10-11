using FluentValidation;
using Logic.Models.DTOs.Personel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class WriteNoteToStudentValidator:AbstractValidator<WriteNotoToStudentDTO>
    {
        public WriteNoteToStudentValidator()
        {
            RuleFor(m=>m.MemberId).NotEmpty().NotNull();
            RuleFor(m=>m.Note).NotEmpty().NotNull().MaximumLength(400);
        }
    }
}
