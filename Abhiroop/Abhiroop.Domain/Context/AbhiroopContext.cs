using Abhiroop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abhiroop.Domain.Context
{
    public class AbhiroopContext : IdentityDbContext<User, Role, string>
    {
        public AbhiroopContext(DbContextOptions<AbhiroopContext> options):base(options)
        {

        }
        public DbSet<Employee> Employees { set; get; }
    }
}
