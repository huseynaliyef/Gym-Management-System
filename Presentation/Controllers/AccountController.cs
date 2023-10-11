using Logic.Models.DTOs.Admin;
using Logic.Models.DTOs.Shared;
using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Collections;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        private readonly IWebHostEnvironment _env;
        public AccountController(IAccountService AccountService, IWebHostEnvironment env)
        {
            _AccountService = AccountService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateTokenWithRefreshToken(string RefreshToken)
        {
            var Response = await _AccountService.GetJwtTokenWithRefreshToken(RefreshToken);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var Response = await _AccountService.Login(model,ModelState);
            return StatusCode(Response.StatusCode, Response);
                
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateInformation([FromForm]UpdateDTO model)
        {
            var Response = await _AccountService.UpdateInformation(model, User, _env, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [HttpPost]
        public async Task<IActionResult> RecoveryPassword([FromForm]RecoveryEmailDTO model)
        {
            var Response = await _AccountService.RecoveryPassword(model, ModelState);
            return StatusCode(Response.StatusCode, Response);
        }


        [HttpGet]
        public async Task<IActionResult> CheckToken(string Token)
        {
            var Response = await _AccountService.CheckToken(Token);
            return StatusCode(Response.StatusCode, Response);
        }


        [HttpPatch]
        public async Task<IActionResult> ChnagePassword(ChangePasswordDTO model)
        {
            var Response = await _AccountService.ChangePassword(model,ModelState);
            return StatusCode(Response.StatusCode, Response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowProfileInformation()
        {
            var Response = await _AccountService.ShowMyProfile(User, _env);
            return StatusCode(Response.StatusCode, Response);
        }

    }
}
