using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Shared;
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
    public interface IMemberService
    {
        Task<GenericResponse<bool>> AddAppointment(AppointmentAddDTO model, ClaimsPrincipal User, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> DisableAppointment(ClaimsPrincipal User);
        Task<GenericResponse<List<ReadTeacherNotesDTO>>> ReadTeacherNotes(ClaimsPrincipal User);
        Task<GenericResponse<List<AppointmentShowDTO>>> ShowAppointments(ClaimsPrincipal User);
    }
}
