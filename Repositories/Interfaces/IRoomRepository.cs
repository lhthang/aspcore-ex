using cinema_core.DTOs.RoomDTOs;
using cinema_core.Form;
using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        ICollection<RoomDTO> GetAllRooms();

        Room GetRoomById(int id);

        Room CreateRoom(RoomRequest roomRequest);

        Room UpdateRoom(int id,RoomRequest roomRequest);

        bool DeleteRoom(Room room);

        bool Save();
    }
}
