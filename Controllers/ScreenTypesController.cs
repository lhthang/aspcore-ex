using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Models;
using cinema_core.Repositories;
using cinema_core.Repositories.Interfaces;
using cinema_core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cinema_core.Controllers
{
    [Route("api/screen-types")]
    [ApiController]
    public class ScreenTypesController : Controller
    {
        private IScreenTypeRepository screenTypeRepository;

        public ScreenTypesController(IScreenTypeRepository repository)
        {
            screenTypeRepository = repository;
        }
        // GET: api/screen-types
        [HttpGet]
        //[Authorize(Policy =Policies.Admin)]
        [Authorize(Roles =Authorize.Admin)]
        public IActionResult Get()
        {
            var screenTypes = screenTypeRepository.GetScreenTypes();
            
            return Ok(screenTypes);
        }

        // GET: api/screen-types/5
        [HttpGet("{id}", Name = "GetScreenType")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var screenType = screenTypeRepository.GetScreenTypeById(id);
            if (screenType == null)
                return NotFound();
            return Ok(screenType);
        }

        // POST: api/screen-types
        [HttpPost]
        public IActionResult Post([FromBody] ScreenType screenType)
        {
            if (screenType == null) return StatusCode(400, ModelState);

            var isExist = screenTypeRepository.GetScreenTypeByName(screenType.Name);

            if (isExist != null)
            {
                ModelState.AddModelError("", $"Screen type {screenType.Name} is exist");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            if (!screenTypeRepository.CreateScreenType(screenType))
            {
                ModelState.AddModelError("", "Something went wrong when save screen type");
                return StatusCode(400, ModelState);
            }
            return CreatedAtRoute("GetScreenType", new { id = screenType.Id }, screenType);
        }

        // PUT: api/screen-types/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ScreenType screenType)
        {
            var isExist = screenTypeRepository.GetScreenTypeById(id);
            if (isExist == null) return NotFound();

            if (screenType == null) return StatusCode(400, ModelState);

            var isDuplicate = screenTypeRepository.GetScreenTypeByName(screenType.Name);

            if (isDuplicate != null)
            {
                ModelState.AddModelError("", $"Screen type {screenType.Name} is exist");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            if (!screenTypeRepository.UpdateScreenType(screenType))
            {
                ModelState.AddModelError("", "Something went wrong when update screen type");
                return StatusCode(400, ModelState);
            }
            return CreatedAtRoute("GetScreenType", new { id = screenType.Id }, screenType);
        }

        // DELETE: api/screen-types/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isExist = screenTypeRepository.GetScreenTypeById(id);
            if (isExist == null) return NotFound();

            if (!screenTypeRepository.DeleteScreenType(isExist))
            {
                ModelState.AddModelError("", "Something went wrong when delete screen type");
                return StatusCode(400, ModelState);
            }
            return Ok(isExist);
        }
    }
}
