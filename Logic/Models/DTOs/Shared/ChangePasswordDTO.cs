using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models.DTOs.Shared
{
    public class ChangePasswordDTO
    {
        public string Token { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set;}
        
    }
}
