using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Payment:BaseEntity
    {
        public string? MemberId { get; set; }
        public Member? Member { get; set; }
        public Decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
