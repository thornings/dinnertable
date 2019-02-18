using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Models
{
    public class FoodContext : DbContext
    {
        public FoodContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cn = @"Data Source=.\SQLEXPRESS;Initial Catalog=madbordetdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(cn);
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Food> Foods { get; set; }


    }
}
