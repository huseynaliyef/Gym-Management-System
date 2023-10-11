using AutoMapper;
using Data.Entities;
using Logic.Models.DTOs.Gym;
using Logic.Models.DTOs.Personel;
using Logic.Models.Response_Model;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Logic.Services.Implementations
{
    public class GymService:IGymService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IGenericRepository<Equipment> _equipmentRepository;
        private readonly IGenericRepository<Service> _serviceRepository;
        private readonly IGenericRepository<Package> _packageRepository;
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<Membership> _membershipRepository;

        public GymService(IMapper mapper,
            IGenericRepository<Branch> branchRepository,
            IGenericRepository<Equipment> equipmentRepository,
            IGenericRepository<Service> serviceRepository,
            IGenericRepository<Package> packageRepository,
            IGenericRepository<Member> memberRepository,
            IGenericRepository<Membership> membershipRepository)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _equipmentRepository = equipmentRepository;
            _serviceRepository = serviceRepository;
            _packageRepository = packageRepository;
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
        }
        public async Task<GenericResponse<bool>> AddBranch(BranchAddDTO model, ModelStateDictionary ModelState)
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

                var Branch = _mapper.Map<Branch>(model);
                await _branchRepository.AddAndCommit(Branch);
                Response.Success(true,200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(AddBranch)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> AddEquipment(EquipmentAddDTO model, ModelStateDictionary ModelState)
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

                var existEquipment = await _equipmentRepository.GetByExperssion(x => x.BranchId == model.BranchId && x.EquipmentName == model.EquipmentName && x.Description == model.Description);
                if (existEquipment.Any())
                {
                    existEquipment[0].count += model.count;
                    _equipmentRepository.Update(existEquipment[0]);
                    await _equipmentRepository.Commit();
                }
                else if (!existEquipment.Any())
                {
                    var newEquipment = _mapper.Map<Equipment>(model);
                    await _equipmentRepository.AddAndCommit(newEquipment);
                }
                Response.Success(true, 200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(AddEquipment)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> UpdateEquipment(EquipmentUpdateDTO model, ModelStateDictionary ModelState)
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

                var equipment = await _equipmentRepository.Find(model.EquipmentId);

                if (equipment == null)
                {
                    Response.Error(404, "Equipment is not fount");
                }
                else
                {
                    equipment = _mapper.Map(model, equipment);
                    await _equipmentRepository.Commit();
                    Response.Success(true, 200);
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(UpdateEquipment)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> UpdateBranch(BranchUpdateDTO model, ModelStateDictionary ModelState)
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

                var branch = await _branchRepository.Find(model.BranchId);

                if (branch == null)
                {
                    Response.Error(404, "Branch is not found");
                }
                else
                {
                    branch = _mapper.Map(model, branch);
                    await _equipmentRepository.Commit();
                    Response.Success(true, 200);
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(UpdateBranch)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }


        public async Task<GenericResponse<bool>> CreateService(ServiceAddDTO model, ModelStateDictionary ModelState)
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

                var existService = await _serviceRepository.GetByExperssion(x => x.ServiceName == model.ServiceName);
                if (existService.Any())
                {
                    Response.Error(400, "Service is already exist");
                }
                else
                {
                    var newService = _mapper.Map<Service>(model);
                    await _serviceRepository.AddAndCommit(newService);
                    Response.Success(true, 200);
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(CreateService)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> CreatePackage(PackageAddDTO model, ModelStateDictionary ModelState)
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

                var existPackage = await _packageRepository.GetByExperssion(x => x.PackageName == model.PackageName);
                if (existPackage.Any())
                {
                    Response.Error(400, "Service is already exist");
                }
                else
                {
                    var newPackage = _mapper.Map<Package>(model);
                    await _packageRepository.AddAndCommit(newPackage);
                    Response.Success(true, 200);
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(CreatePackage)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;

        }

        public async Task<GenericResponse<bool>> AddServiceToPackage(AddServiceToPackageDTO model, ModelStateDictionary ModelState)
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

                var existPackage = await _packageRepository.Find(model.PackageId);
                var existService = await _serviceRepository.Find(model.ServiceId);
                if (existPackage != null && existService != null)
                {
                    existPackage.Services ??= new List<Service>();
                    existPackage.Services.Add(existService);

                    await _packageRepository.Commit();
                    Response.Success(true, 200);
                }
                else
                    Response.Error(404, "Service or Package is not found");

                Log.Logger.Information($"{nameof(GymService)}.{nameof(AddServiceToPackage)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> UpdateService(ServiceUpdateDTO model, ModelStateDictionary ModelState)
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

                var existService = await _serviceRepository.Find(model.ServiceId);
                if (existService is not null)
                {
                    existService = _mapper.Map(model, existService);
                    await _serviceRepository.Commit();
                    Response.Success(true, 200);
                }
                else Response.Error(404, "Service is not found");
                Log.Logger.Information($"{nameof(GymService)}.{nameof(UpdateService)} mehod is executed");

            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> UpdatePackage(PackageUpdateDTO model, ModelStateDictionary ModelState)
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

                var existPackage = await _packageRepository.Find(model.PackageId);
                if (existPackage is not null)
                {
                    existPackage = _mapper.Map(model, existPackage);
                    await _packageRepository.Commit();
                    Response.Success(true, 200);
                }
                else Response.Error(404, "Package is not found");

                Log.Logger.Information($"{nameof(GymService)}.{nameof(UpdatePackage)} mehod is executed");
            }
            catch(Exception ex)
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
                var Personels = await _memberRepository.GetTableWithRelation(x => x.Functions).Where(x=>x.Functions.Count != 0).ToListAsync();
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

        public async Task<GenericResponse<List<BranchShowDTO>>> ShowBranches()
        {
            var Response = new GenericResponse<List<BranchShowDTO>>();
            try
            {
                var Branches = await _branchRepository.GetAll();
                var BrancheList = _mapper.Map<List<BranchShowDTO>>(Branches);
                Response.Success(BrancheList, 200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(ShowBranches)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<List<PackageShowDTO>>> ShowPackages()
        {
            var Response = new GenericResponse<List<PackageShowDTO>>();
            try
            {
                var Packages = await _packageRepository.GetAll();
                var PackageList = _mapper.Map<List<PackageShowDTO>>(Packages);
                Response.Success(PackageList, 200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(ShowPackages)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<List<ServiceShowDTO>>> ShowServices()
        {
            var Response = new GenericResponse<List<ServiceShowDTO>>();
            try
            {
                var Services = await _serviceRepository.GetAll();
                var ServiceList = _mapper.Map<List<ServiceShowDTO>>(Services);
                Response.Success(ServiceList, 200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(ShowServices)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<List<MembershipShowDTO>>> ShowMemberships()
        {
            var Response = new GenericResponse<List<MembershipShowDTO>>();
            try
            {
                var memberships = await _membershipRepository.GetTableWithRelation(x=>x.Package).ToListAsync();
                var membershipList = _mapper.Map<List<MembershipShowDTO>>(memberships);
                Response.Success(membershipList,200);
                Log.Logger.Information($"{nameof(GymService)}.{nameof(ShowMemberships)} mehod is executed");
            }
            catch(Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }
        public async Task<GenericResponse<bool>> CreateMembership(MembershipAddDTO model, ModelStateDictionary ModelState)
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

                var existMembership = (await _membershipRepository.GetByExperssion(x=>x.MembershipName == model.MembershipName)).FirstOrDefault();
                if (existMembership != null)
                {
                    Response.Error(400, "Membership is already exist.");
                }
                else
                {
                    var newMembership = _mapper.Map<Membership>(model);
                    await _membershipRepository.AddAndCommit(newMembership);
                    Response.Success(true, 200);
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(CreateMembership)} mehod is executed");
            }
            catch (Exception ex)
            {
                Response.InternalError(ex.Message);
                Log.Logger.Error(ex, ex.Message);
            }
            return Response;
        }

        public async Task<GenericResponse<bool>> UpdateMembership(MembershipUpdateDTO model, ModelStateDictionary ModelState)
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

                var existMembership = await _membershipRepository.Find(model.Id);

                if(existMembership == null) Response.Error(404, "Membership is not found.");
                
                else
                {
                    existMembership = _mapper.Map(model, existMembership);
                    await _membershipRepository.Commit();
                    Response.Success(true, 200);
                }
                Log.Logger.Information($"{nameof(GymService)}.{nameof(UpdateMembership)} mehod is executed");
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
