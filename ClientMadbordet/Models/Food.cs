using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMadbordet.Models
{
    public class Food
    {
        public Food()
        {
            //Ingredient = new HashSet<Ingredient>();
            //CalendarItems = new HashSet<CalendarItem>();
        }

        [Required]
        [Key]
        public int FoodID { get; set; }

        [Required]
        public string Name { get; set; }

        //public FoodWeightType FoodWeightType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Protein { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Carb { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fat { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Energy { get; set; }

        public int FatEnergyPercent()
        {
            return decimal.ToInt32((Fat*9) / TotalEnergy() * 100);
        }

        public decimal CarbEnergyPercent()
        {
            return decimal.ToInt32((Carb*4) / TotalEnergy() * 100);
        }

        public decimal ProteinEnergyPercent()
        {
            return decimal.ToInt32((Protein*4) / TotalEnergy() * 100);
        }

        private decimal TotalEnergy()
        {
            return (Carb * 4) + (Fat * 9) + (Protein * 4);
        }

        public IList<FoodWeightType> FoodWeightTypes { get; set; }

    }

}

