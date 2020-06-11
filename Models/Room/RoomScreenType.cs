using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models.Room
{
    public class RoomScreenType
    {
        public int RoomId { get; set; }
        public int ScreenTypeId { get; set; }
        public Room Room { get; set; }
        public ScreenType ScreenType { get; set; }
    }
}
