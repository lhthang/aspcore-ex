using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models.Room
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalSeatsPerRow { get; set; }

        public int TotalRows { get; set; }

        public virtual ICollection<RoomScreenType> RoomScreenTypes { get; set; }
    }
}
