using cinema_core.DTOs.LabelDTOs;
using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.DTOs.ScreenTypeDTOs
{
    public class ScreenTypeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public LabelDTO Label { get; set; }

        public ScreenTypeDTO(ScreenType screenType)
        {
            this.Id = screenType.Id;
            this.Name = screenType.Name;
            if (screenType.Label != null)
                this.Label = new LabelDTO(screenType.Label);
        }
    }
}
