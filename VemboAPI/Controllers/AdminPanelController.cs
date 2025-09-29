using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : ControllerBase
    {
        private readonly IAdminPanelService _adminPanelService;

        public AdminPanelController(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        [HttpGet("menu")]
        public IActionResult GetMenu()
        {
            var menu = _adminPanelService.BuildMenu();
            return Ok(menu);
        }
    }
}
