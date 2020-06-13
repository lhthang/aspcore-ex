using cinema_core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models
{
    public class Room : BaseEntity
    {

        public string Name { get; set; }

        public int TotalSeatsPerRow { get; set; }

        public int TotalRows { get; set; }

        public virtual ICollection<RoomScreenType> RoomScreenTypes { get; set; }
    }
}
