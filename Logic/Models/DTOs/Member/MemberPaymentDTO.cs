using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Member
{
    public class MemberPaymentDTO
    {
        public string MemberId { get; set; }
        public int MembershipId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
