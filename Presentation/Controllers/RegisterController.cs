using Logic.Models.DTOs.Gym;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IGymService _gymService;
        public RegisterController(IGymService gymService)
        {
            _gymService = gymService;
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchAddDTO model)
        {
            var Response = await _gymService.AddBranch(model,ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEqipment(EquipmentAddDTO model)
        {
            var Response = await _gymService.AddEquipment(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBranch(BranchUpdateDTO model)
        {
            var Response = await _gymService.UpdateBranch(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEquipment(EquipmentUpdateDTO model)
        {
            var Response = await _gymService.UpdateEquipment(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceAddDTO model)
        {
            var Response = await _gymService.CreateService(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage(PackageAddDTO model)
        {
            var Response = await _gymService.CreatePackage(model,ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceToPackage(AddServiceToPackageDTO model)
        {
            var Response = await _gymService.AddServiceToPackage(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(ServiceUpdateDTO model)
        {
            var Response = await _gymService.UpdateService(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePackage(PackageUpdateDTO model)
        {
            var Response = await _gymService.UpdatePackage(model, ModelState);
            return StatusCode(Response.StatusCode, Response);

        }

        [HttpPost]
        public async Task<IActionResult> CreateMebership(MembershipAddDTO model)
        {
            var Response = await _gymService.CreateMembership(model,ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMembership(MembershipUpdateDTO model)
        {
            var Response = await _gymService.UpdateMembership(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }
    }
}
