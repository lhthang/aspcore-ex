using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Form;
using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories.Interfaces
{
    public interface IScreenTypeRepository
    {
        ICollection<ScreenTypeDTO> GetScreenTypes();
        ScreenType GetScreenTypeById(int Id);
        ScreenType GetScreenTypeByName(string name);

        ScreenType CreateScreenType(ScreenTypeRequest screenTypeRequest);
        ScreenType UpdateScreenType(int id, ScreenTypeRequest screenTypeRequest);
        bool DeleteScreenType(ScreenType screenType);
        bool Save();
    }
}
