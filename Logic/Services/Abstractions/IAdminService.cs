using Logic.Models.DTOs.Admin;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.Response_Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Abstractions
{
    public interface IAdminService
    {
        Task<GenericResponse<List<MemberShowDTO>>> ShowMembers();
        Task<GenericResponse<List<PersonelShowDTO>>> ShowPersonels();
        Task<GenericResponse<bool>> PaymentForMembership(MemberPaymentDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> RemoveUser(RemoveUserDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> TerminateMemberMembership(TerminateMemberMembershipDTO model);
    }
}
