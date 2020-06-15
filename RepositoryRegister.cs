using cinema_core.Repositories.Implements;
using cinema_core.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core
{
    public static class RepositoryRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IScreenTypeRepository, ScreenTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
        }
    }
}
