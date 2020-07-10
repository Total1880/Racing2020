using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System;
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

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GenerateFirstNames(int numberOfNames)
        {
            try
            {
                return Ok(_firstNamesService.GenerateFirstNames(numberOfNames));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
