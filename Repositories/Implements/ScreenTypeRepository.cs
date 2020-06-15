using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Form;
using cinema_core.Models;
using cinema_core.Models.Base;
using cinema_core.Repositories.Interfaces;
using cinema_core.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories.Implements
{
    public class ScreenTypeRepository : IScreenTypeRepository
    {
        private MyDbContext dbContext;

        public ScreenTypeRepository(MyDbContext context)
        {
            dbContext = context;
        }
        public ScreenType CreateScreenType(ScreenTypeRequest screenTypeRequest)
        {
            var label = dbContext.Labels.Where(l => l.Id == screenTypeRequest.LabelId).FirstOrDefault();
            var screenType = new ScreenType()
            {
                Name = screenTypeRequest.Name,
                Label=label,
            };
            dbContext.Add(screenType);
            var isSuccess=Save();
            if (!isSuccess) return null;
            return screenType;
        }

        public bool DeleteScreenType(ScreenType screenType)
        {
            dbContext.Remove(screenType);
            return Save();
        }

        public ScreenType GetScreenTypeById(int Id)
        {
            var screenType = dbContext.ScreenTypes.Include(l => l.Label).Where(sc => sc.Id == Id).FirstOrDefault();
            return screenType;
        }

        public ScreenType GetScreenTypeByName(string name)
        {
            var screenType = dbContext.ScreenTypes.Include(l => l.Label).Where(sc => sc.Name == name).FirstOrDefault();
            return screenType;
        }

        public ICollection<ScreenTypeDTO> GetScreenTypes()
        {
            List<ScreenTypeDTO> results = new List<ScreenTypeDTO>();
            var screenTypes = dbContext.ScreenTypes.Include(l=>l.Label).OrderBy(sc => sc.Id).ToList();
            foreach (ScreenType screenType in screenTypes)
            {
                results.Add(new ScreenTypeDTO(screenType));
            }
            return results;
        }

        public bool Save()
        {
            var save = dbContext.SaveChanges();
            return save > 0;
        }

        public ScreenType UpdateScreenType(int id,ScreenTypeRequest screenTypeRequest)
        {
            var label = dbContext.Labels.Where(l => l.Id == screenTypeRequest.LabelId).FirstOrDefault();

            var screenType = dbContext.ScreenTypes.Where(s => s.Id == id).FirstOrDefault();

            screenType.Label = label;
            Coppier<ScreenTypeRequest, ScreenType>.Copy(screenTypeRequest, screenType);

            dbContext.Update(screenType);
            var isSuccess = Save();
            if (!isSuccess) return null;
            return screenType;

        }
    }
}
