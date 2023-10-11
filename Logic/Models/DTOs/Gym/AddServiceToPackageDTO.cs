using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class AddServiceToPackageDTO
    {
        public int PackageId { get; set; }
        public int ServiceId { get; set; }

    }
}
