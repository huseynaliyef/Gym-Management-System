using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Package:BaseEntity
    {
        public string? PackageName { get; set; }
        public int Duration { get; set; }
        public Decimal Price { get; set; }
        public ICollection<Membership>? Memberships { get; set; }
        public ICollection<Service>? Services { get; set; }
    }
}
