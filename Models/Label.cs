using cinema_core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models
{
    public class Label : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ScreenType> ScreenTypes { get; set; }
    }
}
