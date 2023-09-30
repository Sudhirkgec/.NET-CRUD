using ASPNETMVCCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Task = ASPNETMVCCRUD.Models.Domain.Task;

namespace ASPNETMVCCRUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

    } 
}
