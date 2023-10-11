using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AccountRecoveryEmailToken:BaseEntity
    {
        public string MemberId { get; set; }
        public string Token { get; set; }  
        public DateTime ExpireDate { get; set; }
    }
}
