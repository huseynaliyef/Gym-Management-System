using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class PackageUpdateDTO
    {
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
        public int Duration { get; set; }
        public Decimal Price { get; set; }
    }
}
