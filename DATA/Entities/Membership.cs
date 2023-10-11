using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Membership:BaseEntity
    {
        public string MembershipName { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public ICollection<MemberMembership>? MemberMemberships { get; set; }
    }
}
