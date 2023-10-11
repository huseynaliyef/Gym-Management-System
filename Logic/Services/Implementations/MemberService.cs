using AutoMapper;
using Data.Entities;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Shared;
using Logic.Models.Response_Model;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Implementations
{
    public class MemberService: IMemberService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<MemberMembership> _memberMembershipRepository;
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IGenericRepository<TeacherNotesToStudent> _teacherNotesRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public MemberService(IMapper mapper,
            IGenericRepository<Member> memberRepository,
            IGenericRepository<MemberMembership> memberMembershipRepository,
            IGenericRepository<Appointment> appointmentRepository,
            IGenericRepository<TeacherNotesToStudent> teacherNotesRepository,
            UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _memberMembershipRepository = memberMembershipRepository;
            _appointmentRepository = appointmentRepository;
            _teacherNotesRepository = teacherNotesRepository;
            _userManager = userManager;
        }
        public async Task<GenericResponse<bool>> AddAppointment(AppointmentAddDTO model, ClaimsPrincipal User, ModelStateDictionary ModelState)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    Response.Error(400, error);
                    return Response;
                }

                var loginedUser = await _userManager.GetUserAsync(User);
                var personel = (await _memberRepository.GetByExperssion(x => x.Id == model.PersonelId)).FirstOrDefault();
                var existAppointment = (await _appointmentRepository.GetByExperssion(x => x.MemberId == loginedUser.Id)).FirstOrDefault();
                var membership = (await _memberMembershipRepository.GetByExperssion(x => x.MemberId == loginedUser.Id && x.IsActive == true)).FirstOrDefault();
                if (personel != null && membership != null)
                {
                    if (existAppointment == null)
                    {
                        var newAppointment = _mapper.Map<Appointment>(model);
                        newAppointment.MemberId = loginedUser.Id;
                        await _appointmentRepository.AddAndCommit(newAppointment);
                        Response.Success(true, 200);
                    }
                    else
                    {
                        Response.Error(400, "This user currently has an appointment");
                    }
                }
                else Response.Error(404, "The user's membership or Personel is not found.");
                Log.Logger.Information($"{nameof(GymService)}.{nameof(AddAppointment)} mehod is executed");

            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> DisableAppointment(ClaimsPrincipal User)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                var loginedMember = await _userManager.GetUserAsync(User);
                var existAppointment = (await _appointmentRepository.GetByExperssion(x => x.MemberId == loginedMember.Id)).FirstOrDefault();

                if (existAppointment != null)
                {
                    _appointmentRepository.Delete(existAppointment);
                    await _appointmentRepository.Commit();
                    Response.Success(true, 200);
                }
                else
                {
                    Response.Error(404, "You don't have any appointment.");
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(DisableAppointment)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<List<ReadTeacherNotesDTO>>> ReadTeacherNotes(ClaimsPrincipal User)
        {
            var Response = new GenericResponse<List<ReadTeacherNotesDTO>>();
            try
            {
                var loginedMember = await _userManager.GetUserAsync(User);
                var notes = (await _teacherNotesRepository.GetByExperssion(x => x.MemberId == loginedMember.Id)).FirstOrDefault();
                if(notes != null)
                {
                    var TeacherId = notes.PersonelId;
                    var PersonelName = (await _memberRepository.GetByExperssion(x => x.Id == TeacherId)).FirstOrDefault().UserName;

                    var NoteList = _mapper.Map<List<ReadTeacherNotesDTO>>(notes);
                    NoteList.ForEach(x => x.PersonelName = PersonelName);
                    Response.Success(NoteList, 200);
                }
                else
                {
                    Response.Error(404, "You Don't have any note.");
                }
                

                Log.Logger.Information($"{nameof(GymService)}.{nameof(ReadTeacherNotes)} mehod is executed");

            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<List<AppointmentShowDTO>>> ShowAppointments(ClaimsPrincipal User)
        {
            var Response = new GenericResponse<List<AppointmentShowDTO>>();
            try
            {
                var logsinedUser = await _userManager.GetUserAsync(User);
                var Appointments = await _appointmentRepository.GetByExperssion(m => m.MemberId == logsinedUser.Id);
                var AppointmentList = _mapper.Map<List<AppointmentShowDTO>>(Appointments);
                Response.Success(AppointmentList, 200);
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
            }
            return Response;
        }
    }
}
