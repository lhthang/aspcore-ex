using cinema_core.Models;
using cinema_core.Models.Base;
using cinema_core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories.Implements
{
    public class LabelRepository : ILabelRepository
    {
        private MyDbContext dbContext;
        public LabelRepository(MyDbContext context)
        {
            dbContext = context;
        }

        public bool CreateLabel(Label label)
        {
            dbContext.Add(label);
            return Save();
        }

        public bool DeleteLabel(Label label)
        {
            dbContext.Remove(label);
            return Save();
        }

        public Label GetLabelById(int id)
        {
            return dbContext.Labels.Where(l => l.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            return dbContext.SaveChanges() > 0;
        }
    }
}
