using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Models
{
    public class CalendarContext : DbContext
    {
        public CalendarContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cn = @"Data Source=.\SQLEXPRESS;Initial Catalog=madbordetdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(cn);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalendarFoodItem>().ToTable("FoodItems");
            modelBuilder.Entity<Meal>().HasAlternateKey(m => m.Name);
        }

        public virtual DbSet<CalendarFoodItem> FoodItems { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
    }
}
