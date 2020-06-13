using cinema_core.DTOs.ScreenTypeDTOs;
using cinema_core.Models;
using cinema_core.Models.Base;
using cinema_core.Repositories.Interfaces;
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
        public bool CreateScreenType(ScreenType screenType)
        {
            dbContext.AddAsync(screenType);
            return Save();
        }

        public bool DeleteScreenType(ScreenType screenType)
        {
            dbContext.Remove(screenType);
            return Save();
        }

        public ScreenType GetScreenTypeById(int Id)
        {
            var screenType = dbContext.ScreenTypes.Where(sc => sc.Id == Id).FirstOrDefault();
            return screenType;
        }

        public ScreenType GetScreenTypeByName(string name)
        {
            var screenType = dbContext.ScreenTypes.Where(sc => sc.Name == name).FirstOrDefault();
            return screenType;
        }

        public ICollection<ScreenTypeDTO> GetScreenTypes()
        {
            List<ScreenTypeDTO> results = new List<ScreenTypeDTO>();
            var screenTypes = dbContext.ScreenTypes.OrderBy(sc => sc.Id).ToList();
            foreach (ScreenType screenType in screenTypes)
            {
                results.Add(new ScreenTypeDTO()
                {
                    Id = screenType.Id,
                    Name = screenType.Name,
                });
            }
            return results;
        }

        public bool Save()
        {
            var save = dbContext.SaveChanges();
            return save > 0;
        }

        public bool UpdateScreenType(ScreenType screenType)
        {
            dbContext.Update(screenType);
            return Save();
        }
    }
}
