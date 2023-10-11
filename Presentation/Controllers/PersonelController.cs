using Logic.Models.DTOs.Personel;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        private readonly IPersonelService _personelService;
        private readonly IWebHostEnvironment _env;
        public PersonelController(IAccountService AccountService, IWebHostEnvironment env, IPersonelService personelService)
        {
            _AccountService = AccountService;
            _personelService = personelService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]PersonelRegisterDTO model)
        {
            var Response = await _AccountService.RegisterPersonel(model, _env, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [Authorize(Roles ="Personel")]
        [HttpPost]
        public async Task<IActionResult> AddYourFunction(PersonelAddFunctionDTO model)
        {
            var Response = await _personelService.AddYourFunction(model, User, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [Authorize(Roles ="Personel")]
        [HttpGet]
        public async Task<IActionResult> GetMyStudents()
        {
            var Response = await _personelService.PersonelGetStudents(User);
            return StatusCode(Response.StatusCode, Response);
        }

        [Authorize(Roles = "Personel")]
        [HttpPost]
        public async Task<IActionResult> WriteNoteToStudent(WriteNotoToStudentDTO model)
        {
            var Response = await _personelService.WriteNoteToStudent(model, ModelState, User);
            return StatusCode(Response.StatusCode, Response);
        }
    }
}
