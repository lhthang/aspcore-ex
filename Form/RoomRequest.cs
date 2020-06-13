using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Form
{
    public class RoomRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int TotalSeatsPerRow { get; set; }

        [Required]
        public int TotalRows { get; set; }

        public List<int> ScreenTypeIds { get; set; }
    }
}
