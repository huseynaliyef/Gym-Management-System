using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.Response_Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Abstractions
{
    public interface IPersonelService
    {
        Task<GenericResponse<bool>> AddYourFunction(PersonelAddFunctionDTO model, ClaimsPrincipal userModel, ModelStateDictionary ModelState);
        Task<GenericResponse<List<MemberShowDTO>>> PersonelGetStudents(ClaimsPrincipal User);
        Task<GenericResponse<bool>> WriteNoteToStudent(WriteNotoToStudentDTO model, ModelStateDictionary ModelState, ClaimsPrincipal User);
    }
}
