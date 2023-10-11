using AutoMapper;
using Data.Entities;
using Logic.Models.DTOs.Admin;
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
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Implementations
{
    public class AdminService: IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<Membership> _membershipRepository;
        private readonly IGenericRepository<Payment> _paymentRepository;
        private readonly IGenericRepository<MemberMembership> _memberMembershipRepository;
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminService(IMapper mapper,
            IGenericRepository<Member> memberRepository,
            IGenericRepository<Membership> membershipRepository,
            IGenericRepository<Payment> paymentRepository,
            IGenericRepository<MemberMembership> memberMembershipRepository,
            IGenericRepository<Appointment> appointmentRepository,
            UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _paymentRepository = paymentRepository;
            _memberMembershipRepository = memberMembershipRepository;
            _appointmentRepository = appointmentRepository;
            _userManager = userManager;
        }

        public async Task<GenericResponse<List<MemberShowDTO>>> ShowMembers()
        {
            var Response = new GenericResponse<List<MemberShowDTO>>();
            try
            {
                var existMembers = await _memberRepository.GetTableWithRelation(x => x.MemberMemberships).Where(x => x.MemberMemberships.Where(x => x.IsActive == true).Any()).ToListAsync();
                var Members = _mapper.Map<List<MemberShowDTO>>(existMembers);
                Response.Success(Members, 200);

                Log.Logger.Information($"{nameof(GymService)}.{nameof(ShowMembers)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<List<PersonelShowDTO>>> ShowPersonels()
        {
            var Response = new GenericResponse<List<PersonelShowDTO>>();
            try
            {
                var Personels = await _memberRepository.GetTableWithRelation(x => x.Functions).Where(x => x.Functions.Count != 0).ToListAsync();
                var PersonelList = _mapper.Map<List<PersonelShowDTO>>(Personels);
                Response.Success(PersonelList, 200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(ShowPersonels)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> PaymentForMembership(MemberPaymentDTO model, ModelStateDictionary ModelState)
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

                var existMember = (await _memberRepository.GetByExperssion(x => x.Id == model.MemberId)).FirstOrDefault();
                var existMembership = (await _membershipRepository.GetTableWithRelation(x => x.Package).Where(x => x.Id == model.MembershipId).ToListAsync()).FirstOrDefault();
                var existMemberMembership = (await _memberMembershipRepository.GetByExperssion(x => x.MemberId == existMember.Id && x.MembershipId == existMembership.Id)).FirstOrDefault();
                if (existMember != null && existMembership != null)
                {
                    var payment = _mapper.Map<Payment>(model);
                    await _paymentRepository.AddAndCommit(payment);

                    var membermembership = _mapper.Map<MemberMembership>(model);
                    membermembership.StartDate = model.PaymentDate;
                    membermembership.EndDate = model.PaymentDate.AddMonths(existMembership.Package.Duration);
                    membermembership.IsActive = true;
                    await _memberMembershipRepository.AddAndCommit(membermembership);

                    Response.Success(true, 200);
                }
                else
                {
                    Response.Error(404, "Member or Membership is not found");
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(PaymentForMembership)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<bool>> RemoveUser(RemoveUserDTO model, ModelStateDictionary ModelState)
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

                var existUser = await _userManager.FindByNameAsync(model.UserName);
                if (existUser == null)
                {
                    Response.Error(404, "This user is not found.");
                }
                else
                {
                    await _userManager.DeleteAsync(existUser);
                    Response.Success(true, 200);
                }
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> TerminateMemberMembership(TerminateMemberMembershipDTO model)
        {
            var Response = new GenericResponse<bool>();
            try
            {
                var existMemberMemberships = await _memberMembershipRepository.GetByExperssion(x => x.MemberId == model.MemberId && x.IsActive == true);
                var existApointments = await _appointmentRepository.GetByExperssion(x => x.MemberId == model.MemberId);
                if (!existMemberMemberships.Any())
                {
                    Response.Error(404, "This user does not have an active membership!");
                }
                else
                {
                    foreach (var membermembership in existMemberMemberships)
                    {
                        membermembership.IsActive = false;
                    }
                    
                    if (existApointments.Any())
                    {
                        foreach(var apointment in existApointments)
                        {
                            _appointmentRepository.Delete(apointment);
                        }
                        await _appointmentRepository.Commit();
                    }
                    await _memberMembershipRepository.Commit();
                    Response.Success(true, 200);
                }
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
            }
            return Response;
        }
    }
}
