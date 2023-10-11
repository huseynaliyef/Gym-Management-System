using AutoMapper;
using Data.Entities;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.Response_Model;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Implementations
{
    public class PersonelService: IPersonelService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<PersonelFunctions> _personelFunctionsRepository;
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IGenericRepository<TeacherNotesToStudent> _teacherNotesRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public PersonelService(IMapper mapper,
            IGenericRepository<Member> memberRepository,
            IGenericRepository<PersonelFunctions> personelFunctionsRepository,
            IGenericRepository<Appointment> appointmentRepository,
            IGenericRepository<TeacherNotesToStudent> teacherNotesRepository,
            UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _personelFunctionsRepository = personelFunctionsRepository;
            _appointmentRepository = appointmentRepository;
            _teacherNotesRepository = teacherNotesRepository;
            _userManager = userManager;
        }
        public async Task<GenericResponse<bool>> AddYourFunction(PersonelAddFunctionDTO model, ClaimsPrincipal userModel, ModelStateDictionary ModelState)
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

                Member loginedPersonel = (Member)await _userManager.GetUserAsync(userModel);
                var existFunction = (await _personelFunctionsRepository.GetByExperssion(x => x.FunctionName == model.FunctionName)).FirstOrDefault();
                if (existFunction == null)
                {
                    existFunction = _mapper.Map<PersonelFunctions>(model);
                }


                loginedPersonel.Functions ??= new List<PersonelFunctions>();
                loginedPersonel.Functions.Add(existFunction);
                await _memberRepository.Commit();
                Response.Success(true, 200);

                Log.Logger.Information($"{nameof(GymService)}.{nameof(AddYourFunction)} mehod is executed");

            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<List<MemberShowDTO>>> PersonelGetStudents(ClaimsPrincipal User)
        {
            var Response = new GenericResponse<List<MemberShowDTO>>();

            try
            {
                var loginedPersonel = await _userManager.GetUserAsync(User);
                var PersonelAppointments = await _appointmentRepository.GetTableWithRelation(x => x.Member).Where(x => x.PersonelId == loginedPersonel.Id).ToListAsync();
                var Students = PersonelAppointments.Select(m => m.Member).ToList();

                var StudentList = _mapper.Map<List<MemberShowDTO>>(Students);
                Response.Success(StudentList, 200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(PersonelGetStudents)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<bool>> WriteNoteToStudent(WriteNotoToStudentDTO model, ModelStateDictionary ModelState, ClaimsPrincipal User)
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

                var loginedPersonel = await _userManager.GetUserAsync(User);
                var existAppintment = (await _appointmentRepository.GetByExperssion(x => x.PersonelId == loginedPersonel.Id && x.MemberId == model.MemberId)).FirstOrDefault();
                if (existAppintment != null)
                {
                    var newNote = _mapper.Map<TeacherNotesToStudent>(model);
                    newNote.PersonelId = loginedPersonel.Id;
                    await _teacherNotesRepository.AddAndCommit(newNote);
                    Response.Success(true, 201);
                }
                else
                {
                    Response.Error(400, "The agreement with the student has expired or student is not exist");
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(WriteNoteToStudent)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

    }
}
