﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Service:BaseEntity
    {
        public string? ServiceName { get; set; }
        public ICollection<Package>? Packages { get; set; }
    }
}
