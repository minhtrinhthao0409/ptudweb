using LoginRegisterExample.Models;
using Microsoft.EntityFrameworkCore;

namespace BachendHtml.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
