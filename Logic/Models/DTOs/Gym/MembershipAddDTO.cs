﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Gym
{
    public class MembershipAddDTO
    {
        public string? MembershipName { get; set; }
        public int PackageId { get; set; }
    }
}
