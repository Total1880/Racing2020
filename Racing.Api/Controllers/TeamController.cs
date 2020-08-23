using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System;
using System.Net;

namespace Racing.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateTeam(Team team)
        {
            if (_teamService.CreateTeam(team))
            {
                return Created(string.Empty, team);
            }

            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetTeams()
        {
            try
            {
                return Ok(_teamService.GetTeams());
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult EditTeam(Team team)
        {
            if (_teamService.EditTeam(team))
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
        public IActionResult DeleteTeam(int id)
        {
            if (_teamService.DeleteTeam(id))
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
