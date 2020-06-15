using cinema_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Repositories.Interfaces
{
    public interface ILabelRepository
    {
        public bool DeleteLabel(Label label);
        public bool Save();
        public Label GetLabelById(int id);
        public bool CreateLabel(Label label);
    }
}
