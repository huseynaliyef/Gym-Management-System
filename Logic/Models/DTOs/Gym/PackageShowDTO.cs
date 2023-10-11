using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class PackageShowDTO
    {
        public string? PackageName { get; set; }
        public int Duration { get; set; }
        public Decimal Price { get; set; }
    }
}
