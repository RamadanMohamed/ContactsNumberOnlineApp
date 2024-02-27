using ContactNumbersOnlineApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactNumbersOnlineApplication
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
