using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class MembershipShowDTO
    {
        public string MembershipName { get; set; }
        public PackageShowDTO Package { get; set; }
    }
}
