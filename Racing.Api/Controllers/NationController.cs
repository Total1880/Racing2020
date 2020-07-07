using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services;
using Racing.Model;
using System.Net;

namespace Racing.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NationController : ControllerBase
    {
        private readonly INationService _nationService;

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateNation(Nation nation)
        {
            if (_nationService.CreateNation(nation))
            {
                return Created(string.Empty, nation);
            }
            
            return BadRequest();
        }
    }
}
