using cinema_core.DTOs.RoomDTOs;
using cinema_core.Form;
using cinema_core.Models;
using cinema_core.Models.Base;
using cinema_core.Repositories;
using cinema_core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories.Implements
{
    public class RoomRepository : IRoomRepository
    {
        private MyDbContext dbContext;

        public RoomRepository(MyDbContext context)
        {
            dbContext = context;
        }

        public Room CreateRoom(RoomRequest roomRequest)
        {
            var room = new Room()
            {
                Name = roomRequest.Name,
                TotalRows = roomRequest.TotalRows,
                TotalSeatsPerRow = roomRequest.TotalSeatsPerRow,
            };
            var screenTypes = dbContext.ScreenTypes.Where(s => roomRequest.ScreenTypeIds.Contains(s.Id)).ToList();
            foreach (var screen in screenTypes)
            {
                var roomScreenType = new RoomScreenType()
                {
                    ScreenType = screen,
                    Room = room,
                };
                dbContext.Add(roomScreenType);
            }
            dbContext.Add(room);
            var isSuccess = Save();
            if (!isSuccess) return null;
            return room;
        }

        public bool DeleteRoom(Room room)
        {
            dbContext.Remove(room);
            return Save();
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

        public Room GetRoomById(int id)
        {
            var room = dbContext.Rooms.Where(r => r.Id == id).Include(rs => rs.RoomScreenTypes).ThenInclude(s => s.ScreenType).FirstOrDefault();
            return room;
        }

        public bool Save()
        {
            return dbContext.SaveChanges() > 0;
        }

        public Room UpdateRoom(int id,RoomRequest roomRequest)
        {
            var room = dbContext.Rooms.Where(r => r.Id == id).FirstOrDefault();

            var screenTypesIsDelete = dbContext.RoomScreenTypes.Where(rs => rs.RoomId == id).ToList();

            if (screenTypesIsDelete != null)
                dbContext.RemoveRange(screenTypesIsDelete);

            room.Name = roomRequest.Name;
            room.TotalRows = roomRequest.TotalRows;
            room.TotalSeatsPerRow = roomRequest.TotalSeatsPerRow;

            var screenTypes = dbContext.ScreenTypes.Where(s => roomRequest.ScreenTypeIds.Contains(s.Id)).ToList();
            foreach (var screen in screenTypes)
            {
                var roomScreenType = new RoomScreenType()
                {
                    ScreenType = screen,
                    Room = room,
                };
                dbContext.Add(roomScreenType);
            }
            dbContext.Update(room);
            var isSuccess = Save();
            if (!isSuccess) return null;
            return room;
        }
    }
}
