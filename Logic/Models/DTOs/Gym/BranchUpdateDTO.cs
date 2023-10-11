using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class BranchUpdateDTO
    {
        public int BranchId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public string WorkDateFrom { get; set; }
        public string WorkDateTo { get; set; }
    }
}
