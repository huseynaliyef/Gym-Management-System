using Logic.Models.DTOs.Admin;
using Logic.Models.DTOs.Gym;
using Logic.Models.DTOs.Member;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        private readonly IAdminService _adminService;
        public AdminController(IAccountService AccountService,
                               IAdminService adminService)
        {
            _AccountService = AccountService;
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAdmin()
        {
            var Response = await _AccountService.CreateAdmin();
            return StatusCode(Response.StatusCode, Response);
        }


        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var Response = await _adminService.ShowMembers();
            return StatusCode(Response.StatusCode, Response);
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonels()
        {
            var Response = await _adminService.ShowPersonels();
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPost]
        public async Task<IActionResult> PayForMembership(MemberPaymentDTO model)
        {
            var Response = await _adminService.PaymentForMembership(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPatch]
        public async Task<IActionResult> TerminateMembershipOfMember(TerminateMemberMembershipDTO model)
        {
            var Response = await _adminService.TerminateMemberMembership(model);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(RemoveUserDTO model)
        {
            var Response = await _adminService.RemoveUser(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }
    }
}
