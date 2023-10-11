using Data.Entities;
using Logic.Models.DTOs.Gym;
using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Models.DTOs.Shared;
using Logic.Models.Response_Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Abstractions
{
    public interface IGymService
    {
        Task<GenericResponse<bool>> AddBranch(BranchAddDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> AddEquipment(EquipmentAddDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> UpdateEquipment(EquipmentUpdateDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> UpdateBranch(BranchUpdateDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> CreateService(ServiceAddDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> CreatePackage(PackageAddDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> AddServiceToPackage(AddServiceToPackageDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> UpdateService(ServiceUpdateDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> UpdatePackage(PackageUpdateDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<List<PersonelShowDTO>>> ShowPersonels();
        Task<GenericResponse<List<BranchShowDTO>>> ShowBranches();
        Task<GenericResponse<List<PackageShowDTO>>> ShowPackages();
        Task<GenericResponse<List<ServiceShowDTO>>> ShowServices();
        Task<GenericResponse<List<MembershipShowDTO>>> ShowMemberships();
        Task<GenericResponse<bool>> CreateMembership(MembershipAddDTO model, ModelStateDictionary ModelState);
        Task<GenericResponse<bool>> UpdateMembership(MembershipUpdateDTO model, ModelStateDictionary ModelState);
    }
}
