using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cinema_core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cinema_core.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository roomRepository;

        public RoomsController(IRoomRepository repository)
        {
            roomRepository = repository;
        }
        // GET: api/rooms
        [HttpGet]
        public IActionResult Get()
        {
            var rooms = roomRepository.GetAllRooms();
            return Ok(rooms);
        }
    }
}