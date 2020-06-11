using cinema_core.DTOs.RoomDTOs;
using cinema_core.Models.Room;
using cinema_core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Services.RoomSV
{
    public class RoomsService : IRoomRepository
    {
        private MyDbContext dbContext;

        public RoomsService(MyDbContext context)
        {
            dbContext = context;
        }

        public ICollection<RoomDTO> GetAllRooms()
        {
            List<RoomDTO> results = new List<RoomDTO>();
            List<Room> rooms = dbContext.Rooms.Include(rs => rs.RoomScreenTypes).ThenInclude(s => s.ScreenType).OrderBy(r => r.Id).ToList();
            foreach (Room room in rooms)
            {
                //System.Diagnostics.Debug.WriteLine();
                results.Add(new RoomDTO(room));
            }
            return results;
        }
    }
}
