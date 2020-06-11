using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories
{
    public interface IScreenTypeRepository
    {
        ICollection<ScreenTypeDTO> GetScreenTypes();
        ScreenType GetScreenTypeById(int Id);
        ScreenType GetScreenTypeByName(string name);

        bool CreateScreenType(ScreenType screenType);
        bool UpdateScreenType(ScreenType screenType);
        bool DeleteScreenType(ScreenType screenType);
        bool Save();
    }
}
