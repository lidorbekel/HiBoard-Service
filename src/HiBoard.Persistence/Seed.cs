using HiBoard.Domain.Enums;
using HiBoard.Domain.Models;

namespace HiBoard.Persistence;

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
                Role = UserRole.Manager,
                Department = "Unspecified",
                IsDeleted = false,
                CompanyId = 1
            },
            new("ido@lumigo.io")
            {
                FirstName = "Ido",
                LastName = "Golan",
                UpdatedAt = DateTime.Now,
                Role = UserRole.Manager,
                Department = "Unspecified",
                IsDeleted = false,
                CompanyId = 1
            }
        };
        var companies = new List<Company>
        {
            new Company()
            {
                Users = new List<User>(),
                Description = "Bagira Systems",
                Name = "Bagira",
                Departments = new List<string>(),
                IsDeleted = false,
                UpdatedAt = DateTime.UtcNow,
                Admin = "Nitzan"
            }
        };

        await SeedCompanies(companies);

        await _context.SaveChangesAsync();

        await SeedUsers(users);

        await _context.SaveChangesAsync();
    }

    private async Task SeedCompanies(List<Company> companies)
    {
        if (_context.Companies.Any())
        {
            return;
        }



        foreach (var company in companies)
        {
            await _context.Companies.AddAsync(company);
        }
    }

    private async Task SeedUsers(ICollection<User> users)
    {
        if (_context.Users.Any())
        {
            return;
        }

        foreach (var user in users.Where(user => !_context.Users.Any(x => x.Email == user.Email)))
        {
            await _context.Users.AddAsync(user);
        }
    }
}