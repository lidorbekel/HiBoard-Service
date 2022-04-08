using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.Enums;
using HiBoard.Domain.Models;

namespace HiBoard.Persistence
{
    public class Seed
    {
        private readonly HiBoardDbContext _context;

        public Seed(HiBoardDbContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            if (!_context.Users.Any())
            {
                var users = new List<User>()
                {
                    new User("Israel@gmail.com")
                    {
                        FirstName = "Israel",
                        LastName = "Israeli",
                        CreationDate = DateTime.Now,
                        Role = UserRoles.Manager,
                        Department = UserDepartments.Developers,
                        IsDeleted = false
                    }
                };

                await _context.AddRangeAsync(users);
            }

            await _context.SaveChangesAsync();
        }
    }
}
