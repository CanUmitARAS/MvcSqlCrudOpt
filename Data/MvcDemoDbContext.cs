using Microsoft.EntityFrameworkCore;
using MvcSqlCrudOpt.Models.Domain;

namespace MvcSqlCrudOpt.Data

{
    public class MvcDemoDbContext : DbContext
    {
        public MvcDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
