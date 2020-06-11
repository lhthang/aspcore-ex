using cinema_core.DTOs.RoomDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories
{
    public interface IRoomRepository
    {
        ICollection<RoomDTO> GetAllRooms();
    }
}
