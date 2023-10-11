using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PersonelFunctions:BaseEntity
    {
        public string? FunctionName { get; set; }
        public ICollection<Member>? Personels { get; set; }
    }
}
