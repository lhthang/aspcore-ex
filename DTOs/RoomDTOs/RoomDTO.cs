using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.DTOs.RoomDTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalSeatsPerRow { get; set; }

        public int TotalRows { get; set; }

        public int TotalSeats { get; set; }

        public List<ScreenTypeDTO> ScreenTypes { get; set; }

        public RoomDTO(Room room)
        {
            List<ScreenTypeDTO> list = new List<ScreenTypeDTO>();
            this.Id = room.Id;
            this.Name = room.Name;
            this.TotalRows = room.TotalRows;
            this.TotalSeatsPerRow = room.TotalSeatsPerRow;
            this.TotalSeats = this.TotalSeatsPerRow * this.TotalRows;
            if (room.RoomScreenTypes != null)
            {
                foreach (var roomScreenType in room.RoomScreenTypes)
                {
                    var screenType = new ScreenTypeDTO(roomScreenType.ScreenType);
                    list.Add(screenType);
                }
            }
            this.ScreenTypes = list;
        }
    }
}
