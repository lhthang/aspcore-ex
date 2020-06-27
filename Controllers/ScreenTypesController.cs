using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Form;
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
        private ILabelRepository labelRepository;

        public ScreenTypesController(IScreenTypeRepository repository,ILabelRepository labelRepository)
        {
            screenTypeRepository = repository;
            this.labelRepository = labelRepository;
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
            try
            {
                var screenType = screenTypeRepository.GetScreenTypeById(id);
                return Ok(new ScreenTypeDTO(screenType));
            }
            catch(Exception e)
            {
                throw e;
            };
        }

        // POST: api/screen-types
        [HttpPost]
        public IActionResult Post([FromBody] ScreenTypeRequest screenType)
        {
            try
            {
                if (screenType == null) return StatusCode(400, ModelState);

                var isExist = screenTypeRepository.GetScreenTypeByName(screenType.Name);

                if (!ModelState.IsValid)
                    return StatusCode(400, ModelState);

                var result = screenTypeRepository.CreateScreenType(screenType);
                if (result == null)
                {
                    ModelState.AddModelError("", "Something went wrong when save screen type");
                    return StatusCode(400, ModelState);
                }
                return RedirectToRoute("GetScreenType", new { id = result.Id });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // PUT: api/screen-types/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ScreenTypeRequest screenType)
        {
            try
            {
                var isExist = screenTypeRepository.GetScreenTypeById(id);

                if (screenType == null) return StatusCode(400, ModelState);


                if (!ModelState.IsValid)
                    return StatusCode(400, ModelState);

                var result = screenTypeRepository.UpdateScreenType(id, screenType);
                if (result == null)
                {
                    ModelState.AddModelError("", "Something went wrong when update screen type");
                    return StatusCode(400, ModelState);
                }
                return RedirectToRoute("GetScreenType", new { id = result.Id });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        // DELETE: api/screen-types/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var isExist = screenTypeRepository.GetScreenTypeById(id);

                if (!screenTypeRepository.DeleteScreenType(isExist))
                {
                    ModelState.AddModelError("", "Something went wrong when delete screen type");
                    return StatusCode(400, ModelState);
                }
                return Ok(isExist);
            }
            catch(Exception e)
            {
                throw e;
            };
        }
    }
}
