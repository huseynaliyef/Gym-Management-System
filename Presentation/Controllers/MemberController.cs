using Logic.Models.DTOs.Member;
using Logic.Models.DTOs.Personel;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _env;
        public MemberController(IAccountService AccountService, IMemberService memberService, IWebHostEnvironment env)
        {
            _AccountService = AccountService;
            _memberService = memberService;
            _env = env;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Resgister([FromForm]MemberRegisterDTO model)
        {
           
            var Response = await _AccountService.RegisterMember(model, _env,ModelState);
            return StatusCode(Response.StatusCode, Response);
            
        }

        [HttpPost]
        public async Task<IActionResult> SignInAppointment(AppointmentAddDTO model)
        {
            var Response = await _memberService.AddAppointment(model, User, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }


        [HttpDelete]
        public async Task<IActionResult> DisableYourAppointment()
        {
            var Response = await _memberService.DisableAppointment(User);
            return StatusCode(Response.StatusCode, Response);
        }


        [HttpGet]
        public async Task<IActionResult> ReadTeacherNotes()
        {
            var Response = await _memberService.ReadTeacherNotes(User);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMyAppointMents()
        {
            var Response = await _memberService.ShowAppointments(User);
            return StatusCode(Response.StatusCode, Response);
        }
    }
}
