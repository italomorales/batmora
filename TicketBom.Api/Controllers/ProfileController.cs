using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketBom.Application.Services;
using TicketBom.Application.ViewModels;

namespace TicketBom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        //[Authorize(Roles = "PROFILE.VIEW")]
        public Task<List<ProfileViewModel>> Get()
        {
            return _profileService.GetAll();
        }

    }
}
