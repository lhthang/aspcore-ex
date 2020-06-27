using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cinema_core.DTOs.LabelDTOs;
using cinema_core.Models;
using cinema_core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cinema_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : Controller
    {
        private ILabelRepository labelRepository;
        public LabelsController(ILabelRepository repository)
        {
            this.labelRepository = repository;
        }

        [HttpGet("{id}",Name ="GetLabel")]
        public IActionResult Get(int id)
        {
            try
            {
                var isExist = labelRepository.GetLabelById(id);
                return Ok(new LabelDTO(isExist));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var isExist = labelRepository.GetLabelById(id);

                if (!labelRepository.DeleteLabel(isExist))
                {
                    ModelState.AddModelError("", "Something went wrong when delete label");
                    return StatusCode(400, ModelState);
                }
                return Ok(new LabelDTO(isExist));
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Label label)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(400, ModelState);

                if (!labelRepository.CreateLabel(label))
                {
                    ModelState.AddModelError("", "Something went wrong when delete label");
                    return StatusCode(400, ModelState);
                }
                return RedirectToRoute("GetLabel", new { id = label.Id });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}