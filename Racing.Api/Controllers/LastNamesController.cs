using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System.Collections.Generic;
using System.Net;

namespace Racing.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LastNamesController : ControllerBase
    {
        private readonly ILastNamesService _lastNamesService;

        public LastNamesController(ILastNamesService lastNamesService)
        {
            _lastNamesService = lastNamesService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateLastNames(IList<LastNames> lastNames)
        {
            if (_lastNamesService.CreateNames(lastNames))
            {
                return Created(string.Empty, lastNames);
            }

            return BadRequest();
        }
    }
}
