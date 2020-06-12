using cinema_core.DTOs.RoomDTOs;
using cinema_core.Form.Room;
using cinema_core.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories
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
