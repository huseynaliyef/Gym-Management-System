using Logic.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;
        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonels()
        {
            var Response = await _gymService.ShowPersonels();
            return StatusCode(Response.StatusCode, Response);
        }
        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {
            var Response = await _gymService.ShowBranches();
            return StatusCode(Response.StatusCode, Response);
        }
        [HttpGet]
        public async Task<IActionResult> GetPackages()
        {
            var Response = await _gymService.ShowPackages();
            return StatusCode(Response.StatusCode, Response);
        }
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var Response = await _gymService.ShowServices();
            return StatusCode(Response.StatusCode, Response);
        }
        [HttpGet]
        public async Task<IActionResult> GetMemberships()
        {
            var Response = await _gymService.ShowMemberships();
            return StatusCode(Response.StatusCode, Response);
        }
    }
}
