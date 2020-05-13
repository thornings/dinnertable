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

            modelBuilder.Entity<CalendarFoodItem>()
                .ToTable("FoodItems");

            modelBuilder.Entity<Meal>().HasAlternateKey(m => m.Name);

            modelBuilder.Entity<FoodWeightType>().HasKey(sc => new { sc.FoodId, sc.WeightTypeId });

            //modelBuilder.Entity<Food>().HasData(
            //    new Food{ FoodID = 1, Name = "minimælk 0.4%", Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 2, Name = "Ost, elbo 26 40%", Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 3, Name = "Mælk, minimælk 0.4%",Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 4, Name = "Yoghurt med frugt, 1.4%", Carb = 18.69m, Fat = 1.4m, Protein = 4.6m},
            //    new Food{ FoodID = 5, Name = "Grønkål, frost, kogt", Carb = 5.3m, Fat = 0.5m, Protein = 2.8m},
            //    new Food{ FoodID = 6, Name = "Mælk, minimælk 0.4%", Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 7, Name = "Mælk, minimælk 0.4%", Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 8, Name = "Mælk, minimælk 0.4%", Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 9, Name = "Mælk, minimælk 0.4%", Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 10, Name = "Mælk, minimælk 0.4%",Carb = 4.7m, Fat = 0.4m, Protein = 3.5m},
            //    new Food{ FoodID = 11, Name = "Mælk, minimælk 0.4%",Carb = 4.7m, Fat = 0.4m, Protein = 3.5m}
            //);

            //modelBuilder.Entity<WeightType>().HasData(
            //      new WeightType{ UnitName = "gram", WTID=1 },
            //      new WeightType{ UnitName = "piece", WTID=2 },
            //      new WeightType{ UnitName = "liter", WTID=3 },
            //      new WeightType{ UnitName = "a cup", WTID=4 },
            //      new WeightType{ UnitName = "mililiter", WTID=5 },
            //      new WeightType{ UnitName = "half", WTID=6 },
            //      new WeightType{ UnitName = "quater", WTID=7 },
            //      new WeightType{ UnitName = "cherry tomato", WTID=8 },
            //      new WeightType{ UnitName = "beef tomato", WTID=9 },
            //      new WeightType{ UnitName = "little", WTID=10 },
            //      new WeightType{ UnitName = "dl", WTID=11 },
            //      new WeightType{ UnitName = "big", WTID=12 }
            //);

            //modelBuilder.Entity<FoodWeightType>().HasData(
            //    new FoodWeightType { FoodWeightTypeID = 1, FoodId = 1, WeightTypeId = 3 },
            //    new FoodWeightType { FoodWeightTypeID = 2, FoodId = 1, WeightTypeId = 4 },
                
            //    new FoodWeightType { FoodWeightTypeID = 1, FoodId = 1, WeightTypeId = 3 },
            //    new FoodWeightType { FoodWeightTypeID = 1, FoodId = 1, WeightTypeId = 3 },
            //    new FoodWeightType { FoodWeightTypeID = 1, FoodId = 1, WeightTypeId = 3 },
            //    new FoodWeightType { FoodWeightTypeID = 1, FoodId = 1, WeightTypeId = 3 },
            //    new FoodWeightType { FoodWeightTypeID = 1, FoodId = 1, WeightTypeId = 3 }
            //);
        }

        public virtual DbSet<CalendarFoodItem> FoodItems { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<FoodWeightType> FoodWeightTypes { get; set; }
        public virtual DbSet<WeightType> WeightTypes { get; set; }
    }
}
