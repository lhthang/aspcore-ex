using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.DTOs.LabelDTOs
{
    public class LabelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public LabelDTO(Label label)
        {
            this.Id = label.Id;
            this.Name = label.Name;
        }
    }
}
