using Microsoft.AspNetCore.Mvc;
using Racing.Api.Services.Interfaces;
using Racing.Model;
using System;
using System.Net;

namespace Racing.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateSetting(Setting setting)
        {
            if (_settingService.AddSetting(setting))
            {
                return Created(string.Empty, setting);
            }

            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetSettings()
        {
            try
            {
                return Ok(_settingService.GetSettings());
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult EditSetting(Setting setting)
        {
            if (_settingService.EditSetting(setting))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{description}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetSettingByDescription(string description)
        {
            try
            {
                return Ok(_settingService.GetSettingByDescription(description));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
