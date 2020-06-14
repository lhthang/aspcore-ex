using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cinema_core.DTOs.RoomDTOs;
using cinema_core.Form;
using cinema_core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cinema_core.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository roomRepository;

        private IScreenTypeRepository screenTypeRepository;

        public RoomsController(IRoomRepository repository, IScreenTypeRepository screenTypeRepository)
        {
            roomRepository = repository;
            this.screenTypeRepository = screenTypeRepository;
        }

        // GET: api/rooms
        [HttpGet]
        public IActionResult Get()
        {
            var rooms = roomRepository.GetAllRooms();
            return Ok(rooms);
        }

        // GET: api/rooms/5
        [HttpGet("{id}", Name = "GetRoom")]
        public IActionResult Get(int id)
        {
            var room = roomRepository.GetRoomById(id);
            if (room == null)
                return NotFound();
            var roomDTO = new RoomDTO(room);
            return Ok(roomDTO);
        }

        // POST: api/rooms
        [HttpPost]
        public IActionResult Post([FromBody] RoomRequest roomRequest)
        {
            if (roomRequest == null) return StatusCode(400, ModelState);

            var statusCode = ValidateRoom(roomRequest);


            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            var room = roomRepository.CreateRoom(roomRequest);
            if (room == null)
            {
                ModelState.AddModelError("", "Something went wrong when save room");
                return StatusCode(400, ModelState);
            }
            return RedirectToRoute("GetRoom", new { id = room.Id });
        }

        // POST: api/rooms
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoomRequest roomRequest)
        {
            //if (roomRepository.GetRoomById(id) == null) return NotFound();

            if (roomRequest == null) return StatusCode(400, ModelState);

            var statusCode = ValidateRoom(roomRequest);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);

            var room = roomRepository.UpdateRoom(id, roomRequest);
            if (room == null)
            {
                ModelState.AddModelError("", "Something went wrong when save room");
                return StatusCode(400, ModelState);
            }
            return RedirectToRoute("GetRoom", new { id = id });
        }

        // DELETE: api/room/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isExist = roomRepository.GetRoomById(id);
            if (isExist == null) return NotFound();

            if (!roomRepository.DeleteRoom(isExist))
            {
                ModelState.AddModelError("", "Something went wrong when delete room");
                return StatusCode(400, ModelState);
            }
            return Ok(isExist);
        }

        private StatusCodeResult ValidateRoom(RoomRequest roomRequest)
        {
            if (roomRequest == null || !ModelState.IsValid) return BadRequest();

            foreach (var id in roomRequest.ScreenTypeIds)
            {
                if (screenTypeRepository.GetScreenTypeById(id) == null)
                {
                    ModelState.AddModelError("", $"Id {id} not found");
                    return StatusCode(404);
                }
            }
            return NoContent();
        }
    }
}