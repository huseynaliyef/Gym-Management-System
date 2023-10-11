using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class EquipmentAddDTO
    {
        public int BranchId { get; set; }
        public string? EquipmentName { get; set; }
        public string? Description { get; set; }
        public int count { get; set; }
        public bool Status { get; set; }
    }
}
