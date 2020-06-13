using cinema_core.Models;
using cinema_core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models
{
    public class ScreenType : BaseEntity
    {

        public string Name { get; set; }

        public virtual ICollection<RoomScreenType> RoomScreenTypes { get; set; }
    }
}
