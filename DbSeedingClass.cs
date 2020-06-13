using cinema_core.Models;
using cinema_core.Models.Base;
using cinema_core.Utils.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core
{
    public static class DbSeedingClass
    {
        public static void SeedDataContext(this MyDbContext context)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            var user = new User()
            {
                Username = "cinema-admin",
                FullName = "Cinema Admin",
                Password = passwordHasher.HashPassword("admin@123456"),
                Email = "adm.cinex@gmail.com",
                UserRoles = new List<UserRole>()
                {
                   new UserRole()
                   {
                        Role = new Role()
                        {
                            Name = "Admin",
                        }
                   },
                   new UserRole()
                   {
                        Role = new Role()
                        {
                            Name = "User",
                        }
                   },
                },
            };
            if (context.Users.Any(u => u.Username == "cinema-admin") == false)
            {
                context.AddRange(user);
                context.SaveChanges();
            }

        }
    }
}
