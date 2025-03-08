using Microsoft.EntityFrameworkCore;
using NetCoreAi.Project01_ApiDemp.Entities;

namespace NetCoreAi.Project01_ApiDemp.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-5DFGUGC\\SQLEXPRESS;Initial Catalog=ApiAIDb;Integrated Security=true;TrustServerCertificate=true");

        }

        public DbSet<Customer> Customers {get; set;}
    }
}