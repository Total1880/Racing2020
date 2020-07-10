using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Racing.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FirstNamesController : ControllerBase
    {
        private readonly IFirstNamesService _firstNamesService;

        public FirstNamesController(IFirstNamesService firstNamesService)
        {
            _firstNamesService = firstNamesService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateFirstNames(IList<FirstNames> firstNames)
        {
            if (_firstNamesService.CreateNames(firstNames))
            {
                return Created(string.Empty, firstNames);
            }

            return BadRequest();
        }
    }
}
