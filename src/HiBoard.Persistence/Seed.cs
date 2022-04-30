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

            var users = new List<User>
            {
                new("Israel@gmail.com")
                {
                    FirstName = "Israel",
                    LastName = "Israeli",
                    CreationDate = DateTime.Now,
                    Role = UserRole.Manager,
                    Department = UserDepartments.Developers,
                    IsDeleted = false
                },
                new("ido@lumigo.io")
                {
                    FirstName = "Ido",
                    LastName = "Golan",
                    CreationDate = DateTime.Now,
                    Role = UserRole.Manager,
                    Department = UserDepartments.Developers,
                    IsDeleted = false
                }
            };

            foreach (var user in users.Where(user => !_context.Users.Any(x=> x.Email == user.Email)))
            {
                await _context.Users.AddAsync(user);
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
