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
            var isExist = labelRepository.GetLabelById(id);
            if (isExist == null) return NotFound();
            return Ok(new LabelDTO(isExist));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isExist = labelRepository.GetLabelById(id);
            if (isExist == null) return NotFound();

            if (!labelRepository.DeleteLabel(isExist))
            {
                ModelState.AddModelError("", "Something went wrong when delete label");
                return StatusCode(400, ModelState);
            }
            return Ok(new LabelDTO(isExist));
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Label label)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState);

            if (!labelRepository.CreateLabel(label))
            {
                ModelState.AddModelError("", "Something went wrong when delete label");
                return StatusCode(400, ModelState);
            }
            return RedirectToRoute("GetLabel",new { id = label.Id });
        }
    }
}