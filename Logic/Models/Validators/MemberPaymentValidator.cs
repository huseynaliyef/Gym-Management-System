using FluentValidation;
using Logic.Models.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.Validators
{
    public class MemberPaymentValidator:AbstractValidator<MemberPaymentDTO>
    {
        public MemberPaymentValidator()
        {
            RuleFor(m=>m.MemberId).NotEmpty().NotNull();
            RuleFor(m=>m.MembershipId).NotEmpty().NotNull();
            RuleFor(m => m.PaymentDate).NotEmpty().NotNull();
            RuleFor(m => m.Amount).NotEmpty().NotNull();
            RuleFor(m => m.PaymentMethod).NotEmpty().NotNull().MaximumLength(20);

        }
    }
}
