using AutoMapper;
using Data.Entities;
using Logic.Models.DTOs.Gym;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.DTOs.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Mapper
{
    public class MapperProfiler:Profile
    {
        public MapperProfiler()
        {
            CreateMap<BranchAddDTO, Branch>().ReverseMap();
            CreateMap<EquipmentAddDTO, Equipment>().ReverseMap();
            CreateMap<BranchUpdateDTO, Branch>().ReverseMap();  
            CreateMap<EquipmentUpdateDTO, Equipment>().ReverseMap();  
            CreateMap<ServiceAddDTO, Service>().ReverseMap();
            CreateMap<ServiceUpdateDTO, Service>().ReverseMap();
            CreateMap<PackageAddDTO, Package>().ReverseMap();
            CreateMap<PackageUpdateDTO, Package>().ReverseMap();
            CreateMap<MemberShowDTO, Member>().ReverseMap();
            CreateMap<Member, PersonelShowDTO>().ReverseMap();
            CreateMap<Branch, BranchShowDTO>().ReverseMap();
            CreateMap<Package,  PackageShowDTO>().ReverseMap();
            CreateMap<Service, ServiceShowDTO>().ReverseMap();
            CreateMap<MembershipAddDTO, Membership>().ReverseMap();
            CreateMap<MembershipUpdateDTO, Membership>().ReverseMap();
            CreateMap<MemberPaymentDTO, MemberMembership>().ReverseMap();
            CreateMap<MemberPaymentDTO, Payment>().ReverseMap();
            CreateMap<Membership, MembershipShowDTO>().ReverseMap();
            CreateMap<Member, ShowProfileDTO>().ReverseMap();
            CreateMap<PersonelAddFunctionDTO, PersonelFunctions>().ReverseMap();
            CreateMap<AppointmentAddDTO, Appointment>().ReverseMap();
            CreateMap<WriteNotoToStudentDTO, TeacherNotesToStudent>().ReverseMap();
            CreateMap<TeacherNotesToStudent, ReadTeacherNotesDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentShowDTO>().ReverseMap();
        }
    }
}
