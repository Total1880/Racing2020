using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System;
using System.Net;

namespace Racing.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RaceController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateRace(Race race)
        {
            if (_raceService.CreateRace(race))
            {
                return Created(string.Empty, race);
            }

            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetRaces()
        {
            try
            {
                return Ok(_raceService.GetRaces());
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult EditRace(Race race)
        {
            if (_raceService.EditRace(race))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteRace(int id)
        {
            if (_raceService.DeleteRace(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
